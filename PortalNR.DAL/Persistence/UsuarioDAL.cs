using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using PortalNR.DAL.Entities;
using PortalNR.Security;

namespace PortalNR.DAL.Persistence
{
    public class UsuarioDAL : Conexao
    {
        public void Insert(Usuario u)
        {
            try
            {
                OpenConnection();
                Tr = Con.BeginTransaction();
                Cmd = new SqlCommand("INSERT INTO [dbo].[TB_USUARIO]([NMUSUARIO],[LOGIN],[EMAIL],[SENHA],[DTCADASTRO],[DTDESATIVACAO],[ID_PERFIL],[PRIMEIROACESSO],[ATIVO]) VALUES(@v1,@v2,@v3,@v4,GETDATE(),GETDATE(),@v5,1,@v6)", Con, Tr);
                Cmd.Parameters.AddWithValue("@v1", u.NmUsuario);
                Cmd.Parameters.AddWithValue("@v2", u.Login);
                Cmd.Parameters.AddWithValue("@v3", u.Email);
                Cmd.Parameters.AddWithValue("@v4", u.Senha);
                Cmd.Parameters.AddWithValue("@v5", u.PerfilRel.Id);
                Cmd.Parameters.AddWithValue("@v6", u.Ativo);
                Cmd.ExecuteNonQuery();
                Tr.Commit();
            }
            catch (Exception e)
            {
                if (Tr != null)
                {
                    Tr.Rollback();
                }
                throw new Exception("Erro ao cadastrar usuário: " + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public void Update(Usuario u)
        {
            try
            {
                OpenConnection();
                Tr = Con.BeginTransaction();
                Cmd = new SqlCommand("UPDATE TB_USUARIO SET [NMUSUARIO] = @v1, [LOGIN] = @v2, [EMAIL] = @v3, [ID_PERFIL] = @v4, [ATIVO] = @v5 WHERE [ID] = @v6", Con, Tr);
                Cmd.Parameters.AddWithValue("@v1", u.NmUsuario);
                Cmd.Parameters.AddWithValue("@v2", u.Login);
                Cmd.Parameters.AddWithValue("@v3", u.Email);
                Cmd.Parameters.AddWithValue("@v4", u.PerfilRel.Id);
                Cmd.Parameters.AddWithValue("@v5", u.Ativo);
                Cmd.Parameters.AddWithValue("@v6", u.Id);
                Cmd.ExecuteNonQuery();
                Tr.Commit();
            }
            catch (Exception e)
            {
                if (Tr != null)
                {
                    Tr.Rollback();
                }

                throw new Exception("erro ao executar a ação: " + e.Message);
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
                Cmd = new SqlCommand("DELETE FROM TB_USUARIO WHERE ID = @v1", Con, Tr);
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
                throw new Exception("Não foi possível executar a ação: " + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public List<Usuario> FindAll()
        {
            try
            {
                OpenConnection();
                Cmd = new SqlCommand("SELECT [ID],[NMUSUARIO],[LOGIN],[EMAIL],[DTCADASTRO],[DTDESATIVACAO],[IDPERFIL],[DESCRICAO],[PRIMEIROACESSO],[ATIVO] FROM [vwUSUARIOPERFIL]", Con);
                Dr = Cmd.ExecuteReader();

                List<Usuario> ListaUsu = new List<Usuario>();

                while (Dr.Read())
                {
                    Usuario u = new Usuario();
                    u.PerfilRel = new Perfil();
                    u.Id = (Int32)Dr["ID"];
                    u.NmUsuario = (String)Dr["NMUSUARIO"];
                    u.Login = (String)Dr["LOGIN"];
                    u.Email = (String)Dr["EMAIL"];
                    u.DtCadastro = (DateTime)Dr["DTCADASTRO"];
                    u.PerfilRel.Descricao = (String)Dr["DESCRICAO"];
                    u.Ativo = (Boolean)Dr["ATIVO"];
                    ListaUsu.Add(u);
                }
                return ListaUsu;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao caregar a lista: " + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public List<Usuario> PesquisarLogin(string login)
        {
            try
            {
                OpenConnection();
                Cmd = new SqlCommand("SELECT [ID],[NMUSUARIO],[LOGIN],[EMAIL],[DTCADASTRO],[DTDESATIVACAO],[IDPERFIL],[DESCRICAO],[PRIMEIROACESSO],[ATIVO] FROM [vwUSUARIOPERFIL] WHERE [LOGIN] = @v1", Con);
                Cmd.Parameters.AddWithValue("@v1", login);
                Dr = Cmd.ExecuteReader();

                List<Usuario> ListaUsu = new List<Usuario>();

                while (Dr.Read())
                {
                    Usuario u = new Usuario();
                    u.PerfilRel = new Perfil();
                    u.Id = (Int32)Dr["ID"];
                    u.NmUsuario = (String)Dr["NMUSUARIO"];
                    u.Login = (String)Dr["LOGIN"];
                    u.Email = (String)Dr["EMAIL"];
                    u.DtCadastro = (DateTime)Dr["DTCADASTRO"];
                    u.PerfilRel.Descricao = (String)Dr["DESCRICAO"];
                    u.Ativo = (Boolean)Dr["ATIVO"];
                    ListaUsu.Add(u);
                }
                return ListaUsu;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao pesquisar o login: " + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public Usuario FindById(int Id)
        {
            try
            {
                OpenConnection();
                Cmd = new SqlCommand("SELECT [ID],[NMUSUARIO],[LOGIN],[EMAIL],[DTCADASTRO],[DTDESATIVACAO],[IDPERFIL],[DESCRICAO],[ATIVO] FROM [vwUSUARIOPERFIL] WHERE [ID] = @v1", Con);
                Cmd.Parameters.AddWithValue("@v1", Id);
                Dr = Cmd.ExecuteReader();

                if (Dr.Read())
                {
                    Usuario u = new Usuario();
                    u.PerfilRel = new Perfil();

                    u.Id = (Int32)Dr["Id"];
                    u.NmUsuario = (String)Dr["NMUSUARIO"];
                    u.Login = (String)Dr["LOGIN"];
                    u.Email = (String)Dr["EMAIL"];
                    u.PerfilRel.Id = (Int32)Dr["IDPERFIL"];
                    u.Ativo = (Boolean)Dr["ATIVO"];

                    return u;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao executar a ação: " + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
        
        public Usuario FindUsu(String Login, String Senha)
        {
            try
            {
                OpenConnection();
                Cmd = new SqlCommand("SELECT * FROM [TB_USUARIO] WHERE [LOGIN] = @v1 AND [Senha] = @v2", Con);
                Cmd.Parameters.AddWithValue("@v1", Login);
                Cmd.Parameters.AddWithValue("@v2", Senha);
                Dr = Cmd.ExecuteReader();

                if (Dr.Read())
                {
                    Usuario u = new Usuario();
                    u.PerfilRel = new Perfil();

                    u.Id = (Int32)Dr["Id"];
                    u.NmUsuario = (String)Dr["NmUsuario"];
                    u.Login = (String)Dr["Login"];
                    u.Senha = (String)Dr["Senha"];
                    u.PerfilRel.Id = (Int32)Dr["ID_PERFIL"];
                    u.DtCadastro = (DateTime)Dr["DtCadastro"];
                    u.PrimeiroAcesso = (Boolean)Dr["PRIMEIROACESSO"];
                    u.Ativo = (Boolean)Dr["Ativo"];

                    return u;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception e)
            {
                throw new Exception("Erro ao obter Usuario: " + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public Usuario FindLogin(String Login)
        {
            try
            {
                OpenConnection();
                Cmd = new SqlCommand("SELECT * FROM [TB_USUARIO] WHERE [LOGIN] = @v1", Con);
                Cmd.Parameters.AddWithValue("@v1", Login);
                
                Dr = Cmd.ExecuteReader();

                if (Dr.Read())
                {
                    Usuario u = new Usuario();
                    u.PerfilRel = new Perfil();

                    u.Id = (Int32)Dr["Id"];
                    u.NmUsuario = (String)Dr["NmUsuario"];
                    u.Login = (String)Dr["Login"];
                    u.Senha = (String)Dr["Senha"];
                    u.PerfilRel.Id = (Int32)Dr["ID_PERFIL"];
                    u.DtCadastro = (DateTime)Dr["DtCadastro"];
                    u.PrimeiroAcesso = (Boolean)Dr["PRIMEIROACESSO"];
                    u.Ativo = (Boolean)Dr["Ativo"];

                    return u;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao obter Usuario: " + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }    
    }
}
