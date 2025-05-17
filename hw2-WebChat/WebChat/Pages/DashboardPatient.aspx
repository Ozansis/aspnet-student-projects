<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DashboardPatient.aspx.cs" Inherits="WebChat.Pages.DashboardPatient" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Patient Dashboard</title>
    <link rel="stylesheet" href="../Content/style.css" />
</head>
<body>
    <form id="form1" runat="server">
        <h2>Welcome, Patient!</h2>
        <p><asp:Label ID="lblWelcome" runat="server" /></p>
        <p>
    <a href="Profile.aspx">Edit Profile</a> |
    <a href="Logout.aspx">Logout</a>
</p>

        <h3>Available Doctors:</h3>
        <asp:Repeater ID="rptContacts" runat="server">
            <ItemTemplate>
                <div style="margin-bottom:5px;">
                    <a href="Chat.aspx?contactId=<%# Eval("User_id") %>">
                        <%# Eval("FullName") %> (<%# Eval("Username") %>)
                    </a>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </form>
</body>
</html>
