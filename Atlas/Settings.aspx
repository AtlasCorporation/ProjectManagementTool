<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Settings.aspx.cs" Inherits="Settings" Async="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
<div class="panel panel-default" id="divSettings" runat="server" visible="true">
    <div class="panel-heading"><strong>Change project settings</strong></div>
        <div class="panel-body">
        <div class="form-horizontal">
            <div class="form-group">
                <label class="col-sm-2 control-label" for="txtProjectName">Project name</label>
                <div class="col-sm-5">
                    <asp:textbox class="form-control" id="txtProjectName" placeholder="Enter project name" runat="server"></asp:textbox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-sm-2 control-label" for="txtGithubUser">Github username</label>
                <div class="col-sm-5">
                    <asp:textbox class="form-control" id="txtGithubUser" placeholder="Enter Github username" runat="server" OnTextChanged="txtGithubUser_TextChanged" AutoPostBack="true"></asp:textbox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-sm-2 control-label" for="txtProjectDesc">Project description</label>
                <div class="col-sm-5">
                    <asp:textbox class="form-control" id="txtProjectDesc" placeholder="Enter short description for the project" runat="server" TextMode="MultiLine" Rows="2" style="resize:none"></asp:textbox>
                </div>
            </div>
            <div class="form-group">
                <label class="col-sm-2 control-label" for="ddlGithubRepo">Github repository</label>
                <div class="col-sm-5">
                    <asp:dropdownlist cssclass="form-control" id="ddlGithubRepo" runat="server"></asp:dropdownlist>
                </div>
            </div>
            <div class="form-group">
                <label class="col-sm-2 control-label" for="cbPrivateProject">Private project</label>
                <div class="col-sm-5" style="margin-left:20px">
                    <asp:CheckBox cssclass="checkbox" id="cbPrivateProject" runat="server" />
                </div>
            </div>
            <div class="form-group">
                <div class="col-sm-offset-2 col-sm-5">
                    <asp:button cssclass="btn btn-success" id="btnSaveChanges" onclick="btnSaveChanges_Click" runat="server" text="Save changes"></asp:button>
                    <asp:button cssclass="btn btn-warning" id="btnCancel" onclick="btnCancel_Click" runat="server" text="Cancel"></asp:button>
                    <button type="button" class="btn btn-danger" data-toggle="modal" data-target="#confirmDelete">Delete project</button>
                    <asp:label cssclass="label label-danger" id="lblMessages" runat="server" Text=""></asp:label><br /><br />
                </div>                 
            </div>
        </div>
    </div>
</div>
<div class="alert alert-info" id="divAlert" runat="server" visible="false"></div>

<!-- Confirm delete modal -->
<div class="modal fade" id="confirmDelete" tabindex="-1" role="dialog" aria-labelledby="confirmDeleteLabel">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
        <h4 class="modal-title" id="confirmDeleteLabel">Delete project</h4>
      </div>
      <div class="modal-body">
      Are you sure you want to delete this project?
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
        <asp:button cssclass="btn btn-danger" id="btnDelete" onclick="btnDelete_Click" runat="server" text="Delete"></asp:button><br />
      </div>
    </div>
  </div>
</div>
</asp:Content>