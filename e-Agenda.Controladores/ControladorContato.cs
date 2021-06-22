using e_Agenda.Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Agenda.Controladores
{
    public class ControladorContato :Controlador<Contato>
    {
        ConexaoDB conexao = new ConexaoDB();

        private const string sqlInserirContato =
                @"INSERT INTO TBCONTATO 
	                (
		                [NOME], 
		                [EMAIL], 
		                [TELEFONE],
                        [CARGO], 
		                [EMPRESA]
	                ) 
	                VALUES
	                (
                        @NOME, 
                        @EMAIL,
                        @TELEFONE,
		                @CARGO, 
		                @EMPRESA
	                );
                    SELECT SCOPE_IDENTITY();";
        private const string sqlAtualizarContato =
            @"UPDATE TBCONTATO
                    SET
                        [NOME] = @NOME,
		                [EMAIL] = @EMAIL, 
		                [TELEFONE] = @TELEFONE,
                        [CARGO] = @CARGO,
                        [EMPRESA] = @EMPRESA
                    WHERE 
                        ID = @ID";
        private const string SqlSelecaoPorId =
            @"SELECT
                        [ID],
		                [NOME], 
		                [EMAIL], 
		                [TELEFONE],
                        [CARGO], 
		                [EMPRESA]
	                FROM
                        TBCONTATO
                    WHERE 
                        ID = @ID";
        private const string sqlExcluirContato =
            @"DELETE 
	                FROM
                        TBCONTATO
                    WHERE 
                        ID = @ID";
        private const string sqlListarContatos =
            @"SELECT
                        [ID],
		                [NOME], 
		                [EMAIL], 
		                [TELEFONE],
                        [CARGO], 
		                [EMPRESA]
	                FROM
                        TBCONTATO ORDER BY CARGO;";
        public override string AdicionarNovo(Contato contato)
        {
            string resultadoValidacao = contato.Validar();
            if (resultadoValidacao == "ITEM_VALIDO")
            {
                conexao.AbrirDB();
                SqlCommand comandoInsercao = conexao.Comando(sqlInserirContato);
                ParametrosDeInsercao(contato, comandoInsercao);
                object id = comandoInsercao.ExecuteScalar();
                contato.Id = Convert.ToInt32(id);
                conexao.FecharDB();
            }
            return resultadoValidacao;
        }

       

        public override string Atualizar(int id, Contato contato)
        {
            string resultadoValidacao = contato.Validar();
            if (resultadoValidacao == "ITEM_VALIDO")
            {
                contato.Id = id;
                conexao.AbrirDB();

                SqlCommand comandoAtualizacao = conexao.Comando(sqlAtualizarContato);
                ParametrosAtualizacao(contato, comandoAtualizacao);
                comandoAtualizacao.ExecuteNonQuery();
                conexao.FecharDB();
            }
            return resultadoValidacao;
        }

        public override bool ExisteItem(int id)
        {
            return SelecionarPorId(id) != null;
        }
        public override bool ExcluirItem(int id)
        {
            conexao.AbrirDB();
            SqlCommand comandoExclusao = conexao.Comando(sqlExcluirContato);
            comandoExclusao.Parameters.AddWithValue("ID", id);
            int n = comandoExclusao.ExecuteNonQuery();
            conexao.FecharDB();

            return n > 0 ? true : false;
        }
        public override List<Contato> SelecionarTodos()
        {
            conexao.AbrirDB();
            SqlCommand comandoSelecao = conexao.Comando(sqlListarContatos);
            List<Contato> Contatos = ListarContatos(comandoSelecao);
            conexao.FecharDB();
            return Contatos;
        }

        public override Contato SelecionarPorId(int id)
        {
            conexao.AbrirDB();
            SqlCommand comandoSelecaoPorId = conexao.Comando(SqlSelecaoPorId);
            comandoSelecaoPorId.Parameters.AddWithValue("ID", id);
            SqlDataReader leitorContatos = comandoSelecaoPorId.ExecuteReader();
            if (leitorContatos.Read() == false)
                return null;
            Contato contato = BuscaContatoDB(leitorContatos);
            conexao.FecharDB();

            return contato;
        }
        private static void ParametrosAtualizacao(Contato contato, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("ID", contato.Id);
            comando.Parameters.AddWithValue("NOME", contato.Nome);
            comando.Parameters.AddWithValue("EMAIL", contato.Email);
            comando.Parameters.AddWithValue("TELEFONE", contato.Telefone);
            comando.Parameters.AddWithValue("CARGO", contato.Cargo);
            comando.Parameters.AddWithValue("EMPRESA", contato.Empresa);
        }
        private static Contato BuscaContatoDB(SqlDataReader leitorContatos)
        {
            int id = Convert.ToInt32(leitorContatos["ID"]);
            string nome = Convert.ToString(leitorContatos["NOME"]);
            string email = Convert.ToString(leitorContatos["EMAIL"]);
            string telefone = Convert.ToString(leitorContatos["TELEFONE"]);
            string cargo = Convert.ToString(leitorContatos["CARGO"]);
            string empresa = Convert.ToString(leitorContatos["EMPRESA"]);

            Contato tarefa = new Contato(nome,email,telefone,cargo,empresa);
            tarefa.Id = id;
            return tarefa;
        }
        private static void ParametrosDeInsercao(Contato contato, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("NOME", contato.Nome);
            comando.Parameters.AddWithValue("EMAIL", contato.Email);
            comando.Parameters.AddWithValue("TELEFONE", contato.Telefone);
            comando.Parameters.AddWithValue("CARGO", contato.Cargo);
            comando.Parameters.AddWithValue("EMPRESA", contato.Empresa);
        }
        private static List<Contato> ListarContatos(SqlCommand comandoSelecao)
        {
            SqlDataReader leitorContatos = comandoSelecao.ExecuteReader();

            List<Contato> contatos = new List<Contato>();

            while (leitorContatos.Read())
            {
                int id = Convert.ToInt32(leitorContatos["ID"]);
                string nome = Convert.ToString(leitorContatos["NOME"]);
                string email = Convert.ToString(leitorContatos["EMAIL"]);
                string telefone = Convert.ToString(leitorContatos["TELEFONE"]);
                string cargo = Convert.ToString(leitorContatos["CARGO"]);
                string empresa = Convert.ToString(leitorContatos["EMPRESA"]);

                Contato contato = new Contato(nome,email,telefone,cargo,empresa);
                contato.Id = id;

                contatos.Add(contato);
            }

            return contatos;
        }

        public override List<Tarefa> SelecionarTarefasFechadas()
        {
            throw new NotImplementedException();
        }

        public override List<Tarefa> SelecionarTarefasAbertas()
        {
            throw new NotImplementedException();
        }
    }
}
