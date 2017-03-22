using ApplicationLogic;
using Contracts;
using Model;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AktienSimulator
{
    public partial class Boerse : GenericPage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
            lblBilanz.DataBind();
            lblAccount.DataBind();
        }

        protected void TimerTick(object sender, EventArgs e)
        {
            var aktien = Database.DataSet.Aktie.ToList();
            LogicEvent.UpdateChangeEvent(aktien);
            LogicEvent.UpdateKurswert(aktien);

            if (Account != null)
            {
                var depots = LogicDepot.GetDepots(Account.Nickname);
                LogicDividende.UpdateDividende(Account, depots);

                LogicKredit.UpdateKreditSchuld(Account);
            }

            GridView1.DataBind();
            lblBilanz.DataBind();
            lblSchulden.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Database.SaveDatabase();
        }

        protected void btnTest_Click(object sender, EventArgs e)
        {
            
        }

        protected void lblAccount_DataBinding(object sender, EventArgs e)
        {
            lblAccount.Text = Account?.Nickname;
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
                bool newDepotCreated = false;
                var errorcode = LogicAktie.BuyAktie(Account, Depots, aktie.ID, anzahl, ref newDepotCreated);
                switch (errorcode)
                {
                    case ErrorCodes.BuyAktie.NotEnoughMoney:
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
                bool newDepotCreated = false;
                var errorcode = LogicAktie.SellAktie(Account, Depots, aktie.ID, anzahl, ref newDepotCreated);
                switch (errorcode)
                {
                    case ErrorCodes.SellAktie.NotEnoughAmount:
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

        protected void lblBilanz_DataBinding(object sender, EventArgs e)
        {
            lblBilanz.Text = Account?.Bilanz.ToString("0,0.00");
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Literal litEvent = e.Row.FindControl("litEvent") as Literal;
                Literal litAnzahl = e.Row.FindControl("litAnzahl") as Literal;
                AktienSimulatorDataSet.AktieRow aktie = e.Row.DataItem as AktienSimulatorDataSet.AktieRow;
                litEvent.Text = aktie.EventRow.Bezeichnung;
                var depot = Depots?.FirstOrDefault(x => x.Aktie == aktie.ID);
                if (depot != null)
                    litAnzahl.Text = depot.Anzahl.ToString();
                else
                    litAnzahl.Text = "0";
            }
        }

        protected void btnKreditAufnehmen_Click(object sender, EventArgs e)
        {
            LogicKredit.KreditAufnehmen(Account, Convert.ToDecimal(textKreditHöhe.Text));
        }

        protected void lblSchulden_DataBinding(object sender, EventArgs e)
        {
            lblSchulden.Text = LogicKredit.GetGesamtSchuld(Account.Nickname).ToString("0,0.00");
        }

        protected void btnRepayKredit_Click(object sender, EventArgs e)
        {
            LogicKredit.RepayKredit(Account, Convert.ToDecimal(textKreditHöhe.Text));
        }
    }
}