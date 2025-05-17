<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WebChat.Pages.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register</title>
    <style>
        body {
            font-family: Arial;
            background-color: #f0f2f5;
        }

        .register-container {
            width: 400px;
            margin: 100px auto;
            background: white;
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0 4px 10px rgba(0,0,0,0.1);
        }

        .register-container h2 {
            text-align: center;
            margin-bottom: 20px;
        }

        .register-container input[type="text"],
        .register-container input[type="email"],
        .register-container input[type="password"] {
            width: 100%;
            padding: 10px;
            margin-bottom: 15px;
            border-radius: 6px;
            border: 1px solid #ccc;
        }

        .register-container input[type="submit"] {
            width: 100%;
            padding: 10px;
            background-color: #007bff;
            border: none;
            color: white;
            border-radius: 6px;
            cursor: pointer;
        }

        .message {
            color: green;
            text-align: center;
            margin-top: 10px;
        }

        .error {
            color: red;
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="register-container">
            <h2>Register</h2>

            <asp:TextBox ID="txtFullName" runat="server" placeholder="Full Name" />
            <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" placeholder="Email" />
            <asp:TextBox ID="txtUsername" runat="server" placeholder="Username" />
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Password" />

            <asp:RadioButtonList ID="rblRole" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Text="Doctor" Value="Doctor" />
                <asp:ListItem Text="Patient" Value="Patient" Selected="True" />
            </asp:RadioButtonList>

            <asp:Button ID="btnRegister" runat="server" Text="Sign Up" OnClick="btnRegister_Click" />
            <p style="text-align: center;">
    Already have an account? <a href="Login.aspx">Login here</a>
</p>
            <asp:Label ID="lblMessage" runat="server" CssClass="message" />
        </div>
    </form>
</body>
</html>
