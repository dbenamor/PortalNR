<%@ Page Title="" Language="C#" MasterPageFile="~/Template/Template.Master" AutoEventWireup="true" CodeBehind="DetalharEmail.aspx.cs" Inherits="PortalNR.Admin.DetalharEmail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="toolkitScriptMaster" runat="server" EnableScriptGlobalization="true" EnablePageMethods="true" />
    <asp:UpdatePanel ID="updPesquisar" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <h2>Editar cadastro de email</h2>
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
                <asp:HiddenField ID="txtId" runat="server" />
                <div class="card">
                    <div class="card-body">
                        <div class="row row-cols-12">
                            <div class="col-md-4 mb-1">
                                <label><strong>Coordenação</strong></label>
                                <asp:DropDownList ID="ddlCoordenacao" CssClass="form-control" runat="server" />
                            </div>
                            <div class="col-md-3 mb-1">
                                <label><strong>Colaborador responsável</strong></label>
                                <asp:TextBox ID="txtResponsavel" CssClass="form-control" runat="server" />
                            </div>
                            <div class="col-md-3 mb-1">
                                <label><strong>E-mail responsável</strong></label>
                                <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server" />
                            </div>
                            <div class="col-md-2 mt-4">
                                <asp:CheckBox ID="chkAtivo" CssClass="custom-checkbox-input md-5" Checked="true" runat="server" />
                                <label><strong>&nbsp;Ativo</strong></label>
                            </div>
                        </div>
                        <hr />
                        <div class="row row-cols-12">
                            <div class="col-md-12">
                                <div class="d-flex justify-content-end">
                                    <asp:LinkButton ID="btnAtualizar" CssClass="btn btn-primary" OnClick="btnAtualizar_Click" runat="server"><i class="fa fa-fw fa-floppy-o"></i>&nbsp;Atualizar</asp:LinkButton>&nbsp;
                                    <asp:LinkButton ID="btnCancelar" CssClass="btn btn-secondary" OnClick="btnCancelar_Click" runat="server"><i class="fa fa-fw fa-undo"></i>&nbsp;Cancelar</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!--SEGUNDO CARD-->
                <br />
                <br />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
