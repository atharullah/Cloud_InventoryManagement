<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="InventoryManagement.Pages.Main.LogIn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Log In</title>
    <link href="/CSS/bootstrap.min.css" rel="stylesheet" />
    <link href="/CSS/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="/CSS/Custom.css" rel="stylesheet" />
    <script src="/Scripts/jquery-1.10.2.js"></script>
    <script src="/Scripts/jquery-ui%201.11.14.js"></script>
    <script src="/Scripts/bootstrap.min.js"></script>
    <style>
        body, input {
            font-family: "Segoe UI", "Helvetica Neue", Helvetica, Arial, sans-serif !important;
        }

        .btn-primary {
            background-color: #34495e !important;
            background-image: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" EnablePageMethods="true"></asp:ScriptManager>
        <div class="container">
            <div class="jumbotron" style="width:50%;margin:auto">
                <h4 class="text-center">
                    <asp:Label runat="server" ID="lblMessage"></asp:Label>
                </h4>
                <div>
                    <h3 class="text-center">
                        <span class="glyphicon glyphicon-user"></span>&nbsp;Log In
                    </h3>
                    <div class="form-group">
                        <label>
                            <asp:Label runat="server" Text="User Name"></asp:Label></label>
                        <asp:TextBox CssClass="form-control" runat="server" ID="txtUserName"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>
                            <asp:Label runat="server" Text="Password"></asp:Label></label>
                        <asp:TextBox CssClass="form-control" runat="server" ID="txtPassword" TextMode="Password"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Button CssClass="btn btn-primary btn-block" runat="server" ID="btnLogin" Text="LogIn" OnClick="btnLogin_Click" />
                    </div>
                </div>
            </div>

        </div>
    </form>
</body>
</html>
