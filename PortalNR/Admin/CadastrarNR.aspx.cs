using PortalNR.Code;
using PortalNR.DAL.Entities;
using PortalNR.DAL.Persistence;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace PortalNR.Admin
{
    public partial class CadastrarNR : PageBaseLocal
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IniciarPagina();
            }
        }

        protected void btnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                OcultarDiv();
                Normativos n = new Normativos();
                n.Normativo = txtNR.Text;
                n.Vigencia = Convert.ToInt32(txtVigencia.Text);
                n.Descricao = txtDescricao.Text;
                n.Ativo = chkAtivo.Checked;

                NormativosDAL ndal = new NormativosDAL();
                ndal.Insert(n);

                divSuccess.Visible = true;
                lblSucesso.Text = "Normativo " + n.Normativo + " cadastrado com sucesso.";

                LimparCampos();
                CarregarGrid();
            }
            catch (Exception ex)
            {
                divDanger.Visible = true;
                lblErro.Text = ex.Message;
            }
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        protected void gridNormativo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridNormativo.PageIndex = e.NewPageIndex;
            CarregarGrid();
        }

        protected void gridNormativo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Editar":
                    Response.Redirect("DetalharNr.aspx?IdNormativo=" + e.CommandArgument.ToString());
                    break;
                case "Excluir":
                    ExcluirItem(Convert.ToInt32(e.CommandArgument));
                    break;
            }
        }

        private void IniciarPagina()
        {
            CarregarGrid();
            ConfigurarPerfilUsuario();
        }

        private void CarregarGrid()
        {
            try
            {
                OcultarDiv();
                NormativosDAL n = new NormativosDAL();
                gridNormativo.DataSource = n.FindAll();
                gridNormativo.DataBind();
            }
            catch (Exception ex)
            {
                divDanger.Visible = true;
                lblErro.Text = ex.Message;
            }
        }

        protected void LimparCampos()
        {
            OcultarDiv();
            txtNR.Text = string.Empty;
            txtVigencia.Text = string.Empty;
            txtDescricao.Text = string.Empty;
        }

        private void OcultarDiv()
        {
            divSuccess.Visible = false;
            divWarning.Visible = false;
            divDanger.Visible = false;
        }

        private void ExcluirItem(int Id)
        {
            NormativosDAL nDAL = new NormativosDAL();
            nDAL.Delete(Id);

            CarregarGrid();

            divSuccess.Visible = true;
            lblSucesso.Text = "Normativo excluido com sucesso.";
        }

        private void ConfigurarPerfilUsuario()
        {
            PerfilDAL perfil = new PerfilDAL();

            if (UsuarioLocalLogado == null) return;

            foreach (var item in perfil.ListaPerfil().Where(item => item.Id == UsuarioLocalLogado.PerfilRel.Id))
            {
                if (UsuarioLocalLogado != null && UsuarioLocalLogado.PerfilRel.Id != 1)
                    AlertaRedirecionar("Você não tem permissão de acessar esta página!", "../Login.aspx");
            }
        }
    }
}