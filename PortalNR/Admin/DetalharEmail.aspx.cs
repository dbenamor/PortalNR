using PortalNR.Code;
using PortalNR.DAL.Entities;
using PortalNR.DAL.Persistence;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace PortalNR.Admin
{
    public partial class DetalharEmail : PageBaseLocal
    {
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
                OcultarDIV();
                Emailcoordenacao ec = new Emailcoordenacao();

                ec.Id = Int32.Parse(txtId.Value);
                ec.Coordenacao = ddlCoordenacao.SelectedValue;
                ec.Responsavel = txtResponsavel.Text;
                ec.email = txtEmail.Text;
                ec.Ativo = chkAtivo.Checked;

                EmailcoordenacaoDAL mail = new EmailcoordenacaoDAL();
                mail.Update(ec);

                LimparCampos();

                divSuccess.Visible = true;
                lblSucesso.Text = "Informações atualizada com sucesso.";
            }
            catch (Exception ex)
            {
                divDanger.Visible = true;
                lblErro.Text = ex.Message;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("CadastrarEmail.aspx");
        }

        private void IniciarPagina()
        {
            CarregarCombo();
            CarregarCampos();
            ConfigurarPerfilUsuario();
        }

        private void CarregarCampos()
        {
            try
            {
                int Id = Int32.Parse(Request.QueryString["Id"]);

                EmailcoordenacaoDAL edal = new EmailcoordenacaoDAL();                
                Emailcoordenacao ec = edal.FindbyId(Id);

                txtId.Value = ec.Id.ToString();
                txtResponsavel.Text = ec.Responsavel;
                ddlCoordenacao.SelectedValue = ec.Coordenacao;
                txtEmail.Text = ec.email;
                chkAtivo.Checked = ec.Ativo;
            }
            catch (Exception ex)
            {
                divDanger.Visible = true;
                lblErro.Text = ex.Message;
            }
        }

        private void CarregarCombo()
        {
            try
            {
                EmailcoordenacaoDAL edal = new EmailcoordenacaoDAL();
                ddlCoordenacao.DataSource = edal.ListaCoordenacao();
                ddlCoordenacao.DataTextField = "Coordenacao";
                ddlCoordenacao.DataBind();
                ddlCoordenacao.Items.Insert(0, new ListItem("- Selecione -", ""));                
            }
            catch (Exception ex)
            {
                divDanger.Visible = true;
                lblErro.Text = ex.Message;
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

        protected void LimparCampos()
        {
            OcultarDIV();
            txtEmail.Text = string.Empty;
            txtResponsavel.Text = string.Empty;
            ddlCoordenacao.SelectedIndex = 0;
        }

        private void OcultarDIV()
        {
            divSuccess.Visible = false;
            divDanger.Visible = false;
            divWarning.Visible = false;
        }
    }
}