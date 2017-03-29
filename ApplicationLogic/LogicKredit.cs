using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationLogic
{
    public static class LogicKredit
    {
        public const decimal PROZENTUALE_ZINSEN = 0.001m;
        
        /// <summary>
        ///  Gibt die Summe der Schulden eines Account zurück.
        /// </summary>
        public static decimal GetGesamtSchuld(string nickname)
        {
            var kredite = GetKredite(nickname);
            return kredite.Sum(x => x.Rest);
        }
        
        /// <summary>
        ///  Gibt eine Liste mit allen Krediten eines Accounts zurück.
        /// </summary>
        public static List<AktienSimulatorDataSet.KreditRow> GetKredite(string nickname)
        {
            return Database.DataSet.Kredit.Where(x => x.Account == nickname).ToList();
        }
        
        /// <summary>
        ///  Nimmt einen neuen Kredit mit den übegebenen Betrag für einen Account auf.
        /// </summary>
        public static void KreditAufnehmen(AktienSimulatorDataSet.AccountRow account, decimal amount)
        {
            var row = Database.DataSet.Kredit.NewKreditRow();
            row.Account = account.Nickname;
            row.Höhe = amount;
            row.Rest = amount;
            Database.DataSet.Kredit.Rows.Add(row);
            account.Bilanz += amount;
        }        
        
        /// <summary>
        ///  Zahlt die Kredite mit den übergebenen Betrag zurück.
        /// </summary>
        public static void RepayKredit(AktienSimulatorDataSet.AccountRow account, decimal amount)
        {
            //Holt sich alle Kredite
            var kredite = GetKredite(account.Nickname);
            
            decimal payback = amount;
            foreach (var kredit in kredite)
            {
                if (payback == 0m)
                    break;

                //Fall: Kredit höher als zurückzahlende Menge
                if (kredit.Rest > payback)
                {
                    kredit.Rest -= payback;
                    payback = 0m;
                }
                else //Fall: Einzelner Kredit kann voll beglichen werden
                {
                    payback -= kredit.Rest;
                    kredit.Rest = 0m;
                }
            }

            //Zieht die zurückgezahlte Menge von der Bilanz ab
            //Zu hoch gesetzte Rückzahlungsmenge wird nicht abgezogen.
            account.Bilanz -= amount + payback;
        }
        
        /// <summary>
        ///  Erhöht die Zinsschulden aller Kredite eines Accounts.
        /// </summary>
        public static void UpdateKreditSchuld(AktienSimulatorDataSet.AccountRow account)
        {
            var kredite = GetKredite(account.Nickname);
            foreach (var kredit in kredite)
            {
                kredit.Rest *= 1 + PROZENTUALE_ZINSEN;
                kredit.Rest = Math.Round(kredit.Rest, 2);
            }
        }
    }
}
