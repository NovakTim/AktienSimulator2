using Model;
using System.Collections.Generic;

namespace ApplicationLogic
{
    public static class LogicDividende
    {
        public const decimal PROZENTUALE_DIVIDENDE = 0.4m;

        public static void UpdateDividende(AktienSimulatorDataSet.AccountRow account, List<AktienSimulatorDataSet.DepotRow> depots)
        {
            decimal sum = 0m;
            foreach (var depot in depots)
            {
                sum += depot.Anzahl * depot.AktieRow.Kurs * PROZENTUALE_DIVIDENDE / 100;
            }

            account.Bilanz += sum;
        }
    }
}