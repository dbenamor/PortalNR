using PortalNR.Code;
using PortalNR.DAL.Persistence;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace PortalNR.Pages
{
    public partial class NRVigente : PageBaseLocal
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IniciarPagina();
            }
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                ColaboradorDAL cDAL = new ColaboradorDAL();

                if (ddlNR.SelectedIndex != 0 && txtCoordenacao.Text == "")
                {
                    OcultarDIV();
                    divCard.Visible = true;
                    gridConsulta.DataSource = cDAL.FindNRVigente(ddlNR.SelectedItem.Value, txtCoordenacao.Text);

                }
                else if (ddlNR.SelectedIndex == 0 && txtCoordenacao.Text != "")
                {
                    OcultarDIV();
                    divCard.Visible = true;
                    gridConsulta.DataSource = cDAL.FindNRVigente(ddlNR.SelectedItem.Value, txtCoordenacao.Text);
                }
                else if (ddlNR.SelectedIndex != 0 && txtCoordenacao.Text != "")
                {
                    OcultarDIV();
                    divCard.Visible = true;
                    gridConsulta.DataSource = cDAL.FindNRVigente(ddlNR.SelectedItem.Value, txtCoordenacao.Text);
                }
                else
                {
                    lblAviso.Text = "Informe o normativo ou coordenação.";
                    divWarning.Visible = true;
                    divCard.Visible = false;
                    ddlNR.Focus();
                    ddlNR.SelectedIndex = 0;
                }

                gridConsulta.DataBind();
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
            ddlNR.Text = string.Empty;
            txtCoordenacao.Text = string.Empty;
            gridConsulta.DataSource = null;
            gridConsulta.DataBind();
        }

        protected void IniciarPagina()
        {
            CarregarCombo();
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

        private void OcultarDIV()
        {
            divDanger.Visible = false;
            divWarning.Visible = false;
        }

        protected void gridConsulta_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridConsulta.PageIndex = e.NewPageIndex;
            btnPesquisar_Click(null, null);
        }
    }
}