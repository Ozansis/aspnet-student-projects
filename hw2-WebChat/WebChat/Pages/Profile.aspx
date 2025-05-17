<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="WebChat.Profile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Your Profile</title>
    <link rel="stylesheet" href="../Content/style.css" />
</head>
<body>
     <form id="form1" runat="server">
        <h2>Your Profile</h2>
        <asp:Label ID="lblMessage" runat="server" ForeColor="Green" />

        <p>Full Name: <asp:TextBox ID="txtFullName" runat="server" /></p>
        <p>Email: <asp:TextBox ID="txtEmail" runat="server" /></p>
        <p>Username: <asp:TextBox ID="txtUsername" runat="server" /></p>
        <p>Password: <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" /></p>

        <asp:Button ID="btnSave" runat="server" Text="Update Profile" OnClick="btnSave_Click" />
        <br /><br />
        <a href="Dashboard<%= Session["Role"] %>.aspx">← Back to Dashboard</a>
    </form>
</body>
</html>
