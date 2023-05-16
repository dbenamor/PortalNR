<%@ Page Title="" Language="C#" MasterPageFile="~/Template/Template.Master" AutoEventWireup="true" CodeBehind="CadastrarNR.aspx.cs" Inherits="PortalNR.Admin.CadastrarNR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="toolkitScriptMaster" runat="server" EnableScriptGlobalization="true" EnablePageMethods="true" />
    <asp:UpdatePanel ID="updPesquisar" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <h2>Cadastrar Normativos</h2>
            <p>Cadastro de Regulamentos Normativos</p>

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
                                <label><strong>Normativo</strong></label>
                                <asp:TextBox ID="txtNR" CssClass="form-control" runat="server" />
                                <asp:RequiredFieldValidator ID="requiredNR" ControlToValidate="txtNR" Font-Size="Smaller" ErrorMessage="Campo obrigatório" ForeColor="Red" Display="Dynamic" runat="server" />
                            </div>
                            <div class="col-md-4 mb-1">
                                <label><strong>Vigência</strong></label>
                                <asp:TextBox ID="txtVigencia" CssClass="form-control" runat="server" />
                                <asp:RegularExpressionValidator ID="regexVigencia" runat="server" ControlToValidate="txtVigencia" ErrorMessage="Permitido somente números" Font-Size="Smaller" ForeColor="Red" Display="Dynamic" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="requiredVigencia" ControlToValidate="txtVigencia" Font-Size="Smaller" ErrorMessage="Campo obrigatório" ForeColor="Red" Display="Dynamic" runat="server" />
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
                                <asp:RequiredFieldValidator ID="requiredDescricao" ControlToValidate="txtDescricao" Font-Size="Smaller" ErrorMessage="Campo obrigatório" ForeColor="Red" Display="Dynamic" runat="server" />
                            </div>
                        </div>
                        <hr />
                        <div class="row row-cols-12">
                            <div class="col-md-12">
                                <div class="d-flex justify-content-end">
                                    <asp:LinkButton ID="btnCadastrar" CssClass="btn btn-primary" OnClick="btnCadastrar_Click" runat="server"><i class="fa fa-fw fa-floppy-o"></i>&nbsp;Cadastrar</asp:LinkButton>&nbsp;
                                    <asp:LinkButton ID="btnLimpar" CssClass="btn btn-danger" OnClick="btnLimpar_Click" runat="server"><i class="fa fa-fw fa-trash-o"></i>&nbsp;Limpar</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!--SEGUNDO CARD-->
                <div class="card mt-2">
                    <div class="card-body">
                        <div class="table-responsive">
                            <asp:GridView ID="gridNormativo" CssClass="table table-striped table-hover" Font-Size="Small" GridLines="None" AutoGenerateColumns="false" OnRowCommand="gridNormativo_RowCommand" AllowPaging="true" EnableModelValidation="true" OnPageIndexChanging="gridNormativo_PageIndexChanging" runat="server">
                                <EmptyDataTemplate><label style="color:steelblue; font-weight:bold;">Nenhum resultado encontrado.</label></EmptyDataTemplate>
                                <HeaderStyle CssClass="thead-dark text-center larguraGridView" Font-Size="Small" />
                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" FirstPageText="<<" LastPageText=">>" />
                                <PagerStyle HorizontalAlign="Right" Font-Size="Small" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Normativo">
                                        <ItemTemplate><%# Eval("Normativo") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Vigência">
                                        <ItemTemplate><%# Convert.ToInt32(Eval("Vigencia")) > 1? Eval("Vigencia") + " anos":  Eval("Vigencia") + " ano"  %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Descrição">
                                        <ItemTemplate><%# Eval("Descricao") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ativo">
                                        <ItemTemplate><%# Convert.ToBoolean(Eval("Ativo"))? "Ativo":"Inativo"  %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <div class="d-flex justify-content-center">
                                                <asp:LinkButton ID="btnEditar" CssClass="btn btn-edit btn-sm" ToolTip="Editar" CommandName="Editar" CommandArgument='<%# Eval("IdNormativo")%>' CausesValidation="false" runat="server"><i class="fa fa-fw fa-pencil-square-o"></i></asp:LinkButton>&nbsp;
                                                <asp:LinkButton ID="btnExcluir" CssClass="btn btn-danger btn-sm" ToolTip="Excluir" Visible="false" CommandName="Excluir" CommandArgument='<%# Eval("IdNormativo")%>' CausesValidation="false" OnClientClick="return confirm('Deseja realmente excluir?')" runat="server"><i class="fa fa-fw fa-trash-o"></i></asp:LinkButton>
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
