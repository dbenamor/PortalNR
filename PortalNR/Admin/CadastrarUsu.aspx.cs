using PortalNR.Code;
using PortalNR.DAL.Entities;
using PortalNR.DAL.Persistence;
using PortalNR.Security;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace PortalNR.Admin
{
    public partial class CadastrarUsu : PageBaseLocal
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
                OcultarDIV();
                UsuarioDAL uDAL = new UsuarioDAL();
                Usuario u = new Usuario();
                u.PerfilRel = new Perfil();

                u.NmUsuario = txtNome.Text;
                u.Login = txtLogin.Text;
                u.Email = txtEmail.Text;
                u.Senha = Criptografia.EncriptarMD5("Password01");
                u.PerfilRel.Id = Int32.Parse(ddlPerfil.SelectedItem.Value);
                u.Ativo = chkAtivo.Checked;

                uDAL.Insert(u);

                LimparCampos();
                CarregarGrid();

                divSuccess.Visible = true;
                lblSucesso.Text = "Usuário " + u.Login.ToUpper() + " cadastrado com sucesso.";
            }
            catch (Exception ex)
            {
                divDanger.Visible = true;
                lblErro.Text = ex.Message;
            }
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            OcultarDIV();
            LimparCampos();
            CarregarGrid();
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                OcultarDIV();

                UsuarioDAL uDAL = new UsuarioDAL();
                gridUsuarios.DataSource = uDAL.PesquisarLogin(txtpesquisarLogin.Text);
                gridUsuarios.DataBind();
            }
            catch (Exception ex)
            {
                divDanger.Visible = true;
                lblErro.Text = ex.Message;
            }
        }

        protected void txtLogin_TextChanged(object sender, EventArgs e)
        {
            try
            {
                OcultarDIV();
                UsuarioDAL usuarioDAL = new UsuarioDAL();
                Usuario usuario = usuarioDAL.FindLogin(txtLogin.Text);
                if (usuario != null)
                {
                    divWarning.Visible = true;
                    lblAviso.Text = lblAviso.Text = "O login " + usuario.Login.ToUpper() + " já existe no sistema.";
                    return;
                }
                txtNome.Focus();
            }
            catch (Exception ex)
            {
                divDanger.Visible = true;
                lblErro.Text = ex.Message;
            }
        }

        protected void gridUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            OcultarDIV();
            if (txtpesquisarLogin.Text == "")
            {
                gridUsuarios.PageIndex = e.NewPageIndex;
                CarregarGrid();
            }
            else
            {
                gridUsuarios.PageIndex = e.NewPageIndex;
                btnPesquisar_Click(null, null);
            }
        }

        protected void gridUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Editar":
                    Response.Redirect("DetalharUsu.aspx?Id=" + e.CommandArgument.ToString());
                    break;
                case "Excluir":
                    ExcluirItem(Convert.ToInt32(e.CommandArgument));
                    break;
            }
        }

        private void IniciarPagina()
        {
            CarregarCombo();
            CarregarGrid();
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
                ddlPerfil.Items.Insert(0, "Selecione");
                ddlPerfil.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                divDanger.Visible = true;
                lblErro.Text = "Não foi possível carregar a lista de perfis. " + ex.Message;
            }
        }

        private void CarregarGrid()
        {
            try
            {
                UsuarioDAL uDAL = new UsuarioDAL();
                gridUsuarios.DataSource = uDAL.FindAll();
                gridUsuarios.DataBind();
            }
            catch (Exception ex)
            {
                divDanger.Visible = true;
                lblErro.Text = "Erro ao carregar a lista: " + ex.Message;
            }
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

        private void LimparCampos()
        {
            txtNome.Text = string.Empty;
            txtLogin.Text = string.Empty;
            txtEmail.Text = string.Empty;
            ddlPerfil.SelectedIndex = 0;
            txtpesquisarLogin.Text = string.Empty;
        }

        private void OcultarDIV()
        {
            divSuccess.Visible = false;
            divDanger.Visible = false;
            divWarning.Visible = false;
        }

        private void ExcluirItem(int Id)
        {
            UsuarioDAL uDAL = new UsuarioDAL();
            uDAL.Delete(Id);

            CarregarGrid();

            divSuccess.Visible = true;
            lblSucesso.Text = "Usuário excluido com sucesso.";
        }
    }
}