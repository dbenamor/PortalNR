<%@ Page Title="" Language="C#" MasterPageFile="~/Template/Template.Master" AutoEventWireup="true" CodeBehind="DetalharNR.aspx.cs" Inherits="PortalNR.Admin.DetalharNR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="toolkitScriptMaster" runat="server" EnableScriptGlobalization="true" EnablePageMethods="true" />
    <asp:UpdatePanel ID="updPesquisar" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <h2>Editar Normativos</h2>
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
                <div class="card">
                    <div class="card-body">
                        <div class="row row-cols-12 mb-2">
                            <div class="col-md-4 mb-1">
                                <asp:HiddenField ID="txtId" runat="server" />
                                <label><strong>Normativo</strong></label>
                                <asp:TextBox ID="txtNR" CssClass="form-control" runat="server" />
                                <%--<asp:RequiredFieldValidator ID="requiredVigencia" runat="server" ErrorMessage="Informe a vigência!" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtNR" />--%>
                            </div>
                            <div class="col-md-4 mb-1">
                                <label><strong>Vigência</strong></label>
                                <asp:TextBox ID="txtVigencia" CssClass="form-control" runat="server" />
                                <asp:RegularExpressionValidator ID="regexVigencia" runat="server" ControlToValidate="txtVigencia" ErrorMessage="Permitido somente números" Font-Size="Smaller" ForeColor="Red" Display="Dynamic" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                <%--<asp:RequiredFieldValidator ID="requiredVigencia" runat="server" ErrorMessage="Informe a vigência!" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtVigencia" />--%>
                            </div>
                            <div class="col-md-4 mt-4">
                                <asp:CheckBox ID="chkAtivo" CssClass="custom-checkbox-input md-5" Checked="true" runat="server" />
                                <label><strong>&nbsp;Ativo</strong></label>
                            </div>
                        </div>
                        <div class="row row-cols-12">
                            <div class="col-md-12 mb-1">
                                <label><strong>Descrição</strong></label>
                                <asp:TextBox ID="txtDescricao" TextMode="multiline" CssClass="form-control" Rows="3" style="resize:none;" runat="server" />
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
