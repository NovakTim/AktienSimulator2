<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AktienSimulator.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="CSS/Boerse.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            height: 30px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div class="outerdiv">
<table>
            <tr>
                <td>Nickname:
                </td>
                <td>
                    <asp:TextBox ID="textNickname" runat="server" CssClass="textfield"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Passwort:
                </td>
                <td>
                    <asp:TextBox ID="textPassword" runat="server" CssClass="textfield"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Button ID="btnRegistrieren" runat="server" OnClick="btnRegistrieren_Click" Text="Registrieren" CssClass="button" />
                </td>
                <td class="auto-style1">
                    <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Login" CssClass="button" />
                </td>
            </tr>
        </table>
        </div>
       
        
    </form>
</body>
</html>