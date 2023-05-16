using PortalNR.Code;
using PortalNR.DAL.Entities;
using PortalNR.DAL.Persistence;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace PortalNR.Admin
{
    public partial class CadastrarColab : PageBaseLocal
    {
        #region "Sesssion"
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

        private String dataCertificacao
        {
            get
            {
                if (Session["_dataCertificacao"] == null) return null;
                return Session["_dataCertificacao"].ToString();
            }
            set
            {
                Session["_dataCertificacao"] = value;
            }
        }
       
        #endregion

        #region "Eventos"

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlNR.Focus();
                IniciarPagina();
            }
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            OcultarDIV();
            LimparCampos();
        }

        protected void btnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                OcultarDIV();
                ColaboradorDAL cDAL = new ColaboradorDAL();
                Colaborador c = new Colaborador();

                var entidade = cDAL.FindAll();

                foreach (var item in entidade)
                {
                    if (ddlNR.SelectedValue == "NR-11")
                    {
                        if (item.Matricula == Convert.ToInt32(txtMatric.Text) && item.Normativo == ddlNR.SelectedValue && item.Veiculo == ddlVeiculo.SelectedValue)
                        {
                            lblAviso.Text = "Colaborador já cadastrado para essa NR e equipamento.";
                            divWarning.Visible = true;
                            txtMatric.Focus();
                            return;
                        }
                    }
                    else
                    {
                        if (item.Matricula == Convert.ToInt32(txtMatric.Text) && item.Normativo == ddlNR.SelectedValue)
                        {
                            lblAviso.Text = "Colaborador já cadastrado para essa NR.";
                            divWarning.Visible = true;
                            txtMatric.Focus();
                            return;
                        }
                    }
                }
                c.Normativo = ddlNR.SelectedItem.ToString();
                c.Matricula = Convert.ToInt32(txtMatric.Text);
                c.Nome = txtNome.Text;
                c.Coordenacao = txtCoordenacao.Text;
                c.Situacao = ddlSituacaoNR.SelectedItem.ToString() == "Selecione" ? "" : ddlSituacaoNR.SelectedItem.ToString();
                c.Elegivel = chkElegivel.Checked;
                c.Veiculo = ddlVeiculo.SelectedItem.ToString() == "Selecione" ? "" : ddlVeiculo.SelectedItem.ToString();
                if (txtCertificacao.Text == "")
                    c.DtCertificacao = null;
                else
                    c.DtCertificacao = DateTime.Parse(txtCertificacao.Text);
                if (txtRevalidacao.Text == "")
                    c.DtRevalidacao = null;
                else
                    c.DtRevalidacao = DateTime.Parse(txtRevalidacao.Text);
                if (txtVigencia.Text == "")
                    c.DtVigencia = null;
                else
                    c.DtVigencia = DateTime.Parse(txtVigencia.Text);
                cDAL.Insert(c);

                divSuccess.Visible = true;
                lblSucesso.Text = "Colaborador " + c.Matricula + " - " + c.Nome + " cadastrado com sucesso.";

                LimparCampos();

                CarregarGrid(0);
            }
            catch (Exception ex)
            {
                divDanger.Visible = true;
                lblErro.Text = ex.Message;
            }
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditarColab.aspx");
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {               
                //O carregamento dessa pesquisa é feito no método abaixo.
                CarregarGrid(0);
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
                string Normativo = ddlNR.SelectedValue;

                if (ddlNR.SelectedIndex != 0)
                {
                    NormativosDAL nDAL = new NormativosDAL();
                    Normativos n = nDAL.FindByNormativo(Normativo);

                    //Preenche o campo txtTempo com tempo de vigência do normativo
                    txtTempo.Text = n.Vigencia.ToString();

                    if (ddlNR.SelectedValue == "NR-11")
                    {
                        ddlVeiculo.Enabled = true;
                        txtMatric.ReadOnly = false;
                        txtCoordenacao.ReadOnly = false;
                        ddlSituacaoNR.Enabled = true;
                        txtCertificacao.ReadOnly = false;
                        txtRevalidacao.ReadOnly = false;
                    }
                    else
                    {
                        ddlVeiculo.Enabled = false;
                        txtMatric.ReadOnly = false;
                        txtCoordenacao.ReadOnly = false;
                        ddlSituacaoNR.Enabled = true;
                        txtCertificacao.ReadOnly = false;
                        txtRevalidacao.ReadOnly = false;
                    }
                }
                else
                {
                    txtTempo.Text = string.Empty;
                    ddlVeiculo.Enabled = false;
                    txtMatric.ReadOnly = true;
                    txtCoordenacao.ReadOnly = true;
                    ddlSituacaoNR.Enabled = false;
                    txtCertificacao.ReadOnly = true;
                    txtRevalidacao.ReadOnly = true;
                }

                if (txtCertificacao.Text != null && txtCertificacao.Text != "" || txtRevalidacao.Text != null && txtRevalidacao.Text != "")
                    RegraDataValidacao();
            }
            catch (Exception ex)
            {
                divDanger.Visible = true;
                lblErro.Text = ex.Message;
            }
        }

        protected void txtMatric_TextChanged(object sender, EventArgs e)
        {
            try
            {
                OcultarDIV();
                int Matricula = Int32.Parse(txtMatric.Text);
                ColaboradorDAL cDAL = new ColaboradorDAL();
                Colaborador c = cDAL.FindByMatricula(Matricula);
                if (c == null)
                {
                    divWarning.Visible = true;
                    lblAviso.Text = "Matricula não encontrada no efetivo.";
                    return;
                }
                else
                {
                    txtNome.Text = c.Nome;
                    txtCoordenacao.Text = c.Coordenacao;
                    txtSituacao.Text = c.Situacao;
                }
            }
            catch (Exception ex)
            {
                divDanger.Visible = true;
                lblErro.Text = ex.Message;
            }
        }

        protected void txtCertificacao_TextChanged(object sender, EventArgs e)
        {
            OcultarDIV();
            if (txtRevalidacao.Text != null && txtRevalidacao.Text != "" && txtCertificacao.Text != "")
            {
                DateTime Certificacao = DateTime.Parse(txtCertificacao.Text);
                DateTime Revalidacao = DateTime.Parse(txtRevalidacao.Text);
                if (Certificacao >= Revalidacao)
                    divWarning.Visible = true;
                lblAviso.Text = "A data de certificação não pode ser maior que a data de revalidação.";
            }
            else if (txtCertificacao.Text == null || txtCertificacao.Text == "")
            {
                if (txtRevalidacao.Text == null || txtRevalidacao.Text == "")
                    txtVigencia.Text = string.Empty;
            }
            else
            {
                RegraDataValidacao();
            }
        }

        protected void txtRevalidacao_TextChanged(object sender, EventArgs e)
        {
            OcultarDIV();
            if (txtRevalidacao.Text == null || txtRevalidacao.Text == "")
            {
                if (txtCertificacao.Text == null || txtCertificacao.Text == "")
                    txtVigencia.Text = string.Empty;
                else
                    RegraDataValidacao();
            }
            else
            {
                RegraDataValidacao();
            }
        }

        protected void gridColaborador_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            OcultarDIV();
            if (pesquisarmatricula.Text == "")
            {
                CarregarGrid(e.NewPageIndex);
            }
            else
            {
                CarregarGrid(e.NewPageIndex);
            }
        }

        protected void gridColaborador_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Editar":
                    Response.Redirect("DetalharColab.aspx?Id=" + e.CommandArgument.ToString());
                    break;
                case "Excluir":
                    ExcluirItem(Convert.ToInt32(e.CommandArgument));
                    break;
            }
        }

        #endregion

        #region "Metodos"

        private void IniciarPagina()
        {
            CarregarCombo();
            CarregarGrid(0);
            ConfigurarPerfilUsuario();
        }

        private void CarregarCombo()
        {
            try
            {
                ColaboradorDAL cDAL = new ColaboradorDAL();
                //Listar DropDownList Normativos
                ddlNR.DataSource = cDAL.ListaNormativos();
                ddlNR.DataTextField = "Normativo";
                ddlNR.DataBind();
                ddlNR.Items.Insert(0, "Selecione");
                ddlNR.SelectedIndex = 0;

                //Listar DropDownList Veiculos
                ddlVeiculo.DataSource = cDAL.ListaVeiculo();
                ddlVeiculo.DataTextField = "Descricao";
                ddlVeiculo.DataValueField = "Descricao";
                ddlVeiculo.DataBind();
                ddlVeiculo.Items.Insert(0, "Selecione");
                ddlVeiculo.SelectedIndex = 0;

                //Listar DropDownList SituacaoNR
                ddlSituacaoNR.DataSource = cDAL.ListaSituacaoNR();
                ddlSituacaoNR.DataTextField = "Descricao";
                ddlSituacaoNR.DataValueField = "Descricao";
                ddlSituacaoNR.DataBind();
                ddlSituacaoNR.Items.Insert(0, "Selecione");
                ddlSituacaoNR.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                divDanger.Visible = true;
                lblErro.Text = ex.Message;
            }
        }

        private void CarregarGrid(int pPageIndex)
        {
            try
            {
                ColaboradorDAL cDAL = new ColaboradorDAL();

                if (pesquisarmatricula.Text != "")
                {
                    if (Index == 0 && pPageIndex != 0)
                        Index = pPageIndex;
                    else if (Index != 0 && pPageIndex != 0)
                        Index = pPageIndex;
                    else
                        Index = 0;

                    if (!string.IsNullOrEmpty(pesquisarmatricula.Text))
                        matricula = Int32.Parse(pesquisarmatricula.Text);
                }

                if (matricula != 0)
                {
                    gridColaborador.DataSource = cDAL.FindColab(matricula);
                }
                else
                {
                    if (Index == 0 && pPageIndex != 0)
                        Index = pPageIndex;
                    else if (Index != 0 && pPageIndex != 0)
                        Index = pPageIndex;
                    else
                        Index = 0;
                    gridColaborador.DataSource = cDAL.FindAll();
                }

                gridColaborador.PageIndex = Index;
                gridColaborador.DataBind();
            }
            catch (Exception ex)
            {
                divDanger.Visible = true;
                lblErro.Text = ex.Message;
            }
        }

        private void LimparCampos()
        {
            Index = 0;
            matricula = 0;
            dataCertificacao = null;
            ddlNR.SelectedIndex = 0;
            txtMatric.Text = string.Empty;
            txtMatric.ReadOnly = true;
            txtNome.Text = string.Empty;
            txtCoordenacao.Text = string.Empty;
            txtCoordenacao.ReadOnly = true;
            txtSituacao.Text = string.Empty;
            ddlVeiculo.SelectedIndex = 0;
            txtCertificacao.Text = string.Empty;
            txtCertificacao.ReadOnly = true;
            txtRevalidacao.Text = string.Empty;
            txtRevalidacao.ReadOnly = true;
            txtVigencia.Text = string.Empty;
            ddlSituacaoNR.SelectedIndex = 0;
            ddlSituacaoNR.Enabled = false;
            pesquisarmatricula.Text = string.Empty;

            CarregarGrid(0);
        }

        private void OcultarDIV()
        {
            divSuccess.Visible = false;
            divWarning.Visible = false;
            divDanger.Visible = false;
        }

        private void RegraDataValidacao()
        {
            try
            {
                OcultarDIV();
                if (txtRevalidacao.Text == null || txtRevalidacao.Text == "")
                {
                    String d1 = txtCertificacao.Text;
                    DateTime d = DateTime.Parse(d1);
                    int CurrentYear = DateTime.Parse(txtCertificacao.Text).Year;
                    int Vigencia = Convert.ToInt32(txtTempo.Text);
                    var SomaVigencia = Convert.ToDecimal(CurrentYear + Vigencia);
                    txtVigencia.Text = d.ToString("dd/MM/") + SomaVigencia.ToString();
                }
                else
                {
                    String d1 = txtRevalidacao.Text;
                    DateTime d = DateTime.Parse(d1);
                    int CurrentYear = DateTime.Parse(txtRevalidacao.Text).Year;
                    int Vigencia = Convert.ToInt32(txtTempo.Text);
                    var SomaVigencia = Convert.ToDecimal(CurrentYear + Vigencia);
                    txtVigencia.Text = d.ToString("dd/MM/") + SomaVigencia.ToString();
                }
            }
            catch (Exception ex)
            {
                divDanger.Visible = true;
                lblErro.Text = ex.Message;
            }
        }

        private void ExcluirItem(int pColaborador)
        {
            ColaboradorDAL cDAL = new ColaboradorDAL();
            cDAL.Delete(pColaborador);

            divSuccess.Visible = true;
            lblSucesso.Text = "Operação realizada com sucesso.";

            //Recarregar o gridview
            gridColaborador.DataSource = cDAL.FindAll();
            gridColaborador.DataBind();
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

        #endregion        
    }
}