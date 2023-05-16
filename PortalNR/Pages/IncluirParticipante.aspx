<%@ Page Title="" Language="C#" MasterPageFile="~/Template/Template.Master" AutoEventWireup="true" CodeBehind="IncluirParticipante.aspx.cs" Inherits="PortalNR.Pages.IncluirParticipante" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="toolkitScriptMaster" runat="server" EnableScriptGlobalization="true" EnablePageMethods="true" />
    <asp:UpdatePanel ID="updPesquisar" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <h2>Incluir participantes</h2>
            <p>Incluir participante para participação na turma escolhida.</p>

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
                        <asp:TextBox ID="txtDataHora" CssClass="form-control col-md-3 mb-1" Enabled="false" Visible="false" TextMode="DateTimeLocal" runat="server" />
                        <div class="row row-cols-12">
                            <div class="col-md-2 mb-1">
                                <asp:HiddenField ID="txtID" runat="server" />
                                <asp:HiddenField ID="txtNormativo" runat="server" />
                                <label><strong>Matrícula</strong></label>
                                <asp:TextBox ID="txtMatricula" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtMatricula_TextChanged" runat="server" />
                            </div>
                            <div class="col-md-5 mb-1">
                                <label><strong>Nome</strong></label>
                                <asp:TextBox ID="txtNome" CssClass="form-control" ReadOnly="true" runat="server" />
                            </div>
                            <div class="col-md-5 mb1">
                                <label><strong>Coordenação</strong></label>
                                <asp:TextBox ID="txtCoord" CssClass="form-control" ReadOnly="true" runat="server" />
                            </div>
                            <div class="col-md-3 mb1">
                                <!--DIV VAZIA-->
                            </div>
                        </div>
                        <div class="row row-cols-12" id="divVagas" visible="false" runat="server">
                            <div class="col-md-2 mb-1">
                                <label><strong>Vagas</strong></label>
                                <asp:TextBox ID="txtVagas" CssClass="form-control" ReadOnly="true" runat="server" />
                            </div>
                            <div class="col-md-2 mb1">
                                <label><strong>Vagas Disponíveis</strong></label>
                                <asp:TextBox ID="txtVagasDisponiveis" CssClass="form-control" ReadOnly="true" runat="server" />
                            </div>
                        </div>
                        <hr />
                        <div class="row row-cols-12">
                            <div class="col-md-12 mb1">
                                <label><strong>Informação sobre a turma</strong></label>
                                <asp:TextBox ID="txtObservação" CssClass="form-control" TextMode="MultiLine" Style="resize: none;" Rows="2" ReadOnly="true" runat="server" />
                            </div>
                        </div>
                        <hr />
                        <div class="row row-cols-12">
                            <div class="col-md-6">
                                <div class="d-flex" style="padding-top: 10px;">
                                    <strong><asp:Label ID="lblQuantitativo" CssClass="alert-primary" runat="server" /></strong>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="d-flex justify-content-end">
                                    <asp:LinkButton ID="btnIncluir" CssClass="btn btn-edit" OnClick="btnIncluir_Click" runat="server"><i class="fa fa-fw fa-plus"></i>&nbsp;Incluir</asp:LinkButton>&nbsp;
                                    <asp:LinkButton ID="btnLimpar" CssClass="btn btn-danger" OnClick="btnLimpar_Click" runat="server"><i class="fa fa-fw fa-trash-o"></i>&nbsp;Limpar</asp:LinkButton>&nbsp;
                                    <asp:LinkButton ID="btnCancelar" CssClass="btn btn-secondary" OnClick="btnCancelar_Click" runat="server"><i class="fa fa-fw fa-undo"></i>&nbsp;Voltar</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!--SEGUNDO CARD-->
                <div class="card mt-2">
                    <div class="card-body">
                        <div class="table-responsive">
                            <asp:GridView ID="gridParticipantes" CssClass="table table-striped table-hover" Font-Size="Small" GridLines="None" AutoGenerateColumns="false" runat="server">
                                <EmptyDataTemplate>
                                    <label style="color: steelblue; font-weight: bold;">Nenhum resultado encontrado.</label>
                                </EmptyDataTemplate>
                                <HeaderStyle CssClass="thead-dark text-center larguraGridView" Font-Size="Small" />
                                <Columns>
                                    <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <div class="d-flex justify-content-center">
                                            <asp:LinkButton ID="btnRemover" CssClass="btn btn-info btn-sm" ToolTip="Informação" OnClientClick="return confirm('Para remover o colaborador entre em contato com a Educação Coporativa.')" runat="server"><i class="fa fa-fw fa-info-circle"></i></asp:LinkButton>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Matrícula">
                                        <ItemTemplate><%# Eval("Matricula") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nome">
                                        <ItemTemplate><%# Eval("Nome") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Coordenação">
                                        <ItemTemplate><%# Eval("Coordenacao") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Presença">
                                        <ItemTemplate><%# Convert.ToBoolean(Eval("Presenca"))? "Sim":"Não"  %></ItemTemplate>
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
