using System;
using PortalNR.DAL.Entities;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Configuration;

namespace PortalNR.DAL.Persistence
{
    public class ColaboradorDAL : Conexao
    {
        public void Insert(Colaborador c)
        {
            try
            {
                OpenConnection();
                Tr = Con.BeginTransaction();
                Cmd = new SqlCommand("INSERT INTO [dbo].[TB_COLABORADOR]([NORMATIVO],[MATRICULA],[NOME],[COORDENACAO],[SITUACAO],[VEICULO],[DTCERTIFICACAO],[DTREVALIDACAO],[DTVIGENCIA],[ELEGIVEL])VALUES(@v1,@v2,@v3,@v4,@v5,@v6,@v7,@v8,@v9,@v10)", Con, Tr);
                Cmd.Parameters.AddWithValue("@v1", c.Normativo);
                Cmd.Parameters.AddWithValue("@v2", c.Matricula);
                Cmd.Parameters.AddWithValue("@v3", c.Nome);
                Cmd.Parameters.AddWithValue("@v4", c.Coordenacao);
                Cmd.Parameters.AddWithValue("@v5", c.Situacao);
                Cmd.Parameters.AddWithValue("@v6", c.Veiculo);
                Cmd.Parameters.AddWithValue("@v7", c.DtCertificacao ?? (object)DBNull.Value);
                Cmd.Parameters.AddWithValue("@v8", c.DtRevalidacao ?? (object)DBNull.Value);
                Cmd.Parameters.AddWithValue("@v9", c.DtVigencia ?? (object)DBNull.Value);
                Cmd.Parameters.AddWithValue("@v10", c.Elegivel);
                Cmd.ExecuteNonQuery();
                Tr.Commit();
            }
            catch (Exception e)
            {
                if (Tr != null)
                {
                    Tr.Rollback();
                }

                throw new Exception("Erro ao cadastrar o usuário: " + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public void Update(Colaborador c)
        {
            try
            {
                OpenConnection();
                Tr = Con.BeginTransaction();
                Cmd = new SqlCommand("UPDATE [TB_COLABORADOR] SET [NORMATIVO] = @v1, [VEICULO] = @v2, [DTCERTIFICACAO] = @v3, [DTREVALIDACAO] = @v4, [DTVIGENCIA] = @v5, [ELEGIVEL] = @v6, [COORDENACAO] = @v7, [SITUACAO] = @v8 WHERE ID = @v9", Con, Tr);
                Cmd.Parameters.AddWithValue("@v1", c.Normativo);
                Cmd.Parameters.AddWithValue("@v2", c.Veiculo);
                Cmd.Parameters.AddWithValue("@v3", c.DtCertificacao ?? (object)DBNull.Value);
                Cmd.Parameters.AddWithValue("@v4", c.DtRevalidacao ?? (object)DBNull.Value);
                Cmd.Parameters.AddWithValue("@v5", c.DtVigencia ?? (object)DBNull.Value);
                Cmd.Parameters.AddWithValue("@v6", c.Elegivel);
                Cmd.Parameters.AddWithValue("@v7", c.Coordenacao);
                Cmd.Parameters.AddWithValue("@v8", c.Situacao);
                Cmd.Parameters.AddWithValue("@v9", c.ID);
                Cmd.ExecuteNonQuery();
                Tr.Commit();
            }
            catch (Exception e)
            {
                if (Tr != null)
                {
                    Tr.Rollback();
                }

                throw new Exception("Erro ao atualizar o colaborador: " + e.Message);
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
                Cmd = new SqlCommand("DELETE FROM TB_COLABORADOR WHERE ID = @v1", Con, Tr);
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

                throw new Exception("Erro ao excluir: " + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public List<Colaborador> FindAll()
        {
            try
            {
                OpenConnection();
                Cmd = new SqlCommand("SELECT [ID],[NORMATIVO],[MATRICULA],[NOME],[COORDENACAO],[SITUACAO],[VEICULO],[DTCERTIFICACAO],[DTREVALIDACAO],[DTVIGENCIA],[VENCIMENTO],[STATUSNR],[ELEGIVEL], [SITUACAO_EFETIVO] FROM vwCOLABORADOR", Con);
                Dr = Cmd.ExecuteReader();

                List<Colaborador> lista = new List<Colaborador>();

                while (Dr.Read())
                {
                    Colaborador c = new Colaborador();
                    c.ID = (Int32)Dr["ID"];
                    c.Normativo = Dr.GetString(1);
                    c.Matricula = Dr.GetInt32(2);
                    c.Nome = Dr.GetString(3);
                    c.Coordenacao = Dr.GetString(4);
                    c.Situacao = Dr.GetString(5);
                    c.Veiculo = Dr.GetString(6);
                    c.DtCertificacao = Dr.IsDBNull(7) ? (DateTime?)null : Dr.GetDateTime(7);
                    c.DtRevalidacao = Dr.IsDBNull(8) ? (DateTime?)null : Dr.GetDateTime(8);
                    c.DtVigencia = Dr.IsDBNull(9) ? (DateTime?)null : Dr.GetDateTime(9);
                    c.Vencimento = Dr.IsDBNull(10) ? (Int32?)null : Dr.GetInt32(10);
                    c.StatusNR = Dr.GetString(11);
                    c.Elegivel = Dr.GetBoolean(12);
                    c.SituacaoEfetivo = Dr.GetString(13);
                    lista.Add(c);
                }

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao carregar a lista: " + e.Message);
            }
        }

        public Colaborador FindById(int Id)
        {
            try
            {
                OpenConnection();
                Cmd = new SqlCommand("SELECT [ID],[NORMATIVO],[MATRICULA],[NOME],[COORDENACAO],[SITUACAO],[VEICULO],[DTCERTIFICACAO],[DTREVALIDACAO],[DTVIGENCIA],[ELEGIVEL], [SITUACAO_EFETIVO] FROM [vwCOLABORADOR] WHERE ID = @v1", Con);
                Cmd.Parameters.AddWithValue("@v1", Id);
                Dr = Cmd.ExecuteReader();

                if (Dr.Read())
                {
                    Colaborador c = new Colaborador();
                    c.ID = (Int32)Dr["ID"];
                    c.Normativo = Dr.GetString(1);
                    c.Matricula = Dr.GetInt32(2);
                    c.Nome = Dr.GetString(3);
                    c.Coordenacao = Dr.GetString(4);
                    c.Situacao = Dr.GetString(5);
                    c.Veiculo = Dr.IsDBNull(6) ? null : Dr.GetString(6);
                    c.DtCertificacao = Dr.IsDBNull(7) ? (DateTime?)null : Dr.GetDateTime(7);
                    c.DtRevalidacao = Dr.IsDBNull(8) ? (DateTime?)null : Dr.GetDateTime(8);
                    c.DtVigencia = Dr.IsDBNull(9) ? (DateTime?)null : Dr.GetDateTime(9);
                    c.Elegivel = Dr.GetBoolean(10);
                    c.SituacaoEfetivo = Dr.GetString(11);

                    return c;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao pesquisar Colaborador: " + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public Colaborador FindByMatricula(int Matricula)
        {
            try
            {
                OpenConnection();
                Cmd = new SqlCommand("SELECT [MATRICULA], [FUNCIONARIO], [COORDENACAO], [SITUACAO] FROM TB_FUNCIONARIOS_MTR WHERE [MATRICULA] = @v1 ", Con);
                Cmd.Parameters.AddWithValue("@v1", Matricula);
                Dr = Cmd.ExecuteReader();

                if (Dr.Read())
                {
                    Colaborador c = new Colaborador();
                    c.Matricula = (Int32)Dr["Matricula"];
                    c.Nome = (String)Dr["Funcionario"];
                    c.Coordenacao = (String)Dr["Coordenacao"];
                    c.Situacao = (String)Dr["Situacao"];

                    return c;
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

        /// <summary>
        /// Método usado na página IncluirParticipante.aspx, ao incluir usuário em turma de treinamento.
        /// </summary>
        /// <param name="Matricula"></param>
        /// <param name="Normativo"></param>
        /// <returns></returns>
        public Colaborador FindColaborador(int Matricula, string Normativo)
        {
            try
            {
                OpenConnection();
                Cmd = new SqlCommand("SELECT [MATRICULA], [NOME], [COORDENACAO], [SITUACAO] FROM [vwCOLABORADOR] WHERE [MATRICULA] = @v1 AND [NORMATIVO] = @v2 AND [ELEGIVEL] = 1", Con);
                Cmd.Parameters.AddWithValue("@v1", Matricula);
                Cmd.Parameters.AddWithValue("@v2", Normativo);
                Dr = Cmd.ExecuteReader();

                if (Dr.Read())
                {
                    Colaborador c = new Colaborador();
                    c.Matricula = (Int32)Dr["MATRICULA"];
                    c.Nome = (String)Dr["NOME"];
                    c.Coordenacao = (String)Dr["COORDENACAO"];
                    c.Situacao = (String)Dr["SITUACAO"];

                    return c;
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

        /// <summary>
        /// Méotodo criado para usar no campo de pesquisar na página CadastrarColab.aspx.
        /// </summary>
        /// <param name="Matricula"></param>
        /// <returns></returns>
        public List<Colaborador> FindColab(int Matricula)
        {
            try
            {
                string sql = (@"SELECT [ID],[NORMATIVO],[MATRICULA],[NOME],[COORDENACAO],[SITUACAO],[VEICULO],[DTCERTIFICACAO],[DTREVALIDACAO],[DTVIGENCIA],[VENCIMENTO],[STATUSNR],[ELEGIVEL] 
                                FROM vwCOLABORADOR WHERE [MATRICULA] = @v1");

                Cmd = new SqlCommand(sql, Con);
                Cmd.Parameters.AddWithValue("@v1", Matricula);

                OpenConnection();
                Cmd.CommandText = sql;
                Cmd.Connection = Con;
                Dr = Cmd.ExecuteReader();

                List<Colaborador> ListaC = new List<Colaborador>();

                while (Dr.Read())
                {
                    Colaborador c = new Colaborador();
                    c.ID = (Int32)Dr["ID"];
                    c.Normativo = Dr.GetString(1);
                    c.Matricula = Dr.GetInt32(2);
                    c.Nome = Dr.GetString(3);
                    c.Coordenacao = Dr.GetString(4);
                    c.Situacao = Dr.GetString(5);
                    c.Veiculo = Dr.GetString(6);
                    c.DtCertificacao = Dr.IsDBNull(7) ? (DateTime?)null : Dr.GetDateTime(7);
                    c.DtRevalidacao = Dr.IsDBNull(8) ? (DateTime?)null : Dr.GetDateTime(8);
                    c.DtVigencia = Dr.IsDBNull(9) ? (DateTime?)null : Dr.GetDateTime(9);
                    c.Vencimento = Dr.IsDBNull(10) ? (Int32?)null : Dr.GetInt32(10);
                    c.StatusNR = Dr.GetString(11);
                    c.Elegivel = Dr.GetBoolean(12);
                    ListaC.Add(c);
                }

                return ListaC;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível executar a consulta. " + e.Message);
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

        public List<Veiculo> ListaVeiculo()
        {
            try
            {
                OpenConnection();
                Cmd = new SqlCommand("SELECT * FROM [TB_VEICULOS] WHERE [ATIVO] = 1 ORDER BY [VEICULO]", Con);
                Dr = Cmd.ExecuteReader();
                //Método para popular do DropDownList de Veículos
                List<Veiculo> listaV = new List<Veiculo>();

                while (Dr.Read())
                {
                    Veiculo v = new Veiculo();
                    v.IdVeiculo = (Int32)Dr["ID"];
                    v.Descricao = (String)Dr["VEICULO"];
                    listaV.Add(v);
                }

                return listaV;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao listar veículos: " + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public List<SituacaoNR> ListaSituacaoNR()
        {
            try
            {
                OpenConnection();
                Cmd = new SqlCommand("SELECT [ID],[DESCRICAO],[ATIVO] FROM [TB_SITUACAONR] WHERE [ATIVO] = 1 ORDER BY [DESCRICAO]", Con);
                Dr = Cmd.ExecuteReader();

                List<SituacaoNR> listasnr = new List<SituacaoNR>();

                while (Dr.Read())
                {
                    SituacaoNR snr = new SituacaoNR();
                    snr.IdSituacao = (Int32)Dr["ID"];
                    snr.Descricao = (String)Dr["DESCRICAO"];
                    snr.Ativo = (Boolean)Dr["ATIVO"];
                    listasnr.Add(snr);
                }
                return listasnr;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi ssível executar a ação: " + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        #region Métodos da página ConsultarColabCoord.aspx
        /// <summary>
        /// Lista usada na página ConsultarColabCoord.aspx
        /// Pacote de atualização do dia 27/10/2021
        /// </summary>
        /// <returns></returns>
        public List<Colaborador> ListAll()
        {
            try
            {
                OpenConnection();
                Cmd = new SqlCommand(@"SELECT DISTINCT [MATRICULA],[NOME],[COORDENACAO],[SITUACAO],[SITUACAO_EFETIVO] FROM vwCOLABORADOR ORDER BY COORDENACAO, MATRICULA", Con);
                Dr = Cmd.ExecuteReader();

                List<Colaborador> lista = new List<Colaborador>();

                while (Dr.Read())
                {
                    Colaborador c = new Colaborador();
                    c.Matricula = (Int32)Dr["Matricula"];
                    c.Nome = (String)Dr["Nome"];
                    c.Coordenacao = (String)Dr["Coordenacao"];
                    c.Situacao = (String)Dr["Situacao"];
                    c.SituacaoEfetivo = (String)Dr["Situacao_Efetivo"];

                    lista.Add(c);
                }

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao carregar a lista: " + e.Message);
            }
        }

        /// <summary>
        /// Lista usada na página ConsultarColabCoord.aspx
        /// Pacote de atualização do dia 27/10/2021
        /// </summary>
        /// <returns></returns>
        public List<Colaborador> findMatriculaColabCoord(int Matricula, string Funcionario, string Coordenacao)
        {
            try
            {
                string sql = (@"SELECT DISTINCT [MATRICULA],[NOME],[COORDENACAO],[SITUACAO],[SITUACAO_EFETIVO] FROM vwCOLABORADOR ");
                Cmd = new SqlCommand(sql, Con);

                OpenConnection();
                if (int.Parse(Matricula.ToString()) != 0 && string.IsNullOrEmpty(Funcionario) && string.IsNullOrEmpty(Coordenacao))
                {
                    sql += " WHERE [MATRICULA] = @v1 ORDER BY COORDENACAO, MATRICULA";
                    Cmd.Parameters.AddWithValue("@v1", Matricula);
                }
                else if (int.Parse(Matricula.ToString()) == 0 && !string.IsNullOrEmpty(Funcionario) && string.IsNullOrEmpty(Coordenacao))
                {
                    sql += " WHERE [NOME] LIKE '%' + @v1 + '%' ORDER BY COORDENACAO, MATRICULA";
                    Cmd.Parameters.AddWithValue("@v1", Funcionario);
                }
                else if (int.Parse(Matricula.ToString()) == 0 && string.IsNullOrEmpty(Funcionario) && !string.IsNullOrEmpty(Coordenacao))
                {
                    sql += " WHERE [COORDENACAO] LIKE '%' + @v1 + '%' ORDER BY COORDENACAO, MATRICULA";
                    Cmd.Parameters.AddWithValue("@v1", Coordenacao);
                }
                else if (int.Parse(Matricula.ToString()) == 0 && !string.IsNullOrEmpty(Funcionario) && !string.IsNullOrEmpty(Coordenacao))
                {
                    sql += " WHERE [NOME] LIKE '%' + @v1 + '%' AND [COORDENACAO] LIKE '%' + @v2 + '%' ORDER BY COORDENACAO, MATRICULA";
                    Cmd.Parameters.AddWithValue("@v1", Funcionario);
                    Cmd.Parameters.AddWithValue("@v2", Coordenacao);
                }

                OpenConnection();
                Cmd.CommandText = sql;
                Cmd.Connection = Con;
                Dr = Cmd.ExecuteReader();

                List<Colaborador> ListaC = new List<Colaborador>();

                while (Dr.Read())
                {
                    Colaborador c = new Colaborador();
                    c.Matricula = (Int32)Dr["Matricula"];
                    c.Nome = (String)Dr["Nome"];
                    c.Coordenacao = (String)Dr["Coordenacao"];
                    c.Situacao = (String)Dr["Situacao"];
                    c.SituacaoEfetivo = (String)Dr["Situacao_Efetivo"];
                    ListaC.Add(c);
                }

                return ListaC;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível executar a consulta. " + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
        
        /// <summary>
        /// Consulta usada na página ConsultarColabCoord.aspx
        /// Pacote de atualização do dia 27/10/2021
        /// </summary>
        /// <returns></returns>
        public Colaborador FindByMatriculaCoordenacao(int Matricula, string Coordenacao)
        {
            try
            {
                OpenConnection();
                Cmd = new SqlCommand("SELECT [MATRICULA],[NOME],[COORDENACAO],[SITUACAO], [SITUACAO_EFETIVO] FROM vwCOLABORADOR WHERE [MATRICULA] = @v1 AND [COORDENACAO] = @v2", Con);
                Cmd.Parameters.AddWithValue("@v1", Matricula);
                Cmd.Parameters.AddWithValue("@v2", Coordenacao);
                Dr = Cmd.ExecuteReader();

                if (Dr.Read())
                {
                    Colaborador c = new Colaborador();
                    c.Matricula = (Int32)Dr["Matricula"];
                    c.Nome = (String)Dr["Nome"];
                    c.Coordenacao = (String)Dr["Coordenacao"];
                    c.Situacao = (String)Dr["Situacao"];
                    c.SituacaoEfetivo = (String)Dr["Situacao_Efetivo"];

                    return c;
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

        /// <summary>
        /// Consulta usada na página ConsultarColabCoord.aspx
        /// Pacote de atualização do dia 27/10/2021
        /// </summary>
        /// <returns></returns>
        public void UpdateColabCoord(Colaborador c)
        {
            try
            {
                OpenConnection();
                Tr = Con.BeginTransaction();
                Cmd = new SqlCommand("UPDATE [TB_COLABORADOR] SET [COORDENACAO] = @v1, [SITUACAO] = @v3 WHERE MATRICULA = @v2", Con, Tr);
                Cmd.Parameters.AddWithValue("@v1", c.Coordenacao);
                Cmd.Parameters.AddWithValue("@v2", c.Matricula);
                Cmd.Parameters.AddWithValue("@v3", c.Situacao);
                Cmd.ExecuteNonQuery();
                Tr.Commit();
            }
            catch (Exception e)
            {
                if (Tr != null)
                {
                    Tr.Rollback();
                }

                throw new Exception("Erro ao atualizar a coordenação: " + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
        #endregion        

        #region Métodos usados nas consultas
        /// <summary>
        /// Método usado na página ConsultarNR.aspx
        /// </summary>
        /// <param name="Matricula"></param>
        /// <param name="Colaborador"></param>
        /// <returns></returns>        
        public List<Colaborador> FindELEGIVEL(int Matricula, string Colaborador)
        {
            try
            {
                string sql = (@"SELECT [NORMATIVO],[MATRICULA],[NOME],[COORDENACAO],[DTCERTIFICACAO],[DTREVALIDACAO],[DTVIGENCIA],[SITUACAO],[STATUSNR],[ELEGIVEL],[VEICULO] 
                                FROM vwCOLABORADOR WHERE [ELEGIVEL] = 1");

                Cmd = new SqlCommand(sql, Con);

                if (string.IsNullOrEmpty(Colaborador) && int.Parse(Matricula.ToString()) != 0)
                {
                    sql += " AND [MATRICULA] = @v1 AND [SITUACAO] <> 'DESLIGADO'";
                    Cmd.Parameters.AddWithValue("@v1", Matricula);
                }
                else if (!string.IsNullOrEmpty(Colaborador) && int.Parse(Matricula.ToString()) == 0)
                {
                    sql += " AND [NOME] LIKE '%' + @v1 + '%' AND [SITUACAO] <> 'DESLIGADO'";
                    Cmd.Parameters.AddWithValue("@v1", Colaborador);
                }
                else
                {
                    sql += " AND [MATRICULA] = @v1 AND [NOME] LIKE '%' + @v2 + '%' AND [SITUACAO] <> 'DESLIGADO'";
                    Cmd.Parameters.AddWithValue("@v1", Matricula);
                    Cmd.Parameters.AddWithValue("@v2", Colaborador);
                }

                OpenConnection();
                Cmd.CommandText = sql;
                Cmd.Connection = Con;
                Dr = Cmd.ExecuteReader();

                List<Colaborador> ListaC = new List<Colaborador>();

                while (Dr.Read())
                {
                    Colaborador c = new Colaborador();
                    c.Normativo = Dr.GetString(0);
                    c.Matricula = Dr.GetInt32(1);
                    c.Nome = Dr.GetString(2);
                    c.Coordenacao = Dr.GetString(3);
                    c.DtCertificacao = Dr.IsDBNull(4) ? (DateTime?)null : Dr.GetDateTime(4);
                    c.DtRevalidacao = Dr.IsDBNull(5) ? (DateTime?)null : Dr.GetDateTime(5);
                    c.DtVigencia = Dr.IsDBNull(6) ? (DateTime?)null : Dr.GetDateTime(6);
                    c.Situacao = (String)Dr["SITUACAO"];
                    c.StatusNR = (String)Dr["STATUSNR"];
                    c.Elegivel = (Boolean)Dr["ELEGIVEL"];
                    c.Veiculo = (String)Dr["VEICULO"];
                    ListaC.Add(c);
                }

                return ListaC;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível executar a consulta. " + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        /// <summary>
        /// Método usado na página NRVigente.aspx
        /// </summary>
        /// <param name="Normativo"></param>
        /// <param name="Coordenacao"></param>
        /// <returns></returns>
        public List<Colaborador> FindNRVigente(string Normativo, string Coordenacao)
        {
            try
            {
                string sql = (@"SELECT [NORMATIVO],[MATRICULA],[NOME],[COORDENACAO],[DTCERTIFICACAO],[DTREVALIDACAO],[DTVIGENCIA],[SITUACAO],[STATUSNR],[ELEGIVEL],[VEICULO] FROM vwCOLABORADOR
                                        WHERE [STATUSNR] = 'VIGENTE' AND [SITUACAO] <> 'DESLIGADO' AND [ELEGIVEL] = 1");

                Cmd = new SqlCommand(sql, Con);

                if (!string.IsNullOrEmpty(Normativo) && string.IsNullOrEmpty(Coordenacao))
                {
                    sql += "  AND [NORMATIVO] = '' + @v1 + ''";
                    Cmd.Parameters.AddWithValue("@v1", Normativo);
                }
                else if (string.IsNullOrEmpty(Normativo) && !string.IsNullOrEmpty(Coordenacao))
                {
                    sql += " AND [COORDENACAO] LIKE '%' + @v1 + '%'";
                    Cmd.Parameters.AddWithValue("@v1", Coordenacao);
                }
                else
                {
                    sql += " AND [NORMATIVO] = '' + @v1 + '' AND [COORDENACAO] LIKE '%' + @v2 + '%'";
                    Cmd.Parameters.AddWithValue("@v1", Normativo);
                    Cmd.Parameters.AddWithValue("@v2", Coordenacao);
                }

                OpenConnection();
                Cmd.CommandText = sql;
                Cmd.Connection = Con;
                Dr = Cmd.ExecuteReader();

                List<Colaborador> ListaC = new List<Colaborador>();

                while (Dr.Read())
                {
                    Colaborador c = new Colaborador();
                    c.Normativo = Dr.GetString(0);
                    c.Matricula = Dr.GetInt32(1);
                    c.Nome = Dr.GetString(2);
                    c.Coordenacao = Dr.GetString(3);
                    c.DtCertificacao = Dr.IsDBNull(4) ? (DateTime?)null : Dr.GetDateTime(4);
                    c.DtRevalidacao = Dr.IsDBNull(5) ? (DateTime?)null : Dr.GetDateTime(5);
                    c.DtVigencia = Dr.IsDBNull(6) ? (DateTime?)null : Dr.GetDateTime(6);
                    c.Situacao = (String)Dr["SITUACAO"];
                    c.StatusNR = (String)Dr["STATUSNR"];
                    c.Elegivel = (Boolean)Dr["ELEGIVEL"];
                    c.Veiculo = (String)Dr["VEICULO"];
                    ListaC.Add(c);
                }

                return ListaC;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível executar a consulta. " + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        /// <summary>
        /// Método usado na página AguardandoCertif.aspx
        /// </summary>
        /// <param name="Normativo"></param>
        /// <param name="Coordenacao"></param>
        /// <returns></returns>
        public List<Colaborador> FindNRAguardando(string Normativo, string Coordenacao)
        {
            try
            {
                string sql = (@"SELECT [NORMATIVO],[MATRICULA],[NOME],[COORDENACAO],[DTCERTIFICACAO],[DTREVALIDACAO],[DTVIGENCIA],[SITUACAO],[STATUSNR],[ELEGIVEL],[VEICULO] FROM vwCOLABORADOR
                                        WHERE [STATUSNR] = 'AGUARDANDO CERTIFICAÇÃO' AND [SITUACAO] <> 'DESLIGADO' AND [ELEGIVEL] = 1");

                Cmd = new SqlCommand(sql, Con);

                if (!string.IsNullOrEmpty(Normativo) && string.IsNullOrEmpty(Coordenacao))
                {
                    sql += "  AND [NORMATIVO] = @v1";
                    Cmd.Parameters.AddWithValue("@v1", Normativo);
                }
                else if (string.IsNullOrEmpty(Normativo) && !string.IsNullOrEmpty(Coordenacao))
                {
                    sql += " AND [COORDENACAO] LIKE '%' + @v1 + '%'";
                    Cmd.Parameters.AddWithValue("@v1", Coordenacao);
                }
                else
                {
                    sql += " AND [NORMATIVO] =  @v1 AND [COORDENACAO] LIKE '%' + @v2 + '%'";
                    Cmd.Parameters.AddWithValue("@v1", Normativo);
                    Cmd.Parameters.AddWithValue("@v2", Coordenacao);
                }

                OpenConnection();
                Cmd.CommandText = sql;
                Cmd.Connection = Con;
                Dr = Cmd.ExecuteReader();

                List<Colaborador> ListaC = new List<Colaborador>();

                while (Dr.Read())
                {
                    Colaborador c = new Colaborador();
                    c.Normativo = Dr.GetString(0);
                    c.Matricula = Dr.GetInt32(1);
                    c.Nome = Dr.GetString(2);
                    c.Coordenacao = Dr.GetString(3);
                    c.DtCertificacao = Dr.IsDBNull(4) ? (DateTime?)null : Dr.GetDateTime(4);
                    c.DtRevalidacao = Dr.IsDBNull(5) ? (DateTime?)null : Dr.GetDateTime(5);
                    c.DtVigencia = Dr.IsDBNull(6) ? (DateTime?)null : Dr.GetDateTime(6);
                    c.Situacao = (String)Dr["SITUACAO"];
                    c.StatusNR = (String)Dr["STATUSNR"];
                    c.Elegivel = (Boolean)Dr["ELEGIVEL"];
                    c.Veiculo = (String)Dr["VEICULO"];
                    ListaC.Add(c);
                }

                return ListaC;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível executar a consulta. " + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        /// <summary>
        /// Método usado na página NRExpirado.aspx
        /// </summary>
        /// <param name="Normativo"></param>
        /// <param name="Coordenacao"></param>
        /// <returns></returns>
        public List<Colaborador> FindNRExpirado(string Normativo, string Coordenacao)
        {
            try
            {
                string sql = (@"SELECT [NORMATIVO],[MATRICULA],[NOME],[COORDENACAO],[DTCERTIFICACAO],[DTREVALIDACAO],[DTVIGENCIA],[SITUACAO],[STATUSNR],[ELEGIVEL],[VEICULO] FROM vwCOLABORADOR
                                        WHERE [STATUSNR] = 'EXPIRADO' AND [SITUACAO] <> 'DESLIGADO' AND [ELEGIVEL] = 1");

                Cmd = new SqlCommand(sql, Con);

                if (!string.IsNullOrEmpty(Normativo) && string.IsNullOrEmpty(Coordenacao))
                {
                    sql += "  AND [NORMATIVO] = '' + @v1 + ''";
                    Cmd.Parameters.AddWithValue("@v1", Normativo);
                }
                else if (string.IsNullOrEmpty(Normativo) && !string.IsNullOrEmpty(Coordenacao))
                {
                    sql += " AND [COORDENACAO] LIKE '%' + @v1 + '%'";
                    Cmd.Parameters.AddWithValue("@v1", Coordenacao);
                }
                else
                {
                    sql += " AND [NORMATIVO] = '' + @v1 + '' AND [COORDENACAO] LIKE '%' + @v2 + '%'";
                    Cmd.Parameters.AddWithValue("@v1", Normativo);
                    Cmd.Parameters.AddWithValue("@v2", Coordenacao);
                }

                OpenConnection();
                Cmd.CommandText = sql;
                Cmd.Connection = Con;
                Dr = Cmd.ExecuteReader();

                List<Colaborador> ListaC = new List<Colaborador>();

                while (Dr.Read())
                {
                    Colaborador c = new Colaborador();
                    c.Normativo = Dr.GetString(0);
                    c.Matricula = Dr.GetInt32(1);
                    c.Nome = Dr.GetString(2);
                    c.Coordenacao = Dr.GetString(3);
                    c.DtCertificacao = Dr.IsDBNull(4) ? (DateTime?)null : Dr.GetDateTime(4);
                    c.DtRevalidacao = Dr.IsDBNull(5) ? (DateTime?)null : Dr.GetDateTime(5);
                    c.DtVigencia = Dr.IsDBNull(6) ? (DateTime?)null : Dr.GetDateTime(6);
                    c.Situacao = (String)Dr["SITUACAO"];
                    c.StatusNR = (String)Dr["STATUSNR"];
                    c.Elegivel = (Boolean)Dr["ELEGIVEL"];
                    c.Veiculo = (String)Dr["VEICULO"];
                    ListaC.Add(c);
                }

                return ListaC;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível executar a consulta. " + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        /// <summary>
        /// Método usado na página NRaVEncer.aspx
        /// </summary>
        /// <param name="Normativo"></param>
        /// <param name="Coordenacao"></param>
        /// <returns></returns>
        public List<Colaborador> FindNRaVencer(string Normativo, string Coordenacao)
        {
            try
            {
                string sql = (@"SELECT [NORMATIVO],[MATRICULA],[NOME],[COORDENACAO],[DTCERTIFICACAO],[DTREVALIDACAO],[DTVIGENCIA],[SITUACAO],[STATUSNR],[ELEGIVEL],[VEICULO] FROM vwCOLABORADOR
                                        WHERE [STATUSNR] = 'À VENCER' AND [SITUACAO] <> 'DESLIGADO' AND [ELEGIVEL] = 1");

                Cmd = new SqlCommand(sql, Con);

                if (!string.IsNullOrEmpty(Normativo) && string.IsNullOrEmpty(Coordenacao))
                {
                    sql += "  AND [NORMATIVO] = '' + @v1 + ''";
                    Cmd.Parameters.AddWithValue("@v1", Normativo);
                }
                else if (string.IsNullOrEmpty(Normativo) && !string.IsNullOrEmpty(Coordenacao))
                {
                    sql += " AND [COORDENACAO] LIKE '%' + @v1 + '%'";
                    Cmd.Parameters.AddWithValue("@v1", Coordenacao);
                }
                else
                {
                    sql += " AND [NORMATIVO] = '' + @v1 + '' AND [COORDENACAO] LIKE '%' + @v2 + '%'";
                    Cmd.Parameters.AddWithValue("@v1", Normativo);
                    Cmd.Parameters.AddWithValue("@v2", Coordenacao);
                }

                OpenConnection();
                Cmd.CommandText = sql;
                Cmd.Connection = Con;
                Dr = Cmd.ExecuteReader();

                List<Colaborador> ListaC = new List<Colaborador>();

                while (Dr.Read())
                {
                    Colaborador c = new Colaborador();
                    c.Normativo = Dr.GetString(0);
                    c.Matricula = Dr.GetInt32(1);
                    c.Nome = Dr.GetString(2);
                    c.Coordenacao = Dr.GetString(3);
                    c.DtCertificacao = Dr.IsDBNull(4) ? (DateTime?)null : Dr.GetDateTime(4);
                    c.DtRevalidacao = Dr.IsDBNull(5) ? (DateTime?)null : Dr.GetDateTime(5);
                    c.DtVigencia = Dr.IsDBNull(6) ? (DateTime?)null : Dr.GetDateTime(6);
                    c.Situacao = (String)Dr["SITUACAO"];
                    c.StatusNR = (String)Dr["STATUSNR"];
                    c.Elegivel = (Boolean)Dr["ELEGIVEL"];
                    c.Veiculo = (String)Dr["VEICULO"];
                    ListaC.Add(c);
                }

                return ListaC;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível executar a consulta. " + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
        #endregion

        #region Métodos usados nos relatorios
        /// <summary>
        /// Método criado para o relatório rdlVigentesExpirados
        /// </summary>
        /// <param name="Coordenacao"></param>
        /// <returns></returns>
        public List<Colaborador> FindVigenteExpirado(string Coordenacao)
        {
            try
            {
                OpenConnection();
                Cmd = new SqlCommand(@"SELECT [NORMATIVO],[MATRICULA],[NOME],[COORDENACAO],[DTCERTIFICACAO],[DTREVALIDACAO],[DTVIGENCIA],[STATUSNR],[ELEGIVEL],[VEICULO]
                                       FROM vwCOLABORADOR 
                                       WHERE [COORDENACAO] LIKE '%' + @v1 + '%' AND [SITUACAO] <> 'DESLIGADO' AND [STATUSNR] IN ('VIGENTE','À VENCER','EXPIRADO')", Con);
                Cmd.Parameters.AddWithValue("@v1", Coordenacao);

                Dr = Cmd.ExecuteReader();

                List<Colaborador> ListaC = new List<Colaborador>();

                while (Dr.Read())
                {
                    Colaborador c = new Colaborador();
                    c.Normativo = Dr.GetString(0);
                    c.Matricula = Dr.GetInt32(1);
                    c.Nome = Dr.GetString(2);
                    c.Coordenacao = Dr.GetString(3);
                    c.DtCertificacao = Dr.IsDBNull(4) ? (DateTime?)null : Dr.GetDateTime(4);
                    c.DtRevalidacao = Dr.IsDBNull(5) ? (DateTime?)null : Dr.GetDateTime(5);
                    c.DtVigencia = Dr.IsDBNull(6) ? (DateTime?)null : Dr.GetDateTime(6);
                    c.StatusNR = (String)Dr["STATUSNR"];
                    c.Elegivel = (Boolean)Dr["ELEGIVEL"];
                    c.Veiculo = (String)Dr["VEICULO"];
                    ListaC.Add(c);
                }

                return ListaC;
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

        /// <summary>
        /// Método criado para o relatório rdlPresentesAusentes
        /// </summary>
        /// <param name="DataIni"></param>
        /// <param name="DataFim"></param>
        /// <param name="Coordenacao"></param>
        /// <returns></returns>
        public List<TurmasParticpantes> FindAusentePresente(string DataIni, string DataFim, string Coordenacao)
        {
            try
            {
                string sql = (@"SELECT [ID],[ID_TURMA],[DTTURMA],[MATRICULA],[NOME],[COORDENACAO],[PRESENCA] FROM [dbo].[vwTURMASPARTICIPANTES]");

                Cmd = new SqlCommand(sql, Con);

                sql += " WHERE [DTTURMA] BETWEEN @v1 AND @v2 AND [COORDENACAO] LIKE '%'+ @v3 +'%'";
                Cmd.Parameters.AddWithValue("@v1", DataIni);
                Cmd.Parameters.AddWithValue("@v2", DataFim);
                Cmd.Parameters.AddWithValue("@v3", Coordenacao);

                OpenConnection();
                Cmd.CommandText = sql;
                Cmd.Connection = Con;
                Dr = Cmd.ExecuteReader();

                List<TurmasParticpantes> ListaT = new List<TurmasParticpantes>();

                while (Dr.Read())
                {
                    TurmasParticpantes t = new TurmasParticpantes();
                    t.turmaRel = new Turmas();
                    t.IdParticipante = (Int32)Dr["ID"];
                    t.turmaRel.IdTurma = (Int32)Dr["ID_TURMA"];
                    t.turmaRel.dtTurma = (DateTime)Dr["DTTURMA"];
                    t.Matricula = (Int32)Dr["MATRICULA"];
                    t.Nome = (String)Dr["NOME"];
                    t.Coordenacao = (String)Dr["COORDENACAO"];
                    t.Presenca = (Boolean)Dr["PRESENCA"];
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

        /// <summary>
        /// Método criado para o relatório de NRGeral
        /// </summary>
        /// <param name="Normativo"></param>
        /// <returns></returns>
        public List<Colaborador> FindGeralNR(string Normativo, string Coordenacao)
        {
            try
            {
                string sql = (@"SELECT [ID],[NORMATIVO],[MATRICULA],[NOME],[COORDENACAO],[SITUACAO],[VEICULO],[DTCERTIFICACAO],[DTREVALIDACAO],[DTVIGENCIA],[VENCIMENTO],[STATUSNR],[ELEGIVEL] FROM vwCOLABORADOR 
                                 WHERE [SITUACAO] <> 'DESLIGADO' ");

                Cmd = new SqlCommand(sql, Con);

                if (!string.IsNullOrEmpty(Normativo) && string.IsNullOrEmpty(Coordenacao))
                {
                    sql += "AND [NORMATIVO] = '' + @v1 + ''";
                    Cmd.Parameters.AddWithValue("@v1", Normativo);
                }
                else if (string.IsNullOrEmpty(Normativo) && !string.IsNullOrEmpty(Coordenacao))
                {
                    sql += "AND [COORDENACAO] LIKE '%' + @v1 + '%'";
                    Cmd.Parameters.AddWithValue("@v1", Coordenacao);
                }
                else
                {
                    sql += "AND [NORMATIVO] = '' + @v1 + '' AND [COORDENACAO] LIKE '%' + @v2 + '%'";
                    Cmd.Parameters.AddWithValue("@v1", Normativo);
                    Cmd.Parameters.AddWithValue("@v2", Coordenacao);
                }

                OpenConnection();
                Cmd.CommandText = sql;
                Cmd.Connection = Con;
                Dr = Cmd.ExecuteReader();
                
                List<Colaborador> ListaC = new List<Colaborador>();

                while (Dr.Read())
                {
                    Colaborador c = new Colaborador();
                    c.ID = (Int32)Dr["ID"];
                    c.Normativo = Dr.GetString(1);
                    c.Matricula = Dr.GetInt32(2);
                    c.Nome = Dr.GetString(3);
                    c.Coordenacao = Dr.GetString(4);
                    c.Situacao = Dr.GetString(5);
                    c.Veiculo = Dr.GetString(6);
                    c.DtCertificacao = Dr.IsDBNull(7) ? (DateTime?)null : Dr.GetDateTime(7);
                    c.DtRevalidacao = Dr.IsDBNull(8) ? (DateTime?)null : Dr.GetDateTime(8);
                    c.DtVigencia = Dr.IsDBNull(9) ? (DateTime?)null : Dr.GetDateTime(9);
                    c.Vencimento = Dr.IsDBNull(10) ? (Int32?)null : Dr.GetInt32(10);
                    c.StatusNR = Dr.GetString(11);
                    c.Elegivel = Dr.GetBoolean(12);
                    ListaC.Add(c);
                }

                return ListaC;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível gerar o relatório: " + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
        #endregion

        #region Métodos de exportação
        public List<Colaborador> ExportColaborador(string Normativo, string Coordenacao)
        {
            try
            {
                string sql = (@"SELECT [ID],[NORMATIVO],[MATRICULA],[NOME],[COORDENACAO],[SITUACAO],[VEICULO],[DTCERTIFICACAO],[DTREVALIDACAO],[DTVIGENCIA],[VENCIMENTO],[STATUSNR],[ELEGIVEL] FROM vwCOLABORADOR 
                                 WHERE [SITUACAO] <> 'DESLIGADO' ");

                Cmd = new SqlCommand(sql, Con);

                if (!string.IsNullOrEmpty(Normativo) && string.IsNullOrEmpty(Coordenacao))
                {
                    sql += "AND [NORMATIVO] = '' + @v1 + ''";
                    Cmd.Parameters.AddWithValue("@v1", Normativo);
                }
                else if (string.IsNullOrEmpty(Normativo) && !string.IsNullOrEmpty(Coordenacao))
                {
                    sql += "AND [COORDENACAO] LIKE '%' + @v1 + '%'";
                    Cmd.Parameters.AddWithValue("@v1", Coordenacao);
                }
                else
                {
                    sql += "AND [NORMATIVO] = '' + @v1 + '' AND [COORDENACAO] LIKE '%' + @v2 + '%'";
                    Cmd.Parameters.AddWithValue("@v1", Normativo);
                    Cmd.Parameters.AddWithValue("@v2", Coordenacao);
                }

                OpenConnection();
                Cmd.CommandText = sql;
                Cmd.Connection = Con;
                Dr = Cmd.ExecuteReader();

                List<Colaborador> listaC = new List<Colaborador>();

                while (Dr.Read())
                {
                    Colaborador c = new Colaborador();
                    c.ID = (Int32)Dr["ID"];
                    c.Normativo = Dr.GetString(1);
                    c.Matricula = Dr.GetInt32(2);
                    c.Nome = Dr.GetString(3);
                    c.Coordenacao = Dr.GetString(4);
                    c.Situacao = Dr.GetString(5);
                    c.Veiculo = Dr.GetString(6);
                    c.DtCertificacao = Dr.IsDBNull(7) ? (DateTime?)null : Dr.GetDateTime(7);
                    c.DtRevalidacao = Dr.IsDBNull(8) ? (DateTime?)null : Dr.GetDateTime(8);
                    c.DtVigencia = Dr.IsDBNull(9) ? (DateTime?)null : Dr.GetDateTime(9);
                    c.Vencimento = Dr.IsDBNull(10) ? (Int32?)null : Dr.GetInt32(10);                    
                    c.StatusNR = Dr.GetString(11);
                    c.Elegivel = Dr.GetBoolean(12);
                    listaC.Add(c);
                }
                return listaC;
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
        #endregion
         
    }
}
