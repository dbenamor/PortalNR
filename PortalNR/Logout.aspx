<%@ Page Title="" Language="C#" MasterPageFile="~/Template/Template.Master" AutoEventWireup="true" CodeBehind="Logout.aspx.cs" Inherits="PortalNR.Logout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Portal de Treinamento Normativo</h2>
    <p>Portal de gestão dos treinamentos normativos</p>

    <div class="container-fluid">
        <div class="card">
            <div class="card-header">                
            <strong><asp:Label ID="lblCardHead" Text="Deseja sair do sistema?" runat="server" /></strong>
            </div>
            <div class="card-body">
                <asp:LinkButton ID="btnLeave" CssClass="btn btn-sm btn-danger" OnClick="btnLeave_Click" runat="server"><i class="fa fa-fw fa-sign-out "></i>&nbsp;Logout</asp:LinkButton>&nbsp;
            </div>
        </div>
    </div>
</asp:Content>
