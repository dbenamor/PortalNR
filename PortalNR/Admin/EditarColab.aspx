<%@ Page Title="" Language="C#" MasterPageFile="~/Template/Template.Master" AutoEventWireup="true" CodeBehind="EditarColab.aspx.cs" Inherits="PortalNR.Admin.EditarColab" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ajax" runat="server" />

    <h2>Atualizar Colaboradores</h2>
    <p>Atualizar os dados de treinamento dos colaboradores cadastrados.</p>

    <asp:UpdatePanel ID="painelAjax" runat="server">
        <ContentTemplate>
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
                            <%--<div class="col-md-2 mb-1">
                                <label><strong>Normativo</strong></label>
                                <asp:DropDownList ID="ddlNR" CssClass="form-control" runat="server" />
                            </div> --%>                           
                            <div class="col-md-2 mb-1">
                                <label><strong>Matrícula</strong></label>
                                <asp:TextBox ID="txtMatricula" CssClass="form-control" runat="server" />
                            </div>
                            <div class="col-md-4 mb-1">
                                <label><strong>Colaborador</strong></label>
                                <asp:TextBox ID="txtColaborador" CssClass="form-control" runat="server" />
                            </div>
                            <div class="col-md-4 mb-1">
                                <label><strong>Coordenação</strong></label>
                                <asp:TextBox ID="txtCoordenacao" CssClass="form-control" runat="server" />
                            </div>
                        </div>
                        <hr />
                        <div class="row row-cols-12">
                            <div class="col-md-12">
                                <div class="d-flex justify-content-end">
                                    <asp:LinkButton ID="btnPesquisar" OnClick="btnPesquisar_Click" CssClass="btn btn-success" runat="server"><i class="fa fa-fw fa-search"></i>&nbsp;Pesquisar</asp:LinkButton>&nbsp;                            
                                    <asp:LinkButton ID="btnLimpar" OnClick="btnLimpar_Click" CssClass="btn btn-info" runat="server"><i class="fa fa-fw fa-trash-o"></i>&nbsp;Limpar</asp:LinkButton>&nbsp;
                                    <asp:LinkButton ID="btnVoltar" OnClick="btnCancelar_Click" CssClass="btn btn-secondary" runat="server"><i class="fa fa-fw fa-undo"></i>&nbsp;Voltar</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!--SEGUNDO CARD-->
                <div class="card mt-2">
                    <div class="card-body">
                        <div class="table-responsive">
                            <asp:GridView ID="gridColaborador" CssClass="table table-striped table-hover" Font-Size="X-Small" GridLines="None" AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="gridColaborador_PageIndexChanging" EnableModelValidation="True" runat="server" OnRowCommand="gridColaborador_RowCommand">
                                <EmptyDataTemplate><label style="color: steelblue; font-weight: bold;">Nenhum resultado encontrado.</label></EmptyDataTemplate>
                                <HeaderStyle CssClass="thead-dark text-center larguraGridView" Font-Size="Small" />
                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" FirstPageText="<<" LastPageText=">>" />
                                <PagerStyle HorizontalAlign="Right" Font-Size="Small" />
                                <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <div class="d-flex justify-content-center">
                                                <asp:LinkButton ID="btnEditar" CssClass="btn btn-edit btn-sm" ToolTip="Editar" CommandName="Editar" CommandArgument='<%# Eval("Id")%>' CausesValidation="false" runat="server"><i class="fa fa-fw fa-pencil-square-o"></i></asp:LinkButton>&nbsp;                                               
                                                <asp:LinkButton ID="btnExcluir" CssClass="btn btn-danger btn-sm" ToolTip="Excluir" CommandName="Excluir" CommandArgument='<%# Eval("Id") %>' CausesValidation="false" OnClientClick="return confirm('Deseja realmente excluir?')" runat="server"><i class="fa fa-fw fa-trash-o"></i></asp:LinkButton>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Normativo">
                                        <ItemTemplate><%# Eval("Normativo") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Matrícula">
                                        <ItemTemplate><%# Convert.ToInt32(Eval("Matricula")) %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nome">
                                        <ItemTemplate><%# Eval("Nome") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Coordenação">
                                        <ItemTemplate><%# Eval("Coordenacao") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Situacao">
                                        <ItemTemplate><%# Eval("Situacao") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Certificação">
                                        <ItemTemplate>
                                            <%# Eval("DtCertificacao") == null? "-" : Convert.ToString(Eval("DtCertificacao","{0:dd/MM/yyyy}")) %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Revalidação">
                                        <ItemTemplate>
                                            <%# Eval("DtRevalidacao") == null? "-" : Convert.ToString(Eval("DtRevalidacao","{0:dd/MM/yyyy}")) %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Vigência">
                                        <ItemTemplate>
                                            <%# Eval("DtVigencia") == null? "-" : Convert.ToString(Eval("DtVigencia","{0:dd/MM/yyyy}")) %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Vencimento">
                                        <ItemTemplate><%# Convert.ToInt32(Eval("Vencimento")) <=0? 0 : Eval("Vencimento") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status NR">
                                        <ItemTemplate><%# Eval("StatusNR") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Equipamento">
                                        <ItemTemplate><%# Eval("Veiculo") == ""? "-" : Eval("Veiculo") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Elegível">
                                        <ItemTemplate><%# Convert.ToBoolean(Eval("ELEGIVEL"))? "Sim":"Não"  %></ItemTemplate>
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
