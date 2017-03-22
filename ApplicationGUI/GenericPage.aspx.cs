using ApplicationLogic;
using Model;
using System;
using System.Collections.Generic;

namespace AktienSimulator
{
    public partial class GenericPage : System.Web.UI.Page
    {
        public AktienSimulatorDataSet.AccountRow Account
        {
            get
            {
                return Session["Account"] as AktienSimulatorDataSet.AccountRow;
            }
            set
            {
                Session["Account"] = value;
                UpdateDepots();
            }
        }

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

        public void UpdateDepots()
        {
            Depots = LogicDepot.GetDepots(Account.Nickname);
        }

        protected virtual void Page_Load(object sender, EventArgs e)
        {
        }
    }
}