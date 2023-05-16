using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalNR.DAL.Entities
{
    public class Turmas : Normativos
    {
        public int IdTurma { get; set; }
        public DateTime dtTurma { get; set; }
        public DateTime dtTurmaFim { get; set; }
        public int Carga { get; set; }
        public string Local { get; set; }
        public int Vagas { get; set; }
        public int vagasDisponiveis { get; set; }
        public string Palestrante { get; set; }
        public string Observacao { get; set; }
        public int IdNR { get; set; }
        public int IdTema { get; set; }
        public string Veiculo { get; set; }
        public bool Ativo { get; set; }

        #region Relacionamento        
        public virtual Normativos normativoRel { get; set; }
        public virtual TurmasTema TemaRelac { get; set; }
        public virtual Veiculo VeiculoRel { get; set; }

        public virtual ICollection<TurmasParticpantes> tpCollection { get; set; }        
        #endregion
    }
}
