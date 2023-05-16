using PortalNR.Code;
using PortalNR.DAL.Entities;
using PortalNR.DAL.Persistence;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace PortalNR.Admin
{
    public partial class EditarColabCoord : PageBaseLocal
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IniciarPagina();
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ConsultarColabCoord.aspx");
        }

        protected void btnAtualizar_Click(object sender, EventArgs e)
        {
            try
            {
                OcultarDIV();
                Colaborador c = new Colaborador();

                c.Matricula = Int32.Parse(txtMatricula.Text);
                c.Coordenacao = txtCoordenacao.Text;
                c.Situacao = ddlSituacaoNR.SelectedValue;

                ColaboradorDAL cDAL = new ColaboradorDAL();
                cDAL.UpdateColabCoord(c);

                LimparCampos();

                divSuccess.Visible = true;
                lblSucesso.Text = "Colaborador atualizado com sucesso.";
            }
            catch (Exception ex)
            {
                divDanger.Visible = true;
                lblErro.Text = ex.Message;
            }
        }

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
                ColaboradorDAL cDAL = new ColaboradorDAL();
                //Listar DropDownList SituacaoNR
                ddlSituacaoNR.DataSource = cDAL.ListaSituacaoNR();
                ddlSituacaoNR.DataTextField = "Descricao";
                ddlSituacaoNR.DataValueField = "Descricao";
                ddlSituacaoNR.DataBind();
                ddlSituacaoNR.Items.Insert(0, new ListItem("Selecione"));
                ddlSituacaoNR.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                divDanger.Visible = true;
                lblErro.Text = ex.Message;
            }
        }

        private void CarregarCampos()
        {
            try
            {
                int Matricula = Int32.Parse(Request.QueryString["Matricula"]);
                string Coordenacao = Request.QueryString["Coordenacao"];

                ColaboradorDAL cDAL = new ColaboradorDAL();
                Colaborador c = cDAL.FindByMatriculaCoordenacao(Matricula, Coordenacao);

                txtMatricula.Text = c.Matricula.ToString();
                txtColaborador.Text = c.Nome;
                txtCoordenacao.Text = c.Coordenacao;
                txtSituacao.Text = c.SituacaoEfetivo;
                ddlSituacaoNR.SelectedValue = c.Situacao;
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
            txtMatricula.Text = string.Empty;
            txtColaborador.Text = string.Empty;
            txtCoordenacao.Text = string.Empty;
            txtSituacao.Text = string.Empty;
            ddlSituacaoNR.SelectedIndex = 0;
        }

        protected void OcultarDIV()
        {
            divSuccess.Visible = false;
            divWarning.Visible = false;
            divDanger.Visible = false;
        }
    }
}