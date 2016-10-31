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
        <asp:SqlDataSource runat="server" ID="sdsMyDataSource" ConnectionString="Server=promehub.com;Initial Catalog=atlas;User ID=IIO14S1;Password=vitosenprojekti" ProviderName="MySql.Data.MySqlClient" SelectCommand="SELECT worktime, id FROM donetask WHERE 'id' = '1';" />
        <div>
            <asp:Chart ID="chartGANTT" runat="server" DataSourceID="sdsMyDataSource" Height="153px">
                <Series>
                    <asp:Series Name="Series1" ChartType="Bar" LegendText="gfd" XValueMember="worktime" YValueMembers="id"></asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
        </div>
        <div style="margin-bottom:0;">
            <asp:Label ID="lblFooter" runat="server" />
        </div>
    </form>
</body>
</html>
