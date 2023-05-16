<%@ Page Title="" Language="C#" MasterPageFile="~/Template/Template.Master" AutoEventWireup="true" CodeBehind="TurmasCanceladas.aspx.cs" Inherits="PortalNR.Relatorios.TurmasCanceladas" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="toolkitScriptMaster" runat="server" EnableScriptGlobalization="true" EnablePageMethods="true" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <h2>Relatório de turmas canceladas e abertas</h2>
            <p>Gráfico de turmas canceladas e abertas.</p>

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
                                <label><strong>Normativo</strong></label>
                                <asp:DropDownList ID="ddlNR" class="form-control" runat="server" />
                            </div>
                            <div class="col-md-4 mb-1">
                                <label><strong>De</strong></label>
                                <asp:TextBox ID="txtDtInicial" TextMode="Date" CssClass="form-control" runat="server" />
                            </div>
                            <div class="col-md-4 mb-1">
                                <label><strong>Até</strong></label>
                                <asp:TextBox ID="txtDtFinal" TextMode="Date" CssClass="form-control" runat="server" />
                            </div>
                        </div>
                        <hr />
                        <div class="row row-cols-12">
                            <div class="col-md-12">
                                <div class="d-flex justify-content-end">
                                    <asp:LinkButton ID="btnRelatorio" CssClass="btn btn-primary" OnClick="btnRelatorio_Click" runat="server"><i class="fa fa-fw fa-bar-chart"></i>&nbsp;Executar relatório</asp:LinkButton>&nbsp;
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
                                <rsweb:ReportViewer ID="rdlTurma" Width="100%" Height="100%" SizeToReportContent="true" runat="server"></rsweb:ReportViewer>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
