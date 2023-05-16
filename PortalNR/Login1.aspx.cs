using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortalNR.DAL.Entities;
using PortalNR.DAL.Persistence;
using PortalNR.Security;
using PortalNR.DAL.DA;
using PortalNR.DAL;
using System.Web.Security;
using System.Configuration;

namespace PortalNR
{
    public partial class Login : System.Web.UI.Page
    {

        public Usuario UsuarioLocalLogado
        {
            get
            {
                if (Session["_UsuarioLocalLogado"] == null) return null;
                return (Usuario)Session["_UsuarioLocalLogado"];
            }
            set
            {
                Session["_UsuarioLocalLogado"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["_UsuarioLocalLogado"];
        }

        protected void btnAcesso_Click(object sender, EventArgs e)
        {
            string _login = txtLogin.Text;
            string _senha = txtSenha.Text;
            string habilitarLoginAD = ConfigurationManager.AppSettings["HABILITAR_LOGIN_AD"];

            if (string.IsNullOrEmpty(_login) || string.IsNullOrEmpty(_senha))
            {
                lblMensagem.Text = "Usuário e/ou senha Obrigatórios.";
            }
            else if (habilitarLoginAD != "S")
            {
                UsuarioDAL d = new UsuarioDAL();
                Usuario u = d.FindUsu(_login, Criptografia.EncriptarMD5(_senha));
                if (u != null)
                {
                    PerfilDAL pDal = new PerfilDAL();

                    UsuarioLocalLogado = u;

                    if (UsuarioLocalLogado != null)
                    {
                        foreach (var item in pDal.ListaPerfil().Where(item => item.Id == u.PerfilRel.Id))
                        {
                            if (u.PerfilRel.Id == item.Id)
                            {
                                if (u.Ativo == false)
                                {
                                    lblMensagem.Text = "Usuário sem permissão de acesso.";
                                }
                                else
                                {
                                    int timeout = Convert.ToInt32(ConfigurationManager.AppSettings["TIMEOUT"].ToString());

                                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, u.Login, DateTime.Now, DateTime.Now.AddMinutes(15), false, FormsAuthentication.FormsCookiePath);
                                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
                                    cookie.Expires = DateTime.Now.AddMinutes(timeout);
                                    Response.Cookies.Add(cookie);
                                    Response.Redirect("/Pages/Default.aspx");
                                }
                            }
                            else
                            {
                                lblMensagem.Text = "Usuário sem perfil no sistema.";
                            }
                        }
                    }
                }
                else
                {
                    lblMensagem.Text = "Usuário e/ou senha inválidos.";
                }
            }
            else
            {
                ValidarUsuario(_login, _senha);
            }
        }
        private void ValidarUsuario(String login, String senha)
        {
            //Valida Usuário no AD
            string caminhoDominioAD = ConfigurationManager.AppSettings["CAMINHO_AD"].ToString();
            UsuarioAD adDTO = null;

            VerificaUsuarioAD ad = new VerificaUsuarioAD(caminhoDominioAD);
            adDTO = ad.RetornarUsuario(login, senha);

            if (adDTO == null)
                throw new Exception("Login incorreto ou não encontrado no AD.");
            else
                Response.Redirect("/Pages/Default.aspx");
        }
    }
}