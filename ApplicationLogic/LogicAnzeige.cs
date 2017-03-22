using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogic
{
    public class LogicAnzeige
    {
        public const int COUNT = 7;
        public static Dictionary<int, List<decimal>> dictKurse = new Dictionary<int, List<decimal>>();

        public static void InitializeDictionary()
        {
            foreach (AktienSimulatorDataSet.AktieRow aktie in Database.DataSet.Aktie.Rows)
            {
                List<decimal> list = new List<decimal>();
                for (int i = 0; i < COUNT; i++)
                {
                    list.Add(0);
                }
                dictKurse.Add(aktie.ID, list);

            }
        }

        public static void UpdateDictionary(int id, decimal kurs)
        {
            dictKurse[id].RemoveAt(0);
            dictKurse[id].Add(kurs);
        }
    }
}
