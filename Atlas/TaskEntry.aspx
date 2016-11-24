<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TaskEntry.aspx.cs" Inherits="TaskEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <asp:DropDownList runat="server" ID="ddlTasks" />
    <asp:TextBox runat="server" ID="tbWorkingHours" />
    <asp:TreeView runat="server" ID="twTasks" />
</asp:Content>

