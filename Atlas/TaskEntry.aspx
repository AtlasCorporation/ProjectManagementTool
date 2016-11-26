<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TaskEntry.aspx.cs" Inherits="TaskEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">    
    <div>
        <div class="w3-half w3-content w3-center w3-container">
            <div runat="server" id="virginDiv" visible="false" class="w3-container">                
                <asp:Label runat="server" Text="Create a new task" />   
                <div class="w3-content form-horizontal">
                    <div class="form-group">
                        <asp:Label runat="server" Text="Name:" />
                        <asp:TextBox runat="server" cssclass="form-control" placeholder="Enter task name" ID="tbVirginTask" />  
                    </div>                            
                </div>
                <asp:Button cssclass="btn btn-success" runat="server" Text="Add task" ID="btnVirginTask" OnClick="btnVirginTask_Click" />
            </div>
            <div runat="server" id="taskControlDiv" class="w3-container">
                <div class="w3-content">
                    <asp:Label runat="server" Text="Selected task:" />
                    <asp:Label runat="server" ID="lblSelectedTask" Text="None selected" />
                </div>    
             
                <asp:TreeView runat="server" ID="twTasks"  OnSelectedNodeChanged="twTasks_SelectedNodeChanged"/>
                <asp:Button cssclass="btn btn-success" runat="server" Text="Add task" ID="btnShowAddTask" OnClick="btnShowAddTask_Click" />
                <asp:Button cssclass="btn btn-danger" runat="server" Text="Delete task" ID="btnShowDeleteTask" OnClick="btnShowDeleteTask_Click" />
            
                <div runat="server" id="addTaskDiv" visible="false" class="w3-container">  
                    <asp:Label runat="server" Text="Name of the new task:" />                
                    <asp:TextBox runat="server" cssclass="form-control" placeholder="Enter task name" ID="tbTaskName" />
                    <!--<asp:RequiredFieldValidator runat="server" ControlToValidate="tbTaskName" Text="Required field!" />-->
                    <br />
                    <asp:CheckBox runat="server" Text="Create root task" ID="cbIsRoot" />
                    <div class="w3-content">
                        <asp:Button cssclass="btn btn-success" runat="server" ID="btnAddTask" Text="Ok"  OnClick="btnAddTask_Click"/>
                        <asp:Button cssclass="btn btn-warning" runat="server" ID="btnCancelAddTask" Text="Cancel" OnClick="btnCancelAddTask_Click" />    
                    </div>     
                </div>
                <div runat="server" id="removeTaskDiv" visible="false" class="w3-container">
                    <asp:Label runat="server" Text="Are you sure you want to delete the task? It may contain logged hours." />
                    <div class="w3-content">                        
                        <asp:Button cssclass="btn btn-danger" runat="server" ID="btnConfirmDelete" Text="Yes, delete anyway"  OnClick="btnConfirmDelete_Click" />
                        <asp:Button cssclass="btn btn-warning" runat="server" ID="btnCancelDelete" Text="Cancel" OnClick="btnCancelDelete_Click" />  
                    </div>  
                </div>
            </div>
        </div>   
        <div class="w3-half w3-content w3-center w3-container">
            <div class="w3-container">
                <div class="w3-content">
                    <asp:Calendar runat="server" ID="calendar" /> 
                </div>       
                <div class="w3-content">
                    <asp:Button cssclass="btn" runat="server" ID="btnCalendarBack" Text="< Year" OnClick="btnCalendarBack_Click" />
                    <asp:Button cssclass="btn" runat="server" ID="btnCalendarForward" Text="Year >"  OnClick="btnCalendarForward_Click"/>    
                </div>    
            </div>
            <div class="w3-content">
                <asp:Label runat="server" Text="Starting time:" />
                <asp:DropDownList runat="server" ID="ddlHours" />
                <asp:Label runat="server" Text=":" />
                <asp:DropDownList runat="server" ID="ddlMinutes" />
            </div>
            <div class="w3-content">
                <asp:Label runat="server" Text="Hours worked" />
                <asp:DropDownList runat="server" ID="ddlWorkTime" />
            </div>
            <br />
            <asp:Button cssclass="btn btn-success" runat="server" ID="btnLogHours" Text="Save hours"  OnClick="btnLogHours_Click" Enabled="false"/>
            <br />
            <asp:Label runat="server" ID="lblConfirmSave" />        
        </div>
        <div class="w3-bottombar">
            <div class="w3-center w3-content">
                <asp:Label runat="server" ID="lblHelp" Text="Work work!" />                
            </div>        
        </div>

    </div>
</asp:Content>

