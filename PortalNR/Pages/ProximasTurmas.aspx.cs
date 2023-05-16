using PortalNR.Code;
using PortalNR.DAL.Entities;
using PortalNR.DAL.Persistence;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace PortalNR.Pages
{
    public partial class ProximasTurmas : PageBaseLocal
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IniciarPagina();
            }
        }

        protected void IniciarPagina()
        {
            CarregaDdlNormativo();
            CarregarDdlMeses();
            CarregarGrid();
            ConfigurarPerfilUsuario();
        }

        private void ConfigurarPerfilUsuario()
        {
            PerfilDAL perfil = new PerfilDAL();

            if (UsuarioLocalLogado == null) return;

            foreach (var item in perfil.ListaPerfil().Where(item => item.Id == UsuarioLocalLogado.PerfilRel.Id))
            {
                if (UsuarioLocalLogado != null && UsuarioLocalLogado.PerfilRel.Id != item.Id)
                    AlertaRedirecionar("Você não tem permissão de acessar esta página!", "../Login.aspx");
            }
        }

        private void CarregaDdlNormativo()
        {
            try
            {
                TurmasDAL tDAL = new TurmasDAL();
                //Listar DropDownList Normativos
                ddlNR.DataSource = tDAL.ListaNormativos();
                ddlNR.DataValueField = "IdNormativo";
                ddlNR.DataTextField = "Normativo";
                ddlNR.DataBind();
                ddlNR.Items.Insert(0, new ListItem("Selecione", ""));
                ddlNR.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                divDanger.Visible = true;
                lblErro.Text = "Não foi possível carregar a lista de normativos: " + ex.Message;
            }
        }

        private void CarregarDdlMeses()
        {
            ddlMeses.DataSource = Enum.GetNames(typeof(Meses));
            ddlMeses.DataBind();
            ddlMeses.Items.Insert(0, new ListItem("Selecione", ""));
            ddlMeses.SelectedIndex = 0;
        }

        private void CarregarGrid()
        {
            try
            {
                TurmasDAL cDAL = new TurmasDAL();
                gridTurmas.DataSource = cDAL.FindProximasTurmas();
                gridTurmas.DataBind();
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
            divSuccess.Visible = false;
            divWarning.Visible = false;
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            ddlNR.SelectedIndex = 0;
            ddlMeses.SelectedIndex = 0;
            CarregarGrid();
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                TurmasDAL tDAL = new TurmasDAL();

                string NR = ddlNR.SelectedItem.Text;
                string Mes = ddlMeses.SelectedItem.Text;

                if (ddlNR.SelectedIndex != 0 && ddlMeses.SelectedIndex == 0)
                {
                    OcultarDIV();
                    gridTurmas.DataSource = tDAL.FindTurmasNormativo(ddlNR.SelectedItem.Text, ddlMeses.SelectedItem.Value);
                    gridTurmas.DataBind();
                }
                else if (ddlNR.SelectedIndex == 0 && ddlMeses.SelectedIndex != 0)
                {
                    OcultarDIV();
                    gridTurmas.DataSource = tDAL.FindTurmasNormativo(ddlNR.SelectedItem.Value, ddlMeses.SelectedItem.Text);
                    gridTurmas.DataBind();
                }
                else if (ddlNR.SelectedIndex != 0 && ddlMeses.SelectedIndex != 0)
                {
                    OcultarDIV();
                    gridTurmas.DataSource = tDAL.FindTurmasNormativo(ddlNR.SelectedItem.Text, ddlMeses.SelectedItem.Text);
                    gridTurmas.DataBind();
                }
                else
                {
                    lblAviso.Text = "Informe ao menos um dos filtros para consulta.";
                    divWarning.Visible = true;
                    ddlNR.Focus();
                    ddlNR.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                divDanger.Visible = true;
                lblErro.Text = ex.Message;
            }
        }

        protected void btnVisualizar_Click(object sender, EventArgs e)
        {
            try
            {
                OcultarDIV();
                Card1.Visible = false;
                Card2.Visible = false;
                Card3.Visible = true;

                LinkButton btnVisualizar = (LinkButton)sender;
                int IdTurma = Int32.Parse(btnVisualizar.CommandArgument);

                TurmasDAL tDal = new TurmasDAL();
                Turmas t = tDal.FindById(IdTurma);
                txtObservação.Text = t.Observacao;

                TurmasParticpantesDAL tpDAL = new TurmasParticpantesDAL();
                gridParticipantes.DataSource = tpDAL.FindParticipantes(IdTurma);
                gridParticipantes.DataBind();
            }
            catch (Exception ex)
            {
                divDanger.Visible = true;
                lblErro.Text = ex.Message;
            }
        }

        protected void btnvoltar_Click(object sender, EventArgs e)
        {
            OcultarDIV();
            Card1.Visible = true;
            Card2.Visible = true;
            Card3.Visible = false;
        }

        protected void gridTurmas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var entidade = e.Row.DataItem as Turmas;
                var btnAdicionar = e.Row.FindControl("btnAdicionar") as LinkButton;
                var lblStatus = e.Row.FindControl("lblStatus") as Label;

                TurmasDAL cDAL = new TurmasDAL();

                if (Convert.ToBoolean(entidade.Ativo) != false && entidade.dtTurma >= DateTime.Now.AddHours(12))
                    btnAdicionar.CssClass = "btn btn-primary btn-sm";

                if (Convert.ToBoolean(entidade.Ativo) == true && entidade.dtTurma >= DateTime.Now.AddHours(12))
                {
                    lblStatus.Text = "Aberta";
                    lblStatus.ForeColor = System.Drawing.Color.Green;
                }
                else if (Convert.ToBoolean(entidade.Ativo) == true && entidade.dtTurma <= DateTime.Now.AddHours(12))
                {
                    lblStatus.Text = "Expirada";
                    lblStatus.ForeColor = System.Drawing.Color.DarkBlue;
                }
                else if (Convert.ToBoolean(entidade.Ativo) == false)
                {
                    lblStatus.Text = "Cancelada";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
}