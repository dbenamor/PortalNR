<%@ Page Title="" Language="C#" MasterPageFile="~/Template/Template.Master" AutoEventWireup="true" CodeBehind="CadastrarUsu.aspx.cs" Inherits="PortalNR.Admin.CadastrarUsu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="toolkitScriptMaster" runat="server" EnableScriptGlobalization="true" EnablePageMethods="true" />
    <asp:UpdatePanel ID="updPesquisar" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <h2>Cadastrar Usuários</h2>
            <p>Cadastro de usuários para adminsitração e colaboração do sistema</p>

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
                            <div class="col-md-3 mb-1">
                                <label><strong>Login</strong></label>
                                <asp:TextBox ID="txtLogin" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtLogin_TextChanged" runat="server" />
                                <asp:RequiredFieldValidator ID="requiredLogin" ControlToValidate="txtLogin" Font-Size="Smaller" ErrorMessage="Campo obrigatório" ForeColor="Red" Display="Dynamic" runat="server" />
                            </div>
                            <div class="col-md-4 mb-1">
                                <label><strong>Nome</strong></label>
                                <asp:TextBox ID="txtNome" CssClass="form-control" runat="server" />
                                <asp:RequiredFieldValidator ID="requiredNome" ControlToValidate="txtNome" Font-Size="Smaller" ErrorMessage="Campo obrigatório" ForeColor="Red" Display="Dynamic" runat="server" />
                            </div>                            
                            <div class="col-md-3 mb-1">
                                <label><strong>Perfil</strong></label>
                                <asp:DropDownList ID="ddlPerfil" CssClass="form-control" runat="server" />
                                <asp:RequiredFieldValidator ID="requiredDropPerfil" ControlToValidate="ddlPerfil" InitialValue="Selecione" Font-Size="Smaller" ErrorMessage="Selecione o perfil" ForeColor="Red" Display="Dynamic" runat="server" />
                            </div>
                            <div class="col-md-2 mt-4">
                                <asp:CheckBox ID="chkAtivo" CssClass="custom-checkbox-input md-5" Checked="true" runat="server" />
                                <label><strong>&nbsp;Ativo</strong></label>
                            </div>
                        </div>
                        <div class="row row-cols-12 mb-2">
                            <div class="col-md-6 mb-1">
                                <label><strong>E-mail</strong></label>
                                <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredEmail" ControlToValidate="txtEmail" Font-Size="Smaller" ErrorMessage="Campo obrigatório" ForeColor="Red" Display="Dynamic" runat="server" />
                                <asp:RegularExpressionValidator ID="validateEmail" ControlToValidate="txtEmail" Font-Size="Smaller" ErrorMessage="E-mail inválido" ForeColor="Red" Display="Dynamic" ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" runat="server" />
                            </div>                            
                        </div>
                        <hr />
                        <div class="row row-cols-12">
                            <div class="col-md-12">
                                <div class="d-flex justify-content-end" style="float:left;">
                                    <div class="input-group">
                                        <asp:TextBox ID="txtpesquisarLogin" CssClass="form-control" placeholder="Pesquisar login..." runat="server" />
                                        <div class="input-group-append">
                                            <asp:LinkButton ID="btnPesquisar" CssClass="btn btn-success" CausesValidation="false" OnClick="btnPesquisar_Click" runat="server"><i class="fa fa-fw fa-search"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="d-flex justify-content-end" style="float:right;">
                                    <asp:LinkButton ID="btnCadastrar" CssClass="btn btn-primary" OnClick="btnCadastrar_Click" runat="server"><i class="fa fa-fw fa-floppy-o"></i>&nbsp;Cadastrar</asp:LinkButton>&nbsp;
                                    <asp:LinkButton ID="btnLimpar" CssClass="btn btn-danger" OnClick="btnLimpar_Click" CausesValidation="false" runat="server"><i class="fa fa-fw fa-trash-o"></i>&nbsp;Limpar</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!--SEGUNDO CARD-->
                <div class="card mt-2">
                    <div class="card-body">
                        <div class="table-responsive">
                            <asp:GridView ID="gridUsuarios" CssClass="table table-striped table-hover" Font-Size="Small" GridLines="None" AutoGenerateColumns="false" OnRowCommand="gridUsuarios_RowCommand" AllowPaging="true" EnableModelValidation="True" OnPageIndexChanging="gridUsuarios_PageIndexChanging" runat="server">
                                <EmptyDataTemplate><label style="color:steelblue; font-weight:bold;">Nenhum resultado encontrado.</label></EmptyDataTemplate>
                                <HeaderStyle CssClass="thead-dark text-center larguraGridView" Font-Size="Small" />
                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" FirstPageText="<<" LastPageText=">>" />
                                <PagerStyle HorizontalAlign="Right" Font-Size="Small" />
                                <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <div class="d-flex justify-content-center">
                                                <asp:LinkButton ID="btnEditar" CssClass="btn btn-edit btn-sm" ToolTip="Editar" CommandName="Editar" CommandArgument='<%# Eval("Id")%>' CausesValidation="false" runat="server"><i class="fa fa-fw fa-pencil-square-o"></i></asp:LinkButton>&nbsp;
                                                <asp:LinkButton ID="btnExcluir" CssClass="btn btn-danger btn-sm" ToolTip="Excluir" CommandName="Excluir" CommandArgument='<%# Eval("Id")%>' CausesValidation="false" OnClientClick="return confirm('Deseja realmente excluir?')" runat="server"><i class="fa fa-fw fa-trash-o"></i></asp:LinkButton>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nome">
                                        <ItemTemplate><%# Eval("NmUsuario") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Login">
                                        <ItemTemplate><%# Eval("Login") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Perfil">
                                        <ItemTemplate><%# Eval("perfilRel.Descricao") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Email">
                                        <ItemTemplate><%# Eval("Email") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Data de cadastro">
                                        <ItemTemplate><%# Eval("DtCadastro") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ativo">
                                        <ItemTemplate><%# Convert.ToBoolean(Eval("Ativo"))? "Ativo":"Inativo"  %></ItemTemplate>
                                    </asp:TemplateField>                                    
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
