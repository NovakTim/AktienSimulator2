using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace ApplicationLogic
{
    public static class LogicKredit
    {
        public const decimal PROZENTUALE_ZINSEN = 0.001m;

        public static void KreditAufnehmen(AktienSimulatorDataSet.AccountRow account, decimal amount)
        {
            var row = Database.DataSet.Kredit.NewKreditRow();
            row.Account = account.Nickname;
            row.Höhe = amount;
            row.Rest = amount;
            Database.DataSet.Kredit.Rows.Add(row);
            account.Bilanz += amount;
        }

        public static List<AktienSimulatorDataSet.KreditRow> GetKredite(string nickname)
        {
            return Database.DataSet.Kredit.Where(x => x.Account == nickname).ToList();
        }

        public static void UpdateKreditSchuld(AktienSimulatorDataSet.AccountRow account)
        {
            var kredite = GetKredite(account.Nickname);
            foreach (var kredit in kredite)
            {
                kredit.Rest *= 1 + PROZENTUALE_ZINSEN;
            }
        }

        public static void RepayKredit(AktienSimulatorDataSet.AccountRow account, decimal amount)
        {
            var kredite = GetKredite(account.Nickname);
            decimal payback = amount;
            foreach (var kredit in kredite)
            {
                if (payback == 0m)
                    break;

                if (kredit.Rest > payback)
                {
                    kredit.Rest -= payback;
                    payback = 0m;
                }
                else
                {
                    payback -= kredit.Rest;
                    kredit.Rest = 0m;
                }
            }

            account.Bilanz -= amount + payback;
        }

        public static decimal GetGesamtSchuld(string nickname)
        {
            var kredite = GetKredite(nickname);
            return kredite.Sum(x => x.Rest);
        }
    }
}
