<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
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
    </form>
</body>
</html>
