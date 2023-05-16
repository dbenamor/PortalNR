<%@ Page Title="" Language="C#" MasterPageFile="~/Template/Template.Master" AutoEventWireup="true" CodeBehind="CadastrarColab.aspx.cs" Inherits="PortalNR.Admin.CadastrarColab" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="toolkitScriptMaster" runat="server" EnableScriptGlobalization="true" EnablePageMethods="true" />
    <script type="text/javascript">
        function setFuncionarioPesquisa(source, eventargs) {
            document.getElementById('<%= hdFuncionarioPesquisa.ClientID %>').value = eventargs.get_value();
        }
    </script>
    <asp:UpdatePanel ID="updPesquisar" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <h2>Cadastrar Colaboradores</h2>
            <p>Cadastro de Colaboradores nos normativos</p>

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
                                <label><strong>Normativo</strong></label>
                                <asp:DropDownList ID="ddlNR" CssClass="form-control" AutoPostBack="true" OnTextChanged="ddlNR_TextChanged" runat="server" />
                                <asp:RequiredFieldValidator ID="requiredDropNR" ControlToValidate="ddlNR" InitialValue="Selecione" Font-Size="Smaller" ErrorMessage="Selecione o normativo" ForeColor="Red" Display="Dynamic" runat="server" />
                            </div>
                            <div class="col-md-2 mb-1">
                                <label><strong>Matricula</strong></label>
                                <asp:TextBox ID="txtMatric" CssClass="form-control" ReadOnly="true" AutoPostBack="true" OnTextChanged="txtMatric_TextChanged" runat="server" />
                                <asp:RegularExpressionValidator ID="regexMatric" runat="server" ControlToValidate="txtMatric" ErrorMessage="Permitido somente números" Font-Size="Smaller" ForeColor="Red" Display="Dynamic" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="requiredMatricula" ControlToValidate="txtMatric" Font-Size="Smaller" ErrorMessage="Campo obrigatório" ForeColor="Red" Display="Dynamic" runat="server" />
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
                                <asp:TextBox ID="txtCoordenacao" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
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
                                <asp:DropDownList ID="ddlSituacaoNR" Enabled="false" CssClass="form-control" runat="server" />
                                <asp:RequiredFieldValidator ID="requiredDropSituacao" ControlToValidate="ddlSituacaoNR" InitialValue="Selecione" Font-Size="Smaller" ErrorMessage="Informa a situação" ForeColor="Red" Display="Dynamic" runat="server" />
                            </div>
                        </div>
                        <div class="row row-cols-12">
                            <div class="col-md-3 mb-1">
                                <label><strong>Equipamento <span class="smallfont">(NR-11)</span></strong></label>
                                <asp:DropDownList ID="ddlVeiculo" Enabled="false" CssClass="form-control" runat="server" />
                            </div>
                            <div class="col-md-3 mb-1">
                                <label><strong>Data Certificação</strong></label>
                                <asp:TextBox ID="txtCertificacao" CssClass="form-control" TextMode="Date" ReadOnly="true" AutoPostBack="true" onkeypress="return false;" OnTextChanged="txtCertificacao_TextChanged" runat="server" />
                            </div>
                            <div class="col-md-3 mb-1">
                                <label><strong>Data Revalidação</strong></label>
                                <asp:TextBox ID="txtRevalidacao" CssClass="form-control" TextMode="Date" ReadOnly="true" AutoPostBack="true" onkeypress="return false;" OnTextChanged="txtRevalidacao_TextChanged" runat="server" />
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
                                <div class="d-flex justify-content-end" style="float: left;">
                                    <div class="input-group">
                                        <asp:TextBox ID="pesquisarmatricula" CssClass="form-control" placeholder="Pesquisar matricula..." runat="server" />
                                        <div class="input-group-append">
                                            <asp:LinkButton ID="btnPesquisar" CssClass="btn btn-success" CausesValidation="false" OnClick="btnPesquisar_Click" runat="server"><i class="fa fa-fw fa-search"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="d-flex justify-content-end" style="float: right;">
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
                            <asp:GridView ID="gridColaborador" CssClass="table table-striped table-hover" Font-Size="Small" GridLines="None" AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="gridColaborador_PageIndexChanging" EnableModelValidation="True" runat="server" OnRowCommand="gridColaborador_RowCommand">
                                <EmptyDataTemplate>
                                    <label style="color: steelblue; font-weight: bold;">Nenhum resultado encontrado.</label>
                                </EmptyDataTemplate>
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
                                    <asp:TemplateField HeaderText="Situacao Normativo">
                                        <ItemTemplate><%# Eval("Situacao") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Situacao Efetivo">
                                        <ItemTemplate><%# Eval("SituacaoEfetivo") %></ItemTemplate>
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
