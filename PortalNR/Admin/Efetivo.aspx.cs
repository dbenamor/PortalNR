using PortalNR.Code;
using PortalNR.DAL.Persistence;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace PortalNR.Admin
{
    public partial class Efetivo : PageBaseLocal
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InciarPagina();
            }
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                OcultarDIV();
                EfetivoDAL eDAL = new EfetivoDAL();
                int Matricula = 0;
                string Funcionario = txtFuncionario.Text;
                string Coordenacao = txtCoordenacao.Text;
                OcultarDIV();

                if (txtMatric.Text != "" && txtFuncionario.Text == "" && txtCoordenacao.Text == "")
                {
                    divCard.Visible = true;
                    if (!string.IsNullOrEmpty(txtMatric.Text))
                        Matricula = Int32.Parse(txtMatric.Text);
                    gridEfetivo.DataSource = eDAL.Find(Matricula, Funcionario, Coordenacao);
                }
                else if (txtMatric.Text == "" && txtFuncionario.Text != "" && txtCoordenacao.Text == "")
                {
                    divCard.Visible = true;
                    if (!string.IsNullOrEmpty(txtFuncionario.Text))
                        gridEfetivo.DataSource = eDAL.Find(Matricula, Funcionario, Coordenacao);
                }
                else if (txtMatric.Text == "" && txtFuncionario.Text == "" && txtCoordenacao.Text != "")
                {
                    divCard.Visible = true;
                    if (!string.IsNullOrEmpty(txtCoordenacao.Text))
                        gridEfetivo.DataSource = eDAL.Find(Matricula, Funcionario, Coordenacao);
                }
                else if (txtMatric.Text == "" && txtFuncionario.Text != "" && txtCoordenacao.Text != "")
                {
                    divCard.Visible = true;
                    if (!string.IsNullOrEmpty(txtFuncionario.Text) && !string.IsNullOrEmpty(txtCoordenacao.Text))
                        gridEfetivo.DataSource = eDAL.Find(Matricula, Funcionario, Coordenacao);
                }
                else
                {
                    divWarning.Visible = true;
                    divCard.Visible = false;
                    lblAviso.Text = "Preencha ao menos um dos campos para realizar a consulta.";
                }
                gridEfetivo.DataBind();
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
            CarregarGrid();
        }

        protected void gridEfetivo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            OcultarDIV();
            if (txtMatric.Text == "" && txtFuncionario.Text == "" && txtCoordenacao.Text == "")
            {
                gridEfetivo.PageIndex = e.NewPageIndex;
                CarregarGrid();
            }
            else
            {
                gridEfetivo.PageIndex = e.NewPageIndex;
                btnPesquisar_Click(null, null);
            }
        }

        private void InciarPagina()
        {
            ConfigurarPerfilUsuario();
            CarregarGrid();
        }

        private void CarregarGrid()
        {
            try
            {
                EfetivoDAL d = new EfetivoDAL();

                gridEfetivo.DataSource = d.FindAll();
                gridEfetivo.DataBind();
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

        private void OcultarDIV()
        {
            divDanger.Visible = false;
            divWarning.Visible = false;
        }

        protected void LimparCampos()
        {
            OcultarDIV();
            txtCoordenacao.Text = string.Empty;
            txtFuncionario.Text = string.Empty;
            txtMatric.Text = string.Empty;
            divCard.Visible = true;
            gridEfetivo.PageIndex = 0;
        }
    }
}