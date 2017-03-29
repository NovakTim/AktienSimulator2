using Model;
using System.Collections.Generic;

namespace ApplicationLogic
{
    public class LogicAnzeige
    {
        public const int COUNT = 80;
        public static Dictionary<int, List<decimal>> dictKurse = new Dictionary<int, List<decimal>>();

        /// <summary>
        ///  Initialisiert das Dictionary für den Graphen, damit er angezeigt werden kann.
        /// </summary>
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

        //public static void UpdateDictionary(int id, decimal kurs)
        //{
        //    dictKurse[id].RemoveAt(0);
        //    dictKurse[id].Add(kurs);
        //}

        /// <summary>
        ///  Fügt einen neuen Datensatz für den Graphen einer Aktie hinzu.
        /// </summary>
        public static void UpdateDictionary(List<AktienSimulatorDataSet.AktieRow> aktienList)
        {
            foreach (var aktie in aktienList)
            {
                int i = 0;
                var item = dictKurse[aktie.ID];
                item.RemoveAt(0);
                item.Add(aktie.Kurs);
                i++;
            }
        }
    }
}
