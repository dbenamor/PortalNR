using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalNR.DAL.Entities
{
    public class Veiculo
    {
        public int IdVeiculo { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }

        public virtual ICollection<Colaborador> cCollection { get; set; }
        public virtual ICollection<Turmas> tCollection { get; set; }
    }
}
