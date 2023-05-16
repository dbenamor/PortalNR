using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using PortalNR.DAL.Entities;

namespace PortalNR.DAL.Persistence
{
    public class PerfilDAL : Conexao
    {
        public List<Perfil> ListaPerfil()
        {
            try
            {
                OpenConnection();
                Cmd = new SqlCommand("SELECT * FROM [TB_PERFIL] WHERE [ATIVO] = 1 ORDER BY [DESCRICAO]", Con);
                Dr = Cmd.ExecuteReader();

                List<Perfil> listaP = new List<Perfil>();

                while (Dr.Read())
                {
                    Perfil p = new Perfil();
                    p.Id = (Int32)Dr["ID"];
                    p.Descricao = (String)Dr["DESCRICAO"];
                    p.Ativo = (Boolean)Dr["ATIVO"];

                    listaP.Add(p);
                }

                return listaP;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao listar Perfis " + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public Perfil FindByPerfil(string Descricao)
        {
            try
            {
                OpenConnection();
                Cmd = new SqlCommand("SELECT [ID], [DESCRICAO], [ATIVO] FROM TB_PERFIL WHERE [DESCRICAO] = @v1 ", Con);
                Cmd.Parameters.AddWithValue("@v1", Descricao);
                Dr = Cmd.ExecuteReader();

                if (Dr.Read())
                {
                    Perfil p = new Perfil();
                    p.Id = (Int32)Dr["ID"];
                    p.Descricao = (String)Dr["DESCRICAO"];
                    p.Ativo = (Boolean)Dr["ATIVO"];

                    return p;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao executar a consulta: " + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }        
    }
}
