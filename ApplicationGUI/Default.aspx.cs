using ApplicationLogic;
using Contracts;
using System;

namespace AktienSimulator
{
    public partial class Default : GenericPage
    {
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            ErrorCodes.Login errorcode = ErrorCodes.Login.NoError;
            var account = LogicAccount.LogIn(textNickname.Text, textPassword.Text, ref errorcode);
            switch (errorcode)
            {
                case ErrorCodes.Login.NoError:
                    //Response.Write("<script>alert('Erfolgreich angemeldet.');</script>");
                    Account = account;
                    Response.Redirect("Boerse.aspx");
                    break;

                case ErrorCodes.Login.NicknameNotFound:
                    Response.Write("<script>alert('Es wurde kein Account unter dem angegeben Nicknamen gefunden.');</script>");
                    break;

                case ErrorCodes.Login.WrongPassword:
                    Response.Write("<script>alert('Falsches Passwort.');</script>");
                    break;

                default:
                    break;
            }
        }

        protected void btnRegistrieren_Click(object sender, EventArgs e)
        {
            var errorcode = LogicAccount.RegisterAccount(textNickname.Text, textPassword.Text);
            switch (errorcode)
            {
                case ErrorCodes.Register.NoError:
                    Response.Write("<script>alert('Erfolgreich registriert.');</script>");
                    break;

                case ErrorCodes.Register.NameAlreadyTaken:
                    Response.Write("<script>alert('Benutzername ist schon vergeben.');</script>");
                    break;

                default:
                    break;
            }
        }

        protected override void Page_Load(object sender, EventArgs e)
        {
        }
    }
}