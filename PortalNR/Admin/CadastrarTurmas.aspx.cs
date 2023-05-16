using PortalNR.Code;
using PortalNR.DAL.Entities;
using PortalNR.DAL.Persistence;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace PortalNR.Admin
{
    public partial class CadastrarTurmas : PageBaseLocal
    {
        #region "Session"

        private int Index
        {
            get
            {
                if (Session["_Index"] == null) return 0;
                return Convert.ToInt32(Session["_Index"]);
            }
            set
            {
                Session["_Index"] = value;
            }
        }

        private int matricula
        {
            get
            {
                if (Session["_matricula"] == null) return 0;
                return Convert.ToInt32(Session["_matricula"]);
            }
            set
            {
                Session["_matricula"] = value;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IniciarPagina();
                txtObservacao.Attributes.Add("onkeydown", "return getText(this, event);");
                txtObservacao.Attributes.Add("onkeyup", "setText(this);");
            }
        }

        protected void btnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                OcultarDIV();
                Turmas t = new Turmas();
                t.normativoRel = new Normativos();
                t.TemaRelac = new TurmasTema();
                t.VeiculoRel = new Veiculo();

                t.dtTurma = DateTime.Parse(txtDataHora.Text);
                t.dtTurmaFim = DateTime.Parse(txtDataHoraFim.Text);
                t.Carga = Int32.Parse(txtCarga.Text);
                t.Local = txtLocal.Text;
                t.Vagas = Int32.Parse(txtVagas.Text);
                t.Palestrante = txtPalestrante.Text;
                t.normativoRel.IdNormativo = Int32.Parse(ddlNR.SelectedItem.Value);
                t.Observacao = txtObservacao.Text;
                t.TemaRelac.IdTema = Int32.Parse(ddlTema.SelectedItem.Value);
                t.Veiculo = ddlVeiculo.SelectedItem.ToString() == "Selecione" ? "" : ddlVeiculo.SelectedItem.ToString();

                TurmasDAL tDAL = new TurmasDAL();
                tDAL.Insert(t);

                divSuccess.Visible = true;
                lblSucesso.Text = "Turma cadastrada com sucesso.";

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

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                OcultarDIV();

                if (!string.IsNullOrEmpty(txtDe.Text) && !string.IsNullOrEmpty(txtPara.Text))
                {
                    DateTime De = Convert.ToDateTime(txtDe.Text);
                    DateTime Para = Convert.ToDateTime(txtPara.Text);

                    TurmasDAL tDAL = new TurmasDAL();

                    gridTurmas.DataSource = tDAL.PesquisarTurmas(De, Para);
                    gridTurmas.DataBind();
                }
                else if (string.IsNullOrEmpty(txtDe.Text) || string.IsNullOrEmpty(txtPara.Text))
                {
                    divWarning.Visible = true;
                    lblAviso.Text = "Informe os dois campos de data para pesquisa.";
                }
            }
            catch (Exception ex)
            {
                divDanger.Visible = true;
                lblErro.Text = ex.Message;
            }
        }

        protected void ddlNR_TextChanged(object sender, EventArgs e)
        {
            try
            {
                OcultarDIV();

                if (ddlNR.SelectedIndex != 0)
                {
                    if (ddlNR.SelectedItem.ToString() == "NR-11")
                        ddlVeiculo.Enabled = true;
                    else
                        ddlVeiculo.Enabled = false;
                }
                else
                {
                    ddlVeiculo.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                divDanger.Visible = true;
                lblErro.Text = ex.Message;
            }
        }

        protected void gridTurmas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var entidade = e.Row.DataItem as Turmas;
                var btnExcluir = e.Row.FindControl("btnExcluir") as LinkButton;
                var lblStatus = e.Row.FindControl("lblStatus") as Label;
                TurmasDAL cDAL = new TurmasDAL();

                if (entidade.Vagas == entidade.vagasDisponiveis)
                    btnExcluir.CssClass = "btn btn-danger btn-sm";
                else
                    btnExcluir.CssClass = "btn btn-danger btn-sm disabled";

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

        protected void gridTurmas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Editar":
                    Response.Redirect("DetalharTurmas.aspx?IdTurma=" + e.CommandArgument.ToString());
                    break;
                case "Excluir":
                    ExcluirItem(Convert.ToInt32(e.CommandArgument));
                    break;
            }
        }

        protected void gridTurmas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            OcultarDIV();
            if (string.IsNullOrEmpty(txtDe.Text) && string.IsNullOrEmpty(txtPara.Text))
            {
                gridTurmas.PageIndex = e.NewPageIndex;
                CarregarGrid();
            }
            else
            {
                gridTurmas.PageIndex = e.NewPageIndex;
                btnPesquisar_Click(null, null);
            }
        }

        private void IniciarPagina()
        {
            ConfigurarPerfilUsuario();
            CarregarDropDown();
            CarregarGrid();
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

        private void CarregarDropDown()
        {
            try
            {
                TurmasDAL tDAL = new TurmasDAL();
                //Listar DropDownList Normativos
                ddlNR.DataSource = tDAL.ListaNormativos();
                ddlNR.DataValueField = "IdNormativo";
                ddlNR.DataTextField = "Normativo";
                ddlNR.DataBind();
                ddlNR.Items.Insert(0, "Selecione");
                ddlNR.SelectedIndex = 0;

                //Listar DropDownList Tema
                ddlTema.DataSource = tDAL.ListaTemas();
                ddlTema.DataValueField = "IdTema";
                ddlTema.DataTextField = "Tema";
                ddlTema.DataBind();
                ddlTema.Items.Insert(0, "Selecione");
                ddlTema.SelectedIndex = 0;

                //Listar DropDownList Veiculos
                ddlVeiculo.DataSource = tDAL.ListaVeiculos();
                ddlVeiculo.DataValueField = "IdVeiculo";
                ddlVeiculo.DataTextField = "Descricao";
                ddlVeiculo.DataBind();
                ddlVeiculo.Items.Insert(0, "Selecione");
                ddlVeiculo.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                divDanger.Visible = true;
                lblErro.Text = "Não foi possível carregar a lista de: " + ex.Message;
            }
        }

        private void CarregarGrid()
        {
            try
            {
                TurmasDAL cDAL = new TurmasDAL();
                gridTurmas.DataSource = cDAL.FindAll();
                gridTurmas.DataBind();
            }
            catch (Exception ex)
            {
                divDanger.Visible = true;
                lblErro.Text = ex.Message;
            }
        }

        private void LimparCampos()
        {
            txtDataHora.Text = string.Empty;
            txtDataHoraFim.Text = string.Empty;
            txtCarga.Text = string.Empty;
            txtLocal.Text = string.Empty;
            txtVagas.Text = string.Empty;
            txtPalestrante.Text = string.Empty;
            ddlNR.SelectedIndex = 0;
            ddlTema.SelectedIndex = 0;
            ddlVeiculo.SelectedIndex = 0;
            txtObservacao.Text = string.Empty;
            txtDe.Text = string.Empty;
            txtPara.Text = string.Empty;
        }

        private void OcultarDIV()
        {
            divSuccess.Visible = false;
            divDanger.Visible = false;
            divWarning.Visible = false;
        }

        private void ExcluirItem(int Id)
        {
            TurmasDAL tDAL = new TurmasDAL();
            tDAL.Delete(Id);

            CarregarGrid();

            divSuccess.Visible = true;
            lblSucesso.Text = "Turma excluida com sucesso.";
        }
    }
}