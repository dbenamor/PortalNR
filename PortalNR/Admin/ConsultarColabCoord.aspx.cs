using PortalNR.Code;
using PortalNR.DAL.Persistence;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace PortalNR.Admin
{
    public partial class ConsultarColabCoord : PageBaseLocal
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
                OcultarDIV();
                ColaboradorDAL cDAL = new ColaboradorDAL();
                int Matricula = 0;
                string Funcionario = txtColaborador.Text;
                string Coordenacao = txtCoordenacao.Text;

                if (txtMatricula.Text != "" && txtColaborador.Text == "" && txtCoordenacao.Text == "")
                {
                    if (!string.IsNullOrEmpty(txtMatricula.Text))
                        Matricula = Int32.Parse(txtMatricula.Text);
                    gridColaborador.DataSource = cDAL.findMatriculaColabCoord(Matricula, Funcionario, Coordenacao);
                }
                else if (txtMatricula.Text == "" && txtColaborador.Text != "" && txtCoordenacao.Text == "")
                {
                    if (!string.IsNullOrEmpty(txtColaborador.Text))
                        gridColaborador.DataSource = cDAL.findMatriculaColabCoord(Matricula, Funcionario, Coordenacao);
                }
                else if (txtMatricula.Text == "" && txtColaborador.Text == "" && txtCoordenacao.Text != "")
                {
                    if (!string.IsNullOrEmpty(txtCoordenacao.Text))
                        gridColaborador.DataSource = cDAL.findMatriculaColabCoord(Matricula, Funcionario, Coordenacao);
                }
                else if (txtMatricula.Text == "" && txtColaborador.Text != "" && txtCoordenacao.Text != "")
                {
                    if (!string.IsNullOrEmpty(txtColaborador.Text) && !string.IsNullOrEmpty(txtCoordenacao.Text))
                        gridColaborador.DataSource = cDAL.findMatriculaColabCoord(Matricula, Funcionario, Coordenacao);
                }
                else
                {
                    divWarning.Visible = true;
                    lblAviso.Text = "Preencha ao menos um dos campos para realizar a consulta.";
                }

                if (gridColaborador.DataSource != null)
                    gridColaborador.DataBind();
                else
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

        protected void gridColaborador_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            OcultarDIV();
            if (txtMatricula.Text == "" && txtColaborador.Text == "" && txtCoordenacao.Text == "")
            {
                gridColaborador.PageIndex = e.NewPageIndex;
                CarregarGrid();
            }
            else
            {
                gridColaborador.PageIndex = e.NewPageIndex;
                btnPesquisar_Click(null, null);
            }

        }

        protected void gridColaborador_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Editar":
                    string[] commandArgsAccept = e.CommandArgument.ToString().Split(new char[] { ',' });
                    Response.Redirect("EditarColabCoord.aspx?Matricula=" + commandArgsAccept[0].ToString() + "&Coordenacao=" + commandArgsAccept[1].ToString());
                    break;
            }
        }

        protected void IniciarPagina()
        {
            CarregarGrid();
            ConfigurarPerfilUsuario();
        }

        protected void CarregarGrid()
        {
            try
            {
                ColaboradorDAL cDAL = new ColaboradorDAL();
                gridColaborador.DataSource = cDAL.ListAll();
                gridColaborador.DataBind();
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
            txtMatricula.Text = string.Empty;
            txtColaborador.Text = string.Empty;
            txtCoordenacao.Text = string.Empty;
            Response.Redirect(Request.RawUrl);
        }

        protected void OcultarDIV()
        {
            divSuccess.Visible = false;
            divWarning.Visible = false;
            divDanger.Visible = false;
        }
    }
}