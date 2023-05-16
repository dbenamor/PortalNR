using PortalNR.Code;
using PortalNR.DAL.Entities;
using PortalNR.DAL.Persistence;
using System;
using System.Linq;

namespace PortalNR.Admin
{
    public partial class DetalharNR : PageBaseLocal
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
                Normativos n = new Normativos();

                n.IdNormativo = Int32.Parse(txtId.Value);
                n.Normativo = txtNR.Text;
                n.Vigencia = Int32.Parse(txtVigencia.Text);
                n.Descricao = txtDescricao.Text;
                n.Ativo = chkAtivo.Checked;

                NormativosDAL ndal = new NormativosDAL();
                ndal.Update(n);

                LimparCampos();

                divSuccess.Visible = true;
                lblSucesso.Text = "Normativo " + n.Normativo + " atualizado com sucesso.";
            }
            catch (Exception ex)
            {
                divDanger.Visible = true;
                lblErro.Text = ex.Message;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("CadastrarNR.aspx");
        }

        private void IniciarPagina()
        {
            CarregarCampos();
            ConfigurarPerfilUsuario();
        }

        private void CarregarCampos()
        {
            try
            {
                int id = Int32.Parse(Request.QueryString["IdNormativo"]);
                NormativosDAL ndal = new NormativosDAL();
                Normativos n = ndal.FindById(id);

                txtId.Value = n.IdNormativo.ToString();
                txtNR.Text = n.Normativo;
                txtVigencia.Text = n.Vigencia.ToString();
                txtDescricao.Text = n.Descricao;
                chkAtivo.Checked = n.Ativo;
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
            txtNR.Text = string.Empty;
            txtVigencia.Text = string.Empty;
            txtDescricao.Text = string.Empty;
        }
    }
}