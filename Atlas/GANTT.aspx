<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GANTT.aspx.cs" Inherits="GANTT" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Chart ID="chartGANTT" runat="server" Height="153px">
            <Series>
                <asp:Series Name="Series1" ChartType="Bar"></asp:Series>
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
