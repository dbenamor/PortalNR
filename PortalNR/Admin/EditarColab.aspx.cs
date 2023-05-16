using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortalNR.DAL.Entities;
using PortalNR.DAL.Persistence;

namespace PortalNR.Admin
{
    public partial class EditarColab : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           if (!IsPostBack)
           {                
                IniciarPagina();
                CarregarGrid();
            }
        }
        
        private void IniciarPagina()
        {
            
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
                    gridColaborador.DataSource = cDAL.FindColabEdit(Matricula, Funcionario, Coordenacao);
                }
                else if (txtMatricula.Text == "" && txtColaborador.Text != "" && txtCoordenacao.Text == "")
                {
                    if (!string.IsNullOrEmpty(txtColaborador.Text))
                        gridColaborador.DataSource = cDAL.FindColabEdit(Matricula, Funcionario, Coordenacao);
                }
                else if (txtMatricula.Text == "" && txtColaborador.Text == "" && txtCoordenacao.Text != "")
                {
                    if (!string.IsNullOrEmpty(txtCoordenacao.Text))
                        gridColaborador.DataSource = cDAL.FindColabEdit(Matricula, Funcionario, Coordenacao);
                }
                else if (txtMatricula.Text == "" && txtColaborador.Text != "" && txtCoordenacao.Text != "")
                {
                    if (!string.IsNullOrEmpty(txtColaborador.Text) && !string.IsNullOrEmpty(txtCoordenacao.Text))
                        gridColaborador.DataSource = cDAL.FindColabEdit(Matricula, Funcionario, Coordenacao);
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

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("CadastrarColab.aspx");
        }
        
        private void CarregarGrid()
        {
            try
            {
                ColaboradorDAL cDAL = new ColaboradorDAL();
                gridColaborador.DataSource = cDAL.FindAll();
                gridColaborador.DataBind();
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
            txtColaborador.Text = string.Empty;
            txtCoordenacao.Text = string.Empty;
            Response.Redirect(Request.RawUrl);
        }

        private void OcultarDIV()
        {
            divSuccess.Visible = false;
            divWarning.Visible = false;
            divDanger.Visible = false;
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

        protected void gridColaborador_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridColaborador.PageIndex = e.NewPageIndex;
            CarregarGrid();
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
    }
}