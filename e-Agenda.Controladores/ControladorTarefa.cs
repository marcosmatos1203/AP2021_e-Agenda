using e_Agenda.Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Agenda.Controladores
{
    public class ControladorTarefa : Controlador<Tarefa>
    {
        
        public ControladorTarefa()
        {
            
        }
        ConexaoDB conexao = new ConexaoDB();

        private const string sqlInserirTarefa =
                @"INSERT INTO TBTAREFA 
	                (
		                [PERCENTUALCONCLUIDO], 
		                [TITULO], 
		                [DATACONCLUSAO],
                        [PRIORIDADE], 
		                [DATACRIACAO]
	                ) 
	                VALUES
	                (
                        @PERCENTUALCONCLUIDO, 
                        @TITULO,
                        @DATACONCLUSAO,
		                @PRIORIDADE, 
		                @DATACRIACAO
	                );
                    SELECT SCOPE_IDENTITY();";
        private const string sqlAtualizarTarefa =
            @"UPDATE TBTAREFA
                    SET
                        [PERCENTUALCONCLUIDO] = @PERCENTUALCONCLUIDO,
		                [TITULO] = @TITULO, 
		                [DATACONCLUSAO] = @DATACONCLUSAO,
                        [PRIORIDADE] = @PRIORIDADE
                    WHERE 
                        ID = @ID";
        private const string SqlSelecaoPorId =
            @"SELECT
                        [ID],
		                [PERCENTUALCONCLUIDO], 
		                [TITULO], 
		                [DATACONCLUSAO],
                        [PRIORIDADE], 
		                [DATACRIACAO]
	                FROM
                        TBTAREFA
                    WHERE 
                        ID = @ID";
        private const string sqlExcluirTarefa =
            @"DELETE 
	                FROM
                        TBTAREFA
                    WHERE 
                        ID = @ID";
        private const string sqlListarTarefas =
            @"SELECT
                        [ID],
		                [PERCENTUALCONCLUIDO], 
		                [TITULO], 
		                [DATACONCLUSAO],
                        [PRIORIDADE], 
		                [DATACRIACAO]
	                FROM
                        TBTAREFA";
        public override string AdicionarNovo(Tarefa tarefa)
        {
            string resultadoValidacao = tarefa.Validar();
            if (resultadoValidacao=="ITEM_VALIDO")
            {
                conexao.AbrirDB();
                SqlCommand comandoInsercao = conexao.Comando(sqlInserirTarefa);
                ParametrosInsercaoAtualizacao(tarefa, comandoInsercao);
                object id = comandoInsercao.ExecuteScalar();
                tarefa.Id = Convert.ToInt32(id);
                conexao.FecharDB();
            }
            return resultadoValidacao;
        }
        public override string Atualizar(int id, Tarefa tarefa)
        {
            string resultadoValidacao = tarefa.Validar();
            if (resultadoValidacao == "ITEM_VALIDO")
            {
                tarefa.Id = id;
                conexao.AbrirDB();
                SqlCommand comandoAtualizacao = conexao.Comando(sqlAtualizarTarefa);
                ParametrosAtualizacao(tarefa, comandoAtualizacao);
                comandoAtualizacao.ExecuteNonQuery();
                conexao.FecharDB();
            }
            return resultadoValidacao;
        }

        public override bool ExisteItem(int idPesquisado)
        {
            return SelecionarPorId(idPesquisado) != null;
        }
        public override bool ExcluirItem(int id)
        {
            conexao.AbrirDB();
            SqlCommand comandoExclusao = conexao.Comando(sqlExcluirTarefa);
            comandoExclusao.Parameters.AddWithValue("ID", id);
            int n = comandoExclusao.ExecuteNonQuery();
            conexao.FecharDB();

            return n > 0 ? true : false;
        }
        public override List<Tarefa> SelecionarTodos()
        {
            conexao.AbrirDB();
            SqlCommand comandoSelecao = conexao.Comando(sqlListarTarefas);

            List<Tarefa> tarefas = ListarTarefas(comandoSelecao);
            conexao.FecharDB();
            return tarefas;
        }
        public override Tarefa SelecionarPorId(int idPesquisado)
        {
            conexao.AbrirDB();
            SqlCommand comandoSelecaoPorId = conexao.Comando(SqlSelecaoPorId);
            comandoSelecaoPorId.Parameters.AddWithValue("ID", idPesquisado);
            SqlDataReader leitorTarefas = comandoSelecaoPorId.ExecuteReader();
            if (leitorTarefas.Read() == false)
                return null;
            Tarefa tarefa = BuscaTarefaDB(leitorTarefas);
            conexao.FecharDB();

            return tarefa;
        }

        private static Tarefa BuscaTarefaDB(SqlDataReader leitorTarefas)
        {
            int id = Convert.ToInt32(leitorTarefas["ID"]);
            int percentualConcluido = Convert.ToInt32(leitorTarefas["PERCENTUALCONCLUIDO"]);
            string titulo = Convert.ToString(leitorTarefas["TITULO"]);
            DateTime dataConclusao = Convert.ToDateTime(leitorTarefas["DATACONCLUSAO"]);
            int prioridade = Convert.ToInt32(leitorTarefas["PRIORIDADE"]);
            DateTime dataCriacao = Convert.ToDateTime(leitorTarefas["DATACRIACAO"]);

            Tarefa tarefa = new Tarefa(percentualConcluido, titulo, dataConclusao, prioridade, dataCriacao);
            tarefa.Id = id;
            return tarefa;
        }

        private static List<Tarefa> ListarTarefas(SqlCommand comandoSelecao)
        {
            SqlDataReader leitorTarefas = comandoSelecao.ExecuteReader();

            List<Tarefa> tarefas = new List<Tarefa>();

            while (leitorTarefas.Read())
            {
                int id = Convert.ToInt32(leitorTarefas["ID"]);
                int percentual = Convert.ToInt32(leitorTarefas["PERCENTUALCONCLUIDO"]);
                string titulo = Convert.ToString(leitorTarefas["TITULO"]);
                DateTime dataConclusao = Convert.ToDateTime(leitorTarefas["DATACONCLUSAO"]);
                int prioridade = Convert.ToInt32(leitorTarefas["PRIORIDADE"]);
                DateTime dataCriacao = Convert.ToDateTime(leitorTarefas["DATACRIACAO"]);

                Tarefa tarefa = new Tarefa(percentual, titulo, dataConclusao, prioridade, dataCriacao);
                tarefa.Id = id;

                tarefas.Add(tarefa);
            }

            return tarefas;
        }

        private static void ParametrosInsercaoAtualizacao(Tarefa tarefa, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", tarefa.Id);
            comando.Parameters.AddWithValue("PERCENTUALCONCLUIDO", tarefa.PercentualConcluido);
            comando.Parameters.AddWithValue("TITULO", tarefa.Titulo);
            comando.Parameters.AddWithValue("DATACONCLUSAO", tarefa.DataConclusao);
            comando.Parameters.AddWithValue("PRIORIDADE", tarefa.Prioridade);
            comando.Parameters.AddWithValue("DATACRIACAO", tarefa.DataCriacao);
        }
        private static void ParametrosAtualizacao(Tarefa tarefa, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", tarefa.Id);
            comando.Parameters.AddWithValue("PERCENTUALCONCLUIDO", tarefa.PercentualConcluido);
            comando.Parameters.AddWithValue("TITULO", tarefa.Titulo);
            comando.Parameters.AddWithValue("DATACONCLUSAO", tarefa.DataConclusao);
            comando.Parameters.AddWithValue("PRIORIDADE", tarefa.Prioridade);
        }
        
       
    }
}
