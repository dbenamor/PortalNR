using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalNR.DAL.Entities
{
    public class Emailcoordenacao
    {
        public int Id { get; set; }
        public string Coordenacao { get; set; }
        public string Responsavel { get; set; }
        public string email { get; set; }
        public bool Ativo { get; set; }
    }
}
