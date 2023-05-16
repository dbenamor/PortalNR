using PortalNR.Code;
using PortalNR.DAL.Persistence;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace PortalNR.Pages
{
    public partial class ConsultarNR : PageBaseLocal
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IniciarPagina();
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                OcultarDiv();
                ColaboradorDAL cDAL = new ColaboradorDAL();
                int Matricula = 0;
                string Colaborador = "";

                if (txtMatric.Text == "" && txtColab.Text == "")
                {
                    lblAviso.Text = "Informe a matricula ou colaborador.";
                    divWarning.Visible = true;
                    txtMatric.Focus();
                    txtMatric.Text = string.Empty;
                }
                else
                {
                    OcultarDiv();
                    divCard.Visible = true;
                    if (!string.IsNullOrEmpty(txtMatric.Text))
                        Matricula = Int32.Parse(txtMatric.Text);
                    if (!string.IsNullOrEmpty(txtColab.Text))
                        Colaborador = txtColab.Text;
                    gridConsulta.DataSource = cDAL.FindELEGIVEL(Matricula, Colaborador);
                    gridConsulta.DataBind();
                }
            }
            catch (Exception ex)
            {
                divDanger.Visible = true;
                lblErro.Text = ex.Message;
            }
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            divCard.Visible = false;
            txtMatric.Text = string.Empty;
            txtColab.Text = string.Empty;
            gridConsulta.DataSource = null;
            gridConsulta.DataBind();
        }

        protected void gridConsulta_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridConsulta.PageIndex = e.NewPageIndex;
            btnPesquisar_Click(null, null);
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

        protected void OcultarDiv()
        {
            divDanger.Visible = false;
            divWarning.Visible = false;
        }
    }
}