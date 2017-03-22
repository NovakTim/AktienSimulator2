using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ApplicationLogic
{
    public static class LogicEvent
    {
        public const int CHANGE_EVENT_CHANCE = 20;

        public static void ChangeEvent(AktienSimulatorDataSet.AktieRow aktie, Random random)
        {
            int i = random.Next() % Database.DataSet.Event.Count;
            aktie.Event = Database.DataSet.Event.ElementAt(i).ID;
        }

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

        public static void UpdateKurswert(List<AktienSimulatorDataSet.AktieRow> aktien)
        {
            foreach (var aktie in aktien)
            {
                aktie.Kurs += Math.Round(aktie.Kurs * aktie.EventRow.ProzentualeVeränderung / 100, 2);
            }
        }
    }
}
