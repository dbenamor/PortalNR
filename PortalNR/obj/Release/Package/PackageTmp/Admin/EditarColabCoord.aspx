<%@ Page Title="" Language="C#" MasterPageFile="~/Template/Template.Master" AutoEventWireup="true" CodeBehind="EditarColabCoord.aspx.cs" Inherits="PortalNR.Admin.EditarColabCoord" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="toolkitScriptMaster" runat="server" EnableScriptGlobalization="true" EnablePageMethods="true" />
    <script type="text/javascript">
        function setFuncionarioPesquisa(source, eventargs) {
            document.getElementById('<%= hdFuncionarioPesquisa.ClientID %>').value = eventargs.get_value();
        }
    </script>
    <asp:UpdatePanel ID="updPesquisar" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <h2>Editar Colaborador</h2>
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
                                <label><strong>Matrícula</strong></label>
                                <asp:TextBox ID="txtMatricula" CssClass="form-control" ReadOnly="true" runat="server" />
                            </div>
                            <div class="col-md-5 mb-1">
                                <label><strong>Nome</strong></label>
                                <asp:TextBox ID="txtColaborador" CssClass="form-control" ReadOnly="true" runat="server" />
                            </div>
                            <div class="col-md-5 mb-1">
                                <label><strong>Coordenação</strong></label>
                                <asp:TextBox ID="txtCoordenacao" CssClass="form-control" runat="server" />                                
                                <asp:HiddenField runat="server" ID="hdFuncionarioPesquisa" />
                                <asp:AutoCompleteExtender runat="server" BehaviorID="biUnidade" ID="aceFuncionarioPesquisa" TargetControlID="txtCoordenacao" MinimumPrefixLength="3" CompletionSetCount="30"
                                    FirstRowSelected="true" ServiceMethod="getFuncionario" UseContextKey="true" ServicePath="AutoComplete.aspx" CompletionListCssClass="autocomplete_elemento"
                                    CompletionListItemCssClass="autocomplete_item" CompletionListHighlightedItemCssClass="autocomplete_realce" OnClientItemSelected="setFuncionarioPesquisa" DelimiterCharacters="" Enabled="True" />
                            </div>
                        </div>
                        <div class="row row-cols-12 mb-2">
                            <div class="col-md-5 mb-1">
                                <label><strong>Situação Colaborador <span class="smallfont" style="color: red;">Informação do efetivo</span></strong></label>
                                <asp:TextBox ID="txtSituacao" CssClass="form-control" ReadOnly="true" runat="server" />
                            </div>  
                            <div class="col-md-4 mb-1">
                                <label><strong>Situação Normativo</strong></label>
                                <asp:DropDownList ID="ddlSituacaoNR" CssClass="form-control" runat="server" />
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
