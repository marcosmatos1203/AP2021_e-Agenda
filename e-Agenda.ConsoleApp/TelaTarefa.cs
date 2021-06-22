using e_Agenda.ConsoleApp.shared;
using e_Agenda.Controladores;
using e_Agenda.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Agenda.ConsoleApp
{
    public class TelaTarefa : TelaCadastroBasico<Tarefa>, ICadastravel
    {
        public TelaTarefa(ControladorTarefa controlador)
           : base("Cadastro de Tarefas", controlador)
        {
            this.controlador = controlador;
        }
        public override Tarefa ObterRegistro(TipoAcao tipoAcao)
        {
            Console.Write("Digite o titulo da tarefa: ");
            string titulo = Console.ReadLine();

            Console.Write("Prioridade da tarefa:\nDigite 1-Baixa, 3-Media, 5-Alta : ");
            int prioridade = Convert.ToInt32(Console.ReadLine());

            Console.Write("Digite a data de criação da tarefa: ");
            DateTime dataCriacao = Convert.ToDateTime(Console.ReadLine());

            return new Tarefa(titulo, prioridade, dataCriacao);
        }

        protected override void ConfiguraTela(List<Tarefa> registros)
        {
            string configuracaColunasTabela = "{0,-5} | {1,-25} | {2,-15} | {3,-15} | {4,-15} | {5,-15}";

            MontarCabecalhoTabela(configuracaColunasTabela, "Id", "Titulo", "Prioridade", "DataCriacao", "% Concluido", "DataConclusao");

            foreach (Tarefa tarefa in registros)
            {
                Console.WriteLine(configuracaColunasTabela, tarefa.Id, tarefa.Titulo, tarefa.Prioridade, tarefa.DataCriacao.ToShortDateString(), tarefa.PercentualConcluido, tarefa.DataConclusao.ToShortDateString());
            }
        }
        public override string ObterOpcao()
        {
            Console.WriteLine("Digite 1 para registrar tarefa");
            Console.WriteLine("Digite 2 para editar tarefa");
            Console.WriteLine("Digite 3 para excluir tarefa");
            Console.WriteLine("Digite 4 para visualizar tarefas pendentes");
            Console.WriteLine("Digite 5 para visualizar tarefas fechadas");

            Console.WriteLine("Digite S para Voltar");
            Console.WriteLine();

            Console.Write("Opção: ");
            string opcao = Console.ReadLine();

            return opcao;
        }

        internal void ExcluirTarefa()
        {
            ExcluirRegistro();
        }

        internal bool VisualizarTarefasPendentes()
        {
            
                ConfigurarTela(SubtituloDeVisualizacao());

            List<Tarefa> tarefas = controlador.SelecionarTarefasAbertas();

            if (tarefas.Count == 0)
            {
                ApresentarMensagem(MensagemDeListaVazia(), TipoMensagem.Atencao);
                return false;
            }
            ConfiguraTela(tarefas);

            return true;
        }

        internal bool VisualizarTarefasFechadas()
        {
            
                ConfigurarTela(SubtituloDeVisualizacao());

            List<Tarefa> tarefas = controlador.SelecionarTarefasFechadas();

            if (tarefas.Count == 0)
            {
                ApresentarMensagem(MensagemDeListaVazia(), TipoMensagem.Atencao);
                return false;
            }
            ConfiguraTela(tarefas);

            return true;
        }

        internal void EditarTarefa()
        {
            EditarRegistro();
        }

        internal void RegistrarTarefa()
        {
            InserirNovoRegistro();
        }

        public override Tarefa AtualizarRegistro(TipoAcao tipoAcao)
        {
            Console.Write("Atualize o titulo da tarefa: ");
            string titulo = Console.ReadLine();

            Console.Write("Prioridade da tarefa:\nDigite 1:baixa, 3:Media, 5:Alta ");
            int prioridade = Convert.ToInt32(Console.ReadLine());

            Console.Write("Atualize o percentual de conclusão ");
            int percentualConcluido = Convert.ToInt32(Console.ReadLine());
            DateTime dataConclusao = new DateTime(1900, 01, 01);
            if (percentualConcluido == 100)
                dataConclusao = DateTime.Now;
            return new Tarefa(percentualConcluido, titulo, dataConclusao, prioridade);
        }
    }
}
