<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GANTT.aspx.cs" Inherits="GANTT" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <!-- SELECT SUM(worktime) AS Ohjelmointi FROM donetask WHERE 'id' = '3'; -->
    <form id="form1" runat="server">
        <div>
            <asp:Chart ID="chartGANTT" runat="server">
                <Titles>
                    <asp:Title Text="Time spent"/>
                </Titles>
                 <Series>
                    <asp:Series IsValueShownAsLabel="true" ChartType="Bar" Name="Dokumentaatio" Color="Yellow" />
                    <asp:Series IsValueShownAsLabel="true" ChartType="Bar" Name="Ohjelmointi" Color="LightBlue"/>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1">
                        <AxisY Enabled="False"/>
                        <AxisX Enabled="False"/>
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
            <asp:GridView runat="server" ID="gvData"></asp:GridView>
        </div>
        <div style="margin-bottom:0;">
            <asp:Label ID="lblFooter" runat="server" />
        </div>
    </form>
</body>
</html>
