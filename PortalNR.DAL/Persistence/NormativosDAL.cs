using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortalNR.DAL.Entities;
using System.Data.SqlClient;

namespace PortalNR.DAL.Persistence
{
    public class NormativosDAL : Conexao
    {
        public void Insert(Normativos n)
        {
            try
            {
                OpenConnection();
                Tr = Con.BeginTransaction();
                Cmd = new SqlCommand("INSERT INTO TB_NORMATIVOS(NORMATIVO, VIGENCIA, DESCRICAO, ATIVO) VALUES(@v1, @v2, @v3, @v4)", Con, Tr);
                Cmd.Parameters.AddWithValue("@v1", n.Normativo);
                Cmd.Parameters.AddWithValue("@v2", n.Vigencia);
                Cmd.Parameters.AddWithValue("@v3", n.Descricao);
                Cmd.Parameters.AddWithValue("@v4", n.Ativo);
                Cmd.ExecuteNonQuery();
                Tr.Commit();
            }
            catch (Exception e)
            {
                if (Tr != null)
                {
                    Tr.Rollback();
                }

                throw new Exception("Erro ao inserir normativo: " + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
        
        public void Delete(int IdNormativo)
        {
            try
            {
                OpenConnection();
                Tr = Con.BeginTransaction();
                Cmd = new SqlCommand("DELETE FROM TB_NORMATIVOS WHERE ID = @v1", Con, Tr);
                Cmd.Parameters.AddWithValue("@v1", IdNormativo);
                Cmd.ExecuteNonQuery();
                Tr.Commit();
            }
            catch(Exception e)
            {
                if (Tr != null)
                {
                    Tr.Rollback();
                }

                throw new Exception("Erro ao deletar o normetivo: " + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public void Update(Normativos n)
        {
            try
            {
                OpenConnection();
                Tr = Con.BeginTransaction();
                Cmd = new SqlCommand("UPDATE TB_NORMATIVOS SET NORMATIVO = @v1, VIGENCIA = @v2, DESCRICAO = @v3, ATIVO = @v4 WHERE ID = @v5", Con, Tr);
                Cmd.Parameters.AddWithValue("@v1", n.Normativo);
                Cmd.Parameters.AddWithValue("@v2", n.Vigencia);
                Cmd.Parameters.AddWithValue("@v3", n.Descricao);
                Cmd.Parameters.AddWithValue("@v4", n.Ativo);
                Cmd.Parameters.AddWithValue("@v5", n.IdNormativo);
                Cmd.ExecuteNonQuery();
                Tr.Commit();
            }
            catch (Exception e)
            {
                if (Tr != null)
                {
                    Tr.Rollback();
                }
                throw new Exception("Erro ao atualizar o normativo: " + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public List<Normativos> FindAll()
        {
            try
            {
                OpenConnection();
                Cmd = new SqlCommand("SELECT [ID] ,[NORMATIVO] ,[VIGENCIA] ,[DESCRICAO] ,[ATIVO] FROM [dbo].[TB_NORMATIVOS]", Con);
                Dr = Cmd.ExecuteReader();

                List<Normativos> lista = new List<Normativos>();

                while (Dr.Read())
                {
                    Normativos n = new Normativos();
                    n.IdNormativo = (Int32)Dr["Id"];
                    n.Normativo = (String)Dr["Normativo"];
                    n.Vigencia = (Int32)Dr["Vigencia"];
                    n.Descricao = (String)Dr["Descricao"];
                    n.Ativo = (Boolean)Dr["Ativo"];
                    lista.Add(n);
                }

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao listar normativos: " + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }        

        public Normativos FindById(int IdNormativo)
        {
            try
            {
                OpenConnection();
                Cmd = new SqlCommand("SELECT * FROM TB_NORMATIVOS WHERE ID = @v1", Con);
                Cmd.Parameters.AddWithValue("@v1", IdNormativo);
                Dr = Cmd.ExecuteReader();

                if (Dr.Read())
                {
                    Normativos n = new Normativos();                    
                    n.IdNormativo = (Int32)Dr["Id"];
                    n.Normativo = (String)Dr["Normativo"];
                    n.Vigencia = (Int32)Dr["Vigencia"];
                    n.Descricao = (String)Dr["Descricao"];
                    n.Ativo = (Boolean)Dr["Ativo"];

                    return n;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao pesquisar normativo: " + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public Normativos FindByNormativo(string Normativo)
        {
            try
            {
                OpenConnection();
                Cmd = new SqlCommand("SELECT [NORMATIVO], [VIGENCIA], [DESCRICAO] FROM TB_NORMATIVOS WHERE [NORMATIVO] = @v1 ", Con);
                Cmd.Parameters.AddWithValue("@v1", Normativo);
                Dr = Cmd.ExecuteReader();

                if (Dr.Read())
                {
                    Normativos n = new Normativos();
                    n.Normativo = (String)Dr["NORMATIVO"];
                    n.Vigencia = (Int32)Dr["VIGENCIA"];
                    n.Descricao = (String)Dr["DESCRICAO"];

                    return n;
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

        #region Consulta Utilizada no relatorio
        public List<Normativos> ListaNormativos()
        {
            try
            {
                OpenConnection();
                Cmd = new SqlCommand("SELECT * FROM [TB_NORMATIVOS] WHERE [ATIVO] = 1 ORDER BY [NORMATIVO]", Con);
                Dr = Cmd.ExecuteReader();
                //Método para popular do DropDownList de Normativos
                List<Normativos> listaN = new List<Normativos>();

                while (Dr.Read())
                {
                    Normativos n = new Normativos();
                    n.IdNormativo = (Int32)Dr["ID"];
                    n.Normativo = (String)Dr["NORMATIVO"];

                    listaN.Add(n);
                }

                return listaN;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao listar os normativos: " + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
        #endregion
    }
}
