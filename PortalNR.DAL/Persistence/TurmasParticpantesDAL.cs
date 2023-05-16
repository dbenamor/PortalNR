using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using PortalNR.DAL.Entities;

namespace PortalNR.DAL.Persistence
{
    public class TurmasParticpantesDAL : Conexao
    {
        public void Adicionar(TurmasParticpantes tp)
        {
            try
            {
                OpenConnection();
                Tr = Con.BeginTransaction();
                Cmd = new SqlCommand("INSERT INTO [dbo].[TB_TURMASPARTICIPANTES]([MATRICULA],[NOME],[COORDENACAO],[PRESENCA],[ID_TURMA]) VALUES(@v1, @v2, @v3, @v4, @v5)", Con, Tr);
                Cmd.Parameters.AddWithValue("@v1", tp.Matricula);
                Cmd.Parameters.AddWithValue("@v2", tp.Nome);
                Cmd.Parameters.AddWithValue("@v3", tp.Coordenacao);
                Cmd.Parameters.AddWithValue("@v4", tp.Presenca);
                Cmd.Parameters.AddWithValue("@v5", tp.turmaRel.IdTurma);
                Cmd.ExecuteNonQuery();
                Tr.Commit();
            }
            catch (Exception e)
            {
                if (Tr != null)
                {
                    Tr.Rollback();
                }
                throw new Exception("Não foi possível adicionar o colaborador: " + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public void Remover(int IdParticipante)
        {
            try
            {
                OpenConnection();
                Tr = Con.BeginTransaction();

                Cmd = new SqlCommand("DELETE FROM [dbo].[TB_TURMASPARTICIPANTES] WHERE ID = @v1", Con, Tr);
                Cmd.Parameters.AddWithValue("@v1", IdParticipante);
                Cmd.ExecuteNonQuery();
                Tr.Commit();
            }
            catch (Exception e)
            {
                if (Tr != null)
                {
                    Tr.Rollback();
                }
                throw new Exception("Erro ao executar a ação: " + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public void Update(TurmasParticpantes tp)
        {
            try
            {
                OpenConnection();
                Tr = Con.BeginTransaction();
                Cmd = new SqlCommand("UPDATE [TB_TURMASPARTICIPANTES] SET PRESENCA = @v1 WHERE ID = @v2", Con, Tr);
                Cmd.Parameters.AddWithValue("@v1", tp.Presenca);
                Cmd.Parameters.AddWithValue("@v2", tp.IdParticipante);
                Cmd.ExecuteNonQuery();
                Tr.Commit();
            }
            catch (Exception e)
            {
                if (Tr != null)
                {
                    Tr.Rollback();
                }
                throw new Exception("Erro ao executar a ação: " + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public void UpdateVagas()
        {
            try
            {
                OpenConnection();
                Tr = Con.BeginTransaction();
                Cmd = new SqlCommand(@"UPDATE [TB_TURMAS] SET [TB_TURMAS].[VAGASDISPONIVEIS] = [vwTURMAS].[VAGASDISPONIVEIS] FROM [TB_TURMAS]
                                       INNER JOIN [vwTURMAS] ON [TB_TURMAS].[ID] = [vwTURMAS].[ID] WHERE [TB_TURMAS].[ID] = [vwTURMAS].[ID]", Con, Tr);
                Cmd.ExecuteNonQuery();
                Tr.Commit();

            }
            catch (Exception e)
            {
                if (Tr != null)
                {
                    Tr.Rollback();
                }
                throw new Exception("Erro ao executar a ação: " + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public TurmasParticpantes FindById(int IdParticipante)
        {
            try
            {
                OpenConnection();
                Cmd = new SqlCommand("SELECT * FROM [TB_TURMASPARTICIPANTES] WHERE ID = @v1", Con);
                Cmd.Parameters.AddWithValue("@v1", IdParticipante);
                Dr = Cmd.ExecuteReader();

                if (Dr.Read())
                {
                    TurmasParticpantes tp = new TurmasParticpantes();
                    tp.IdParticipante = (Int32)Dr["ID"];
                    tp.Matricula = (Int32)Dr["Matricula"];
                    tp.Nome = (String)Dr["Nome"];
                    tp.Coordenacao = (String)Dr["COORDENACAO"];
                    //tp.turmaRel.IdTurma = (Int32)Dr["ID_TURMA"];
                    tp.Presenca = (Boolean)Dr["PRESENCA"];

                    return tp;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao buscar o participante: " + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public List<TurmasParticpantes> FindParticipantes(int IdTurma)
        {
            try
            {
                OpenConnection();
                Cmd = new SqlCommand("SELECT [ID],[NOME],[COORDENACAO],[ID_TURMA],[MATRICULA],[PRESENCA] FROM [dbo].[TB_TURMASPARTICIPANTES] WHERE [ID_TURMA] = @v1", Con);
                Cmd.Parameters.AddWithValue("@v1", IdTurma);
                Dr = Cmd.ExecuteReader();

                List<TurmasParticpantes> listaTurmaP = new List<TurmasParticpantes>();

                while (Dr.Read())
                {
                    TurmasParticpantes tp = new TurmasParticpantes();
                    tp.turmaRel = new Turmas();

                    tp.IdParticipante = (Int32)Dr["ID"];
                    tp.turmaRel.IdTurma = (Int32)Dr["ID_TURMA"];
                    tp.Matricula = (Int32)Dr["MATRICULA"];
                    tp.Nome = (String)Dr["NOME"];
                    tp.Coordenacao = (String)Dr["COORDENACAO"];
                    tp.Presenca = (Boolean)Dr["PRESENCA"];
                    listaTurmaP.Add(tp);
                }
                return listaTurmaP;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao carregar a lista: " + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public List<TurmasParticpantes> ExportParticipantes(int IdTurma)
        {
            try
            {
                OpenConnection();
                Cmd = new SqlCommand("SELECT [ID],[NR],[DTTURMA],[PALESTRANTE],[TEMA],[DESCRICAO],[MATRICULA],[NOME],[COORDENACAO],[PRESENCA] FROM [dbo].[vwTURMASPARTICIPANTES] WHERE [ID] = @v1", Con);
                Cmd.Parameters.AddWithValue("@v1", IdTurma);
                Dr = Cmd.ExecuteReader();

                List<TurmasParticpantes> listaTurmaP = new List<TurmasParticpantes>();

                while (Dr.Read())
                {
                    TurmasParticpantes tp = new TurmasParticpantes();
                    tp.turmaRel = new Turmas();
                    tp.temaRel = new TurmasTema();

                    tp.turmaRel.IdTurma = (Int32)Dr["ID"];
                    tp.Normativo = (String)Dr["NR"];
                    tp.turmaRel.dtTurma = (DateTime)Dr["DTTURMA"];
                    tp.Palestrante = (String)Dr["PALESTRANTE"];
                    tp.temaRel.Tema = (String)Dr["TEMA"];
                    tp.turmaRel.Descricao = (String)Dr["DESCRICAO"];
                    tp.Matricula = (Int32)Dr["MATRICULA"];
                    tp.Nome = (String)Dr["NOME"];
                    tp.Coordenacao = (String)Dr["COORDENACAO"];
                    tp.Presenca = (Boolean)Dr["PRESENCA"];
                    listaTurmaP.Add(tp);
                }
                return listaTurmaP;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao carregar a lista: " + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public List<TurmasParticpantes> ListarParticipantes(int IdTurma)
        {
            try
            {
                OpenConnection();
                Cmd = new SqlCommand("SELECT [ID_PARTICIPANTE],[NOME],[COORDENACAO],[ID_TURMA],[DESCRICAO],[MATRICULA],[PRESENCA] FROM [dbo].[vwTURMASPARTICIPANTES] WHERE [ID_TURMA] = @v1", Con);
                Cmd.Parameters.AddWithValue("@v1", IdTurma);
                Dr = Cmd.ExecuteReader();

                List<TurmasParticpantes> listaTurmaP = new List<TurmasParticpantes>();

                while (Dr.Read())
                {
                    TurmasParticpantes tp = new TurmasParticpantes();
                    tp.turmaRel = new Turmas();

                    tp.IdParticipante = (Int32)Dr["ID_PARTICIPANTE"];
                    tp.turmaRel.IdTurma = (Int32)Dr["ID_TURMA"];
                    tp.turmaRel.Descricao = (String)Dr["DESCRICAO"];
                    tp.Matricula = (Int32)Dr["MATRICULA"];
                    tp.Nome = (String)Dr["NOME"];
                    tp.Coordenacao = (String)Dr["COORDENACAO"];
                    tp.Presenca = (Boolean)Dr["PRESENCA"];
                    listaTurmaP.Add(tp);
                }
                return listaTurmaP;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao carregar a lista: " + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
