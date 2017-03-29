using ApplicationLogic;
using Contracts;
using Model;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace AktienSimulator
{
    public partial class Boerse : GenericPage
    {
        private static int countRefresh = 20;

        protected void btnKreditAufnehmen_Click(object sender, EventArgs e)
        {
            if (Account != null)
            {
                try
                {
                    decimal value = Convert.ToDecimal(textKreditHöhe.Text);
                    //Kredit aufnehmen
                    LogicKredit.KreditAufnehmen(Account, value);
                }
                catch (Exception)
                {
                    //Es wurde keine Zahl im Textfeld eingegeben
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Falsches Eingabeformat!');", true);
                }
            }
        }

        protected void btnRepayKredit_Click(object sender, EventArgs e)
        {
            if (Account != null)
            {
                try
                {
                    decimal value = Convert.ToDecimal(textKreditHöhe.Text);
                    //Zahlt die Kredite zurück
                    LogicKredit.RepayKredit(Account, value);
                }
                catch (Exception)
                {
                    //Es wurde keine Zahl im Textfeld eingegeben
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Falsches Eingabeformat!');", true);
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //Speichert die Datenbank ab
            Database.SaveDatabase(Account);
        }

        protected void btnTest_Click(object sender, EventArgs e)
        {
        }

        protected void GridView1_DataBinding(object sender, EventArgs e)
        {
            GridView1.DataSource = Database.DataSet.Aktie.ToList();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            int anzahl = Convert.ToInt32(textAnzahl.Text);
            var aktie = Database.DataSet.Aktie.ElementAt(index);
            
            if (e.CommandName.Equals("Kaufen"))
            {
                //Es wurde auf Kaufen geklickt
                bool newDepotCreated = false;
                //Kaufe Aktie
                var errorcode = LogicAktie.BuyAktie(Account, Depots, aktie.ID, anzahl, ref newDepotCreated);
                switch (errorcode)
                {
                    case ErrorCodes.BuyAktie.NotEnoughMoney: //Nicht genug Geld auf dem Konto
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Sie haben nicht genügend Geld zur Verfügung!');", true);
                        break;

                    default:
                        if (newDepotCreated)
                            UpdateDepots();
                        break;
                }
            }
            else if (e.CommandName.Equals("Verkaufen"))
            {
                //Es wurde auf Verkaufen geklickt
                bool newDepotCreated = false;
                //Verkaufe Aktie
                var errorcode = LogicAktie.SellAktie(Account, Depots, aktie.ID, anzahl, ref newDepotCreated);
                switch (errorcode)
                {
                    case ErrorCodes.SellAktie.NotEnoughAmount: //Nicht genug Anteil an der Aktie
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('Sie besitzen nicht die gewünschte Menge zum Verkaufen!');", true);
                        break;

                    default:
                        if (newDepotCreated)
                            UpdateDepots();
                        break;
                }
            }

            GridView1.DataBind();
            lblBilanz.DataBind();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Literal litEvent = e.Row.FindControl("litEvent") as Literal;
                Literal litAnzahl = e.Row.FindControl("litAnzahl") as Literal;

                //Chart bakChart = e.Row.FindControl("bakChart") as Chart;
                //Series bakstockCourse = courseChart.Series["stockCourse"];

                AktienSimulatorDataSet.AktieRow aktie = e.Row.DataItem as AktienSimulatorDataSet.AktieRow;
                var kurse = LogicAnzeige.dictKurse[aktie.ID];
                for (int i = 0; i < kurse.Count; i++)
                {
                    // var kurs in kurse
                    if (countRefresh == 0)
                    {
                        Chart courseChart = e.Row.FindControl("courseChart") as Chart;
                        Series courseChartSeries = courseChart.Series["stockCourse"];
                        countRefresh = 20;
                        courseChartSeries.Points.AddXY(i, kurse[i]);
                    }
                    countRefresh--;
                    //bakstockCourse.Points.AddXY(i, kurse[i]);
                }

                //LogicAnzeige.dictKurse[aktie.ID]
                litEvent.Text = aktie.EventRow.Bezeichnung;
                var depot = Depots?.FirstOrDefault(x => x.Aktie == aktie.ID);
                if (depot != null)
                    litAnzahl.Text = depot.Anzahl.ToString();
                else
                    litAnzahl.Text = "0";
            }
        }

        protected void lblAccount_DataBinding(object sender, EventArgs e)
        {
            lblAccount.Text = Account?.Nickname;
        }

        protected void lblBilanz_DataBinding(object sender, EventArgs e)
        {
            lblBilanz.Text = Account?.Bilanz.ToString("0,0.00");
        }

        protected void lblSchulden_DataBinding(object sender, EventArgs e)
        {
            //Zeigt die Gesamtschuld an
            if (Account != null)
                lblSchulden.Text = LogicKredit.GetGesamtSchuld(Account.Nickname).ToString("0,0.00");
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
            lblBilanz.DataBind();
            lblAccount.DataBind();
        }

        protected void TimerTick(object sender, EventArgs e)
        {
            //Aktualisiert die UpdatePanel-Elemente
            var aktien = Database.DataSet.Aktie.ToList();
            LogicEvent.UpdateChangeEvent(aktien);
            LogicEvent.UpdateKurswert(aktien);

            if (Account != null)
            {
                var depots = LogicDepot.GetDepots(Account.Nickname);
                LogicDividende.UpdateDividende(Account, depots);
                LogicKredit.UpdateKreditSchuld(Account);

                LogicAnzeige.UpdateDictionary(aktien);
            }

            GridView1.DataBind();
            lblBilanz.DataBind();
            lblSchulden.DataBind();
        }
    }
}
