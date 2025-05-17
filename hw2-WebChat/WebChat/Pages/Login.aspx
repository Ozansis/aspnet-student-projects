<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebChat.Pages.Login" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="max-width:300px;margin:50px auto">
            <h2>Chat Girişi</h2>
            <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />
            <div>
                <asp:TextBox ID="txtUsername" runat="server" Placeholder="Kullanıcı Adı" />
            </div>
            <div>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Placeholder="Parola" />
            </div>
            <div>
                <asp:Button ID="btnLogin" runat="server" Text="Giriş Yap" OnClick="btnLogin_Click" />
            </div>
        </div>
    </form>
</body>
</html>
