<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="username" runat="server"></asp:TextBox>
        <br />
        <asp:TextBox ID="password" runat="server" TextMode="Password"></asp:TextBox>
        <br />
        <asp:Label ID="result" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="hashResult" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Button ID="createAcc" runat="server" Text="Log In" OnClick="createAcc_Click" />
    </div>
    </form>
</body>
</html>
