<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TaskEntry.aspx.cs" Inherits="TaskEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .calendar {
            display: block; 
            width: 200px;
            margin: 0 auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">   
    <div runat="server" id="reminderDiv" class="alert alert-info w3-section w3-center">
        <asp:Label runat="server" ID="lblReminder" Text="<strong>Please login and select a project before managing logged data!</strong>" />
    </div> 
    <div runat="server" id="mainDiv" class="w3-section panel panel-default">
        <div>
            <div class="w3-center alert-info">
                    <asp:Label runat="server" Text="Selected task:" />
                    <asp:Label runat="server" ID="lblSelectedTask" Text="None selected" />
            </div>
            <div class="w3-content w3-center w3-padding-medium w3-twothird">
                <!-- DONETASKS GRID VIEW -->
                <div class="w3-content" style="float:right;">
                    <asp:GridView runat="server" ID="gvDonetasks" AutoGenerateColumns="false" >
                    <Columns>                  
                      <asp:BoundField DataField="id" Visible="false" ReadOnly="true" />
                      <asp:BoundField DataField="worktime" HeaderText="Duration" ReadOnly="true"/>
                      <asp:BoundField DataField="date" HeaderText="Starting date and time" ReadOnly="true" />
                    </Columns> 
                    </asp:GridView>
               </div>
                <div runat="server" id="virginDiv" visible="false" class="w3-padding">                
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
                    <div style="margin-bottom:5%;">
                        <asp:TreeView runat="server" ID="twTasks" OnSelectedNodeChanged="twTasks_SelectedNodeChanged"/>
                    </div> 
                    
                    <!-- Add popup -->
                    <div runat="server" id="addTaskDiv" visible="false" class="w3-container w3-left" style="margin-top:10%; width:60%; float:left;">  
                        <asp:Label runat="server" Text="Name of the new task:" />                
                        <asp:TextBox runat="server" cssclass="form-control" placeholder="Enter task name" ID="tbTaskName" />
                        <!--<asp:RequiredFieldValidator runat="server" ControlToValidate="tbTaskName" Text="Required field!" />-->
                        <br />
                         <asp:CheckBox runat="server" Text="Create root task" ID="cbIsRoot" OnCheckedChanged="cbIsRoot_CheckedChanged" AutoPostBack="true" />
                        <div runat="server" id="parentSelectionDiv">                            
                            <asp:Label runat="server" ID="lblparentTask" Text="Parent task:" />
                            <asp:Label runat="server" ID="lblNewRootTask" Text="Creating new root task..." />
                            <asp:Label runat="server" ID="lblParent" Text="None selected" />
                        </div>                       
                        <div class="w3-content w3-margin" style="float:left;">
                            <asp:Button cssclass="btn btn-success" cssstyle="float:left;" runat="server" ID="btnAddTask" Text="Ok"  OnClick="btnAddTask_Click"/>
                            <asp:Button cssclass="btn btn-warning" runat="server" ID="btnCancelAddTask" Text="Cancel" OnClick="btnCancelAddTask_Click" />    
                        </div>     
                    </div>
                    <!-- Remove popup -->
                    <div runat="server" id="removeTaskDiv" visible="false" class="w3-container w3-left" style="width:60%; float:left;">
                        <asp:Label runat="server" Text="Are you sure you want to delete the task? It may contain logged hours." />
                        <br />
                        <div class="w3-content w3-left">                        
                            <asp:Button cssclass="btn btn-danger" runat="server" ID="btnConfirmDelete" Text="Yes, delete anyway"  OnClick="btnConfirmDelete_Click" />
                            <asp:Button cssclass="btn btn-warning" runat="server" ID="btnCancelDelete" Text="Cancel" OnClick="btnCancelDelete_Click" />  
                        </div>  
                    </div>
                    <!-- ADD/DEL -->
                    <div runat="server" class="w3-container w3-left" style="width:60%; float:left;">
                        <div class="w3-content w3-left" style="margin-top:1%; margin-bottom:1%;">                        
                              <asp:Button cssclass="btn btn-success" runat="server" Text="Add task" ID="btnShowAddTask" OnClick="btnShowAddTask_Click" />
                              <asp:Button cssclass="btn btn-danger" runat="server" Text="Delete task" ID="btnShowDeleteTask" OnClick="btnShowDeleteTask_Click" />
                        </div>  
                    </div>

                    <!-- Add USER to project -->
                    <div class="w3-container w3-left" style="width:60%; float:left;">
                        <div class="w3-content w3-left" style="margin-top:1%; margin-bottom:1%;">         
                               <button type="button" class="btn btn-primary btn-lg" data-toggle="modal" data-target="#addUsers">Add users to this project</button><br />
                               <asp:Label runat="server" ID="lblHelp" Text="" />                
                        </div>   
                    </div>
             </div>
        </div>   
            <div class="w3-third w3-content w3-center w3-padding-medium" style="display:block">
                <div class="w3-container">
                    <asp:Calendar runat="server" ID="calendar" CssClass="calendar" />                 
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
                <asp:Button cssclass="btn btn-success" runat="server" ID="btnLogHours" Text="Save work"  OnClick="btnLogHours_Click" Enabled="true"/>
                <br />
                <asp:Label runat="server" ID="lblConfirmSave" />        
                
            </div>                           
        </div>
    </div>

<!-- Add users modal -->
<div class="modal fade" id="addUsers" tabindex="-1" role="dialog" aria-labelledby="addUsersLabel">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <h4 class="modal-title" id="addUsersLabel">Add users to this project</h4>
      </div>
      <div class="modal-body">
        <div class="container">
          <asp:CheckBoxList ID="cblUsers" cssclass="checkbox" runat="server"></asp:CheckBoxList>
        </div>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
        <asp:Button ID="btnAddUsersToProject" runat="server" Text="Confirm" cssclass="btn btn-primary" OnClick="btnAddUsersToProject_Click" /><br />
      </div>
    </div>
  </div>
</div>
</asp:Content>

