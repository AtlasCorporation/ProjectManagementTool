<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <div id="divAccountCreated" class="alert alert-success" style="display: none;" runat="server">
        <strong>Account created successfully!</strong> Please login below.
    </div>
    <div class="panel panel-default w3-section">      
    <div class="panel-heading"><strong>Login</strong></div>
        <div class="panel-body">
            <div class="form-horizontal">
                <div class="form-group">
                    <label class="col-sm-2 control-label" for="usernamelogin">Username</label>
                    <div class="col-sm-5">
                        <asp:TextBox ID="usernamelogin" runat="server" cssclass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label" for="passwordlogin">Password</label>
                    <div class="col-sm-5">
			       	    <asp:TextBox ID="passwordlogin" runat="server" TextMode="Password" cssclass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-5">
                        <asp:Button ID="LogIn" runat="server" Text="Login" cssclass="btn btn-success" OnClick="LogIn_Click" /><br />
                        <asp:label cssclass="label label-danger" id="lblMessages" runat="server" Text=""></asp:label><br />
                        <p><a href="CreateUser.aspx">Register for an account?</a></p>
                    </div>                 
                </div>
            </div>
        </div>
    </div>
</asp:Content>

