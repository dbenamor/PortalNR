<%@ Page Title="" Language="C#" MasterPageFile="~/Template/Template.Master" AutoEventWireup="true" CodeBehind="Efetivo.aspx.cs" Inherits="PortalNR.Admin.Efetivo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ajax" runat="server" />

    <h2>Consultar Efetivo</h2>
    <p>Consulta de informações do efetivo.</p>

    <asp:UpdatePanel ID="painelAjax" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
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
                                <label><strong>Matrícula</strong></label>
                                <asp:TextBox ID="txtMatric" CssClass="form-control" runat="server" />
                            </div>
                            <div class="col-md-4 mb-1">
                                <label><strong>Colaborador</strong></label>
                                <asp:TextBox ID="txtFuncionario" CssClass="form-control" runat="server" />
                            </div>
                            <div class="col-md-4 mb-1">
                                <label><strong>Coordenação</strong></label>
                                <asp:TextBox ID="txtCoordenacao" CssClass="form-control" runat="server" />
                            </div>
                        </div>
                        <div class="row row-cols-12">
                            <%--DIV VAZIA--%>
                        </div>
                        <hr />
                        <div class="row row-cols-12">
                            <div class="col-md-12">
                                <div class="d-flex justify-content-end">
                                    <asp:LinkButton ID="btnPesquisar" CssClass="btn btn-success" OnClick="btnPesquisar_Click" runat="server"><i class="fa fa-fw fa-search"></i>&nbsp;Pesquisar</asp:LinkButton>&nbsp;                           
                            <asp:LinkButton ID="btnLimpar" CssClass="btn btn-danger" OnClick="btnLimpar_Click" runat="server"><i class="fa fa-fw fa-trash-o"></i>&nbsp;Limpar</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!--SEGUNDO CARD-->
                <div class="card mt-2" id="divCard" visible="true" runat="server">
                    <div class="card-body">
                        <div class="table-responsive">
                            <asp:GridView ID="gridEfetivo" CssClass="table table-striped table-hover" Font-Size="X-Small" GridLines="None" AutoGenerateColumns="false" AllowPaging="true" EnableModelValidation="True" OnPageIndexChanging="gridEfetivo_PageIndexChanging" runat="server">
                                <EmptyDataTemplate>
                                    <label style="color: steelblue; font-weight: bold;">Nenhum resultado encontrado.</label></EmptyDataTemplate>
                                <HeaderStyle CssClass="thead-dark text-center larguraGridView" Font-Size="Small" />
                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" FirstPageText="<<" LastPageText=">>" />
                                <PagerStyle HorizontalAlign="Right" Font-Size="Small" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Empresa">
                                        <ItemTemplate><%# Eval("Empresa") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Matrícula">
                                        <ItemTemplate><%# Eval("Matricula") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Colaborador">
                                        <ItemTemplate><%# Eval("Funcionario") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cargo">
                                        <ItemTemplate><%# Eval("Cargo") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Coordenação">
                                        <ItemTemplate><%# Eval("Coordenacao") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Gerência">
                                        <ItemTemplate><%# Eval("Gerencia") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Centro de custo">
                                        <ItemTemplate><%# Eval("CentroCusto") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Descrição CC">
                                        <ItemTemplate><%# Eval("DescricaoCC") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Situação">
                                        <ItemTemplate><%# Eval("Situacao") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Desc GH">
                                        <ItemTemplate><%# Eval("DescGH") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Data Situação">
                                        <ItemTemplate><%# Eval("DtSituacao","{0:dd/MM/yyyy}") %></ItemTemplate>
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
