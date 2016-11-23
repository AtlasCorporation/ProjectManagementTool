<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Documentation.aspx.cs" Inherits="Documentation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
     <div>
        <div style="width:60%;">
             <asp:Label ID="ShowDocument" runat="server" Text="Label"></asp:Label>
              <asp:TextBox Wrap="true" TextMode="MultiLine" Width="100%" Height="600" ID="ModeText" runat="server"></asp:TextBox>
        </div>
        <br />
        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
        <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" />
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
    </div>
</asp:Content>

