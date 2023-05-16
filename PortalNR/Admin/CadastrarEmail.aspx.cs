using PortalNR.Code;
using PortalNR.DAL.Entities;
using PortalNR.DAL.Persistence;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace PortalNR.Admin
{
    public partial class CadastrarEmail : PageBaseLocal
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

                Emailcoordenacao email = new Emailcoordenacao();
                email.Coordenacao = ddlCoordenacao.SelectedItem.ToString();
                email.Responsavel = txtResponsavel.Text;
                email.email = txtEmail.Text;
                email.Ativo = chkAtivo.Checked;

                EmailcoordenacaoDAL edal = new EmailcoordenacaoDAL();
                edal.Insert(email);

                divSuccess.Visible = true;
                lblSucesso.Text = "Email cadastrado com sucesso.";

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
            OcultarDIV();
            LimparCampos();
        }

        protected void gridEmail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridEmail.PageIndex = e.NewPageIndex;
            CarregarGrid();
        }

        protected void gridEmail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Editar":
                    Response.Redirect("DetalharEmail.aspx?Id=" + e.CommandArgument.ToString());
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
                EmailcoordenacaoDAL edal = new EmailcoordenacaoDAL();
                //Popular o DropDownList
                ddlCoordenacao.DataSource = edal.ListaCoordenacao();
                ddlCoordenacao.DataTextField = "Coordenacao";
                ddlCoordenacao.DataBind();
                ddlCoordenacao.Items.Insert(0, "Selecione");
            }
            catch (Exception ex)
            {
                divDanger.Visible = true;
                lblErro.Text = ex.Message;
            }
        }

        private void CarregarGrid()
        {
            try
            {
                EmailcoordenacaoDAL edal = new EmailcoordenacaoDAL();
                gridEmail.DataSource = edal.FindAll();
                gridEmail.DataBind();
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
            divSuccess.Visible = false;
        }

        protected void LimparCampos()
        {
            ddlCoordenacao.SelectedIndex = 0;
            txtResponsavel.Text = string.Empty;
            txtEmail.Text = string.Empty;
        }

        private void ExcluirItem(int Id)
        {
            EmailcoordenacaoDAL eDAL = new EmailcoordenacaoDAL();
            eDAL.Delete(Id);

            CarregarGrid();

            divSuccess.Visible = true;
            lblSucesso.Text = "Email excluido com sucesso.";
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