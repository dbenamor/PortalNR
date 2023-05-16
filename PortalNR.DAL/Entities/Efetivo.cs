using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalNR.DAL.Entities
{
    public class Efetivo
    {
        public int Codigo { get; set; }
        public int Codemp { get; set; }
        public string Empresa { get; set; }
        public int Matricula { get; set; }
        public string Funcionario { get; set; }
        public string Cargo { get; set; }
        public string Coordenacao { get; set; }
        public string Gerencia { get; set; }
        public string CentroCusto { get; set; }
        public string DescricaoCC { get; set; }
        public string Situacao { get; set; }
        public string CodGH { get; set; }        
        public string DescGH { get; set; }
        public DateTime DtSituacao { get; set; }
        public DateTime DtAdmissao { get; set; }  
        public DateTime DtAtulizado { get; set; }
    }
}
