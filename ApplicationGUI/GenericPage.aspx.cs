using ApplicationLogic;
using Model;
using System;
using System.Collections.Generic;

namespace AktienSimulator
{
    public partial class GenericPage : System.Web.UI.Page
    {        
        /// <summary>
        ///  Cached den angemeldeten Account.
        /// </summary>
        public AktienSimulatorDataSet.AccountRow Account
        {
            get
            {
                return Database.DataSet.Account.Rows.Find(Session["Account"]) as AktienSimulatorDataSet.AccountRow;
            }
            set
            {
                Session["Account"] = value.Nickname;
                UpdateDepots();
            }
        }
        
        /// <summary>
        ///  Lädt die Depots eines Accounts in einen Cache
        /// </summary>
        public List<AktienSimulatorDataSet.DepotRow> Depots
        {
            get
            {
                return Session["Depots"] as List<AktienSimulatorDataSet.DepotRow>;
            }
            set
            {
                Session["Depots"] = value;
            }
        }
        
        /// <summary>
        ///  Aktualisiert die Depots für den Account.
        /// </summary>
        public void UpdateDepots()
        {
            Depots = LogicDepot.GetDepots(Account.Nickname);
        }

        protected virtual void Page_Load(object sender, EventArgs e)
        {
        }
    }
}
