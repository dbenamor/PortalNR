using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalNR.DAL.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string NmUsuario { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime DtCadastro { get; set; }
        public DateTime DtDesativacao { get; set; }
        public int IdPerfil { get; set; }
        public bool PrimeiroAcesso { get; set; }
        public bool Ativo { get; set; }

        #region Relacionamento Perfil
        public virtual Perfil PerfilRel { get; set; }
        #endregion
    }
}
