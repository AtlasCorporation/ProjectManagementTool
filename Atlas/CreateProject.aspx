<%@ Page Title="Atlas - Create Project" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CreateProject.aspx.cs" Inherits="CreateProject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading"><b>Create new project</b></div>
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
                        <asp:textbox class="form-control" id="txtGithubUser" placeholder="Enter Github username" runat="server"></asp:textbox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label" for="txtGithubRepo">Github repository</label>
                    <div class="col-sm-5">
                        <asp:textbox class="form-control" id="txtGithubRepo" placeholder="Enter Github repository name" runat="server"></asp:textbox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-5">
                        <asp:label cssclass="label label-danger" id="lblMessages" runat="server"></asp:label>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-5">
                        <asp:button cssclass="btn btn-success" id="btnCreateProject" onclick="btnCreateProject_Click" runat="server" text="Create"></asp:button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

