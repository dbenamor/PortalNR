using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PortalNR.Security
{
    public class Criptografia
    {
        public static string EncriptarMD5(string senha)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(senha));

            return BitConverter.ToString(hash).Replace("-", string.Empty);
        }
    }
}
