using Microsoft.Reporting.WebForms;
using PortalNR.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

namespace PortalNR.Code
{
    public class PageBaseLocal : System.Web.UI.Page
    {
        public virtual ScriptManager ScriptManagerMain
        {
            get
            {
                return ScriptManager.GetCurrent(this);
            }
        }

        public String PaginaLogin
        {
            get
            {
                if (Session["_PaginaLogin"] == null) return null;
                return Session["_PaginaLogin"].ToString();
            }
            set
            {
                Session["_PaginaLogin"] = value;
            }
        }

        public Perfil PerfilUsuarioLogado
        {
            get
            {
                if (Session["_PerfilUsuarioLogado"] == null) return null;
                return (Perfil)Session["_PerfilUsuarioLogado"];
            }

            set
            {
                Session["_PerfilUsuarioLogado"] = value;
            }
        }

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

        public new bool IsUsuarioAutenticado
        {
            get
            {
                if (Session["_IsUsuarioAutenticado"] == null) Session["_IsUsuarioAutenticado"] = false;
                return Convert.ToBoolean(Session["_IsUsuarioAutenticado"]);
            }
            set
            {
                Session["_IsUsuarioAutenticado"] = value;
            }
        }

        protected override void OnInitComplete(EventArgs e)
        {
            if (!IsUsuarioAutenticado)
                if (!string.IsNullOrEmpty(PaginaLogin))
                    Response.Redirect(PaginaLogin);
                else
                    Response.Redirect("~/Login.aspx");
        }

        public static string TratarMensagem(string msg)
        {
            if (msg == null)
                msg = String.Empty;

            msg = msg.Replace("'", "");
            msg = msg.Replace(Convert.ToChar(13).ToString(), " ");
            msg = msg.Replace(Convert.ToChar(10).ToString(), " ");

            return msg;
        }

        public static string TratarMensagem(IList<string> msgs, string enter)
        {
            StringBuilder retorno = new StringBuilder();
            foreach (string msg in msgs)
            {
                retorno.AppendFormat(String.Format("{0}{1}", TratarMensagem(msg), enter));
            }

            return retorno.ToString();
        }

        public virtual void AlertaRedirecionar(string msg, string url)
        {
            if (ScriptManagerMain != null)
                ScriptManager.RegisterStartupScript(this, this.GetType(), String.Concat("msg", this.ClientID), string.Format("<script>alert('{0}'); window.location.href = \"{1}\";</script>", TratarMensagem(msg), url), false);
            else
                this.ClientScript.RegisterStartupScript(typeof(string), String.Concat("msg", this.ClientID), string.Format("<script>alert('{0}'); window.location.href = \"{1}\";</script>", TratarMensagem(msg), url));
        }

        public new List<ReportParameter> ReportParameters
        {
            get
            {
                if (Session["_ReportParameter"] == null) Session["_ReportParameter"] = new List<ReportParameter>();
                return (List<ReportParameter>)Session["_ReportParameter"];
            }
            set
            {
                Session["_ReportParameter"] = value;
            }
        }
    }
}