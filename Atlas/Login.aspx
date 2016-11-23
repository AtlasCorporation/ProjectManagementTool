<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
     <div>
        <asp:Label ID="Label3" runat="server" Text="Username"></asp:Label>
        <asp:TextBox ID="usernamelogin" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label4" runat="server" Text="Password"></asp:Label>
        <asp:TextBox ID="passwordlogin" runat="server" TextMode="Password"></asp:TextBox>
        <br />
        <asp:Button ID="LogIn" runat="server" Text="Log In" OnClick="LogIn_Click" />
    </div>
     <asp:Label ID="lblMessages" runat="server" Text="Label"></asp:Label>
</asp:Content>

