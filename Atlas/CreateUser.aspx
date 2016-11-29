<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CreateUser.aspx.cs" Inherits="CreateUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <div class="panel panel-default">
    <div class="panel-heading"><strong>Register</strong></div>
        <div class="panel-body">
            <div class="form-horizontal">
                <div class="form-group">
                    <label class="col-sm-2 control-label" for="username">Username</label>
                    <div class="col-sm-5">
                        <asp:TextBox ID="username" runat="server" cssclass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label" for="password">Password</label>
                    <div class="col-sm-5">
                        <asp:TextBox ID="password" runat="server" TextMode="Password" cssclass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label" for="repassword">Repeat password</label>
                    <div class="col-sm-5">
                         <asp:TextBox ID="repassword" runat="server" TextMode="Password" cssclass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-5">
                        <asp:Button ID="createAcc" runat="server" Text="Register" cssclass="btn btn-success" OnClick="createAcc_Click" /><br />
                        <asp:label cssclass="label label-danger" id="lblMessages" runat="server" Text=""></asp:label><br />
                        <p><a href="Login.aspx">Already have an account?</a></p>
                    </div>                 
                </div>
            </div>
        </div>
    </div>
</asp:Content>

