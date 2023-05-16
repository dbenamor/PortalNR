<%@ Page Title="" Language="C#" MasterPageFile="~/Template/Template.Master" AutoEventWireup="true" CodeBehind="CadastrarCoordenacao.aspx.cs" Inherits="PortalNR.Admin.CadastrarCoordenacao" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="toolkitScriptMaster" runat="server" EnableScriptGlobalization="true" EnablePageMethods="true" />
    <asp:UpdatePanel ID="updPesquisar" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <h2>Cadastrar Coordenação</h2>
            <p>Cadastro de Coordenação</p>

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
                                <label><strong>Coordenação</strong></label>
                                <asp:TextBox ID="txtDescricao" CssClass="form-control" runat="server" />
                            </div>
                            <div class="col-md-4 mb-1">
                                <!--DIV VAZIA -->
                            </div>
                            <div class="col-md-4 mt-4">
                                <!--DIV VAZIA -->
                            </div>
                        </div>
                        <hr />
                        <div class="row row-cols-12">
                            <div class="col-md-12">
                                <div class="d-flex justify-content-end">
                                    <asp:LinkButton ID="btnPesquisar" CssClass="btn btn-success" OnClick="btnPesquisar_Click" runat="server"><i class="fa fa-fw fa-search"></i>&nbsp;Pesquisar</asp:LinkButton>&nbsp;
                                    <asp:LinkButton ID="btnCadastrar" CssClass="btn btn-primary" OnClick="btnCadastrar_Click" runat="server"><i class="fa fa-fw fa-floppy-o"></i>&nbsp;Cadastrar</asp:LinkButton>&nbsp;
                                    <asp:LinkButton ID="btnLimpar" CssClass="btn btn-info" OnClick="btnLimpar_Click" runat="server"><i class="fa fa-fw fa-trash-o"></i>&nbsp;Limpar</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!--SEGUNDO CARD-->
                <div class="card mt-2">
                    <div class="card-body">
                        <div class="table-responsive">
                            <asp:GridView ID="gridCoordenacao" CssClass="table table-striped table-hover" Font-Size="Small" GridLines="None" AutoGenerateColumns="false" runat="server">
                                <EmptyDataTemplate><label style="color:steelblue; font-weight:bold;">Nenhum resultado encontrado.</label></EmptyDataTemplate>
                                <HeaderStyle CssClass="thead-dark text-center larguraGridView" Font-Size="Small" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Coordenação">
                                        <ItemTemplate><%# Eval("Descricao") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Data de cadastro">
                                        <ItemTemplate><%# Convert.ToString(Eval("DtCadastro","{0:dd/MM/yyyy}")) %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <div class="d-flex justify-content-center">
                                                <%--<asp:LinkButton ID="btnEditar" CssClass="btn btn-edit btn-sm" PostBackUrl='<%# string.Format("DetalharCoordenacao.aspx?id={0}", Eval("id")) %>' runat="server"><i class="fa fa-fw fa-pencil-square-o"></i></asp:LinkButton>&nbsp;--%>
                                                <asp:LinkButton ID="btnExcluir" CssClass="btn btn-danger btn-sm" CommandArgument='<%# Eval("Id") %>' OnClick="btnExcluir_Click" OnClientClick="return confirm('Deseja realmente excluir?')" runat="server"><i class="fa fa-fw fa-trash-o"></i></asp:LinkButton>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <br />
                <br />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
