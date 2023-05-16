using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalNR.DAL.Entities
{
    public class Colaborador
    {
        public int ID { get; set; }
        public string Normativo { get; set; }
        public int Matricula { get; set; }
        public string Nome { get; set; }
        public string Coordenacao { get; set; }
        public string Situacao { get; set; }
        public string Veiculo { get; set; }
        public DateTime? DtCertificacao { get; set; }
        public DateTime? DtRevalidacao { get; set; }
        public DateTime? DtVigencia { get; set; }
        public int? Vencimento { get; set; }
        public string StatusNR { get; set; }
        public bool Elegivel { get; set; }

        public string SituacaoEfetivo { get; set; }

        public virtual Normativos NormativoRel { get; set; }
        public virtual SituacaoNR SituacaoRel { get; set; }
        public virtual Veiculo VeiculoRel { get; set; }
    }
}
