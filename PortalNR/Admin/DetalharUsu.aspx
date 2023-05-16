<%@ Page Title="" Language="C#" MasterPageFile="~/Template/Template.Master" AutoEventWireup="true" CodeBehind="DetalharUsu.aspx.cs" Inherits="PortalNR.Admin.DetalharUsu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="toolkitScriptMaster" runat="server" EnableScriptGlobalization="true" EnablePageMethods="true" />
    <asp:UpdatePanel ID="updPesquisar" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <h2>Editar Usuários</h2>
            <p></p>

            <div class="container-fluid">
                <div class="alert alert-success alert-dismissible fade show" id="divSuccess" visible="false" runat="server">
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                    <strong>Successo!&nbsp;</strong><asp:Label ID="lblSucesso" runat="server" />
                </div>
                <div class="alert alert-warning alert-dismissible fade show" id="divWarning" visible="false" runat="server">
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                    <strong>Atenção!&nbsp;</strong><asp:Label ID="lblAviso" runat="server" />
                </div>
                <div class="alert alert-danger alert-dismissible fade show" id="divDanger" visible="false" runat="server">
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                    <strong>Erro!&nbsp;</strong><asp:Label ID="lblErro" runat="server" />
                </div>
                <!--PRIMEIRO CARD-->
                <div class="card">
                    <div class="card-body">
                        <div class="row row-cols-12">
                            <div class="col-md-4 mb-1">
                                <asp:HiddenField ID="txtId" runat="server" />
                                <label><strong>Nome</strong></label>
                                <asp:TextBox ID="txtNome" CssClass="form-control" runat="server" />
                                <asp:RequiredFieldValidator ID="requiredNome" ControlToValidate="txtNome" Font-Size="Smaller" ErrorMessage="Campo obrigatório" ForeColor="Red" Display="Dynamic" runat="server" />
                            </div>
                            <div class="col-md-3 mb-1">
                                <label><strong>Login</strong></label>
                                <asp:TextBox ID="txtLogin" CssClass="form-control" runat="server" />
                                <asp:RequiredFieldValidator ID="requiredLogin" ControlToValidate="txtLogin" Font-Size="Smaller" ErrorMessage="Campo obrigatório" ForeColor="Red" Display="Dynamic" runat="server" />
                            </div>
                            <div class="col-md-3 mb-1">
                                <label><strong>Perfil</strong></label>
                                <asp:DropDownList ID="ddlPerfil" CssClass="form-control" runat="server" />
                                <asp:RequiredFieldValidator ID="requiredDropPerfil" ControlToValidate="ddlPerfil" InitialValue="Selecione" Font-Size="Smaller" ErrorMessage="Selecione o perfil" ForeColor="Red" Display="Dynamic" runat="server" />
                            </div>
                            <div class="col-md-2 mt-4">
                                <asp:CheckBox ID="chkAtivo" CssClass="custom-checkbox-input md-5" Checked="true" runat="server" />
                                <label><strong>&nbsp;Ativo</strong></label>
                            </div>
                        </div>
                        <div class="row row-cols-12 mb-2">
                            <div class="col-md-6 mb-1">
                                <label><strong>E-mail</strong></label>
                                <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredEmail" ControlToValidate="txtEmail" Font-Size="Smaller" ErrorMessage="Campo obrigatório" ForeColor="Red" Display="Dynamic" runat="server" />
                                <asp:RegularExpressionValidator ID="validateEmail" ControlToValidate="txtEmail" Font-Size="Smaller" ErrorMessage="E-mail inválido" ForeColor="Red" Display="Dynamic" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" runat="server" />
                            </div>                            
                        </div>
                        <hr />
                        <div class="row row-cols-12">
                            <div class="col-md-12">
                                <div class="d-flex justify-content-end">
                                    <asp:LinkButton ID="btnAtualizar" CssClass="btn btn-primary" OnClick="btnAtualizar_Click" runat="server"><i class="fa fa-fw fa-floppy-o"></i>&nbsp;Atualizar</asp:LinkButton>&nbsp;
                                    <asp:LinkButton ID="btnCancelar" CssClass="btn btn-secondary" OnClick="btnCancelar_Click" CausesValidation="false" runat="server"><i class="fa fa-fw fa-undo"></i>&nbsp;Cancelar</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
