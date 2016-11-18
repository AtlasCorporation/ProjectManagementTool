<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Documentation.aspx.cs" Inherits="Documentation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="ShowDocument" runat="server" Text="Label"></asp:Label>
        <asp:TextBox ID="ModeText" runat="server"></asp:TextBox>
        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
    </div>
    </form>
</body>
</html>
