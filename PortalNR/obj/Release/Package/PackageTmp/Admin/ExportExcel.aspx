<%@ Page Title="" Language="C#" MasterPageFile="~/Template/Template.Master" AutoEventWireup="true" CodeBehind="ExportExcel.aspx.cs" Inherits="PortalNR.Admin.ExportExcel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:ScriptManager ID="toolkitScriptMaster" runat="server" EnableScriptGlobalization="true" EnablePageMethods="true" />   
            <asp:Button ID="btnExportToExcel" Text="Export" runat="server" />
</asp:Content>
