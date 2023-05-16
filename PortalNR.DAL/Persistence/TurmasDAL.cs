using PortalNR.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PortalNR.DAL.Persistence
{
    public class TurmasDAL : Conexao
    {
        public void Insert(Turmas t)
        {
            try
            {
                OpenConnection();
                Tr = Con.BeginTransaction();
                Cmd = new SqlCommand("INSERT INTO [dbo].[TB_TURMAS]([DTTURMA],[DTTURMAFIM],[CARGA],[LOCAL],[VAGAS],[VAGASDISPONIVEIS],[PALESTRANTE],[ID_NR],[ID_TEMA],[DESCRICAO],[VEICULO])VALUES(@v1, @v2, @v3, @v4, @v5, @v6, @v7, @v8, @v9, @v10, @v11)", Con, Tr);
                Cmd.Parameters.AddWithValue("@v1", t.dtTurma);
                Cmd.Parameters.AddWithValue("@v2", t.dtTurmaFim);
                Cmd.Parameters.AddWithValue("@v3", t.Carga);
                Cmd.Parameters.AddWithValue("@v4", t.Local);
                Cmd.Parameters.AddWithValue("@v5", t.Vagas);
                Cmd.Parameters.AddWithValue("@v6", t.vagasDisponiveis);
                Cmd.Parameters.AddWithValue("@v7", t.Palestrante);
                Cmd.Parameters.AddWithValue("@v8", t.normativoRel.IdNormativo);
                Cmd.Parameters.AddWithValue("@v9", t.TemaRelac.IdTema);
                Cmd.Parameters.AddWithValue("@v10", t.Observacao);
                Cmd.Parameters.AddWithValue("@v11", t.Veiculo);
                Cmd.ExecuteNonQuery();
                Tr.Commit();
            }
            catch (Exception e)
            {
                if (Tr != null)
                {
                    Tr.Rollback();
                }
                throw new Exception("Erro ao cadastrar: " + e.Message);
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
                Cmd = new SqlCommand("DELETE FROM TB_TURMAS WHERE ID = @v1", Con, Tr);
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

        public void Update(Turmas t)
        {
            try
            {
                OpenConnection();
                Tr = Con.BeginTransaction();
                Cmd = new SqlCommand("UPDATE TB_TURMAS SET [DTTURMA] = @v1, [DTTURMAFIM] = @v2, [CARGA] = @v3, [LOCAL] = @v4, [VAGAS] = @v5, [PALESTRANTE] = @v6, [ID_NR] = @v7, [ID_TEMA] = @v8, [DESCRICAO] = @v9, [VEICULO] = @v11 WHERE [ID] = @v10", Con, Tr);
                Cmd.Parameters.AddWithValue("@v1", t.dtTurma);
                Cmd.Parameters.AddWithValue("@v2", t.dtTurmaFim);
                Cmd.Parameters.AddWithValue("@v3", t.Carga);
                Cmd.Parameters.AddWithValue("@v4", t.Local);
                Cmd.Parameters.AddWithValue("@v5", t.Vagas);
                Cmd.Parameters.AddWithValue("@v6", t.Palestrante);
                Cmd.Parameters.AddWithValue("@v7", t.normativoRel.IdNormativo);
                Cmd.Parameters.AddWithValue("@v8", t.TemaRelac.IdTema);
                Cmd.Parameters.AddWithValue("@v9", t.Observacao);
                Cmd.Parameters.AddWithValue("@v10", t.IdTurma);
                Cmd.Parameters.AddWithValue("@v11", t.Veiculo);
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

        public void CancelarTurma(Turmas t)
        {
            try
            {
                OpenConnection();
                Tr = Con.BeginTransaction();
                Cmd = new SqlCommand("UPDATE TB_TURMAS SET [ATIVO] = 0 WHERE [ID] = @v1", Con, Tr);
                Cmd.Parameters.AddWithValue("@v1", t.IdTurma);
                Cmd.ExecuteNonQuery();
                Tr.Commit();
            }
            catch (Exception e)
            {
                if (Tr != null)
                {
                    Tr.Rollback();
                }
                throw new Exception("Erro ao cancelar a turma:" + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public Turmas FindById(int id)
        {
            try
            {
                OpenConnection();
                Cmd = new SqlCommand("SELECT * FROM vwTURMAS WHERE ID = @v1", Con);
                Cmd.Parameters.AddWithValue("@v1", id);
                Dr = Cmd.ExecuteReader();

                if (Dr.Read())
                {
                    Turmas t = new Turmas();
                    t.normativoRel = new Normativos();
                    t.TemaRelac = new TurmasTema();

                    t.IdTurma = (Int32)Dr["ID"];
                    t.dtTurma = (DateTime)Dr["DTTURMA"];
                    t.dtTurmaFim = (DateTime)Dr["DTTURMAFIM"];
                    t.Carga = (Int32)Dr["CARGA"];
                    t.normativoRel.IdNormativo = (Int32)Dr["ID_NR"];
                    t.normativoRel.Normativo = (String)Dr["NR"];
                    t.Local = (String)Dr["LOCAL"];
                    t.Vagas = (Int32)Dr["VAGAS"];
                    t.vagasDisponiveis = (Int32)Dr["VAGASDISPONIVEIS"];
                    t.Palestrante = (String)Dr["PALESTRANTE"];
                    t.TemaRelac.IdTema = (Int32)Dr["ID_TEMA"];
                    t.TemaRelac.Tema = (String)Dr["TEMA"];
                    t.Veiculo = (String)Dr["VEICULO"];
                    t.Observacao = (String)Dr["DESCRICAO"];
                    t.Ativo = (Boolean)Dr["ATIVO"];

                    return t;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao carregar as informações: " + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        /// <summary>
        /// Consulta criada para carregar a grid da página cadastrarturmas.aspx ao incia-la.
        /// </summary>
        /// <returns></returns>
        public List<Turmas> FindAll()
        {
            try
            {
                OpenConnection();
                Cmd = new SqlCommand("SELECT [ID],[DTTURMA],[DTTURMAFIM],[CARGA],[NR],[LOCAL],[VAGAS],[VAGASDISPONIVEIS],[PALESTRANTE],[TEMA],[DESCRICAO],[VEICULO],[ATIVO] FROM [dbo].[vwTURMAS] ORDER BY [DTTURMA] DESC", Con);
                Dr = Cmd.ExecuteReader();

                List<Turmas> listaTurma = new List<Turmas>();

                while (Dr.Read())
                {
                    Turmas t = new Turmas();
                    t.normativoRel = new Normativos();
                    t.TemaRelac = new TurmasTema();

                    t.IdTurma = (Int32)Dr["ID"];
                    t.dtTurma = (DateTime)Dr["DTTURMA"];
                    t.dtTurmaFim = (DateTime)Dr["DTTURMAFIM"];
                    t.Carga = (Int32)Dr["CARGA"];
                    t.normativoRel.Normativo = (String)Dr["NR"];
                    t.Local = (String)Dr["LOCAL"];
                    t.Vagas = (Int32)Dr["VAGAS"];
                    t.vagasDisponiveis = (Int32)Dr["VAGASDISPONIVEIS"];
                    t.Palestrante = (String)Dr["PALESTRANTE"];
                    t.TemaRelac.Tema = (String)Dr["TEMA"];
                    t.Observacao = (String)Dr["DESCRICAO"];
                    t.Veiculo = (String)Dr["VEICULO"];
                    t.Ativo = (Boolean)Dr["ATIVO"];
                    listaTurma.Add(t);
                }
                return listaTurma;
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

        /// <summary>
        /// Consulta criada para carregar a grid da página proximasturmas.aspx ao incia-la.
        /// </summary>
        /// <returns></returns>
        public List<Turmas> FindProximasTurmas()
        {
            try
            {
                OpenConnection();
                //Alterado a pedido do usuário para trazer somente as turmas abertas, não mais expiradas e canceladas. Incluido na consulta: AND (DATEDIFF(HOUR,[DTTURMA], GETDATE())*-1) >= 1
                Cmd = new SqlCommand(@"SELECT [ID],[DTTURMA],[DTTURMAFIM],[CARGA],[NR],[LOCAL],[VAGAS],[VAGASDISPONIVEIS],[PALESTRANTE],[TEMA],[DESCRICAO],[VEICULO],[ATIVO] 
                                       FROM [dbo].[vwTURMAS] WHERE ATIVO = 1 AND (DATEDIFF(HOUR,[DTTURMA], GETDATE())*-1) >= 1 ORDER BY [DTTURMA] DESC", Con);
                Dr = Cmd.ExecuteReader();

                List<Turmas> listaTurma = new List<Turmas>();

                while (Dr.Read())
                {
                    Turmas t = new Turmas();
                    t.normativoRel = new Normativos();
                    t.TemaRelac = new TurmasTema();

                    t.IdTurma = (Int32)Dr["ID"];
                    t.dtTurma = (DateTime)Dr["DTTURMA"];
                    t.dtTurmaFim = (DateTime)Dr["DTTURMAFIM"];
                    t.Carga = (Int32)Dr["CARGA"];
                    t.normativoRel.Normativo = (String)Dr["NR"];
                    t.Local = (String)Dr["LOCAL"];
                    t.Vagas = (Int32)Dr["VAGAS"];
                    t.vagasDisponiveis = (Int32)Dr["VAGASDISPONIVEIS"];
                    t.Palestrante = (String)Dr["PALESTRANTE"];
                    t.TemaRelac.Tema = (String)Dr["TEMA"];
                    t.Observacao = (String)Dr["DESCRICAO"];
                    t.Veiculo = (String)Dr["VEICULO"];
                    t.Ativo = (Boolean)Dr["ATIVO"];
                    listaTurma.Add(t);
                }
                return listaTurma;
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

        /// <summary>
        /// Consulta criada para ser usada no botão pesquisar da página ProximasTurmas.aspx.
        /// </summary>
        /// <param name="Normativo"></param>
        /// <param name="Mes"></param>
        /// <returns></returns>
        public List<Turmas> FindTurmasNormativo(string Normativo, string Mes)
        {
            try
            {
                string sql = (@"SET LANGUAGE Portuguese SELECT [ID],[DTTURMA], [DTTURMAFIM],[CARGA],[NR],[LOCAL],[VAGAS],[VAGASDISPONIVEIS],[PALESTRANTE],[TEMA],[DESCRICAO],[VEICULO],[ATIVO] 
                                FROM [vwTURMAS] WHERE DATEPART(Year,[DTTURMA]) = DATEPART(Year,GETDATE()) AND ATIVO = 1 AND (DATEDIFF(HOUR,[DTTURMA], GETDATE())*-1) >= 1 ");

                Cmd = new SqlCommand(sql, Con);

                if (!string.IsNullOrEmpty(Normativo) && string.IsNullOrEmpty(Mes))
                {
                    sql += "AND [NR] = '' + @v1 + '' ORDER BY [DTTURMA] DESC";
                    Cmd.Parameters.AddWithValue("@v1", Normativo);
                }
                else if (string.IsNullOrEmpty(Normativo) && !string.IsNullOrEmpty(Mes))
                {
                    sql += "AND DATENAME(MONTH, [DTTURMA]) = '' + @v1 + '' ORDER BY [DTTURMA] DESC";
                    Cmd.Parameters.AddWithValue("@v1", Mes);
                }
                else
                {
                    sql += "AND [NR] = '' + @v1 + '' AND DATENAME(MONTH, [DTTURMA]) = '' + @v2 + '' ORDER BY [DTTURMA] DESC";
                    Cmd.Parameters.AddWithValue("@v1", Normativo);
                    Cmd.Parameters.AddWithValue("@v2", Mes);
                }
                OpenConnection();
                Cmd.CommandText = sql;
                Cmd.Connection = Con;
                Dr = Cmd.ExecuteReader();

                List<Turmas> Listaturma = new List<Turmas>();

                while (Dr.Read())
                {
                    Turmas t = new Turmas();
                    t.normativoRel = new Normativos();
                    t.TemaRelac = new TurmasTema();

                    t.IdTurma = (Int32)Dr["ID"];
                    t.dtTurma = (DateTime)Dr["DTTURMA"];
                    t.dtTurmaFim = (DateTime)Dr["DTTURMAFIM"];
                    t.Carga = (Int32)Dr["CARGA"];
                    t.normativoRel.Normativo = (String)Dr["NR"];
                    t.Local = (String)Dr["LOCAL"];
                    t.Vagas = (Int32)Dr["VAGAS"];
                    t.vagasDisponiveis = (Int32)Dr["VAGASDISPONIVEIS"];
                    t.Palestrante = (String)Dr["PALESTRANTE"];
                    t.TemaRelac.Tema = (String)Dr["TEMA"];
                    t.Observacao = (String)Dr["DESCRICAO"];
                    t.Veiculo = (String)Dr["VEICULO"];
                    t.Ativo = (Boolean)Dr["ATIVO"];
                    Listaturma.Add(t);
                }
                return Listaturma;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao carregar as informações: " + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public List<Turmas> PesquisarTurmas(DateTime De, DateTime Para)
        {
            try
            {
                string sql = (@"SELECT [ID],[DTTURMA], [DTTURMAFIM],[CARGA],[NR],[LOCAL],[VAGAS],[VAGASDISPONIVEIS],[PALESTRANTE],[TEMA],[DESCRICAO],[VEICULO],[ATIVO] 
                                FROM [vwTURMAS] WHERE [DTTURMA] BETWEEN @v1 AND @v2 ORDER BY [DTTURMA] DESC");

                Cmd = new SqlCommand(sql, Con);
                Cmd.Parameters.AddWithValue("@v1", De);
                Cmd.Parameters.AddWithValue("@v2", Para);

                OpenConnection();
                Cmd.CommandText = sql;
                Cmd.Connection = Con;
                Dr = Cmd.ExecuteReader();

                List<Turmas> Listaturma = new List<Turmas>();

                while (Dr.Read())
                {
                    Turmas t = new Turmas();
                    t.normativoRel = new Normativos();
                    t.TemaRelac = new TurmasTema();

                    t.IdTurma = (Int32)Dr["ID"];
                    t.dtTurma = (DateTime)Dr["DTTURMA"];
                    t.dtTurmaFim = (DateTime)Dr["DTTURMAFIM"];
                    t.Carga = (Int32)Dr["CARGA"];
                    t.normativoRel.Normativo = (String)Dr["NR"];
                    t.Local = (String)Dr["LOCAL"];
                    t.Vagas = (Int32)Dr["VAGAS"];
                    t.vagasDisponiveis = (Int32)Dr["VAGASDISPONIVEIS"];
                    t.Palestrante = (String)Dr["PALESTRANTE"];
                    t.TemaRelac.Tema = (String)Dr["TEMA"];
                    t.Observacao = (String)Dr["DESCRICAO"];
                    t.Veiculo = (String)Dr["VEICULO"];
                    t.Ativo = (Boolean)Dr["ATIVO"];
                    Listaturma.Add(t);
                }
                return Listaturma;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao carregar as informações: " + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

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

        public List<TurmasTema> ListaTemas()
        {
            try
            {
                OpenConnection();
                Cmd = new SqlCommand("SELECT [ID], [DESCRICAO], [ATIVO] FROM [dbo].[TB_TURMASTEMAS] WHERE [ATIVO] = 1 ORDER BY [DESCRICAO]", Con);
                Dr = Cmd.ExecuteReader();
                //Método para popular do DropDownList de Normativos
                List<TurmasTema> listTema = new List<TurmasTema>();

                while (Dr.Read())
                {
                    TurmasTema t = new TurmasTema();
                    t.IdTema = (Int32)Dr["ID"];
                    t.Tema = (String)Dr["DESCRICAO"];

                    listTema.Add(t);
                }

                return listTema;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao listar os temas: " + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public List<Veiculo> ListaVeiculos()
        {
            try
            {
                OpenConnection();
                Cmd = new SqlCommand("SELECT [ID], [VEICULO] FROM [TB_VEICULOS] WHERE [ATIVO] = 1 ORDER BY [VEICULO]", Con);
                Dr = Cmd.ExecuteReader();

                List<Veiculo> ListaV = new List<Veiculo>();

                while (Dr.Read())
                {
                    Veiculo v = new Veiculo();
                    v.IdVeiculo = (Int32)Dr["ID"];
                    v.Descricao = (String)Dr["VEICULO"];

                    ListaV.Add(v);
                }
                return ListaV;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao listar os equipamentos: " + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        #region Report
        /// <summary>
        /// Consulta usada no relatorio de turmas
        /// </summary>
        /// <param name="DataIni"></param>
        /// <param name="DataFim"></param>
        /// <param name="Normativo"></param>
        /// <returns></returns>
        public List<Turmas> FindTurmas(string DataIni, string DataFim, string Normativo)
        {
            try
            {
                //string sql = (@"SELECT [ID],[NR],[DTTURMA],[DTTURMAFIM],DATENAME(MONTH,[DTTURMA]) AS [MES],CASE WHEN [ATIVO] = 1 THEN 'ABERTA' ELSE 'CANCELADA' END AS [ATIVO] FROM [dbo].[vwTURMAS]");
                string sql = (@"SELECT [ID],[NR],[DTTURMA],[DTTURMAFIM], [ATIVO] FROM [dbo].[vwTURMAS]");

                Cmd = new SqlCommand(sql, Con);

                sql += " WHERE [DTTURMA] BETWEEN @v1 AND @v2 AND [NR] LIKE ''+ @v3 +''";
                Cmd.Parameters.AddWithValue("@v1", DataIni);
                Cmd.Parameters.AddWithValue("@v2", DataFim);
                Cmd.Parameters.AddWithValue("@v3", Normativo);

                OpenConnection();
                Cmd.CommandText = sql;
                Cmd.Connection = Con;
                Dr = Cmd.ExecuteReader();

                List<Turmas> ListaT = new List<Turmas>();

                while (Dr.Read())
                {
                    Turmas t = new Turmas();
                    t.normativoRel = new Normativos();
                    t.IdTurma = (Int32)Dr["ID"];
                    t.normativoRel.Normativo = (String)Dr["NR"];
                    t.dtTurma = (DateTime)Dr["DTTURMA"];
                    t.dtTurmaFim = (DateTime)Dr["DTTURMAFIM"];
                    t.Ativo = (Boolean)Dr["ATIVO"];
                    ListaT.Add(t);
                }

                return ListaT;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
        #endregion
    }
}
