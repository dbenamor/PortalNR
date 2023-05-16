using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalNR.DAL.Entities
{
    public class TurmasTema
    {
        public int IdTema { get; set; }
        public string Tema { get; set; }
        public bool Ativo { get; set; }

        #region Relacionamento
        public virtual TurmasParticpantes tpRelac { get; set; }
        public virtual ICollection<Turmas> tCollection { get; set; }
        #endregion
        
    }
}
