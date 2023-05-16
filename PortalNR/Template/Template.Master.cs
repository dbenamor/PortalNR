using PortalNR.Code;
using PortalNR.DAL.Persistence;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PortalNR.Template
{
    public partial class Template : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (HttpContext.Current.User.Identity.Name != "")
                {                    
                    lblUsuario.Text = "<i class='fa fa-fw fa-user'></i>&nbsp;Bem vindo,&nbsp;<span class='caret'></span>" + HttpContext.Current.User.Identity.Name;

                    TratarPermissaoPagina();
                    ValidarPerfilUsuarioLogado();
                }
            }
        }
     
        private void TratarPermissaoPagina()
        {
            //System.Web.UI.Page pagina = this.Page as System.Web.UI.Page;
            PageBaseLocal pagina = this.Page as PageBaseLocal;

            if (pagina != null)
            {
                if (pagina.IsUsuarioAutenticado)
                {
                    List<HtmlAnchor> itens = new List<HtmlAnchor>();
                    //Home
                    itens.Add(a100);
                    //Consultar NR
                    itens.Add(a200);
                    itens.Add(a201);
                    itens.Add(a202);
                    itens.Add(a203);
                    itens.Add(a204);
                    itens.Add(a205);
                    //Calendário das turmas
                    itens.Add(a300);
                    itens.Add(a301);
                    //Relatórios
                    itens.Add(a400);
                    itens.Add(a401);
                    itens.Add(a402);
                    itens.Add(a403);
                    itens.Add(a404);
                    //Administração
                    itens.Add(a500);
                    itens.Add(a501);
                    itens.Add(a502);
                    itens.Add(a502a);
                    itens.Add(a502b);
                    itens.Add(a503);
                    //itens.Add(a504); //Menu cadastro de coordenação
                    itens.Add(a505);
                    itens.Add(a506);
                    itens.Add(a507);
                    //Sair
                    itens.Add(a600);

                    var paginaSolicitadaVerficada = pagina.Page.AppRelativeVirtualPath;
                    var paginaLinkSolicitado = itens.SingleOrDefault(item => item.HRef.ToUpper() == paginaSolicitadaVerficada.ToUpper());

                    if (paginaLinkSolicitado != null && paginaLinkSolicitado.Visible == false)
                        Response.Redirect("~/Login.aspx");
                }
            }
        }

        private void ValidarPerfilUsuarioLogado()
        {
            //System.Web.UI.Page pagina = this.Page as System.Web.UI.Page;
            PageBaseLocal pagina = this.Page as PageBaseLocal;

            if (pagina != null && pagina.IsUsuarioAutenticado)
            {
                UsuarioDAL d = new UsuarioDAL();

                var query = d.FindLogin(HttpContext.Current.User.Identity.Name);

                if (query != null)
                {
                    if (query.PerfilRel.Id == 1)
                    {
                        a100.Visible = true;
                        a200.Visible = a201.Visible = a202.Visible = a203.Visible = a204.Visible = a205.Visible = true;
                        a300.Visible = a301.Visible = true;
                        a400.Visible = a401.Visible = a402.Visible = a403.Visible = a404.Visible = true;
                        a500.Visible = a501.Visible = a502.Visible = a502a.Visible = a502b.Visible = a503.Visible = a505.Visible = a506.Visible = a507.Visible = true;
                        a600.Visible = true;
                    }
                    else if (query.PerfilRel.Id == 2)
                    {
                        a100.Visible = true;
                        a200.Visible = a201.Visible = a202.Visible = a203.Visible = a204.Visible = a205.Visible = true;
                        a300.Visible = a301.Visible = true;
                        a400.Visible = a401.Visible = a402.Visible = a403.Visible = true;
                        a600.Visible = true;
                    }
                }
                else
                    Response.Redirect("~/Login.aspx");
            }
            else
                Response.Redirect("~/Login.aspx");
        }

        protected void lknSair_Click(object sender, EventArgs e)
        {
            //System.Web.UI.Page page = this.Page as System.Web.UI.Page;
            PageBaseLocal page = this.Page as PageBaseLocal;

            if (page != null)
            {    
                Response.Redirect("~/Logout.aspx");
            }
        }      
    }
}