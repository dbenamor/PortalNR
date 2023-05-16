<%@ Page Title="" Language="C#" MasterPageFile="~/Template/Template.Master" AutoEventWireup="true" CodeBehind="PresentesXAusentes.aspx.cs" Inherits="PortalNR.Relatorios.PresentesXAusentes" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="toolkitScriptMaster" runat="server" EnableScriptGlobalization="true" EnablePageMethods="true" />
    <script type="text/javascript">
        function setFuncionarioPesquisa(source, eventargs) {
            document.getElementById('<%= hdFuncionarioPesquisa.ClientID %>').value = eventargs.get_value();
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <h2>Relatório de participação nos treinamentos</h2>
            <p>Relatório de participação x ausência nas turmas para treinamento.</p>

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
                                <label><strong>Data e hora início</strong></label>
                                <asp:TextBox ID="txtDtInicio" CssClass="form-control" TextMode="Date" runat="server" />
                            </div>
                            <div class="col-md-4 mb-1">
                                <label><strong>Data e hora final</strong></label>
                                <asp:TextBox ID="txtDtFim" CssClass="form-control" TextMode="Date" runat="server" />
                            </div>
                            <div class="col-md-4 mb-1">
                                <label><strong>Coordenação</strong></label>
                                <asp:TextBox ID="txtCoordenacao" CssClass="form-control" runat="server" AutoPostBack="True"></asp:TextBox>
                                <asp:HiddenField runat="server" ID="hdFuncionarioPesquisa" />
                                <asp:AutoCompleteExtender runat="server" BehaviorID="biUnidade" ID="aceFuncionarioPesquisa" TargetControlID="txtCoordenacao" MinimumPrefixLength="3" CompletionSetCount="30"
                                    FirstRowSelected="true" ServiceMethod="getFuncionario" UseContextKey="true" ServicePath="AutoComplete.aspx" CompletionListCssClass="autocomplete_elemento"
                                    CompletionListItemCssClass="autocomplete_item" CompletionListHighlightedItemCssClass="autocomplete_realce" OnClientItemSelected="setFuncionarioPesquisa" DelimiterCharacters="" Enabled="True" />
                            </div>
                        </div>
                        <hr />
                        <div class="row row-cols-12">
                            <div class="col-md-12">
                                <div class="d-flex justify-content-end">
                                    <asp:LinkButton ID="btnRelatorio" OnClick="btnRelatorio_Click" CssClass="btn btn-primary" runat="server"><i class="fa fa-fw fa-bar-chart"></i>&nbsp;Executar relatório</asp:LinkButton>&nbsp;                                     
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!--SEGUNDO CARD-->
                <div class="card mt-2" id="divCard" runat="server">
                    <div class="card-body">
                        <div class="d-flex justify-content-center">
                            <p>
                                <rsweb:ReportViewer ID="rdlPresAusente" Width="100%" Height="100%" SizeToReportContent="true" runat="server"></rsweb:ReportViewer>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
