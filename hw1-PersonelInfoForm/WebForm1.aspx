<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebFormsHW1.Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Web Forms Assignment</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background: linear-gradient(135deg, #00bcd4, #009688);
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            color: #333;
            overflow-x: hidden;
        }
        .container {
            max-width: 600px;
            background: rgba(255, 255, 255, 0.9);
            padding: 40px;
            border-radius: 15px;
            box-shadow: 0px 15px 30px rgba(0, 188, 212, 0.4);
            margin-top: 80px;
            border: 2px solid #00bcd4;
        }
        h2 {
            text-align: center;
            color: #00bcd4;
            font-size: 30px;
            margin-bottom: 30px;
            font-weight: bold;
            text-transform: uppercase;
        }
        .btn-custom {
            width: 100%;
            background-color: #009688;
            color: white;
            border: none;
            padding: 15px;
            border-radius: 8px;
            font-size: 18px;
            font-weight: bold;
            transition: background-color 0.3s ease, transform 0.2s ease, box-shadow 0.3s ease;
            box-shadow: 0 0 15px rgba(0, 188, 212, 0.5);
        }
        .btn-custom:hover {
            background-color: #00bcd4;
            cursor: pointer;
            transform: translateY(-3px);
            box-shadow: 0 0 20px rgba(0, 188, 212, 0.7);
        }
        #lblResult {
            display: block;
            width: 100%;
            padding: 15px;
            background-color: rgba(0, 188, 212, 0.1);
            border-radius: 8px;
            margin-top: 20px;
            color: #00bcd4;
            font-size: 16px;
            white-space: pre-line;
            border: 2px solid #00bcd4;
            box-shadow: 0px 4px 12px rgba(0, 188, 212, 0.3);
        }
        #lblResult strong {
            font-weight: bold;
            color: #00bcd4;
            display: block;
            margin-top: 10px;
        }
        #lblResult ul {
            padding-left: 20px;
        }
        #lblResult li {
            list-style-type: disc;
            margin-left: 20px;
        }
        .form-label {
            font-size: 16px;
            font-weight: 600;
            color: #00bcd4;
        }
        .form-control, .form-select, .form-check {
            border-radius: 8px;
            border: 1px solid #009688;
            background-color: #fff;
            color: #333;
            box-shadow: none;
            padding: 12px;
            font-size: 16px;
            transition: border-color 0.3s ease, box-shadow 0.3s ease;
        }
        .form-control:focus, .form-select:focus, .form-check:focus {
            border-color: #00bcd4;
            box-shadow: 0px 0px 8px rgba(0, 188, 212, 0.5);
        }
        .form-check {
            padding-left: 20px;
        }
        .form-text {
            font-size: 14px;
            color: #666;
        }
        .mb-3 {
            margin-bottom: 20px;
        }
        .form-check input[type="checkbox"], .form-check input[type="radio"] {
            width: 20px;
            height: 20px;
        }
        .form-check label {
            font-size: 16px;
        }
    </style>
</head>
<body>
    <div class="container">
        <h2>Personal Information Form</h2>
        <form id="form1" runat="server">
            <div class="mb-3">
                <label class="form-label">Name:</label>
                <asp:TextBox ID="txtName" CssClass="form-control" runat="server"></asp:TextBox>
            </div>

            <div class="mb-3">
                <label class="form-label">Country:</label>
                <asp:DropDownList ID="ddlCountry" CssClass="form-select" runat="server">
                    <asp:ListItem Text="Select Country" Value="" />
                    <asp:ListItem Text="USA" Value="USA" />
                    <asp:ListItem Text="UK" Value="UK" />
                    <asp:ListItem Text="Canada" Value="Canada" />
                </asp:DropDownList>
            </div>

            <div class="mb-3">
                <label class="form-label">Hobbies:</label>
                <asp:CheckBoxList ID="chkHobbies" runat="server" CssClass="form-check">
                    <asp:ListItem Text="Reading" Value="Reading" />
                    <asp:ListItem Text="Gaming" Value="Gaming" />
                    <asp:ListItem Text="Traveling" Value="Traveling" />
                </asp:CheckBoxList>
            </div>

            <div class="mb-3">
                <label class="form-label">Gender:</label>
                <asp:RadioButtonList ID="rblGender" runat="server" CssClass="form-check">
                    <asp:ListItem Text="Male" Value="Male" />
                    <asp:ListItem Text="Female" Value="Female" />
                </asp:RadioButtonList>
            </div>

            <div class="mb-3">
                <label class="form-label">Select Birth Date:</label>
                <asp:Calendar ID="calBirthDate" runat="server" CssClass="form-control" OnSelectionChanged="calBirthDate_SelectionChanged"></asp:Calendar>
                <asp:Label ID="lblSelectedDate" CssClass="form-text text-muted" runat="server" Text=""></asp:Label>
            </div>

            <asp:Button ID="btnSubmit" runat="server" CssClass="btn-custom" Text="Submit" OnClick="btnSubmit_Click" />

            <div class="mt-4">
                <asp:Label ID="lblResult" CssClass="alert alert-info" runat="server" Text=""></asp:Label>
            </div>
        </form>
    </div>
</body>
</html>
