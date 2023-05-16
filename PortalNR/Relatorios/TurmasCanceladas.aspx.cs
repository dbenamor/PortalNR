using Microsoft.Reporting.WebForms;
using PortalNR.Code;
using PortalNR.DAL.Entities;
using PortalNR.DAL.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PortalNR.Relatorios
{
    public partial class TurmasCanceladas : PageBaseLocal
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IniciarPagina();
            }
        }

        protected void btnRelatorio_Click(object sender, EventArgs e)
        {
            try
            {
                OcultarDivs();

                if (ddlNR.SelectedIndex == 0 && string.IsNullOrEmpty(txtDtInicial.Text) && string.IsNullOrEmpty(txtDtFinal.Text))
                {
                    divWarning.Visible = true;
                    lblAviso.Text = "Informe um normativo e um range de data para consulta.";
                }
                else if (ddlNR.SelectedIndex != 0 && string.IsNullOrEmpty(txtDtInicial.Text) && string.IsNullOrEmpty(txtDtFinal.Text))
                {
                    divWarning.Visible = true;
                    lblAviso.Text = "Necessário informar um range de data além do normativo.";
                }
                else if (ddlNR.SelectedIndex != 0 && string.IsNullOrEmpty(txtDtInicial.Text) && string.IsNullOrEmpty(txtDtFinal.Text))
                {
                    divWarning.Visible = true;
                    lblAviso.Text = "Necessário informar um range de data além do normativo.";
                }
                else if (ddlNR.SelectedIndex != 0 && !string.IsNullOrEmpty(txtDtInicial.Text) && !string.IsNullOrEmpty(txtDtFinal.Text))
                {
                    if (Convert.ToDateTime(txtDtInicial.Text) >= Convert.ToDateTime(txtDtFinal.Text))
                    {
                        divWarning.Visible = true;
                        lblAviso.Text = "A data final não pode ser menor ou igual a data inicial.";
                    }
                    else
                    {
                        GerarRelatorio(txtDtInicial.Text, txtDtFinal.Text, ddlNR.SelectedValue);
                    }
                }
            }
            catch (Exception ex)
            {
                divWarning.Visible = true;
                lblAviso.Text = ex.Message;
            }
        }

        private void GerarRelatorio(string DataIni, string DataFim, string Normativo)
        {
            TurmasDAL tDAL = new TurmasDAL();
            List<Turmas> ListaT = tDAL.FindTurmas(DataIni, DataFim, Normativo);

            rdlTurma.LocalReport.DataSources.Clear();
            rdlTurma.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("rdlTurmaCancelada.rdlc");
            rdlTurma.LocalReport.DataSources.Add(new ReportDataSource("DataSetTurmasCanceladas", ListaT));
            rdlTurma.DataBind();
        }

        private void IniciarPagina()
        {
            ConfigurarPerfilUsuario();
            CarregarCombo();
        }

        private void CarregarCombo()
        {
            try
            {
                TurmasDAL tDAL = new TurmasDAL();

                ddlNR.DataSource = tDAL.ListaNormativos();
                ddlNR.DataValueField = "Normativo";
                ddlNR.DataTextField = "Normativo";
                ddlNR.DataBind();
                ddlNR.Items.Insert(0, new ListItem("Selecione", ""));
                ddlNR.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                divDanger.Visible = true;
                lblErro.Text = ex.Message;
            }
        }

        private void LimparCampos()
        {
            OcultarDivs();
            ddlNR.SelectedIndex = 0;
            txtDtInicial.Text = string.Empty;
            txtDtFinal.Text = string.Empty;
        }

        private void OcultarDivs()
        {
            divDanger.Visible = false;
            divWarning.Visible = false;
        }

        private void ConfigurarPerfilUsuario()
        {
            PerfilDAL perfil = new PerfilDAL();

            if (UsuarioLocalLogado == null) return;

            foreach (var item in perfil.ListaPerfil().Where(item => item.Id == UsuarioLocalLogado.PerfilRel.Id))
            {
                if (UsuarioLocalLogado != null && UsuarioLocalLogado.PerfilRel.Id != item.Id)
                    AlertaRedirecionar("Você não tem permissão de acessar esta página!", "../Login.aspx");
            }
        }
    }
}