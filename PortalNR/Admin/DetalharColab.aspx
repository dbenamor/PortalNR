<%@ Page Title="" Language="C#" MasterPageFile="~/Template/Template.Master" AutoEventWireup="true" CodeBehind="DetalharColab.aspx.cs" Inherits="PortalNR.Admin.DetalharColab" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="toolkitScriptMaster" runat="server" EnableScriptGlobalization="true" EnablePageMethods="true" />
    <script type="text/javascript">
        function setFuncionarioPesquisa(source, eventargs) {
            document.getElementById('<%= hdFuncionarioPesquisa.ClientID %>').value = eventargs.get_value();
        }
    </script>
    <asp:UpdatePanel ID="updPesquisar" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <h2>Atualizar Colaborador</h2>
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
                            <div class="col-md-2 mb-1">
                                <asp:HiddenField ID="txtId" runat="server" />
                                <label><strong>Normativo</strong></label>
                                <asp:DropDownList ID="ddlNR" CssClass="form-control" AutoPostBack="true" OnTextChanged="ddlNR_TextChanged" runat="server" />
                            </div>
                            <div class="col-md-2 mb-1">
                                <label><strong>Matricula</strong></label>
                                <asp:TextBox ID="txtMatric" CssClass="form-control" ReadOnly="true" AutoPostBack="true" runat="server" />
                            </div>
                            <div class="col-md-6 mb-1">
                                <label><strong>Nome</strong></label>
                                <asp:TextBox ID="txtNome" CssClass="form-control" ReadOnly="true" runat="server" />
                            </div>
                            <div class="col-md-2 mt-5">
                                <asp:CheckBox ID="chkElegivel" CssClass="custom-checkbox-input" Checked="true" runat="server" />
                                <label><strong>&nbsp;Elegível</strong></label>
                            </div>
                        </div>
                        <div class="row row-cols-12 mb-2">
                            <div class="col-md-4 mb-1">
                                <label><strong>Coordenação</strong></label>
                                <asp:TextBox ID="txtCoordenacao" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hdFuncionarioPesquisa" />
                                <asp:AutoCompleteExtender runat="server" BehaviorID="biUnidade" ID="aceFuncionarioPesquisa" TargetControlID="txtCoordenacao" MinimumPrefixLength="3" CompletionSetCount="30"
                                    FirstRowSelected="true" ServiceMethod="getFuncionario" UseContextKey="true" ServicePath="AutoComplete.aspx" CompletionListCssClass="autocomplete_elemento"
                                    CompletionListItemCssClass="autocomplete_item" CompletionListHighlightedItemCssClass="autocomplete_realce" OnClientItemSelected="setFuncionarioPesquisa" DelimiterCharacters="" Enabled="True" />
                            </div>
                            <div class="col-md-4 mb-1">
                                <label><strong>Situação Colaborador <span class="smallfont" style="color: red;">Informação do efetivo</span></strong></label>
                                <asp:TextBox ID="txtSituacao" CssClass="form-control" ReadOnly="true" runat="server" />
                            </div>
                            <div class="col-md-4 mb-1">
                                <label><strong>Situação Normativo</strong></label>
                                <asp:DropDownList ID="ddlSituacaoNR" CssClass="form-control" Enabled="false" runat="server" />
                            </div>
                        </div>
                        <div class="row row-cols-12">
                            <div class="col-md-3 mb-1">
                                <label><strong>Equipamento <span class="smallfont">(NR-11)</span></strong></label>
                                <asp:DropDownList ID="ddlVeiculo" Enabled="false" CssClass="form-control" runat="server" />
                            </div>
                            <div class="col-md-3 mb-1">
                                <label><strong>Data Certificação</strong></label>
                                <asp:TextBox ID="txtCertificacao" CssClass="form-control" TextMode="Date" AutoPostBack="true" onkeypress="return false;" OnTextChanged="txtCertificacao_TextChanged" runat="server" />
                            </div>
                            <div class="col-md-3 mb-1">
                                <label><strong>Data Revalidação</strong></label>
                                <asp:TextBox ID="txtRevalidacao" CssClass="form-control" TextMode="Date" AutoPostBack="true" onkeypress="return false;" OnTextChanged="txtRevalidacao_TextChanged" runat="server" />
                            </div>
                            <div class="col-md-3 mb-1">
                                <label><strong>Data Vigência</strong></label>
                                <asp:TextBox ID="txtTempo" Visible="false" runat="server" />
                                <asp:TextBox ID="txtVigencia" CssClass="form-control" TextMode="DateTime" ReadOnly="true" runat="server" />
                            </div>
                        </div>
                        <hr />
                        <div class="row row-cols-12">
                            <div class="col-md-12">
                                <div class="d-flex justify-content-end">
                                    <asp:LinkButton ID="btnAtualizar" CssClass="btn btn-primary" OnClick="btnAtualizar_Click" runat="server"><i class="fa fa-fw fa-floppy-o"></i>&nbsp;Atualizar</asp:LinkButton>&nbsp;
                                    <asp:LinkButton ID="btnCancelar" CssClass="btn btn-secondary" OnClick="btnCancelar_Click" runat="server"><i class="fa fa-fw fa-undo"></i>&nbsp;Voltar</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>                
                <br />
                <br />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
