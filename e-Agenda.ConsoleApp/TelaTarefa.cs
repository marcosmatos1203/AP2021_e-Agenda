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
        }
        public override Tarefa ObterRegistro(TipoAcao tipoAcao)
        {
            Console.Write("Digite o titulo da tarefa: ");
            string titulo = Console.ReadLine();

            Console.Write("Prioridade da tarefa:\nDigite 1-Baixa, 3-Media, 5-Alta : ");
            int prioridade = Convert.ToInt32(Console.ReadLine());
           
            Console.Write("Digite a data de criação da tarefa: ");
            DateTime dataCriacao = Convert.ToDateTime(Console.ReadLine());

            return new Tarefa(titulo,prioridade,dataCriacao);
        }

        protected override void ConfiguraTela(List<Tarefa> registros)
        {
            string configuracaColunasTabela = "{0,-5} | {1,-15} | {2,-15} | {3,-15} | {4,-15} | {5,-15}";

            MontarCabecalhoTabela(configuracaColunasTabela, "Id", "Titulo", "Prioridade", "DataCriacao", "PercentualConcluido", "DataConclusao");

            foreach (Tarefa tarefa in registros)
            {
                Console.WriteLine(configuracaColunasTabela, tarefa.Id, tarefa.Titulo, tarefa.Prioridade, tarefa.DataCriacao.ToShortDateString(), tarefa.PercentualConcluido, tarefa.DataConclusao.ToShortDateString());
            }
        }
        public override Tarefa  AtualizarRegistro(TipoAcao tipoAcao)
        { 
            Console.Write("Atualize o titulo da tarefa: ");
            string titulo = Console.ReadLine();

            Console.Write("Prioridade da tarefa:\nDigite 1:baixa, 3:Media, 5:Alta ");
            int prioridade = Convert.ToInt32(Console.ReadLine());

            Console.Write("Atualize o percentual de conclusão ");
            int percentualConcluido = Convert.ToInt32(Console.ReadLine());

            Console.Write("Digite a data de conclusão da tarefa: ");
            DateTime dataConclusao = Convert.ToDateTime(Console.ReadLine());

            

            return new Tarefa(percentualConcluido, titulo, dataConclusao, prioridade);
        }
    }
}
