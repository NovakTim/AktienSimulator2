using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationLogic
{
    public static class LogicEvent
    {
        public const int CHANGE_EVENT_CHANCE = 20;
        
        /// <summary>
        ///  Wechselt das Event einer Aktie mit einem neuen zufälligen Event aus.
        /// </summary>
        public static void ChangeEvent(AktienSimulatorDataSet.AktieRow aktie, Random random)
        {
            int i = random.Next() % Database.DataSet.Event.Count;
            aktie.Event = Database.DataSet.Event.ElementAt(i).ID;
        }
        
        /// <summary>
        ///  Würfelt mit der Wahrscheinlichkeit, ob eine Aktie ein neues Event bekommen soll und macht dies dann.
        /// </summary>
        public static void UpdateChangeEvent(List<AktienSimulatorDataSet.AktieRow> aktien)
        {
            Random random = new Random();

            foreach (var aktie in aktien)
            {
                int i = random.Next();
                if (i % 100 + 1 <= CHANGE_EVENT_CHANCE)
                {
                    ChangeEvent(aktie, random);
                }
            }
        }
        
        /// <summary>
        ///  Verändert den Kurswert einer Aktie, je nachdem in welchem Event es sich gerade befindet.
        /// </summary>
        public static void UpdateKurswert(List<AktienSimulatorDataSet.AktieRow> aktien)
        {
            foreach (var aktie in aktien)
            {
                aktie.Kurs += Math.Round(aktie.Kurs * aktie.EventRow.ProzentualeVeränderung / 100, 2);
            }
        }
    }
}
