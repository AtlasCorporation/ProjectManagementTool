<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" async="true"%>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <asp:label cssclass="label label-danger" id="lblMessages" runat="server"></asp:label>
    <h1><asp:Label id="lblProjectName" runat="server" Text="Project name"></asp:Label></h1>
    <blockquote><asp:Label id="lblProjectDesc" runat="server" Text="Project description"></asp:Label></blockquote>
    <h3>Latest commits</h3>
    <asp:Label id="lblCommitFeed" runat="server" Text=""></asp:Label>
    <div>
        <asp:Chart ID="usersPieChart" runat="server" />   
        <asp:Chart ID="userPieChart" runat="server" />           
    </div>
</asp:Content>