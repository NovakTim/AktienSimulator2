using Contracts;
using Model;
using Model.AktienSimulatorDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ApplicationLogic
{
    public static class LogicAccount
    {
        public static ErrorCodes.Register RegisterAccount(string nickname, string password)
        {
            try
            {
                AccountTableAdapter accountAdapter = new AccountTableAdapter();
                accountAdapter.Insert(nickname, password, 0m);
                return ErrorCodes.Register.NoError;
            }
            catch (OleDbException)
            {
                return ErrorCodes.Register.NameAlreadyTaken;
            }
        }

        public static AktienSimulatorDataSet.AccountRow LogIn(string nickname, string password, ref ErrorCodes.Login errorcode)
        {
            var account = Database.CheckLogIn(nickname, password, ref errorcode);

            return account;
        }
    }
}
