using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalNR.DAL.DA
{
    public class VerificaUsuarioAD
    {
        private String caminhoDominio = ConfigurationManager.AppSettings["CAMINHO_AD"].ToString();

        public VerificaUsuarioAD(String caminhoDominio)
        {
            this.caminhoDominio = caminhoDominio;
        }

        public bool VerificaLoginAD(string login, string senha)
        {
            string userName = login;
            DirectorySearcher search = new DirectorySearcher();
            search.Filter = String.Format("(SAMAccountName={0})", login);
            search.PropertiesToLoad.Add("cn");
            SearchResult result = search.FindOne();

            if (result == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public UsuarioAD RetornarUsuarioAD(String login, String senha)
        {
            DirectoryEntry entry;
            DirectorySearcher searcher;
            SearchResult result;
            try
            {
                entry = new DirectoryEntry(caminhoDominio, login, senha);

                searcher = new DirectorySearcher(entry);
                searcher.Filter = String.Format("(SAMAccountName={0})", login);
                searcher.PropertiesToLoad.Add("cn");
                result = searcher.FindOne();

                if (result == null || string.IsNullOrEmpty(result.Path))
                {
                    return null;
                }
                else
                {
                    UsuarioAD ad = new UsuarioAD();
                    ad.Login = login;
                    ad.NomeCompleto = result.Properties["cn"][0].ToString();
                    return ad;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                entry = null;
                searcher = null;
                result = null;
            }
        }

        public UsuarioAD retornarUsuarioAD(string login, string senha, string usuarioParaBuscar)
        {
            DirectoryEntry entry;
            DirectorySearcher searcher;
            SearchResult result;
            UsuarioAD usuarioAD = new UsuarioAD();

            try
            {
                entry = new DirectoryEntry(caminhoDominio, login, senha);
                searcher = new DirectorySearcher(entry);

                searcher.SearchScope = SearchScope.Subtree;

                searcher.Filter = "(SAMAccountName=" + usuarioParaBuscar + ")";
                searcher.PropertiesToLoad.Add("cn");
                result = searcher.FindOne();

                if (result == null)
                    return null;

                if (!result.Properties.Contains("cn"))
                    throw new Exception("Erro ao obter o Nome de Exibição do usuário no AD " + usuarioParaBuscar);

                usuarioAD.Login = usuarioParaBuscar.ToLower().Trim();
                usuarioAD.NomeCompleto = result.Properties["cn"][0].ToString();

                entry.Dispose();
                searcher.Dispose();
                result = null;

                return usuarioAD;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao logar no Domínio. Provavelmente os dados do login estão incorretos.", ex);
            }
            finally
            {
                entry = null;
                searcher = null;
                result = null;
            }
        }
    }
}
