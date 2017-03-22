using Contracts;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic
{
    public static class LogicAktie
    {
        public static ErrorCodes.BuyAktie BuyAktie(AktienSimulatorDataSet.AccountRow account, List<AktienSimulatorDataSet.DepotRow> depots, int aktieID, int anzahl, ref bool newDepotCreated)
        {
            var depot = LogicDepot.GetDepotOrCreate(account.Nickname, depots, aktieID, ref newDepotCreated);

            var sum = depot.AktieRow.Kurs * anzahl;
            if(account.Bilanz >= sum)
            {
                depot.Anzahl += anzahl;
                account.Bilanz -= sum;
                return ErrorCodes.BuyAktie.NoError;
            }

            return ErrorCodes.BuyAktie.NotEnoughMoney;
        }

        public static ErrorCodes.SellAktie SellAktie(AktienSimulatorDataSet.AccountRow account, List<AktienSimulatorDataSet.DepotRow> depots, int aktieID, int anzahl, ref bool newDepotCreated)
        {
            var depot = LogicDepot.GetDepotOrCreate(account.Nickname, depots, aktieID, ref newDepotCreated);
            if (depot.Anzahl >= anzahl)
            {
                var sum = depot.AktieRow.Kurs * anzahl;
                account.Bilanz += sum;
                depot.Anzahl -= anzahl;

                return ErrorCodes.SellAktie.NoError;
            }

            return ErrorCodes.SellAktie.NotEnoughAmount;
        }
    }
}
