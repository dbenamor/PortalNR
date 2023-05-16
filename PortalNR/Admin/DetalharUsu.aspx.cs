using PortalNR.Code;
using PortalNR.DAL.Entities;
using PortalNR.DAL.Persistence;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace PortalNR.Admin
{
    public partial class DetalharUsu : PageBaseLocal
    {
        #region "Propriedades"

        #endregion

        #region "Eventos"

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IniciarPagina();
            }
        }

        protected void btnAtualizar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario u = new Usuario();
                u.PerfilRel = new Perfil();

                u.Id = Int32.Parse(txtId.Value);
                u.NmUsuario = txtNome.Text;
                u.Login = txtLogin.Text;
                u.Email = txtEmail.Text;
                u.PerfilRel.Id = Convert.ToInt32(ddlPerfil.SelectedValue);
                u.Ativo = chkAtivo.Checked;

                UsuarioDAL uDAL = new UsuarioDAL();
                uDAL.Update(u);

                OcultarDIV();
                LimparCampos();

                divSuccess.Visible = true;
                lblSucesso.Text = "Usuario " + u.Login + " atualizada com sucesso.";
            }
            catch (Exception ex)
            {
                divDanger.Visible = true;
                lblErro.Text = ex.Message;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("CadastrarUsu.aspx");
        }

        #endregion

        #region "Metodos"

        private void IniciarPagina()
        {
            CarregarCombo();
            CarregarCampos();
            ConfigurarPerfilUsuario();
        }

        private void CarregarCombo()
        {
            try
            {
                PerfilDAL pDAL = new PerfilDAL();
                ddlPerfil.DataSource = pDAL.ListaPerfil();
                ddlPerfil.DataValueField = "Id";
                ddlPerfil.DataTextField = "Descricao";
                ddlPerfil.DataBind();
                ddlPerfil.Items.Insert(0, new ListItem("Selecione", ""));
                ddlPerfil.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                divDanger.Visible = true;
                lblErro.Text = "Não foi possível carregar a lista de perfis. " + ex.Message;
            }
        }

        private void CarregarCampos()
        {
            try
            {
                int Id = Int32.Parse(Request.QueryString["Id"]);
                UsuarioDAL uDAL = new UsuarioDAL();
                Usuario u = uDAL.FindById(Id);

                txtId.Value = u.Id.ToString();
                txtNome.Text = u.NmUsuario;
                txtLogin.Text = u.Login;
                txtEmail.Text = u.Email;
                ddlPerfil.SelectedValue = u.PerfilRel.Id.ToString();
                chkAtivo.Checked = u.Ativo;
            }
            catch (Exception ex)
            {
                divDanger.Visible = true;
                lblErro.Text = ex.Message;
            }
        }

        private void LimparCampos()
        {
            txtNome.Text = string.Empty;
            txtLogin.Text = string.Empty;
            txtEmail.Text = string.Empty;
            ddlPerfil.SelectedIndex = 0;
        }

        private void OcultarDIV()
        {
            divSuccess.Visible = false;
            divDanger.Visible = false;
            divWarning.Visible = false;
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

        #endregion
    }
}