<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Text="Username"></asp:Label>
        <asp:TextBox ID="username" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Password"></asp:Label>
        <asp:TextBox ID="password" runat="server" TextMode="Password"></asp:TextBox>
        <br />
        <asp:Label ID="Label7" runat="server" Text="Retype Password"></asp:Label>
        <asp:TextBox ID="repassword" runat="server" TextMode="Password"></asp:TextBox>
        <br />
        <asp:Label ID="result" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="hashResult" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Button ID="createAcc" runat="server" Text="Create acc" OnClick="createAcc_Click" />
    </div>
    <div>
        <asp:Label ID="Label3" runat="server" Text="Username"></asp:Label>
        <asp:TextBox ID="usernamelogin" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label4" runat="server" Text="Password"></asp:Label>
        <asp:TextBox ID="passwordlogin" runat="server" TextMode="Password"></asp:TextBox>
        <br />
        <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Button ID="LogIn" runat="server" Text="Log In" OnClick="LogIn_Click" />
    </div>
    </form>
</body>
</html>
