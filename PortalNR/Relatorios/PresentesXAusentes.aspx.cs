using Microsoft.Reporting.WebForms;
using PortalNR.Code;
using PortalNR.DAL.Entities;
using PortalNR.DAL.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalNR.Relatorios
{
    public partial class PresentesXAusentes : PageBaseLocal
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IniciarPagina();
        }

        private void IniciarPagina()
        {
            ConfigurarPerfilUsuario();
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

        protected void btnRelatorio_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDtInicio.Text == null || txtDtInicio.Text == "")
                {
                    OcultarDivs();
                    divWarning.Visible = true;
                    lblAviso.Text = "Escolha um período de datas.";
                }
                else if (txtDtFim.Text == null || txtDtFim.Text == "")
                {
                    OcultarDivs();
                    divWarning.Visible = true;
                    lblAviso.Text = "Escolha um período de datas.";
                }
                else if (Convert.ToDateTime(txtDtFim.Text) < Convert.ToDateTime(txtDtInicio.Text))
                {
                    OcultarDivs();
                    divWarning.Visible = true;
                    lblAviso.Text = "A data final não pode ser menor que a data inicial.";
                }
                else
                {
                    OcultarDivs();

                    ColaboradorDAL cDAL = new ColaboradorDAL();
                    List<TurmasParticpantes> lista = cDAL.FindAusentePresente(txtDtInicio.Text, txtDtFim.Text, txtCoordenacao.Text);
                    rdlPresAusente.LocalReport.DataSources.Clear();
                    rdlPresAusente.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("rdlAusentesPresentes.rdlc");
                    rdlPresAusente.LocalReport.DataSources.Add(new ReportDataSource("DataSetPresenca", lista));
                    rdlPresAusente.DataBind();
                }
            }
            catch (Exception ex)
            {
                divWarning.Visible = false;
                divDanger.Visible = true;
                lblErro.Text = ex.Message;
            }
        }

        private void OcultarDivs()
        {
            divWarning.Visible = false;
            divDanger.Visible = false;
        }
    }
}