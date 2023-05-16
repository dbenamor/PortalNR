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
    public partial class VigentesXExpirados : PageBaseLocal
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
                string Coordenacao = txtCoordenacao.Text;

                if (string.IsNullOrEmpty(Coordenacao))
                {
                    divWarning.Visible = true;
                    lblAviso.Text = "Informe ao menos 1 coordenação para consulta.";
                }
                else
                {
                    OcultarDIV();

                    ColaboradorDAL cDAL = new ColaboradorDAL();
                    List<Colaborador> ListaC = cDAL.FindVigenteExpirado(Coordenacao);

                    rdlExpVig.LocalReport.DataSources.Clear();
                    rdlExpVig.LocalReport.ReportPath = HttpContext.Current.Server.MapPath("rdlVigentesExpirados.rdlc");
                    rdlExpVig.LocalReport.DataSources.Add(new ReportDataSource("DataSetVigencia", ListaC));
                    rdlExpVig.DataBind();
                }
            }
            catch (Exception ex)
            {
                divWarning.Visible = true;
                lblAviso.Text = ex.Message;
            }
        }

        private void OcultarDIV()
        {
            divWarning.Visible = false;
            divDanger.Visible = false;
        }
    }
}