using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortalNR.DAL.Entities;
using PortalNR.DAL.Persistence;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace PortalNR.Admin
{
    public partial class ExportExcel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TurmasParticpantesDAL tpDAL = new TurmasParticpantesDAL();

            if (Request.QueryString["Id"] != null)
                ExibirRelatorio(tpDAL.ExportParticipantes(Convert.ToInt32(Request.QueryString["Id"])));
        }

        private void ExibirRelatorio(List<TurmasParticpantes> list)
        {
            var planilha = new GridView();
            planilha.DataSource = from x in list.ToList()
                                  select new
                                  {
                                      Normativo = x.Normativo,
                                      DataTurma = x.turmaRel.dtTurma,
                                      Palestrante = x.Palestrante,
                                      Tema = x.temaRel.Tema,
                                      Observação = x.turmaRel.Descricao,
                                      Matrícula = x.Matricula,
                                      Nome = x.Nome,
                                      Coordenação = x.Coordenacao,
                                      Presença = x.Presenca == true ? "SIM" : "NÃO"
                                  };
            planilha.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=ListaParticipantes_" + DateTime.Today.ToString("yyyyMMdd") + ".xls");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            planilha.RenderControl(htw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
}