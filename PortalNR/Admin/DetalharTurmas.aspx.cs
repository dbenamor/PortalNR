using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using PortalNR.Code;
using PortalNR.DAL.Entities;
using PortalNR.DAL.Persistence;

namespace PortalNR.Admin
{
    public partial class DetalharTurmas : PageBaseLocal
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IniciarPagina();
                txtObservacao.Attributes.Add("onkeydown", "return getText(this, event);");
                txtObservacao.Attributes.Add("onkeyup", "setText(this);");
            }
        }

        protected void btnAtualizar_Click(object sender, EventArgs e)
        {
            try
            {
                OcultarDiv();
                Turmas t = new Turmas();
                t.normativoRel = new Normativos();
                t.TemaRelac = new TurmasTema();

                t.IdTurma = Int32.Parse(txtId.Value);
                t.dtTurma = DateTime.Parse(txtDataHora.Text);
                t.dtTurmaFim = DateTime.Parse(txtDataHoraFim.Text);
                t.Carga = Int32.Parse(txtCarga.Text);
                t.Local = txtLocal.Text;
                t.Vagas = Int32.Parse(txtVagas.Text);
                t.Palestrante = txtPalestrante.Text;
                t.normativoRel.IdNormativo = Int32.Parse(ddlNR.SelectedValue);
                t.TemaRelac.IdTema = Int32.Parse(ddlTema.SelectedValue);
                t.Observacao = txtObservacao.Text;
                t.Veiculo = ddlVeiculo.SelectedItem.ToString() == "Selecione" ? "" : ddlVeiculo.SelectedItem.ToString();

                TurmasDAL tDAL = new TurmasDAL();
                tDAL.Update(t);

                LimparCampos();

                divSuccess.Visible = true;
                lblSucesso.Text = "Turma atualizada com sucesso.";
            }
            catch (Exception ex)
            {
                divDanger.Visible = true;
                lblErro.Text = ex.Message;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("CadastrarTurmas.aspx");
        }

        protected void btnCancelarTurma_Click(object sender, EventArgs e)
        {
            try
            {
                OcultarDiv();
                Turmas t = new Turmas();
                t.IdTurma = Int32.Parse(txtId.Value);

                TurmasDAL tDAL = new TurmasDAL();
                tDAL.CancelarTurma(t);
                divSuccess.Visible = true;
                lblSucesso.Text = "Turma cancelada.";
            }
            catch (Exception ex)
            {
                divDanger.Visible = true;
                lblErro.Text = ex.Message;
            }
        }

        protected void btnRemover_Click(object sender, EventArgs e)
        {
            try
            {
                OcultarDiv();
                LinkButton btnRemover = (LinkButton)sender;
                int IdParticipante = Int32.Parse(btnRemover.CommandArgument);

                TurmasParticpantesDAL tpDAL = new TurmasParticpantesDAL();
                tpDAL.Remover(IdParticipante);

                CarregaGrid();

                divSuccess.Visible = true;
                lblSucesso.Text = "Matrícula removida com sucesso.";
            }
            catch (Exception ex)
            {
                divDanger.Visible = true;
                lblErro.Text = ex.Message;
            }
        }
        
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                OcultarDiv();
                TurmasParticpantesDAL tpDAL = new TurmasParticpantesDAL();
                TurmasParticpantes tp = new TurmasParticpantes();
                int qtd = 0;

                foreach (GridViewRow linhas in gridParticipantes.Rows)
                {
                    CheckBox chkPresenca = (CheckBox)linhas.FindControl("chkPresenca");

                    if (chkPresenca.Checked == true || chkPresenca.Checked == false)
                    {
                        HiddenField txtIdParticipante = (HiddenField)linhas.FindControl("txtIdParticipante");
                        int IdParticipante = Int32.Parse(txtIdParticipante.Value);

                        tp.IdParticipante = IdParticipante;
                        tp.Presenca = chkPresenca.Checked;
                        tpDAL.FindById(tp.IdParticipante);
                        tpDAL.Update(tp);
                        qtd++;                        
                    }
                } 

                divSuccess.Visible = true;
                lblSucesso.Text = qtd + " participantes atualizados com sucesso.";
                CarregaGrid();
            }
            catch (Exception ex)
            {
                divDanger.Visible = true;
                lblErro.Text = ex.Message;
            }
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            OcultarDiv();
            int IdTurma = Convert.ToInt32(txtId.Value);
            Response.Redirect("ExportExcel.aspx?Id=" + IdTurma);
        }

        protected void ddlNR_TextChanged(object sender, EventArgs e)
        {
            try
            {
                OcultarDiv();

                if (ddlNR.SelectedIndex != 0)
                {
                    if (ddlNR.SelectedItem.ToString() == "NR-11")
                    {
                        ddlVeiculo.Enabled = true;
                        AtualizaDropDownVeiculo();
                    }
                    else
                    {
                        ddlVeiculo.Enabled = false;
                        ddlVeiculo.SelectedIndex = 0;
                    }
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

        protected void ddlSelecao_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int opcao = Int32.Parse(ddlSelecao.SelectedValue);

                foreach (GridViewRow linha in gridParticipantes.Rows)
                {
                    CheckBox chkPresenca = (CheckBox)linha.FindControl("chkPresenca");

                    switch (opcao)
                    {
                        case 1: chkPresenca.Checked = true;
                            break;
                        case 2: chkPresenca.Checked = false;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                divDanger.Visible = true;
                lblErro.Text = ex.Message;

            }
        }

        protected void gridParticipantes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var entidade = e.Row.DataItem as TurmasParticpantes;
                var chkPresenca = e.Row.FindControl("chkPresenca") as CheckBox;

                if (entidade.Presenca == true)
                    chkPresenca.Checked = true;
                else
                    chkPresenca.Checked = false;
            }
        }

        ///Removida a paginação a pedido do usuário, pois estava adando erro ao paginar - 19/10/2021
        //protected void gridParticipantes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    gridParticipantes.PageIndex = e.NewPageIndex;
        //}

        protected void IniciarPagina()
        {
            CarregarDropDown();
            CarregaDadosCampos();
            CarregaGrid();
            ConfigurarPerfilUsuario();
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

        protected void CarregarDropDown()
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

                //Listar DropDownList Tema
                ddlTema.DataSource = tDAL.ListaTemas();
                ddlTema.DataValueField = "IdTema";
                ddlTema.DataTextField = "Tema";
                ddlTema.DataBind();
                ddlTema.Items.Insert(0, new ListItem("Selecione", ""));

                //Listar DropDownList Veiculos
                ddlVeiculo.DataSource = tDAL.ListaVeiculos();
                ddlVeiculo.DataValueField = "Descricao";
                ddlVeiculo.DataTextField = "Descricao";
                ddlVeiculo.DataBind();
                ddlVeiculo.Items.Insert(0, new ListItem("Selecione", ""));
                ddlVeiculo.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                divDanger.Visible = true;
                lblErro.Text = "Não foi possível carregar a lista de normativos. " + ex.Message;
            }
        }

        protected void CarregaDadosCampos()
        {
            int id = Int32.Parse(Request.QueryString["IdTurma"]);
            TurmasDAL tDAL = new TurmasDAL();
            Turmas t = tDAL.FindById(id);

            txtId.Value = Convert.ToString(t.IdTurma);
            txtDataHora.Text = Convert.ToDateTime(t.dtTurma).ToString("yyyy-MM-dd HH:mm").Replace(' ', 'T');
            txtDataHoraFim.Text = Convert.ToDateTime(t.dtTurmaFim).ToString("yyyy-MM-dd HH:mm").Replace(' ', 'T');
            txtCarga.Text = Convert.ToInt32(t.Carga).ToString();
            txtLocal.Text = t.Local;
            txtVagas.Text = Convert.ToInt32(t.Vagas).ToString();
            txtPalestrante.Text = t.Palestrante;
            ddlTema.SelectedValue = t.TemaRelac.IdTema.ToString();
            ddlNR.SelectedValue = t.normativoRel.IdNormativo.ToString();
            ddlVeiculo.SelectedValue = t.Veiculo;
            txtObservacao.Text = t.Observacao;

            if (t.Ativo == false)
                btnCancelarTurma.CssClass = "btn btn-danger disabled";

        }

        protected void AtualizaDropDownVeiculo()
        {
            int id = Int32.Parse(Request.QueryString["IdTurma"]);
            TurmasDAL tDAL = new TurmasDAL();
            Turmas t = tDAL.FindById(id);

            ddlVeiculo.SelectedValue = t.Veiculo;
        }

        protected void CarregaGrid()
        {
            try
            {
                int IdTurma = Int32.Parse(Request.QueryString["IdTurma"]);

                TurmasParticpantesDAL tpDAL = new TurmasParticpantesDAL();
                gridParticipantes.DataSource = tpDAL.FindParticipantes(IdTurma);

                var count = ((ICollection<TurmasParticpantes>)gridParticipantes.DataSource).Count;

                if (count == 0)
                {                    
                    txtDataHora.ReadOnly = false;
                    txtDataHoraFim.ReadOnly = false;
                    txtCarga.ReadOnly = false;
                    ddlNR.Enabled = true;
                    ddlTema.Enabled = true;
                    ddlVeiculo.Enabled = true;
                    divExportacao.Visible = false;
                }
                gridParticipantes.DataBind();
            }
            catch (Exception ex)
            {
                divDanger.Visible = true;
                lblErro.Text = ex.Message;
            }
        }

        protected void LimparCampos()
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
        }

        private void OcultarDiv()
        {
            divSuccess.Visible = false;
            divWarning.Visible = false;
            divDanger.Visible = false;
        }
    }
}