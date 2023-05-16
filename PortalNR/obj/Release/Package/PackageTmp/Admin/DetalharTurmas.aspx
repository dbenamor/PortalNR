<%@ Page Title="" Language="C#" MasterPageFile="~/Template/Template.Master" AutoEventWireup="true" CodeBehind="DetalharTurmas.aspx.cs" Inherits="PortalNR.Admin.DetalharTurmas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="toolkitScriptMaster" runat="server" EnableScriptGlobalization="true" EnablePageMethods="true" />
    <asp:UpdatePanel ID="updPesquisar" runat="server" UpdateMode="Conditional">
        <ContentTemplate>

            <h2>Editar turmas</h2>
            <p>Editar e cadastar a participação dos colaboradores nas turmas</p>

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
                            <div class="col-md-3 mb-1">
                                <asp:HiddenField ID="txtId" runat="server" />
                                <label><strong>Data e hora</strong></label>
                                <asp:TextBox ID="txtDataHora" CssClass="form-control" TextMode="DateTimeLocal" ReadOnly="true" runat="server" />
                            </div>
                            <div class="col-md-3 mb-1">
                                <label><strong>Data e hora final</strong></label>
                                <asp:TextBox ID="txtDataHoraFim" CssClass="form-control" TextMode="DateTimeLocal" ReadOnly="true" runat="server" />
                            </div>
                            <div class="col-md-2 mb-1">
                                <label><strong>Carga horária</strong></label>
                                <asp:TextBox ID="txtCarga" CssClass="form-control" ReadOnly="true" runat="server" />
                                <asp:RegularExpressionValidator ID="ValidaCarga" runat="server" ControlToValidate="txtCarga" ErrorMessage="Permitido somente números" Font-Size="Smaller" ForeColor="Red" Display="Dynamic" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                <%--<asp:RequiredFieldValidator ID="requiredCarga" ControlToValidate="txtCarga" Font-Size="Smaller" ErrorMessage="Campo obrigatório" ForeColor="Red" Display="Dynamic" runat="server" />--%>
                            </div>
                            <div class="col-md-4 mb-1">
                                <label><strong>Local</strong></label>
                                <asp:TextBox ID="txtLocal" CssClass="form-control" runat="server" />
                                <asp:RequiredFieldValidator ID="requiredLocal" ControlToValidate="txtLocal" Font-Size="Smaller" ErrorMessage="Campo obrigatório" ForeColor="Red" Display="Dynamic" runat="server" />
                            </div>
                        </div>
                        <div class="row row-cols-12 mb-2">
                            <div class="col-md-3 mb1">
                                <label><strong>Vagas</strong></label>
                                <asp:TextBox ID="txtVagas" CssClass="form-control" runat="server" />
                                <asp:RegularExpressionValidator ID="ValidaVagas" runat="server" ControlToValidate="txtVagas" ErrorMessage="Permitido somente números" Font-Size="Smaller" ForeColor="Red" Display="Dynamic" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="requiredVagas" ControlToValidate="txtVagas" Font-Size="Smaller" ErrorMessage="Campo obrigatório" ForeColor="Red" Display="Dynamic" runat="server" />
                            </div>
                            <div class="col-md-3 mb1">
                                <label><strong>Tema</strong></label>
                                <asp:DropDownList ID="ddlTema" runat="server" Enabled="false" CssClass="form-control" />
                            </div>
                            <div class="col-md-2 mb-1">
                                <label><strong>Normativo</strong></label>
                                <asp:DropDownList ID="ddlNR" runat="server" Enabled="false" CssClass="form-control" AutoPostBack="true" OnTextChanged="ddlNR_TextChanged" />
                            </div>
                            <div class="col-md-4 mb-1">
                                <label><strong>Instrutor</strong></label>
                                <asp:TextBox ID="txtPalestrante" CssClass="form-control" runat="server" />
                                <asp:RequiredFieldValidator ID="requiredInstrutor" ControlToValidate="txtPalestrante" Font-Size="Smaller" ErrorMessage="Campo obrigatório" ForeColor="Red" Display="Dynamic" runat="server" />
                            </div>
                        </div>
                        <div class="row row-cols-12 mb-2">
                            <div class="col-md-3">
                                <label><strong>Equipamento<span class="smallfont">&nbsp;(NR-11)</span></strong></label>
                                <asp:DropDownList ID="ddlVeiculo" runat="server" CssClass="form-control" Enabled="false" />
                            </div>
                            <div class="col-md-9">
                                <label><strong>Observação</strong></label>
                                <asp:TextBox ID="txtObservacao" CssClass="form-control" TextMode="MultiLine" Style="resize: none;" Rows="2" onpaste="return false" oncopy="return false" runat="server" />
                                <div class="d-flex justify-content-end">
                                    <p style="font-size: x-small; color: GrayText">
                                        <asp:Label ID="lblCount" runat="server" ReadOnly="true">0</asp:Label>&nbsp;de 250 caracteres.
                                    </p>
                                </div>
                            </div>
                        </div>
                        <hr />
                        <div class="row row-cols-12">
                            <div class="col-md-12">
                                <div class="d-flex" style="float: left">
                                    <asp:LinkButton ID="btnCancelarTurma" CssClass="btn btn-danger" OnClick="btnCancelarTurma_Click" OnClientClick="return confirm('Deseja realmente cancelar a turma?')" runat="server"><i class="fa fa-fw fa-power-off "></i>&nbsp;Cancelar turma</asp:LinkButton>
                                </div>
                                <div class="d-flex" style="float: right">
                                    <asp:LinkButton ID="btnAtualizar" CssClass="btn btn-primary" OnClick="btnAtualizar_Click" runat="server"><i class="fa fa-fw fa-floppy-o"></i>&nbsp;Atualizar</asp:LinkButton>&nbsp;
                                    <asp:LinkButton ID="btnCancelar" CssClass="btn btn-secondary" OnClick="btnCancelar_Click" CausesValidation="false" runat="server"><i class="fa fa-fw fa-undo"></i>&nbsp;Voltar</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!--SEGUNDO CARD-->
                <div class="card mt-2">
                    <div class="card-body">
                        <div class="table-responsive">
                            <asp:GridView ID="gridParticipantes" CssClass="table table-striped table-hover" Font-Size="Small" GridLines="None" AutoGenerateColumns="false" runat="server" OnRowDataBound="gridParticipantes_RowDataBound" EnableModelValidation="True">
                                <EmptyDataTemplate>
                                    <label style="color: steelblue; font-weight: bold;">Nenhum resultado encontrado.</label>
                                </EmptyDataTemplate>
                                <HeaderStyle CssClass="thead-dark text-center larguraGridView" Font-Size="Small" />
                                <%--<PagerSettings Mode="NumericFirstLast" PageButtonCount="10" FirstPageText="<<" LastPageText=">>" />
                                <PagerStyle HorizontalAlign="Right" Font-Size="Small" />--%>
                                <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <div class="d-flex justify-content-center">
                                                <asp:LinkButton ID="btnRemover" CssClass="btn btn-danger btn-sm" ToolTip="Remover participante" OnClick="btnRemover_Click" CommandArgument='<%# Eval("IdParticipante") %>' OnClientClick="return confirm('Deseja realmente excluir?')" runat="server"><i class="fa fa-fw fa-minus"></i></asp:LinkButton>&nbsp;
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
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkPresenca" CssClass="form-check" runat="server" />
                                            <asp:HiddenField ID="txtIdParticipante" runat="server" Value='<%# Convert.ToInt32(Eval("IdParticipante")) %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <hr />
                            <div class="d-flex justify-content-end" runat="server" visible="true" id="divExportacao">
                                <div class="d-flex justify-content-end">
                                    <asp:DropDownList ID="ddlSelecao" CssClass="form-control-sm" OnSelectedIndexChanged="ddlSelecao_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                        <asp:ListItem Value="0" Text=" Escolha uma opção " />
                                        <asp:ListItem Value="1" Text=" Marcar todos " />
                                        <asp:ListItem Value="2" Text=" Desmarcar todos " />
                                    </asp:DropDownList>&nbsp;
                                        <asp:LinkButton ID="btnSalvar" OnClick="btnSalvar_Click" CssClass="btn btn-success btn-sm" runat="server"><i class="fa fa-fw fa-floppy-o"></i>&nbsp;Salvar</asp:LinkButton>&nbsp;
                                        <asp:LinkButton ID="btnExportar" OnClick="btnExportar_Click" CssClass="btn btn-warning btn-sm" runat="server"><i class="fa fa-fw fa-file-excel-o "></i>&nbsp;Exportar excel</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
    <button onclick="topFunction()" id="btnTopo" class="btnTop" title="Voltar ao topo"><i class="fa fa-2x fa-arrow-circle-o-up"></i></button>

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

        //Get the button
        var mybutton = document.getElementById("btnTopo");
        // When the user scrolls down 20px from the top of the document, show the button
        window.onscroll = function () { scrollFunction() };
        function scrollFunction() {
            if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
                mybutton.style.display = "block";
            } else {
                mybutton.style.display = "none";
            }
        }
        // When the user clicks on the button, scroll to the top of the document
        function topFunction() {
            document.body.scrollTop = 0;
            document.documentElement.scrollTop = 0;
        }
    </script>
</asp:Content>
