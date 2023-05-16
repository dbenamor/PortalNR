<%@ Page Title="" Language="C#" MasterPageFile="~/Template/Template.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PortalNR.Pages.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h2>Portal de Treinamento Normativo</h2>
    <p>Portal de gestão dos treinamentos normativos</p>

    <div class="card">
        <div class="card-header">
            <asp:Label ID="lblCardHead" Text="Links de acesso rápido" runat="server" />
        </div>
        <div class="card-body">
            <div class="container-fluid my-3 bg-transparent">
                <div class="row">
                    <div class="col-sm-4 text-center">
                        <asp:LinkButton ID="btnLink1" CssClass="btn btn-redondo btn-calendario" OnClick="btnLink1_Click" runat="server"></asp:LinkButton>
                        <asp:Label ID="lblLink1" Text="Próximas turmas" runat="server" />
                    </div>
                    <div class="col-sm-4 text-center">
                        <asp:LinkButton ID="btnLink2" CssClass="btn btn-redondo btn-avencer" OnClick="btnLink2_Click" runat="server"></asp:LinkButton>
                        <asp:Label ID="lblLink2" Text="Normativos à vencer" runat="server" />
                    </div>
                    <div class="col-sm-4 text-center">
                        <asp:LinkButton ID="btnLink3" CssClass="btn btn-redondo btn-expirados" OnClick="btnLink3_Click" runat="server"></asp:LinkButton>                        
                        <asp:Label ID="lblLink3" Text="Normativos expirados" runat="server" />
                    </div>
                    </div>
                </div>
            </div>
        </div>
    <br />
    <div class="card">
        <div class="card-header">
            <asp:Label ID="lblCardHead2" Text="Para informações sobre NR/REG, clique nos botões abaixo." runat="server" />
        </div>
        <div class="card-body">
            <div class="container-fluid my-3 bg-transparent">
                <div class="row">
                    <div class="col-sm-3 text-center">
                        <asp:LinkButton ID="btnNR10" CssClass="btn btn-redondo btn-nr10" data-toggle="modal" data-target="#ModalNR10" runat="server"></asp:LinkButton>
                        <br />
                        <asp:Label ID="lblNR10" Text="NR-10" runat="server" />
                    </div>
                    <div class="col-sm-3 text-center">
                        <asp:LinkButton ID="btnNR10Sep" CssClass="btn btn-redondo btn-nr10SEP" data-toggle="modal" data-target="#ModalNR10SEP" runat="server"></asp:LinkButton>
                        <br />
                        <asp:Label ID="lblNR10SEP" Text="NR-10 SEP" runat="server" />
                    </div>
                    <div class="col-sm-3 text-center">
                        <asp:LinkButton ID="btnNR11" CssClass="btn btn-redondo btn-nr11" data-toggle="modal" data-target="#ModalNR11" runat="server"></asp:LinkButton>
                        <br />
                        <asp:Label ID="lblNR11" Text="NR-11" runat="server" />
                    </div>
                    <div class="col-sm-3 text-center">
                        <asp:LinkButton ID="btnNR12" CssClass="btn btn-redondo btn-nr12" data-toggle="modal" data-target="#ModalNR12" runat="server"></asp:LinkButton>
                        <br />
                        <asp:Label ID="lblNR12" Text="NR-12" runat="server" />
                    </div>
                </div>
                <div class="row my-4">
                    <div class="col-sm-4 text-center">
                        <asp:LinkButton ID="btnNR33" CssClass="btn btn-redondo btn-nr33" data-toggle="modal" data-target="#ModalNR33" runat="server"></asp:LinkButton>
                        <br />
                        <asp:Label ID="lblNR33" Text="NR-33" runat="server" />
                    </div>
                    <div class="col-sm-4 text-center">
                        <asp:LinkButton ID="btnNR35" CssClass="btn btn-redondo btn-nr35" data-toggle="modal" data-target="#ModalNR35" runat="server"></asp:LinkButton>
                        <br />
                        <asp:Label ID="lblNR35" Text="NR-35" runat="server" />
                    </div>
                    <div class="col-sm-4 text-center">
                        <asp:LinkButton ID="btnREG55" CssClass="btn btn-redondo btn-reg55" data-toggle="modal" data-target="#ModalREG55" runat="server" ></asp:LinkButton>
                        <br />
                        <asp:Label ID="lblREG55" Text="REG-55" runat="server" />
                    </div>
                </div>
            </div>
        </div>
        <!-- JANELA MODAL NR-10 -->
        <div class="modal fade" id="ModalNR10">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <!-- Modal Header -->
                    <div class="modal-header">
                        <h4 class="modal-title">Saiba mais</h4>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                    <!-- Modal body -->
                    <div class="modal-body">
                        <div class="row">
                            <div class="col">
                                <asp:HiddenField ID="txtId" runat="server" />
                                <strong>Normativo nº: </strong>
                                <asp:Label ID="lblNormativoNR10" runat="server"></asp:Label>
                            </div>
                            <div class="col">
                                <strong>Vigência: </strong>
                                <asp:Label ID="lblVigenciaNR10" runat="server"></asp:Label>
                            </div>
                        </div>
                        <hr />
                        <div class="row col-auto">                            
                            <p class="text-justify">
                                <asp:Label ID="lblDescricaoNR10" runat="server"></asp:Label>
                            </p>
                        </div>
                    </div>
                    <!-- Modal footer -->
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger btn-sm" data-dismiss="modal">Fechar</button>
                    </div>
                </div>
            </div>
        </div>
        <!-- JANELA MODAL NR-10SEP -->
        <div class="modal fade" id="ModalNR10SEP">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <!-- Modal Header -->
                    <div class="modal-header">
                        <h4 class="modal-title">Saiba mais</h4>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                    <!-- Modal body -->
                    <div class="modal-body">
                        <div class="row">
                            <div class="col">
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                <strong>Normativo nº: </strong>
                                <asp:Label ID="lblNormativoNR10SEP" runat="server"></asp:Label>
                            </div>
                            <div class="col">
                                <strong>Vigência: </strong>
                                <asp:Label ID="lblVigenciaNR10SEP" runat="server"></asp:Label>
                            </div>
                        </div>
                        <hr />
                        <div class="row col-auto">
                            <p class="text-justify">
                                <asp:Label ID="lblDescricaoNR10SEP" runat="server"></asp:Label>
                            </p>
                        </div>
                    </div>
                    <!-- Modal footer -->
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger btn-sm" data-dismiss="modal">Fechar</button>
                    </div>
                </div>
            </div>
        </div>
        <!-- JANELA MODAL NR-11 -->
        <div class="modal fade" id="ModalNR11">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <!-- Modal Header -->
                    <div class="modal-header">
                        <h4 class="modal-title">Saiba mais</h4>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                    <!-- Modal body -->
                    <div class="modal-body">
                        <div class="row">
                            <div class="col">
                                <asp:HiddenField ID="HiddenField2" runat="server" />
                                <strong>Normativo nº: </strong>
                                <asp:Label ID="lblNormativoNR11" runat="server"></asp:Label>
                            </div>
                            <div class="col">
                                <strong>Vigência: </strong>
                                <asp:Label ID="lblVigenciaNR11" runat="server"></asp:Label>
                            </div>
                        </div>
                        <hr />
                        <div class="row col-auto">
                            <p class="text-justify">
                                <asp:Label ID="lblDescricaoNR11" runat="server"></asp:Label>
                            </p>
                        </div>
                    </div>
                    <!-- Modal footer -->
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger btn-sm" data-dismiss="modal">Fechar</button>
                    </div>
                </div>
            </div>
        </div>
        <!-- JANELA MODAL NR-12 -->
        <div class="modal fade" id="ModalNR12">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <!-- Modal Header -->
                    <div class="modal-header">
                        <h4 class="modal-title">Saiba mais</h4>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                    <!-- Modal body -->
                    <div class="modal-body">
                        <div class="row">
                            <div class="col">
                                <asp:HiddenField ID="HiddenField3" runat="server" />
                                <strong>Normativo nº: </strong>
                                <asp:Label ID="lblNormativoNR12" runat="server"></asp:Label>
                            </div>
                            <div class="col">
                                <strong>Vigência: </strong>
                                <asp:Label ID="lblVigenciaNR12" runat="server"></asp:Label>
                            </div>
                        </div>
                        <hr />
                        <div class="row col-auto">
                            <p class="text-justify">
                                <asp:Label ID="lblDescricaoNR12" runat="server"></asp:Label>
                            </p>
                        </div>
                    </div>
                    <!-- Modal footer -->
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger btn-sm" data-dismiss="modal">Fechar</button>
                    </div>
                </div>
            </div>
        </div>
        <!-- JANELA MODAL NR-33 -->
        <div class="modal fade" id="ModalNR33">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <!-- Modal Header -->
                    <div class="modal-header">
                        <h4 class="modal-title">Saiba mais</h4>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                    <!-- Modal body -->
                    <div class="modal-body">
                        <div class="row">
                            <div class="col">
                                <asp:HiddenField ID="HiddenField4" runat="server" />
                                <strong>Normativo nº: </strong>
                                <asp:Label ID="lblNormativoNR33" runat="server"></asp:Label>
                            </div>
                            <div class="col">
                                <strong>Vigência: </strong>
                                <asp:Label ID="lblVigenciaNR33" runat="server"></asp:Label>
                            </div>
                        </div>
                        <hr />
                        <div class="row col-auto">
                            <p class="text-justify">
                                <asp:Label ID="lblDescricaoNR33" runat="server"></asp:Label>
                            </p>
                        </div>
                    </div>
                    <!-- Modal footer -->
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger btn-sm" data-dismiss="modal">Fechar</button>
                    </div>
                </div>
            </div>
        </div>
        <!-- JANELA MODAL NR-35 -->
        <div class="modal fade" id="ModalNR35">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <!-- Modal Header -->
                    <div class="modal-header">
                        <h4 class="modal-title">Saiba mais</h4>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                    <!-- Modal body -->
                    <div class="modal-body">
                        <div class="row">
                            <div class="col">
                                <asp:HiddenField ID="HiddenField5" runat="server" />
                                <strong>Normativo nº: </strong>
                                <asp:Label ID="lblNormativoNR35" runat="server"></asp:Label>
                            </div>
                            <div class="col">
                                <strong>Vigência: </strong>
                                <asp:Label ID="lblVigenciaNR35" runat="server"></asp:Label>
                            </div>
                        </div>
                        <hr />
                        <div class="row col-auto">
                            <p class="text-justify">
                                <asp:Label ID="lblDescricaoNR35" runat="server"></asp:Label>
                            </p>
                        </div>
                    </div>
                    <!-- Modal footer -->
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger btn-sm" data-dismiss="modal">Fechar</button>
                    </div>
                </div>
            </div>
        </div>
        <!-- JANELA MODAL REG-55 -->
        <div class="modal fade" id="ModalREG55">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <!-- Modal Header -->
                    <div class="modal-header">
                        <h4 class="modal-title">Saiba mais</h4>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                    <!-- Modal body -->
                    <div class="modal-body">
                        <div class="row">
                            <div class="col">
                                <asp:HiddenField ID="HiddenField6" runat="server" />
                                <strong>Normativo nº: </strong>
                                <asp:Label ID="lblNormativoREG55" runat="server"></asp:Label>
                            </div>
                            <div class="col">
                                <strong>Vigência: </strong>
                                <asp:Label ID="lblVigenciaREG55" runat="server"></asp:Label>
                            </div>
                        </div>
                        <hr />
                        <div class="row col-auto">
                            <p class="text-justify">
                                <asp:Label ID="lblDescricaoREG55" runat="server"></asp:Label>
                            </p>
                        </div>
                    </div>
                    <!-- Modal footer -->
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger btn-sm" data-dismiss="modal">Fechar</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <br /> 
</asp:Content>
