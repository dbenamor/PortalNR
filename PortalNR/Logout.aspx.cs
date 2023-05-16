using PortalNR.Code;
using PortalNR.DAL.Persistence;
using System;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace PortalNR
{
    public partial class Logout : PageBaseLocal
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["_UsuarioLocalLogado"];
            IniciarPagina();
        }

        private void IniciarPagina()
        {
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

        protected void btnLeave_Click(object sender, EventArgs e)
        {
            PageBaseLocal page = this.Page as PageBaseLocal;

            if (page != null)
            {
                Session["_IsUsuarioAutenticado"] = null;
                Session["_IsUsuarioAutenticado"] = false;
                FormsAuthentication.SignOut();
                HttpCookie cookie = Request.Cookies["_UsuarioLocalLogado"];
                Session.Abandon();
                Response.Cookies.Clear();
                Response.Redirect(page.PaginaLogin);
            }
        }
    }
}