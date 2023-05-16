using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortalNR.DAL.Entities;
using System.Data.SqlClient;

namespace PortalNR.DAL.Persistence
{
    public class EfetivoDAL : Conexao
    {        
        public List<Efetivo> FindAll()
        {
            try
            {
                OpenConnection();
                Cmd = new SqlCommand("SELECT * FROM vwFUNCIONARIOS_MTR", Con);
                Dr = Cmd.ExecuteReader();

                List<Efetivo> lista = new List<Efetivo>();
                while (Dr.Read())
                {
                    Efetivo e = new Efetivo();
                    e.Codigo = (Int32)Dr["CODIGO"];
                    e.Codemp = (Int32)Dr["CODEMP"];
                    e.Empresa = (String)Dr["EMPRESA"];
                    e.Matricula = (Int32)Dr["MATRICULA"];
                    e.Funcionario = (String)Dr["FUNCIONARIO"];
                    e.Cargo = (String)Dr["CARGO"];
                    e.Coordenacao = (String)Dr["COORDENACAO"];
                    e.Gerencia = (String)Dr["GERENCIA"];
                    e.CentroCusto = (String)Dr["CENTRO_CUSTO"];
                    e.DescricaoCC = (String)Dr["DESCRICAO_CC"];
                    e.Situacao = (String)Dr["SITUACAO"];
                    e.CodGH = (String)Dr["COD_GH"];               
                    e.DescGH = (String)Dr["DESC_GH"];
                    e.DtSituacao = (DateTime)Dr["DTA_SITUACAO"];
                    e.DtAdmissao = (DateTime)Dr["DTA_ADMISSAO"];      
                    e.DtAtulizado = (DateTime)Dr["DTA_ATUALIZACAO"];

                    lista.Add(e);
                }
                
                return lista;
            }
            catch(Exception e)
            {
                throw new Exception("Erro ao listar o efetivo: " + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }        

        public List<Efetivo> Find(int Matricula, string Funcionario, string Coordenacao)
        {
            try
            {
                string sql = (@"SELECT * FROM vwFUNCIONARIOS_MTR");

                Cmd = new SqlCommand(sql, Con);

                if (int.Parse(Matricula.ToString()) != 0 && string.IsNullOrEmpty(Funcionario) && string.IsNullOrEmpty(Coordenacao))
                {
                    sql += " WHERE [MATRICULA] = @v1";
                    Cmd.Parameters.AddWithValue("@v1", Matricula);
                }
                else if (int.Parse(Matricula.ToString()) == 0 && !string.IsNullOrEmpty(Funcionario) && string.IsNullOrEmpty(Coordenacao))
                {
                    sql += " WHERE [FUNCIONARIO] LIKE '%' + @v1 + '%'";
                    Cmd.Parameters.AddWithValue("@v1", Funcionario);
                }
                else if (int.Parse(Matricula.ToString()) == 0 && string.IsNullOrEmpty(Funcionario) && !string.IsNullOrEmpty(Coordenacao))
                {
                    sql += " WHERE [COORDENACAO] LIKE '%' + @v1 + '%'";
                    Cmd.Parameters.AddWithValue("@v1", Coordenacao);
                }
                else if (int.Parse(Matricula.ToString()) == 0 && !string.IsNullOrEmpty(Funcionario) && !string.IsNullOrEmpty(Coordenacao))
                {
                    sql += " WHERE [FUNCIONARIO] LIKE '%' + @v1 + '%' AND [COORDENACAO] LIKE '%' + @v2 + '%'";
                    Cmd.Parameters.AddWithValue("@v1", Funcionario);
                    Cmd.Parameters.AddWithValue("@v2", Coordenacao);
                }

                OpenConnection();
                Cmd.CommandText = sql;
                Cmd.Connection = Con;
                Dr = Cmd.ExecuteReader();

                List<Efetivo> lista = new List<Efetivo>();

                while (Dr.Read())
                {
                    Efetivo e = new Efetivo();
                    e.Codigo = (Int32)Dr["CODIGO"];
                    e.Codemp = (Int32)Dr["CODEMP"];
                    e.Empresa = (String)Dr["EMPRESA"];
                    e.Matricula = (Int32)Dr["MATRICULA"];
                    e.Funcionario = (String)Dr["FUNCIONARIO"];
                    e.Cargo = (String)Dr["CARGO"];
                    e.Coordenacao = (String)Dr["COORDENACAO"];
                    e.Gerencia = (String)Dr["GERENCIA"];
                    e.CentroCusto = (String)Dr["CENTRO_CUSTO"];
                    e.DescricaoCC = (String)Dr["DESCRICAO_CC"];
                    e.Situacao = (String)Dr["SITUACAO"];
                    e.CodGH = (String)Dr["COD_GH"];
                    e.DescGH = (String)Dr["DESC_GH"];
                    e.DtSituacao = (DateTime)Dr["DTA_SITUACAO"];
                    e.DtAdmissao = (DateTime)Dr["DTA_ADMISSAO"];
                    e.DtAtulizado = (DateTime)Dr["DTA_ATUALIZACAO"];

                    lista.Add(e);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception("Não foi possível executar a operação. " + e.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
