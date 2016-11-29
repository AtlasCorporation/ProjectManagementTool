<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Gantt.aspx.cs" Inherits="Gantt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="Scripts/dhtmlxgantt.js"></script>
    <link href="Content/dhtmlxgantt/dhtmlxgantt_broadway.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <div runat="server" id="reminderDiv" class="alert alert-info w3-section w3-center" visible="false">
        <asp:Label runat="server" ID="lblReminder" Text="<strong>Please select a project first to show the gantt-chart!</strong>" />     
    </div> 
    <div runat="server" id="mainDiv" visible="true">
        <div id="ganttDiv" class="w3-section">
            <script type="text/javascript">    
               // gantt.config.subscales = [{ unit: "hour", step: 12, date: "%H:%i" }];
                gantt.config.duration_unit = "hour";
                gantt.config.autosize = true;
                gantt.config.duration_step = 1;
                gantt.config.readonly = true;
                gantt.config.skip_off_time = true;
                gantt.init("ganttDiv");
                gantt.parse(<%= GetJsonData() %>);
            </script>
            <p>
                <asp:Label runat="server" ID="lblFooter" />
            </p>        
        </div>
    </div>
</asp:Content>

