using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortalNR.DAL.Entities;
using System.Data.SqlClient;

namespace PortalNR.DAL.Persistence
{
    public class CoordenacaoDAL : Conexao
    {
        public void Insert(Coordenacao c)
        {
            try
            {
                OpenConnection();
                Tr = Con.BeginTransaction();
                Cmd = new SqlCommand("INSERT INTO [dbo].[TB_COORDENACAO] ([COORDENACAO],[DTCADASTRO]) VALUES(@v1,GETDATE())", Con, Tr);
                Cmd.Parameters.AddWithValue("@v1", c.Descricao);
                
                Cmd.ExecuteNonQuery();
                Tr.Commit();
            }
            catch (Exception e)
            {
                if (Tr != null)
                {
                    Tr.Rollback();
                }

                throw new Exception("Erro ao cadastrar a coordenação: " + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public void Delete(int Id)
        {
            try
            {
                OpenConnection();
                Tr = Con.BeginTransaction();
                Cmd = new SqlCommand("DELETE FROM TB_COORDENACAO WHERE ID = @v1", Con, Tr);
                Cmd.Parameters.AddWithValue("@v1", Id);
                Cmd.ExecuteNonQuery();
                Tr.Commit();

            }
            catch(Exception e)
            {
                if (Tr != null)
                {
                    Tr.Rollback();
                }

                throw new Exception("Falaha ao executar a ação: " + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public List<Coordenacao> Find(string Descricao)
        {
            try
            {
                OpenConnection();
                Cmd = new SqlCommand("SELECT * FROM TB_COORDENACAO WHERE COORDENACAO LIKE '%' + @v1 + '%'", Con);
                Cmd.Parameters.AddWithValue("@v1", Descricao);
                Dr = Cmd.ExecuteReader();

                List<Coordenacao> Lista = new List<Coordenacao>();

                while (Dr.Read())
                {
                    Coordenacao c = new Coordenacao();
                    c.Id = (Int32)Dr["ID"];
                    c.Descricao = (String)Dr["COORDENACAO"];
                    c.DtCadastro = (DateTime)Dr["DTCADASTRO"];

                    Lista.Add(c);
                }

                return Lista;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao carregar o resultado: " + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public List<Coordenacao> ListAll()
        {
            try
            {
                OpenConnection();
                Cmd = new SqlCommand("SELECT * FROM TB_COORDENACAO WHERE [COORDENACAO] <> '-'", Con);
                Dr = Cmd.ExecuteReader();

                List<Coordenacao> lista = new List<Coordenacao>();

                while (Dr.Read())
                {
                    Coordenacao c = new Coordenacao();
                    c.Id = (Int32)Dr["Id"];
                    c.Descricao = (String)Dr["Coordenacao"];
                    c.DtCadastro = (DateTime)Dr["DtCadastro"];

                    lista.Add(c);
                }

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao listar a coordenação: " + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }  
    }
}
