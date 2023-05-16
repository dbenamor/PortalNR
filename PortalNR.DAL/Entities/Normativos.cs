using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalNR.DAL.Entities
{
    public class Normativos
    {
        public int IdNormativo { get; set; }
        public string Normativo { get; set; }
        public int Vigencia { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }

        #region Relacionamento Turmas
        public virtual ICollection<Turmas> tCollection { get; set; }
        public virtual ICollection<Colaborador> cCollection { get; set; }
        #endregion
    }
}
