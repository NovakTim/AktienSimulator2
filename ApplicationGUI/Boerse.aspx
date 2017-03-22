<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Boerse.aspx.cs" Inherits="AktienSimulator.Boerse" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="CSS/Boerse.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel ID="Panel1" runat="server" Direction="RightToLeft" Height="56px" Style="margin-top: 0px">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Speichern" />
            <br />
            <asp:Button ID="btnTest" runat="server" OnClick="btnTest_Click" Text="Test" Width="87px" />
        </asp:Panel>
        <div class="innerdiv">
            <table>
                <tr>
                    <td>Account:
                    </td>
                    <td>
                        <asp:Label ID="lblAccount" runat="server" OnDataBinding="lblAccount_DataBinding"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Kredit aufnehmen:
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="textKreditHöhe" runat="server" CssClass="textfield"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnKreditAufnehmen" runat="server" OnClick="btnKreditAufnehmen_Click" Text="Kredit aufnehmen" CssClass="button" />
                        <asp:Button ID="btnRepayKredit" runat="server" OnClick="btnRepayKredit_Click" Text="Kredit zurückzahlen" CssClass="button"/>
                    </td>
                </tr>
            </table>

            <br />

            <br />
            <div class="innerdiv">
                Anzahl:
                <asp:TextBox ID="textAnzahl" runat="server" CssClass="textfield">1</asp:TextBox>

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" OnDataBinding="GridView1_DataBinding" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" CssClass="shadowTable">
                            <Columns>
                                <asp:BoundField DataField="Bezeichnung" HeaderText="Aktie" ReadOnly="True" SortExpression="Aktie" />
                                <asp:BoundField DataField="Kurs" DataFormatString="{0:C2}" HeaderText="Kurs" ReadOnly="True" SortExpression="Kurs" />

                                <asp:TemplateField HeaderText="Event" ItemStyle-Width="300">
                                    <ItemTemplate>
                                        <asp:Literal ID="litEvent" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Anzahl">
                                    <ItemTemplate>
                                        <asp:Literal ID="litAnzahl" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:ButtonField ButtonType="Button" CommandName="Kaufen" Text="Kaufen" ControlStyle-CssClass="button"/>
                                <asp:ButtonField ButtonType="Button" CommandName="Verkaufen" Text="Verkaufen" ControlStyle-CssClass="button" />
                            </Columns>
                        </asp:GridView>
                        <br />
                        <table>
                            <tr>
                                <td>
                                    Ihre Bilanz:
                                </td>
                                <td>
                <asp:Label ID="lblBilanz" runat="server" OnDataBinding="lblBilanz_DataBinding"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Ihre Schulden:
                                </td>
                                <td>
                        <asp:Label ID="lblSchulden" runat="server" OnDataBinding="lblSchulden_DataBinding" Text="0"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        
                        <br />
                        <asp:Timer ID="Timer1" runat="server" Interval="1000" OnTick="TimerTick"></asp:Timer>
                        
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <br />
            <br />
        </div>
    </form>
</body>
</html>