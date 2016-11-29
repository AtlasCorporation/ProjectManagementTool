<%@ Page Title="Atlas - Create Project" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CreateProject.aspx.cs" Inherits="CreateProject" Async="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <div class="panel panel-default">
        <div class="panel-heading"><strong>Create new project</strong></div>
        <div class="panel-body">
            <div class="form-horizontal">
                <div class="form-group">
                    <label class="col-sm-2 control-label" for="txtProjectName"><span style="color:red">*&nbsp;</span>Project name</label>
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
                        <asp:button cssclass="btn btn-success" id="btnCreateProject" onclick="btnCreateProject_Click" runat="server" text="Create"></asp:button><br />
                        <asp:label cssclass="label label-danger" id="lblMessages" runat="server" Text=""></asp:label><br /><br />
                        <span style="color:red">*</span> Indicates required field.
                    </div>                 
                </div>
            </div>
        </div>
    </div>
</asp:Content>

