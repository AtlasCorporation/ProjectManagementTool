<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TaskEntry.aspx.cs" Inherits="TaskEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">    
    <div class="w3-half w3-content">

        <div>
            <asp:Label runat="server" Text="Selected task:" />
            <asp:Label runat="server" ID="lblSelectedTask" Text="None selected" />
        </div>    
             
        <asp:TreeView runat="server" ID="twTasks"  OnSelectedNodeChanged="twTasks_SelectedNodeChanged"/>
        <asp:Button cssclass="btn btn-success" runat="server" Text="Add task" ID="btnShowAddTask" OnClick="btnShowAddTask_Click" />
        <asp:Button cssclass="btn btn-danger" runat="server" Text="Delete task" ID="btnShowDeleteTask" OnClick="btnShowDeleteTask_Click" />

        <div runat="server" id="addTaskDiv" visible="false">  
            <asp:Label runat="server" Text="Name of the new task:" />                
            <asp:TextBox runat="server" placeholder="Enter task name" ID="tbTaskName" />
            <!--<asp:RequiredFieldValidator runat="server" ControlToValidate="tbTaskName" Text="Required field!" />-->
            <br />
            <asp:CheckBox runat="server" Text="Create root task" ID="cbIsRoot" />
            <div>
                <asp:Button cssclass="btn btn-success" runat="server" ID="btnAddTask" Text="Ok"  OnClick="btnAddTask_Click"/>
                <asp:Button cssclass="btn btn-warning" runat="server" ID="btnCancelAddTask" Text="Cancel" OnClick="btnCancelAddTask_Click" />    
            </div>     
        </div>
        <div runat="server" id="removeTaskDiv" visible="false">
            <asp:Label runat="server" Text="Are you sure you want to delete the task? It may contain logged hours." />
            <asp:Button cssclass="btn btn-danger" runat="server" ID="btnConfirmDelete" Text="Yes, delete anyway"  OnClick="btnConfirmDelete_Click" />
            <asp:Button cssclass="btn btn-warning" runat="server" ID="btnCancelDelete" Text="Cancel" OnClick="btnCancelDelete_Click" />    
        </div>
        <div>
            <asp:Label runat="server" ID="lblHelp" />
        </div>
    </div>   
    <div class="w3-half w3-content">
        <asp:Calendar runat="server" ID="calendar" />        
        <asp:Button cssclass="btn" runat="server" ID="btnCalendarBack" Text="< Year" OnClick="btnCalendarBack_Click" />
        <asp:Button cssclass="btn" runat="server" ID="btnCalendarForward" Text="Year >"  OnClick="btnCalendarForward_Click"/>        
        <div>
            <asp:Label runat="server" Text="Starting time:" />
            <asp:DropDownList runat="server" ID="ddlHours" />
            <asp:Label runat="server" Text=":" />
            <asp:DropDownList runat="server" ID="ddlMinutes" />
        </div>
        <div>
            <asp:Label runat="server" Text="Hours worked" />
            <asp:DropDownList runat="server" ID="ddlWorkTime" />
        </div>
        <br />
        <asp:Button cssclass="btn btn-success" runat="server" ID="btnLogHours" Text="Save hours"  OnClick="btnLogHours_Click"/>
        <br />
        <asp:Label runat="server" ID="lblConfirmSave" />
    </div>
</asp:Content>

