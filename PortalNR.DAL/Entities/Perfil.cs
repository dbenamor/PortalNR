using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalNR.DAL.Entities
{
    public class Perfil
    {
        public int Id { get; set; }
        public string Descricao { get; set;}
        public bool Ativo { get; set; }

        #region Relacionamento Usuario
        public virtual ICollection<Usuario> Usuario { get; set; }
        #endregion
    }
}
