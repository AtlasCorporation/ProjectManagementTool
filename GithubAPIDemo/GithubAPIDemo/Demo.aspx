<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Demo.aspx.cs" Inherits="GithubAPIDemo.Demo" Async="true"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Github API demo</title>
</head>

<body>
    <form id="form1" runat="server">
    <div>
        Github username: <asp:TextBox ID="txtUser" runat="server"></asp:TextBox><br />
        Repo name: <asp:TextBox ID="txtRepo" runat="server"></asp:TextBox><br />
        <asp:Button ID="btnHae" runat="server" Text="Hae" OnClick="btnHae_Click" /><br />
        <asp:Label runat="server" Text="" ID="lblMessages" /><br />
        <h3>Project languages:</h3>
        <asp:label runat="server" Text="" ID="lblLanguages"></asp:label>
        <asp:label runat="server" Text="" ID="lblWiki"></asp:label>
        <asp:label runat="server" Text="" ID="lblHeader"></asp:label>
        <asp:label runat="server" Text="" ID="lblCommitFeed"></asp:label>
    </div>
    </form>
</body>
</html>
