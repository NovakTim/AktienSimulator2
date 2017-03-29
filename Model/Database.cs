using Contracts;
using Model.AktienSimulatorDataSetTableAdapters;
using System;
using System.Data;
using System.Data.OleDb;

namespace Model
{
    public static class Database
    {
        public static AktienSimulatorDataSet DataSet { get; set; }
        public static TableAdapterManager TableAdapterManager { get; set; }
        
        /// <summary>
        ///  Füllt das DataSet mit allen notwendigen Tabellen.
        /// </summary>
        public static void CacheRelevantTables()
        {
            TableAdapterManager.AccountTableAdapter.Fill(DataSet.Account);
            TableAdapterManager.AktieTableAdapter.Fill(DataSet.Aktie);
            TableAdapterManager.EventTableAdapter.Fill(DataSet.Event);
            TableAdapterManager.DepotTableAdapter.Fill(DataSet.Depot);
            TableAdapterManager.KreditTableAdapter.Fill(DataSet.Kredit);
        }
        
        /// <summary>
        ///  Macht eine einzelne SQL-Abfrage, um einen Account einzuloggen. Gibt den Account und den Fehlercode zurück.
        /// </summary>
        public static AktienSimulatorDataSet.AccountRow CheckLogIn(string nickname, string password, ref ErrorCodes.Login errorcode)
        {
            OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.AktienSimulatorConnectionString);
            connection.Open();

            //SQL Injection verhindern
            string queryString = "SELECT * FROM Account WHERE Nickname = @Nickname";
            OleDbCommand command = new OleDbCommand(queryString, connection);
            command.Parameters.Add("@Nickname", OleDbType.VarChar, 255);
            command.Parameters["@Nickname"].Value = nickname;

            //Account suchen
            var reader = command.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.HasRows)
            {
                reader.Read();
                AktienSimulatorDataSet.AccountRow row = DataSet.Account.NewAccountRow();
                row.Nickname = reader["Nickname"].ToString();
                row.Passwort = reader["Passwort"].ToString();
                row.Bilanz = Convert.ToDecimal(reader["Bilanz"]);

                //Prüft das Passwort
                if (row.Passwort == password)
                { // Passwort korrekt
                    errorcode = ErrorCodes.Login.NoError;
                    connection.Close();
                    return row;
                }
                else
                { //Passwort inkorrekt
                    errorcode = ErrorCodes.Login.WrongPassword;
                }
            }
            else
            { //Account wurde nicht gefunden
                errorcode = ErrorCodes.Login.NicknameNotFound;
            }

            connection.Close();
            return null;
        }
        
        /// <summary>
        ///  Füllt die Depots einens Accounts in das DataSet.
        /// </summary>
        public static void FillDepots(string nickname)
        {
            //SQL Injection verhindern
            string queryString = "SELECT * FROM Depot WHERE Account = @Account";
            TableAdapterManager.DepotTableAdapter.Adapter.SelectCommand = new OleDbCommand(queryString, TableAdapterManager.DepotTableAdapter.Connection);
            TableAdapterManager.DepotTableAdapter.Adapter.SelectCommand.Parameters.Add("@Account", OleDbType.VarChar, 255);
            TableAdapterManager.DepotTableAdapter.Adapter.SelectCommand.Parameters["@Account"].Value = nickname;

            TableAdapterManager.DepotTableAdapter.Adapter.Fill(DataSet.Depot);
        }
        
        /// <summary>
        ///  Initialisiert das DataSet und den TableAdapterManager.
        /// </summary>
        public static void Initialize()
        {
            DataSet = new AktienSimulatorDataSet();

            TableAdapterManager = new TableAdapterManager();
            TableAdapterManager.AccountTableAdapter = new AccountTableAdapter();
            TableAdapterManager.AktieTableAdapter = new AktieTableAdapter();
            TableAdapterManager.DepotTableAdapter = new DepotTableAdapter();
            TableAdapterManager.EventTableAdapter = new EventTableAdapter();
            TableAdapterManager.KreditTableAdapter = new KreditTableAdapter();
        }
        
        /// <summary>
        ///  Speichert die notwendigen Tabellen ab.
        /// </summary>
        public static void SaveDatabase(AktienSimulatorDataSet.AccountRow account)
        {
            TableAdapterManager.DepotTableAdapter.Update(DataSet.Depot);
            TableAdapterManager.AccountTableAdapter.Update(account);
            TableAdapterManager.KreditTableAdapter.Update(DataSet.Kredit);
        }
    }
}
