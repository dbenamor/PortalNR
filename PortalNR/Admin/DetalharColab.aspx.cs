using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortalNR.DAL.Entities;
using PortalNR.DAL.Persistence;
using System.Globalization;
using PortalNR.Code;

namespace PortalNR.Admin
{
    public partial class DetalharColab : PageBaseLocal
    {
        #region "Sesssion"
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
                try
                {
                    IniciarPagina();

                    int Id = Int32.Parse(Request.QueryString["Id"]);
                    ColaboradorDAL cDAL = new ColaboradorDAL();
                    Colaborador c = cDAL.FindById(Id);

                    txtId.Value = c.ID.ToString();
                    ddlNR.SelectedValue = c.Normativo;
                    if (ddlNR.SelectedValue == "NR-11") 
                    {
                        ddlVeiculo.SelectedValue = c.Veiculo;
                        ddlVeiculo.Enabled = true;
                    }
                    else
                    {
                        ddlVeiculo.SelectedValue = c.Veiculo;
                    }                      
                    ddlSituacaoNR.SelectedValue = c.Situacao;
                    txtSituacao.Text = c.SituacaoEfetivo;
                    txtMatric.Text = c.Matricula.ToString();
                    txtNome.Text = c.Nome;
                    txtCoordenacao.Text = c.Coordenacao;
                    chkElegivel.Checked = c.Elegivel;
                    if (c.DtCertificacao.HasValue)
                        txtCertificacao.Text = c.DtCertificacao.Value.ToString("yyyy-MM-dd");
                    if (c.DtRevalidacao.HasValue)
                        txtRevalidacao.Text = c.DtRevalidacao.Value.ToString("yyyy-MM-dd");
                    if (c.DtVigencia.HasValue)
                        txtVigencia.Text = c.DtVigencia.Value.ToString("dd/MM/yyyy");
                }
                catch (Exception ex)
                {
                    divDanger.Visible = true;
                    lblErro.Text = ex.Message;
                }
            }
        }

        protected void btnAtualizar_Click(object sender, EventArgs e)
        {
            try
            {
                Colaborador c = new Colaborador();

                c.ID = Int32.Parse(txtId.Value);
                c.Normativo = ddlNR.SelectedValue;
                c.Elegivel = chkElegivel.Checked;
                c.Coordenacao = txtCoordenacao.Text;
                //c.Veiculo = ddlVeiculo.SelectedValue;
                c.Veiculo = ddlVeiculo.SelectedItem.ToString() == "Selecione" ? "" : ddlVeiculo.SelectedItem.ToString();
                c.Situacao = ddlSituacaoNR.SelectedValue;
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

                ColaboradorDAL cDAL = new ColaboradorDAL();
                cDAL.Update(c);

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

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("CadastrarColab.aspx");
        }

        protected void ddlNR_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string Normativo = ddlNR.SelectedValue;
                NormativosDAL nDAL = new NormativosDAL();
                Normativos n = nDAL.FindByNormativo(Normativo);

                //Preenche o campo txtTempo com tempo de vigência do normativo
                txtTempo.Text = n.Vigencia.ToString();

                if (ddlNR.SelectedValue == "NR-11")
                {                    
                    ddlVeiculo.Enabled = true;
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
        #endregion

        #region "Metodos"
        private void IniciarPagina()
        {
            CarregarCombos();
            ConfigurarPerfilUsuario();
        }

        private void CarregarCombos()
        {
            try
            {
                ColaboradorDAL cDAL = new ColaboradorDAL();
                //Listar DropDownList Normativos
                ddlNR.DataSource = cDAL.ListaNormativos();
                ddlNR.DataTextField = "Normativo";
                ddlNR.DataValueField = "Normativo";
                ddlNR.DataBind();
                ddlNR.Items.Insert(0, new ListItem("Selecione"));
                ddlNR.SelectedIndex = 0;

                //Listar DropDownList Veiculos
                ddlVeiculo.DataSource = cDAL.ListaVeiculo();
                ddlVeiculo.DataTextField = "Descricao";
                ddlVeiculo.DataValueField = "Descricao";
                ddlVeiculo.DataBind();
                ddlVeiculo.Items.Insert(0, new ListItem("Selecione"));
                ddlVeiculo.SelectedIndex = 0;

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
            dataCertificacao = null;
            ddlNR.SelectedIndex = 0;
            txtMatric.Text = string.Empty;
            txtNome.Text = string.Empty;
            txtCoordenacao.Text = string.Empty;
            //txtSituacao.Text = string.Empty;
            ddlVeiculo.SelectedIndex = 0;
            txtCertificacao.Text = string.Empty;
            txtRevalidacao.Text = string.Empty;
            txtVigencia.Text = string.Empty;
            chkElegivel.Checked = false;
            ddlSituacaoNR.SelectedIndex = 0;
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
                string Normativo = ddlNR.SelectedValue;
                NormativosDAL nDAL = new NormativosDAL();
                Normativos n = nDAL.FindByNormativo(Normativo);                
                txtTempo.Text = n.Vigencia.ToString();

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
        #endregion
    }
}