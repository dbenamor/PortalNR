using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalNR.DAL.Entities
{
    public class TurmasParticpantes : Turmas
    {
        public int IdParticipante { get; set; }
        public int Matricula { get; set; }
        public string Nome { get; set; }
        public string Coordenacao { get; set; }
        public bool Presenca { get; set; }

        #region Relacionamento
        public virtual Turmas turmaRel { get; set; }
        public virtual TurmasTema temaRel { get; set; }
        #endregion
        
    }
}
