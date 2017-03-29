using Contracts;
using Model;
using System.Collections.Generic;

namespace ApplicationLogic
{
    public static class LogicAktie
    {
        /// <summary>
        ///  Es wird versucht eine Aktie mit der angegebenen Anzahl zu kaufen. Der Fehlercode wird zurückgegeben.
        /// </summary>
        public static ErrorCodes.BuyAktie BuyAktie(AktienSimulatorDataSet.AccountRow account, List<AktienSimulatorDataSet.DepotRow> depots, int aktieID, int anzahl, ref bool newDepotCreated)
        {
            var depot = LogicDepot.GetDepotOrCreate(account.Nickname, depots, aktieID, ref newDepotCreated);

            var sum = depot.AktieRow.Kurs * anzahl;
            if (account.Bilanz >= sum)
            {
                depot.Anzahl += anzahl;
                account.Bilanz -= sum;
                return ErrorCodes.BuyAktie.NoError;
            }

            return ErrorCodes.BuyAktie.NotEnoughMoney;
        }
        
        /// <summary>
        ///  Es wird versucht die Aktien mit der angegeben Anzahl zu verkaufen. Der Fehlercode wird zurückgegeben.
        /// </summary>
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
