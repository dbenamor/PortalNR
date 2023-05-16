<%@ Page Title="" Language="C#" MasterPageFile="~/Template/Template.Master" AutoEventWireup="true" CodeBehind="ProximasTurmas.aspx.cs" Inherits="PortalNR.Pages.ProximasTurmas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h2>Próximas turmas</h2>
    <p>Turmas para certificação/revalidação dos colaboradores.</p>

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
        <div class="card" id="Card1" visible="true" runat="server">
            <div class="card-body">
                <div class="row row-cols-12">
                    <div class="col-md-4 mb-1">
                        <label><strong>Normativo</strong></label>
                        <asp:DropDownList ID="ddlNR" class="form-control" runat="server" />
                    </div>
                    <div class="col-md-4 mb-1">
                        <label><strong>Turmas no mês de:</strong></label>
                        <asp:DropDownList ID="ddlMeses" class="form-control" runat="server" />
                    </div>
                    <div class="col-md-4 mb1">
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
        <div class="card mt-2" id="Card2" visible="true" runat="server">
            <div class="card-body">
                <div class="table-responsive">
                    <asp:GridView ID="gridTurmas" CssClass="table table-striped table-hover" Font-Size="Small" GridLines="None" AutoGenerateColumns="false" runat="server" OnRowDataBound="gridTurmas_RowDataBound">
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
                                        <asp:LinkButton ID="btnAdicionar" CssClass="btn btn-primary btn-sm disabled" ToolTip="Adicionar participante" PostBackUrl='<%# string.Format("IncluirParticipante.aspx?id={0}", Eval("IdTurma")) %>' runat="server"><i class="fa fa-fw fa-plus"></i></asp:LinkButton>&nbsp;                            
                                        <asp:LinkButton ID="btnVisualizar" CssClass="btn btn-success btn-sm" ToolTip="Detalhes da turma" OnClick="btnVisualizar_Click" CommandArgument='<%# Eval("IdTurma") %>' runat="server"><i class="fa fa-fw fa-search"></i></asp:LinkButton>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Data e hora início">
                                <ItemTemplate><%# Eval("DtTurma") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Data e hora fim">
                                <ItemTemplate><%# Eval("DtTurmaFim") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Carga horária">
                                <ItemTemplate><%# Eval("Carga") + " h"  %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status da turma">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Ativo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Normativo">
                                <ItemTemplate><%# Eval("normativoRel.Normativo") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Equipamento">
                                <ItemTemplate><%# Eval("Veiculo").Equals("")? "" : Eval("Veiculo") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Local">
                                <ItemTemplate><%# Eval("Local") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tema">
                                <ItemTemplate>
                                    <label style="word-wrap: normal;"><%# Eval("TemaRelac.Tema") %></label>
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
                            <%--                           
                            <asp:TemplateField HeaderText="Observação">
                                <ItemTemplate><%# Eval("Observacao") %></ItemTemplate>
                            </asp:TemplateField>--%>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
        <!--TERCEIRO CARD-->
        <div class="card mt-2" id="Card3" visible="false" runat="server">
            <div class="card-header">
                <asp:Label ID="lblCabecalho" Font-Bold="true" ForeColor="Black" runat="server">Lista de participantes</asp:Label>
            </div>
            <div class="card-body">
                <div class="row row-cols-12">
                    <div class="col-md-12 mb1">
                        <label><strong>Informação sobre a turma</strong></label>
                        <asp:TextBox ID="txtObservação" CssClass="form-control" TextMode="MultiLine" Style="resize: none;" Rows="2" ReadOnly="true" runat="server" />
                    </div>
                </div>
                <hr />
                <div class="table-responsive">
                    <asp:GridView ID="gridParticipantes" CssClass="table table-striped table-hover" Font-Size="Small" GridLines="None" AutoGenerateColumns="false" runat="server">
                        <EmptyDataTemplate>
                            <label style="color: steelblue; font-weight: bold;">Nenhum resultado encontrado.</label>
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="thead-light text-center table-striped larguraGridView" Font-Size="Small" />
                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="10" FirstPageText="<<" LastPageText=">>" />
                        <PagerStyle HorizontalAlign="Right" Font-Size="Small" />
                        <Columns>
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
            <div class="card-footer">
                <div class="d-flex justify-content-end">
                    <asp:LinkButton ID="btnVoltar" CssClass="btn btn-secondary btn-sm" OnClick="btnvoltar_Click" runat="server"><i class="fa fa-fw fa-reply "></i>&nbsp;Voltar</asp:LinkButton>
                </div>
            </div>
        </div>
        <br />
        <br />
    </div>
    <button onclick="topFunction()" id="btnTopo" class="btnTop" title="Voltar ao topo"><i class="fa fa-2x fa-arrow-circle-o-up"></i></button>
    <script>
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
