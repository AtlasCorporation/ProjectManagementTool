<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" async="true"%>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .commit-feed {
          padding: 15px;
        }
        .commit-feed .feed-item {
          position: relative;
          padding-bottom: 20px;
          padding-left: 30px;
          border-left: 2px solid #e4e8eb;
        }
        .commit-feed .feed-item:last-child {
          border-color: transparent;
        }
        .commit-feed .feed-item:after {
          content: "";
          display: block;
          position: absolute;
          top: 0;
          left: -6px;
          width: 10px;
          height: 10px;
          border-radius: 6px;
          background: #fff;
          border: 1px solid #000000;
        }
        .commit-feed .feed-item .date {
          position: relative;
          top: -5px;
          color: #8c96a3;
          font-size: 13px;
        }
        .commit-feed .feed-item .text {
          position: relative;
          top: -3px;
          margin-left: 8px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    <div runat="server" id="divAlert" class="alert alert-info w3-section w3-center" visible="false">
        <asp:Label runat="server" ID="lblAlert" Text="<strong>Please select a project!</strong>" />
    </div> 
    <div class="row" runat="server" id="divHome" visible="true">
          <div class="col-sm-4">
            <asp:label cssclass="label label-danger" id="lblMessages" runat="server"></asp:label>
            <h1><asp:Label id="lblProjectName" runat="server" Text="Project name"></asp:Label></h1>
            <!-- Programming languages -->
            <div id="divLanguages" runat="server"></div><br />
            <blockquote><asp:Label id="lblProjectDesc" runat="server" Text="Project description"></asp:Label></blockquote>
            <!-- Pie charts -->
            <asp:Chart ID="usersPieChart" runat="server" /><br />
            <asp:Chart ID="userPieChart" runat="server" />           
          </div>
          <div class="col-sm-4 w3-right">
            <!-- Commit feed -->
            <h3>Latest commits</h3>
            <div class="commit-feed" id="divCommitFeed" runat="server"></div>
          </div>
    </div>
</asp:Content>