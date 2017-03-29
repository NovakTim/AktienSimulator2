using Model;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ApplicationLogic
{
    public static class LogicDepot
    {   
        /// <summary>
        ///  Erstellt ein neues Depot einer Aktie für einen Account.
        /// </summary>
        public static AktienSimulatorDataSet.DepotRow AddAktieToDepots(string nickname, List<AktienSimulatorDataSet.DepotRow> depots, int aktieID)
        {
            var row = Database.DataSet.Depot.NewDepotRow();
            row.Account = nickname;
            row.Aktie = aktieID;
            row.Anzahl = 0;
            Database.DataSet.Depot.Rows.Add(row);
            return row;
        }
        
        /// <summary>
        ///  Es wird nach einen bestehenden Depot gesucht und zurückgegeben.
        ///  Wenn keines gefunden wurde, erstellt er ein neues Depot.
        /// </summary>
        public static AktienSimulatorDataSet.DepotRow GetDepotOrCreate(string nickname, List<AktienSimulatorDataSet.DepotRow> depots, int aktieID, ref bool newDepotCreated)
        {
            var depot = depots.FirstOrDefault(x => x.Aktie == aktieID);
            if (depot == null)
            {
                depot = AddAktieToDepots(nickname, depots, aktieID);
                newDepotCreated = true;
            }
            else
                newDepotCreated = false;

            return depot;
        }
        
        /// <summary>
        ///  Es wird eine Liste mit allen Depots vom einem Account zurückgeben.
        /// </summary>
        public static List<AktienSimulatorDataSet.DepotRow> GetDepots(string nickname)
        {
            return Database.DataSet.Depot.Where(x => x.Account == nickname).ToList();
        }
    }
}
