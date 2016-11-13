<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MainGantt.aspx.cs" Inherits="MainGantt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="Scripts/dhtmlxgantt.js"></script>
    <link href="Content/dhtmlxgantt/dhtmlxgantt_broadway.css" rel="stylesheet" />
    <title>GANTT test</title>
</head>
<body>
    <form id="form1" runat="server" style='width:100%; height:600px;'>
    <div id="ganttDiv" style='width:100%; height:100%;'>
        <script type="text/javascript">    
            gantt.init("ganttDiv");
            gantt.parse(<%= GetJsonData() %>);
        </script>
        <p>
            <asp:Label runat="server" ID="lblFooter" />
        </p>        
    </div>
    </form>
</body>
</html>
