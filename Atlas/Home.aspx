<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <asp:label cssclass="label label-danger" id="lblMessages" runat="server"></asp:label>
    <h1><asp:Label id="txtProjectName" runat="server" Text="Project name"></asp:Label></h1>
    <blockquote><asp:Label id="txtProjectDesc" runat="server" Text="Project description"></asp:Label></blockquote>
</asp:Content>