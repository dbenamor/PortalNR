<%@ Page Title="" Language="C#" MasterPageFile="~/Template/Template.Master" AutoEventWireup="true" CodeBehind="ConsultarNR.aspx.cs" Inherits="PortalNR.Pages.ConsultarNR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Consultar vigência</h2>
    <p>Consulte a situação do colaborador pesquisando por matrícula ou por nome.</p>

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
                        <asp:TextBox ID="txtColab" CssClass="form-control" runat="server" />
                    </div>
                    <div class="col-md-4 mb-1">
                        <!--DIV Vazia -->
                    </div>
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
        <div class="card mt-2" id="divCard" visible="false" runat="server">
            <div class="card-body">
                <div class="table-responsive">
                    <asp:GridView ID="gridConsulta" CssClass="table table-striped table-hover" Font-Size="Small" GridLines="None" AutoGenerateColumns="false" AllowPaging="true" EnableModelValidation="True" OnPageIndexChanging="gridConsulta_PageIndexChanging" runat="server">
                        <EmptyDataTemplate><label style="color: steelblue; font-weight: bold;">Nenhum resultado encontrado.</label></EmptyDataTemplate>
                        <HeaderStyle CssClass="thead-dark text-center larguraGridView" Font-Size="Small" />
                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" FirstPageText="<<" LastPageText=">>" />
                        <PagerStyle HorizontalAlign="Right" Font-Size="Small" />
                        <Columns>
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
                            <asp:TemplateField HeaderText="Situação">
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
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate><%# Eval("StatusNR") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Elegível">
                                <ItemTemplate><%# Convert.ToBoolean(Eval("ELEGIVEL"))? "Sim":"Não"  %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Veículo">
                                <ItemTemplate><%# Eval("Veiculo") == ""? "-" : Eval("Veiculo") %></ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    <br /><br />
</asp:Content>
