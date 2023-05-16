<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PortalNR.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="content-type" content="text/html; charset=Windows-1252" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="content-type" content="application/xhtml+xml; charset=UTF-8" />
    <title>&nbsp;Portal Normativos</title>

    <link rel="shortcut icon" type="image/x-icon" href="../favicon.ico" />

    <link href="Content/bootstrap.css" rel="stylesheet" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link href="Content/Button.css" rel="stylesheet" />
    <link href="Content/Custom.css" rel="stylesheet" />
    <link href="css/autocomplete.css" rel="stylesheet" />

    <script src="Scripts/jquery-3.0.0.min.js"></script>
    <script src="Scripts/popper.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="container" style="margin-top: 50px">
                <br />
                <hr />
                <div class="row">
                    <aside class="col-sm-4">
                        <!-- coluna 1 -->
                        <!-- coluna vazia -->
                    </aside>
                    <aside class="col-sm-4">
                        <!-- coluna 2 -->
                        <br />
                        <div class="card">
                            <div class="card-body">
                                <h4 class="card-title text-center mb-4 mt-1">Portal Normativos</h4>
                                <hr />
                                <div class="form-group">
                                    <p style="text-align: center">
                                        <img src="Images/Logo_Metro_sombra.png" width="230" height="230" />
                                    </p>
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text"><i class="fa fa-user"></i></span>
                                        </div>
                                        <asp:TextBox ID="txtLogin" CssClass="form-control" placeholder="Login" runat="server" />
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="input-group">
                                        <div class="input-group-prepend">
                                            <span class="input-group-text"><i class="fa fa-lock"></i></span>
                                        </div>
                                        <asp:TextBox ID="txtSenha" CssClass="form-control" placeholder="******" TextMode="password" runat="server" />
                                        <%--<asp:RegularExpressionValidator ID="revSenha" runat="server" ControlToValidate="txtSenha" Display="None" SetFocusOnError="true" ErrorMessage="Senha deve ter entre 3 a 10 caracteres!" ValidationExpression="^[a-zA-Z0-9'@&#.\s]{3,10}$" />--%>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Button ID="btnAcesso" Text="Login" CssClass="btn btn-primary btn-block" OnClick="btnAcesso_Click" runat="server" />
                                    <br />
                                    <p style="text-align: center">
                                        <asp:Label ID="lblMensagem" CssClass="alert-danger" runat="server" />
                                    </p>
                                </div>
                                <%--<p class="text-center"><a href="#" class="btn">Esqueceu a senha?</a></p>--%>
                            </div>
                        </div>
                    </aside>
                    <aside class="col-sm-4">
                        <!-- coluna 3 -->
                        <!-- coluna vazia -->
                    </aside>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
