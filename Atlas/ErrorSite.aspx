<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ErrorSite.aspx.cs" Inherits="ErrorSite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
     <div>
        <h1>You didn't try anything suspicious did you?</h1>
        <asp:Image ID="Burglar" runat="server" ImageUrl="~/Resources/burglar.png" />
        <br />
        <asp:Button ID="btnBackToPrevious" runat="server" Text="Back to previous page" OnClick="btnBackToPrevious_Click" />
    </div>
</asp:Content>

