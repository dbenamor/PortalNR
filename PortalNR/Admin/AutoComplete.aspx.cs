using AjaxControlToolkit;
using PortalNR.DAL.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PortalNR.Admin
{
    public partial class AutoComplete : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod]
        public static string[] getFuncionario(string prefixText, int count, string contextKey)
        {
            EmailcoordenacaoDAL edal = new EmailcoordenacaoDAL();

            if (string.IsNullOrWhiteSpace(prefixText.Trim()))
                return null;

            string[] retorno = (from query in edal.AutoCompleteCoordenacao(prefixText)
                                orderby (query.Coordenacao)
                                select AutoCompleteExtender.CreateAutoCompleteItem(query.Coordenacao, query.Id.ToString())).ToArray();

            if (retorno.Length > 0) return retorno;
            if (contextKey != "None")
                return new string[] { AutoCompleteExtender.CreateAutoCompleteItem("COORDENAÇÃO NÃO ENCONTRADA!", "-1") };

            return new string[] { AutoCompleteExtender.CreateAutoCompleteItem(prefixText, "-1") };
        }
    }
}