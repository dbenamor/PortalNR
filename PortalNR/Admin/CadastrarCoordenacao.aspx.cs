using PortalNR.Code;
using PortalNR.DAL.Entities;
using PortalNR.DAL.Persistence;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace PortalNR.Admin
{
    public partial class CadastrarCoordenacao : PageBaseLocal
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IniciarPaginda();
            }
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparCampos();

            CoordenacaoDAL c = new CoordenacaoDAL();

            gridCoordenacao.DataSource = c.ListAll();
            gridCoordenacao.DataBind();
        }

        protected void btnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                Coordenacao c = new Coordenacao();
                c.Descricao = txtDescricao.Text;

                CoordenacaoDAL cdal = new CoordenacaoDAL();
                cdal.Insert(c);

                divSuccess.Visible = true;
                lblSucesso.Text = "Cadastro realizado com sucesso.";

                LimparCampos();

                gridCoordenacao.DataSource = cdal.ListAll();
                gridCoordenacao.DataBind();

            }
            catch (Exception ex)
            {
                divDanger.Visible = true;
                lblErro.Text = ex.Message;
            }
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                string Descricao = txtDescricao.Text;

                CoordenacaoDAL cdal = new CoordenacaoDAL();
                gridCoordenacao.DataSource = cdal.Find(Descricao);
                gridCoordenacao.DataBind();
            }
            catch (Exception ex)
            {
                divDanger.Visible = true;
                lblErro.Text = ex.Message;
            }
        }

        protected void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton btnExcluir = (LinkButton)sender;
                int Id = Int32.Parse(btnExcluir.CommandArgument);

                CoordenacaoDAL cdal = new CoordenacaoDAL();
                cdal.Delete(Id);

                divSuccess.Visible = true;
                lblSucesso.Text = "Registro excluído com sucesso.";

                gridCoordenacao.DataSource = cdal.ListAll();
                gridCoordenacao.DataBind();

            }
            catch (Exception ex)
            {
                divDanger.Visible = true;
                lblErro.Text = ex.Message;
            }
        }

        protected void IniciarPaginda()
        {
            CarregarGrid();
            ConfigurarPerfilUsuario();
        }

        protected void CarregarGrid()
        {
            try
            {
                CoordenacaoDAL c = new CoordenacaoDAL();

                gridCoordenacao.DataSource = c.ListAll();
                gridCoordenacao.DataBind();
            }
            catch (Exception ex)
            {
                divDanger.Visible = true;
                lblErro.Text = ex.Message;
            }
        }

        protected void LimparCampos()
        {
            txtDescricao.Text = string.Empty;
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