using PortalNR.Code;
using PortalNR.DAL.Entities;
using PortalNR.DAL.Persistence;
using System;
using System.Linq;

namespace PortalNR.Pages
{
    public partial class IncluirParticipante : PageBaseLocal
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IniciarPagina();
            }
        }

        protected void txtMatricula_TextChanged(object sender, EventArgs e)
        {
            try
            {
                OcultarDIV();
                if (txtMatricula.Text == "" || txtMatricula.Text == null)
                {
                    OcultarDIV();
                    txtNome.Text = string.Empty;
                    txtCoord.Text = string.Empty;
                }
                else
                {
                    VerificaColaborador();
                }
            }
            catch (Exception ex)
            {
                divDanger.Visible = true;
                lblErro.Text = ex.Message;
            }
        }

        protected void btnIncluir_Click(object sender, EventArgs e)
        {
            try
            {
                OcultarDIV();
                if (txtMatricula.Text == "" || txtMatricula.Text == null)
                {
                    divWarning.Visible = true;
                    lblAviso.Text = "Informe a matricula do colaborador.";
                }
                else
                {
                    TurmasParticpantes tp = new TurmasParticpantes();
                    tp.turmaRel = new Turmas();

                    tp.turmaRel.IdTurma = Int32.Parse(txtID.Value);
                    tp.Matricula = Int32.Parse(txtMatricula.Text);
                    tp.Nome = txtNome.Text;
                    tp.Coordenacao = txtCoord.Text;

                    var validaTempo = Convert.ToDateTime(txtDataHora.Text).ToString("dd/MM/yyyy HH:mm").Replace('T', ' ');

                    if (Convert.ToInt32(txtVagasDisponiveis.Text) == 0)
                    {
                        lblQuantitativo.Text = "Turma sem vaga disponível.";
                        divWarning.Visible = true;
                        lblAviso.Text = "Não é possível incluir participante nessa turma pelo motivo abaixo.";
                    }
                    else
                    {
                        if (Convert.ToDateTime(validaTempo) <= DateTime.Now.AddHours(12))
                        {
                            divWarning.Visible = true;
                            lblAviso.Text = "Não é possível incluir participante. Turma com menos de 12h para início.";
                        }
                        else
                        {
                            TurmasParticpantesDAL tpDAL = new TurmasParticpantesDAL();
                            Int32 IdTurma = Int32.Parse(txtID.Value);
                            var participante = tpDAL.FindParticipantes(IdTurma);

                            tp.Matricula = Int32.Parse(txtMatricula.Text);
                            tp.turmaRel.IdTurma = Int32.Parse(txtID.Value);

                            foreach (var item in participante)
                            {
                                if (item.Matricula == Int32.Parse(txtMatricula.Text) && item.turmaRel.IdTurma == Int32.Parse(txtID.Value))
                                {
                                    lblAviso.Text = "Colaborador já cadastrado para essa turma.";
                                    divWarning.Visible = true;
                                    txtMatricula.Focus();
                                    return;
                                }
                            }

                            //VerificaColaborador();
                            tpDAL.Adicionar(tp);

                            UpdateVagas();
                            LimparCampos();
                            CarregaGrid();
                            RecarregarCampos();

                            divSuccess.Visible = true;
                            lblSucesso.Text = "Matricula " + tp.Matricula + " incluida na turma com sucesso.";
                        }
                    }
                }
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
            btnIncluir.CssClass = "btn btn-edit";
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProximasTurmas.aspx");
        }

        protected void IniciarPagina()
        {
            CarregarCampos();
            CarregaGrid();
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

        private void CarregarCampos()
        {
            try
            {
                int Id = Int32.Parse(Request.QueryString["Id"]);
                TurmasDAL tDAL = new TurmasDAL();
                Turmas t = tDAL.FindById(Id);

                txtID.Value = Convert.ToString(t.IdTurma);
                txtNormativo.Value = Convert.ToString(t.normativoRel.Normativo);
                txtVagas.Text = Convert.ToString(t.Vagas);
                txtVagasDisponiveis.Text = Convert.ToString(t.vagasDisponiveis);
                txtObservação.Text = t.Observacao;
                txtDataHora.Text = Convert.ToDateTime(t.dtTurma).ToString("yyyy-MM-dd HH:mm").Replace(' ', 'T');

                if (Convert.ToInt32(t.vagasDisponiveis) == 0)
                {
                    lblQuantitativo.Text = "Turma sem vaga disponível.";
                }
                else
                {
                    lblQuantitativo.Text = "Turma com " + Convert.ToString(t.vagasDisponiveis) + " vagas disponiveis. ";
                }
                CarregaGrid();
            }
            catch (Exception ex)
            {
                divDanger.Visible = true;
                lblErro.Text = ex.Message;
            }
        }

        protected void LimparCampos()
        {
            txtMatricula.Text = string.Empty;
            txtNome.Text = string.Empty;
            txtCoord.Text = string.Empty;
        }

        private void OcultarDIV()
        {
            divDanger.Visible = false;
            divSuccess.Visible = false;
            divWarning.Visible = false;
        }

        protected void CarregaGrid()
        {
            try
            {
                Int32 IdTurma = Int32.Parse(txtID.Value);
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

        private void UpdateVagas()
        {
            TurmasParticpantesDAL tpDAL = new TurmasParticpantesDAL();
            tpDAL.UpdateVagas();
        }

        private void RecarregarCampos()
        {
            int Id = Int32.Parse(Request.QueryString["Id"]);
            TurmasDAL tDAL = new TurmasDAL();
            Turmas t = tDAL.FindById(Id);

            txtVagas.Text = Convert.ToString(t.Vagas);
            txtVagasDisponiveis.Text = Convert.ToString(t.vagasDisponiveis);
            if (Convert.ToInt32(txtVagasDisponiveis.Text) == 0)
                lblQuantitativo.Text = "Turma sem vaga disponível.";
            else
                lblQuantitativo.Text = "Turma com " + Convert.ToString(t.vagasDisponiveis) + " vagas disponiveis. ";
        }

        private void VerificaColaborador()
        {
            int Matricula = Int32.Parse(txtMatricula.Text);
            string Normativo = txtNormativo.Value;
            ColaboradorDAL cDAL = new ColaboradorDAL();
            Colaborador c = cDAL.FindColaborador(Matricula, Normativo);

            if (c == null)
            {
                divWarning.Visible = true;
                lblAviso.Text = "Colaborador não encontrado ou não elegível.";
                txtNome.Text = string.Empty;
                txtCoord.Text = string.Empty;
                btnIncluir.CssClass = "btn btn-edit disabled";
            }
            else
            {
                txtNome.Text = c.Nome;
                txtCoord.Text = c.Coordenacao;
                btnIncluir.CssClass = "btn btn-edit";
            }
        }
    }
}