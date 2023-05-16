<%@ Page Title="" Language="C#" MasterPageFile="~/Template/Template.Master" AutoEventWireup="true" CodeBehind="Login1.aspx.cs" Inherits="PortalNR.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="toolkitScriptMaster" runat="server" EnableScriptGlobalization="true" EnablePageMethods="true" />
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
                        <p style="text-align: center"><img src="Images/Logo_auth.png" width="50%" height="50%"/></p>
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
                            </div>
                        </div>
                        <div class="form-group">                            
                            <asp:Button ID="btnAcesso" Text="Login" CssClass="btn btn-primary btn-block" OnClick="btnAcesso_Click" runat="server" />
                            <br />
                            <p style="text-align: center"><asp:Label ID="lblMensagem" CssClass="alert-danger" runat="server" /></p>
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
    <br />
    <br />
    <br />
</asp:Content>
