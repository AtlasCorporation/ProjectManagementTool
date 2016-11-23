<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Gantt.aspx.cs" Inherits="Gantt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="Scripts/dhtmlxgantt.js"></script>
    <link href="Content/dhtmlxgantt/dhtmlxgantt_broadway.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <div id="ganttDiv"  style='width:100%; min-height:400px;'>
            <script type="text/javascript">    
               // gantt.config.subscales = [{ unit: "hour", step: 12, date: "%H:%i" }];
                gantt.config.duration_unit = "hour";
                gantt.config.duration_step = 1;
                gantt.config.readonly = true;
                gantt.init("ganttDiv");
                gantt.parse(<%= GetJsonData() %>);
            </script>
            <p>
                <asp:Label runat="server" ID="lblFooter" />
            </p>        
        </div>
</asp:Content>

