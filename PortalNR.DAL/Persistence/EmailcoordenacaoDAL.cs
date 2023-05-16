using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortalNR.DAL.Entities;
using System.Data.SqlClient;

namespace PortalNR.DAL.Persistence
{
    public class EmailcoordenacaoDAL : Conexao
    {
        public void Insert(Emailcoordenacao ec)
        {
            try
            {
                OpenConnection();
                Tr = Con.BeginTransaction();
                Cmd = new SqlCommand("INSERT INTO [dbo].[TB_EMAILCOORDENACAO] ([COORDENACAO],[RESPONSAVEL],[EMAIL],[ATIVO]) VALUES(@v1, @v2, @v3, @v4)", Con, Tr);
                Cmd.Parameters.AddWithValue("@v1", ec.Coordenacao);
                Cmd.Parameters.AddWithValue("@v2", ec.Responsavel);
                Cmd.Parameters.AddWithValue("@v3", ec.email);
                Cmd.Parameters.AddWithValue("@v4", ec.Ativo);
                Cmd.ExecuteNonQuery();
                Tr.Commit();
            }
            catch (Exception e)
            {
                if (Tr != null)
                {
                    Tr.Rollback();
                }

                throw new Exception("Erro ao cadastrar o e-mail: " + e.Message);
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
                Cmd = new SqlCommand("DELETE FROM [TB_EMAILCOORDENACAO] WHERE ID = @v1", Con, Tr);
                Cmd.Parameters.AddWithValue("@v1", Id);
                Cmd.ExecuteNonQuery();
                Tr.Commit();
            }
            catch (Exception e)
            {
                if (Tr != null)
                {
                    Tr.Rollback();
                }

                throw new Exception("Erro ao excluir o registro: " + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public void Update(Emailcoordenacao ec)
        {
            try
            {
                OpenConnection();
                Tr = Con.BeginTransaction();
                Cmd = new SqlCommand("UPDATE [TB_EMAILCOORDENACAO] SET [COORDENACAO] = @v1,[RESPONSAVEL] = @v2,[EMAIL] = @v3,[ATIVO] = @v4 WHERE [ID] = @v5", Con, Tr);
                Cmd.Parameters.AddWithValue("@v1", ec.Coordenacao);
                Cmd.Parameters.AddWithValue("@v2", ec.Responsavel);
                Cmd.Parameters.AddWithValue("@v3", ec.email);
                Cmd.Parameters.AddWithValue("@v4", ec.Ativo);
                Cmd.Parameters.AddWithValue("@v5", ec.Id);
                Cmd.ExecuteNonQuery();
                Tr.Commit();
            }
            catch (Exception e)
            {
                if (Tr != null)
                {
                    Tr.Rollback();
                }

                throw new Exception("Erro ao atualizar o registro:" + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public Emailcoordenacao FindbyId(int Id)
        {
            try
            {
                OpenConnection();
                Cmd = new SqlCommand("SELECT * FROM TB_EMAILCOORDENACAO WHERE Id = @v1", Con);
                Cmd.Parameters.AddWithValue("@v1", Id);
                Dr = Cmd.ExecuteReader();

                if (Dr.Read())
                {
                    Emailcoordenacao mail = new Emailcoordenacao();
                    mail.Id = (Int32)Dr["Id"];
                    mail.Coordenacao = (String)Dr["Coordenacao"];
                    mail.Responsavel = (String)Dr["Responsavel"];
                    mail.email = (String)Dr["Email"];
                    mail.Ativo = (Boolean)Dr["Ativo"];

                    return mail;
                }
                else
                {
                    return null;
                }                
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao executar ação: " + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public List<Emailcoordenacao> FindAll()
        {
            try
            {
                OpenConnection();
                Cmd = new SqlCommand("SELECT * FROM TB_EMAILCOORDENACAO", Con);
                Dr = Cmd.ExecuteReader();

                List<Emailcoordenacao> lista = new List<Emailcoordenacao>();

                while (Dr.Read())
                {
                    Emailcoordenacao ec = new Emailcoordenacao();
                    ec.Id = (Int32)Dr["Id"];
                    ec.Coordenacao = (String)Dr["Coordenacao"];
                    ec.Responsavel = (String)Dr["Responsavel"];
                    ec.email = (String)Dr["Email"];
                    ec.Ativo = (Boolean)Dr["Ativo"];

                    lista.Add(ec);
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
                
        public List<Emailcoordenacao> ListaCoordenacao()
        {
            try
            {
                OpenConnection();
                //Cmd = new SqlCommand("SELECT DISTINCT [COORDENACAO] FROM TB_FUNCIONARIOS_MTR WHERE [COORDENACAO] <> '-' ORDER BY [COORDENACAO]", Con);
                Cmd = new SqlCommand("SELECT DISTINCT [COORDENACAO] FROM [TB_COLABORADOR] WHERE [COORDENACAO] <> '-' ORDER BY [COORDENACAO]", Con);
                Dr = Cmd.ExecuteReader();
                //Método para Popular o DropDownList
                List<Emailcoordenacao> lista = new List<Emailcoordenacao>();

                while (Dr.Read())
                {
                    Emailcoordenacao ec = new Emailcoordenacao();                    
                    ec.Coordenacao = (String)Dr["Coordenacao"];                    

                    lista.Add(ec);
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

        public List<Emailcoordenacao> AutoCompleteCoordenacao(string prefixText)
        {
            try
            {
                OpenConnection();
                Cmd = new SqlCommand("SELECT DISTINCT [COORDENACAO] FROM [TB_COLABORADOR] WHERE [COORDENACAO] LIKE '%' + @v1 + '%'", Con);
                Cmd.Parameters.AddWithValue("@v1", prefixText);
                Dr = Cmd.ExecuteReader();

                List<Emailcoordenacao> lista = new List<Emailcoordenacao>();

                while (Dr.Read())
                {
                    Emailcoordenacao ec = new Emailcoordenacao();
                    ec.Coordenacao = (String)Dr["Coordenacao"];
                    lista.Add(ec);
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
