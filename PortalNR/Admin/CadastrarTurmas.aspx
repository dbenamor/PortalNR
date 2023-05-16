<%@ Page Title="" Language="C#" MasterPageFile="~/Template/Template.Master" AutoEventWireup="true" CodeBehind="CadastrarTurmas.aspx.cs" Inherits="PortalNR.Admin.CadastrarTurmas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="toolkitScriptMaster" runat="server" EnableScriptGlobalization="true" EnablePageMethods="true" />
    <asp:UpdatePanel ID="updPesquisar" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <h2>Cadastrar turmas</h2>
            <p>Cadastro de turmas para certificação/revalidação dos colaboradores.</p>

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
                        <div class="row row-cols-12 mb-2">
                            <div class="col-md-3">
                                <label><strong>Data e hora início</strong></label>
                                <asp:TextBox ID="txtDataHora" CssClass="form-control" TextMode="DateTimeLocal" runat="server" />
                                <asp:RequiredFieldValidator ID="requiredDataeHora" ControlToValidate="txtDataHora" Font-Size="Smaller" ErrorMessage="Campo obrigatório" ForeColor="Red" Display="Dynamic" runat="server" />
                            </div>
                            <div class="col-md-3">
                                <label><strong>Data e hora final</strong></label>
                                <asp:TextBox ID="txtDataHoraFim" CssClass="form-control" TextMode="DateTimeLocal" runat="server" />
                                <asp:RequiredFieldValidator ID="requiredDataHoraFim" ControlToValidate="txtDataHoraFim" Font-Size="Smaller" ErrorMessage="Campo obrigatório" ForeColor="Red" Display="Dynamic" runat="server" />
                            </div>
                            <div class="col-md-2">
                                <label><strong>Carga horária</strong></label>
                                <asp:TextBox ID="txtCarga" CssClass="form-control" runat="server" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtCarga" ErrorMessage="Permitido somente números" Font-Size="Smaller" ForeColor="Red" Display="Dynamic" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="requiredCarga" ControlToValidate="txtCarga" Font-Size="Smaller" ErrorMessage="Campo obrigatório" ForeColor="Red" Display="Dynamic" runat="server" />
                            </div>
                            <div class="col-md-4">
                                <label><strong>Local</strong></label>
                                <asp:TextBox ID="txtLocal" CssClass="form-control" runat="server" />
                                <asp:RequiredFieldValidator ID="requiredLocal" ControlToValidate="txtLocal" Font-Size="Smaller" ErrorMessage="Campo obrigatório" ForeColor="Red" Display="Dynamic" runat="server" />
                            </div>
                        </div>
                        <div class="row row-cols-12 mb-2">
                            <div class="col-md-3">
                                <label><strong>Vagas</strong></label>
                                <asp:TextBox ID="txtVagas" CssClass="form-control" runat="server" />
                                <asp:RegularExpressionValidator ID="regexVagas" runat="server" ControlToValidate="txtVagas" ErrorMessage="Permitido somente números" Font-Size="Smaller" ForeColor="Red" Display="Dynamic" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="requiredVagas" ControlToValidate="txtVagas" Font-Size="Smaller" ErrorMessage="Campo obrigatório" ForeColor="Red" Display="Dynamic" runat="server" />
                            </div>
                            <div class="col-md-3">
                                <label><strong>Tema</strong></label>
                                <asp:DropDownList ID="ddlTema" runat="server" CssClass="form-control" />
                                <asp:RequiredFieldValidator ID="requiredDropTema" ControlToValidate="ddlTema" InitialValue="Selecione" Font-Size="Smaller" ErrorMessage="Selecione o tema" ForeColor="Red" Display="Dynamic" runat="server" />
                            </div>
                            <div class="col-md-2">
                                <label><strong>Normativo</strong></label>
                                <asp:DropDownList ID="ddlNR" runat="server" CssClass="form-control" OnTextChanged="ddlNR_TextChanged" AutoPostBack="true" />
                                <asp:RequiredFieldValidator ID="requiredDropNR" ControlToValidate="ddlNR" InitialValue="Selecione" Font-Size="Smaller" ErrorMessage="Selecione o normativo" ForeColor="Red" Display="Dynamic" runat="server" />
                            </div>
                            <div class="col-md-4">
                                <label><strong>Instrutor</strong></label>
                                <asp:TextBox ID="txtPalestrante" CssClass="form-control" runat="server" />
                                <asp:RequiredFieldValidator ID="requirePalestrante" ControlToValidate="txtPalestrante" Font-Size="Smaller" ErrorMessage="Campo obrigatório" ForeColor="Red" Display="Dynamic" runat="server" />
                            </div>
                        </div>
                        <div class="row row-cols-12">
                            <div class="col-md-3">
                                <label><strong>Equipamento<span class="smallfont">&nbsp;(NR-11)</span></strong></label>
                                <asp:DropDownList ID="ddlVeiculo" runat="server" CssClass="form-control" Enabled="false" />
                            </div>
                            <div class="col-md-9">
                                <label><strong>Observação</strong></label>
                                <asp:RequiredFieldValidator ID="requiredObservacao" ControlToValidate="txtObservacao" Font-Size="Smaller" ErrorMessage=" Campo obrigatório" ForeColor="Red" Font-Bold="true" Display="Dynamic" runat="server" />
                                <asp:TextBox ID="txtObservacao" CssClass="form-control" TextMode="MultiLine" Style="resize: none;" onpaste="return false" oncopy="return false" Rows="2" runat="server" />
                                <div class="d-flex justify-content-end">
                                    <p style="font-size: small; color: GrayText">
                                        <asp:Label ID="lblCount" runat="server" ReadOnly="true">0</asp:Label>&nbsp;de 250 caracteres.
                                    </p>
                                </div>
                            </div>
                        </div>
                        <hr />
                        <div class="row row-cols-12">
                            <div class="col-md-7">
                                <form>
                                    <div class="form-row align-items-center">
                                        <div class="col-auto">
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">De</span>
                                                </div>
                                                <asp:TextBox ID="txtDe" CssClass="form-control" TextMode="Date" runat="server" />
                                            </div>
                                        </div>
                                        <div class="col-auto">
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">Até</span>
                                                </div>
                                                <asp:TextBox ID="txtPara" CssClass="form-control" TextMode="Date" runat="server" />
                                            </div>
                                        </div>
                                        <div class="col-auto">
                                            <asp:LinkButton ID="btnPesquisar" CssClass="btn btn-success" CausesValidation="false" OnClick="btnPesquisar_Click" runat="server"><i class="fa fa-fw fa-search"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                </form>
                            </div>
                            <div class="col-md-5">
                                <div class="d-flex justify-content-end" style="float: right;">
                                    <asp:LinkButton ID="btnCadastrar" CssClass="btn btn-primary" OnClick="btnCadastrar_Click" runat="server"><i class="fa fa-fw fa-floppy-o"></i>&nbsp;Cadastrar</asp:LinkButton>&nbsp;
                                    <asp:LinkButton ID="btnLimpar" CssClass="btn btn-danger" CausesValidation="false" OnClick="btnLimpar_Click" runat="server"><i class="fa fa-fw fa-trash-o"></i>&nbsp;Limpar</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!--SEGUNDO CARD-->
                <div class="card mt-2">
                    <div class="card-body">
                        <div class="table-responsive">
                            <asp:GridView ID="gridTurmas" CssClass="table table-striped table-hover" Font-Size="Small" GridLines="None" AutoGenerateColumns="false" runat="server" OnRowCommand="gridTurmas_RowCommand" OnRowDataBound="gridTurmas_RowDataBound" AllowPaging="true" EnableModelValidation="True" OnPageIndexChanging="gridTurmas_PageIndexChanging">
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
                                                <asp:LinkButton ID="btnEditar" CssClass="btn btn-edit btn-sm" ToolTip="Editar" CommandName="Editar" CommandArgument='<%# Eval("IdTurma")%>' CausesValidation="false" runat="server"><i class="fa fa-fw fa-pencil-square-o"></i></asp:LinkButton>&nbsp;
                                                <asp:LinkButton ID="btnExcluir" CssClass="btn btn-danger btn-sm" ToolTip="Excluir" CommandName="Excluir" CommandArgument='<%# Eval("IdTurma") %>' OnClientClick="return confirm('Deseja realmente excluir?')" CausesValidation="false" runat="server"><i class="fa fa-fw fa-trash-o"></i></asp:LinkButton>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Normativvo">
                                        <ItemTemplate><%# Eval("normativoRel.Normativo") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Equipamento">
                                        <ItemTemplate><%# Eval("Veiculo") == ""? "-" : Eval("Veiculo") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Data e hora início">
                                        <ItemTemplate><%# Eval("DtTurma") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Data e hora final">
                                        <ItemTemplate><%# Eval("dtTurmaFim") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Carga horário">
                                        <ItemTemplate><%# Eval("Carga") + " h"  %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Local">
                                        <ItemTemplate><%# Eval("Local") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tema">
                                        <ItemTemplate><%# Eval("TemaRelac.Tema") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Situação">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Ativo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Vagas">
                                        <ItemTemplate><%# Eval("Vagas") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Vagas disponíveis">
                                        <ItemTemplate><%# Eval("VagasDisponiveis") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Instrutor">
                                        <ItemTemplate><%# Eval("Palestrante") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Observação">
                                        <ItemTemplate>
                                            <div style="width: 100px; overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">
                                                <asp:Label ID="lblObservacao" Text='<%# Eval("Observacao") %>' ToolTip='<%# Eval("Observacao") %>' runat="server" />
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function getText(txtbox, e) {
            var maxlength = 250;
            var keyCode;
            if (window.event)
                keyCode = window.event.keyCode;
            else
                keyCode = e.which;

            switch (keyCode) {
                case 8:
                    return true;
                default:
                    if (txtbox.value.length == maxlength)
                        return false;
                    else
                        setText(txtbox);
            }
            return true;
        }
        function setText(txtbox) {
            document.getElementById('<%=lblCount.ClientID %>').innerHTML = txtbox.value.length.toString();
        }
    </script>
</asp:Content>
