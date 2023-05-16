using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalNR.DAL.DA
{
    public class UsuarioAD
    {
        private String nomeCompleto;

        public String NomeCompleto
        {
            get { return nomeCompleto; }
            set { nomeCompleto = value; }
        }

        private String login;

        public String Login
        {
            get { return login; }
            set { login = value; }
        }

        private String email;

        public String Email
        {
            get { return email; }
            set { email = value; }
        }
    }
}
