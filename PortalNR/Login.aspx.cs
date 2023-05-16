using System;
using System.Web;
using System.Linq;
using PortalNR.DAL.DA;
using PortalNR.Security;
using System.Web.Security;
using System.Configuration;
using PortalNR.DAL.Entities;
using PortalNR.DAL.Persistence;
using System.Web.UI;
using PortalNR.Code;

namespace PortalNR
{
    public partial class Login : PageBaseLocal
    {
        #region "Eventos"

        protected override void OnInitComplete(EventArgs e)
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            PaginaLogin = "~/Login.aspx";

            if (!IsPostBack)
                IniciarPagina();
        }

        protected void btnAcesso_Click(object sender, EventArgs e)
        {
            string habilitarLoginAD = ConfigurationManager.AppSettings["HABILITAR_LOGIN_AD"];
            string _login = txtLogin.Text;
            string _senha = txtSenha.Text;

            if (string.IsNullOrEmpty(_login) || string.IsNullOrEmpty(_senha))
            {
                lblMensagem.Text = "Usuário e/ou senha Obrigatórios.";
                return;
            }
            else if (habilitarLoginAD != "S")
            {
                UsuarioDAL uDAL = new UsuarioDAL();
                PerfilDAL pDAL = new PerfilDAL();
                Usuario u = uDAL.FindUsu(_login, Criptografia.EncriptarMD5(_senha));
                if (u != null)
                {
                    UsuarioLocalLogado = u;
                    IsUsuarioAutenticado = true;

                    if (UsuarioLocalLogado != null)
                    {
                        foreach (var item in pDAL.ListaPerfil().Where(item => item.Id == u.PerfilRel.Id))
                        {
                            if (u.PerfilRel.Id == item.Id)
                            {
                                if (u.Ativo == false)
                                {
                                    lblMensagem.Text = "Usuário sem permissão de acesso.";
                                    return;
                                }
                                else
                                {                                   
                                    PerfilUsuarioLogado = item;
                                    FormsAuthenticationTicket(u);
                                }
                            }
                            else
                            {
                                lblMensagem.Text = "Usuário sem perfil no sistema.";
                                return;
                            }
                        }
                    }
                }
                else
                {
                    lblMensagem.Text = "Usuário e/ou senha inválidos.";
                    return;
                }
            }
            else
            {
                ValidarUsuario(_login, _senha);
            }
        }

        #endregion

        #region "Metodos"

        private void IniciarPagina()
        {
            HttpCookie cookie = Request.Cookies["_UsuarioLocalLogado"];
        }

        private void ValidarUsuario(String login, String senha)
        {
            //Valida Usuário no AD
            string caminhoDominioAD = ConfigurationManager.AppSettings["CAMINHO_AD"].ToString();
            VerificaUsuarioAD ad = new VerificaUsuarioAD(caminhoDominioAD);
            UsuarioAD usuarioAD = null;

            try
            {
                usuarioAD = ad.retornarUsuarioAD(login, senha, login);

                if (usuarioAD == null)
                {
                    lblMensagem.Text = "Usuário e/ou senha não conferem.";
                    return;
                }
            }
            catch (Exception ex)
            {
                lblMensagem.Text = ex.Message;
                return;
            }

            UsuarioDAL uDAL = new UsuarioDAL();
            Usuario usuario = uDAL.FindLogin(txtLogin.Text);            

            IsUsuarioAutenticado = true;
            UsuarioLocalLogado = usuario;                        

            FormsAuthenticationTicketAD(usuarioAD);
        }

        private void FormsAuthenticationTicket(Usuario usuario)
        {
            int timeout = Convert.ToInt32(ConfigurationManager.AppSettings["TIMEOUT"].ToString());

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, usuario.Login, DateTime.Now, DateTime.Now.AddMinutes(timeout), false, FormsAuthentication.FormsCookiePath);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            cookie.Expires = DateTime.Now.AddMinutes(timeout);
            Response.Cookies.Add(cookie);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
            Response.Redirect("~/Pages/Default.aspx");
        }

        private void FormsAuthenticationTicketAD(UsuarioAD usuarioAD)
        {
            int timeout = Convert.ToInt32(ConfigurationManager.AppSettings["TIMEOUT"].ToString());

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, usuarioAD.Login, DateTime.Now, DateTime.Now.AddMinutes(timeout), false, FormsAuthentication.FormsCookiePath);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            cookie.Expires = DateTime.Now.AddMinutes(timeout);
            Response.Cookies.Add(cookie);
            Response.Redirect("~/Pages/Default.aspx");
        }

        #endregion
    }
}