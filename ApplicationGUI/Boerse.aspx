<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Boerse.aspx.cs" Inherits="AktienSimulator.Boerse" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="de">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Börse</title>
    <link href="CSS/Boerse.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <div class="innerdiv">
            <table class="resizeTable">
                <tr>
                    <td>Account:
                    </td>
                    <td>
                        <asp:Label ID="lblAccount" runat="server" OnDataBinding="lblAccount_DataBinding"></asp:Label>
                    </td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Speichern" class="button" />
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
                        <asp:Button ID="btnRepayKredit" runat="server" OnClick="btnRepayKredit_Click" Text="Kredit zurückzahlen" CssClass="button" />
                    </td>
                </tr>
            </table>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td>Ihre Bilanz:
                            </td>
                            <td>
                                <asp:Label ID="lblBilanz" runat="server" OnDataBinding="lblBilanz_DataBinding"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Ihre Schulden:
                            </td>
                            <td>
                                <asp:Label ID="lblSchulden" runat="server" OnDataBinding="lblSchulden_DataBinding" Text="0"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>

            <%--<asp:Button ID="btnTest" runat="server" OnClick="btnTest_Click" Text="Test" Width="87px" />--%>

            <br />
            <div class="innerdiv">
                Anzahl:
                <asp:TextBox ID="textAnzahl" runat="server" CssClass="textfield">1</asp:TextBox>

                <asp:UpdatePanel ID="UpdatePanelStock" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" OnDataBinding="GridView1_DataBinding" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" BorderStyle="None" CssClass="resizeTable">
                            <Columns>
                                <asp:BoundField DataField="Bezeichnung" HeaderText="Aktie" ReadOnly="True" SortExpression="Aktie" />
                                <asp:BoundField DataField="Kurs" ItemStyle-CssClass="StockmarketCourse" DataFormatString="{0:C2}" HeaderText="Kurs" ReadOnly="True" SortExpression="Kurs" />

                                <asp:TemplateField HeaderText="Event" ItemStyle-CssClass="StockmarketEvent">
                                    <ItemTemplate>
                                        <asp:Literal ID="litEvent" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Anzahl">
                                    <ItemTemplate>
                                        <asp:Literal ID="litAnzahl" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Verlauf">
                                    <ItemTemplate>
                                        <asp:Chart runat="server" BackColor="Transparent" ID="courseChart" Height="60">
                                            <Series>
                                                <asp:Series Name="stockCourse" ChartType="Area"></asp:Series>
                                            </Series>
                                            <ChartAreas>
                                                <asp:ChartArea Name="ChartArea1" BackColor="Transparent">
                                                    <AxisY Interval="100">
                                                        <LabelStyle Enabled="False" />
                                                    </AxisY>
                                                    <AxisX Enabled="False"></AxisX>
                                                    <AxisX2 Enabled="False"></AxisX2>
                                                    <AxisY2 Enabled="False"></AxisY2>
                                                </asp:ChartArea>
                                            </ChartAreas>
                                        </asp:Chart>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:ButtonField ButtonType="Button" CommandName="Kaufen" Text="Kaufen" ControlStyle-CssClass="button" />
                                <asp:ButtonField ButtonType="Button" CommandName="Verkaufen" Text="Verkaufen" ControlStyle-CssClass="button" />
                            </Columns>
                        </asp:GridView>
                        <asp:Timer ID="Timer1" runat="server" Interval="1000" OnTick="TimerTick"></asp:Timer>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </form>
</body>
</html>