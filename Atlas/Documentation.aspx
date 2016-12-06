<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Documentation.aspx.cs" Inherits="Documentation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" Runat="Server">
    
     <div runat="server" id="divAlert" class="alert alert-info w3-section w3-center" visible="false">
        <asp:Label runat="server" ID="lblAlert" Text="<strong>Please select a project first!</strong>" />     
     </div> 
     <div runat="server" id="divDocumentation">
        <div style="width:60%;">
             <asp:Label ID="ShowDocument" runat="server" Text="Label"></asp:Label>
              <asp:TextBox Wrap="true" TextMode="MultiLine" Width="100%" Height="600" ID="ModeText" runat="server"></asp:TextBox>
        </div>
        <br />
        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
        <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" />
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
        <asp:Label ID="lblTemplate" runat="server" Text="Select text template: "></asp:Label>
        <asp:dropdownlist ID="ddlSelectTemplate" runat="server"></asp:dropdownlist>
    </div>
</asp:Content>

