using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortalNR.Security;
using PortalNR.DAL.DA;
using System.Configuration;
using PortalNR.DAL.Entities;
using System;
using System.Web.Util;

namespace PortalNR.DAL
{
    public class ControleDeAcesso
    {
        public Usuario UsuarioLocalLogado
        {
            get
            {
                if (HttpContext.Current.Session["_UsuarioLocalLogado"] == null) return null;
                return (Usuario)HttpContext.Current.Session["_UsuarioLocalLogado"];
            }
            set
            {
                HttpContext.Current.Session["_UsuarioLocalLogado"] = value;
            }
        }
    }
}
