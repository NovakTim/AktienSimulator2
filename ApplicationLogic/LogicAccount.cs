﻿using Contracts;
using Model;
using Model.AktienSimulatorDataSetTableAdapters;
using System.Data.OleDb;

namespace ApplicationLogic
{
    public static class LogicAccount
    {
        /// <summary>
        ///  Versucht mit dem Account einzuloggen und gibt den Account und den Fehlercode zurück.
        /// </summary>
        public static AktienSimulatorDataSet.AccountRow LogIn(string nickname, string password, ref ErrorCodes.Login errorcode)
        {
            var account = Database.CheckLogIn(nickname, password, ref errorcode);

            return account;
        }

        /// <summary>
        ///  Versucht einen Account zu registrieren und gibt den Fehlercode zurück.
        /// </summary>
        public static ErrorCodes.Register RegisterAccount(string nickname, string password)
        {
            try
            {
                AccountTableAdapter accountAdapter = new AccountTableAdapter();
                accountAdapter.Insert(nickname, password, 0m);
                Database.TableAdapterManager.AccountTableAdapter.Fill(Database.DataSet.Account);
                return ErrorCodes.Register.NoError;
            }
            catch (OleDbException)
            {
                return ErrorCodes.Register.NameAlreadyTaken;
            }
        }
    }
}
