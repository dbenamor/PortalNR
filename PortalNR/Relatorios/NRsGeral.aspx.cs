using PortalNR.Code;
using PortalNR.DAL.Entities;
using PortalNR.DAL.Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PortalNR.Relatorios
{
    public partial class NRsGeral : PageBaseLocal
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                IniciarPagina();
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                ColaboradorDAL cDAL = new ColaboradorDAL();

                OcultarDIV();
                string Coordenacao = txtCoordenacao.Text;

                if (ddlNR.SelectedIndex != 0 && txtCoordenacao.Text == "")
                {
                    gridConsulta.DataSource = cDAL.FindGeralNR(ddlNR.SelectedItem.Value, txtCoordenacao.Text);
                    divCard.Visible = true;
                }
                else if (ddlNR.SelectedIndex == 0 && txtCoordenacao.Text != "")
                {
                    gridConsulta.DataSource = cDAL.FindGeralNR(ddlNR.SelectedItem.Value, txtCoordenacao.Text);
                    divCard.Visible = true;
                }
                else if (ddlNR.SelectedIndex != 0 && txtCoordenacao.Text != "")
                {
                    gridConsulta.DataSource = cDAL.FindGeralNR(ddlNR.SelectedItem.Value, txtCoordenacao.Text);
                    divCard.Visible = true;
                }
                else
                {
                    divWarning.Visible = true;
                    divCard.Visible = false;
                    lblAviso.Text = "Informe o normativo e coordenação.";
                    ddlNR.Focus();
                    ddlNR.SelectedIndex = 0;
                }
                gridConsulta.DataBind();
            }
            catch (Exception ex)
            {
                divDanger.Visible = true;
                lblErro.Text = ex.Message;
            }
        }

        protected void btnLimpar_Click(object sender, EventArgs e)
        {
            ddlNR.SelectedIndex = 0;
            txtCoordenacao.Text = string.Empty;
            divCard.Visible = false;
            gridConsulta.PageIndex = 0;
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            ColaboradorDAL cDAL = new ColaboradorDAL();
            ExibirRelatorio(cDAL.ExportColaborador(ddlNR.SelectedValue.ToString(), txtCoordenacao.Text));
        }

        protected void ExibirRelatorio(List<Colaborador> listaC)
        {
            var planilha = new GridView();
            planilha.DataSource = from x in listaC.ToList()
                                  select new
                                  {
                                      Normativo = x.Normativo,
                                      Matrícula = x.Matricula,
                                      Nome = x.Nome,
                                      Coordenação = x.Coordenacao,
                                      Situação = x.Situacao,
                                      DtCerticação = x.DtCertificacao,
                                      DtRevalidação = x.DtRevalidacao,
                                      DtVigência = x.DtVigencia,
                                      Status = x.StatusNR,
                                      Equipamento = x.Veiculo,
                                      Elegível = x.Elegivel == true ? "SIM" : "NÃO"
                                  };
            planilha.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=RelatorioNrGeral_" + DateTime.Today.ToString("yyyyMMdd") + ".xls");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            planilha.RenderControl(htw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

        protected void gridConsulta_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridConsulta.PageIndex = e.NewPageIndex;
            btnPesquisar_Click(null, null);
        }

        private void IniciarPagina()
        {
            CarregarCombo();
            ConfigurarPerfilUsuario();
        }

        private void CarregarCombo()
        {
            try
            {
                ColaboradorDAL cDAL = new ColaboradorDAL();

                ddlNR.DataSource = cDAL.ListaNormativos();
                ddlNR.DataValueField = "Normativo";
                ddlNR.DataTextField = "Normativo";
                ddlNR.DataBind();
                ddlNR.Items.Insert(0, new ListItem("Todos", ""));
                ddlNR.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                divDanger.Visible = true;
                lblErro.Text = "Errao ao carregar o campo: " + ex.Message;
            }
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

        private void OcultarDIV()
        {
            divDanger.Visible = false;
            divWarning.Visible = false;
        }
    }
}