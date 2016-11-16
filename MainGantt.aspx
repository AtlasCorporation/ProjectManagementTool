<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MainGantt.aspx.cs" Inherits="MainGantt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="Scripts/dhtmlxgantt.js"></script>
    <link href="Content/dhtmlxgantt/dhtmlxgantt_broadway.css" rel="stylesheet" />
    <title>GANTT test</title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="ganttDiv"  style='width:100%; min-height:400px;'>
            <script type="text/javascript">    
               // gantt.config.subscales = [{ unit: "hour", step: 12, date: "%H:%i" }];
                gantt.config.duration_unit = "hour";
                gantt.config.duration_step = 1;

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
