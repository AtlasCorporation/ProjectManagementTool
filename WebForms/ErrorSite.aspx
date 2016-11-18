<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ErrorSite.aspx.cs" Inherits="Resources_ErrorSite" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>You didn't try anything suspicious did you?</h1>
        <asp:Image ID="Burglar" runat="server" ImageUrl="~/Resources/burglar.png" />
        <br />
        <asp:Button ID="btnBackToPrevious" runat="server" Text="Back to previous page" OnClick="btnBackToPrevious_Click" />
    </div>
    </form>
</body>
</html>
