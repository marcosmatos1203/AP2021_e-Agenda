using e_Agenda.Controladores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Agenda.ConsoleApp.shared
{
    public class TelaPrincipal : TelaBase
    {
        private readonly ControladorTarefa controladorTarefa;
        private readonly ControladorContato controladorContato;
        //private readonly ControladorRevista controladorRevista;
        //private readonly ControladorEmprestimo controladorEmprestimo;

        private readonly TelaTarefa telaTarefa;
        private readonly TelaContato telaContato;
        //private readonly TelaAmigo telaAmigo;
        //private readonly TelaEmprestimo telaEmprestimo;

        public TelaPrincipal() : base("Tela Principal")
        {
            controladorTarefa = new ControladorTarefa();
            controladorContato = new ControladorContato();
            //controladorRevista = new ControladorRevista();
            //controladorEmprestimo = new ControladorEmprestimo();

            telaTarefa = new TelaTarefa(controladorTarefa);
            telaContato = new TelaContato(controladorContato);
            //telaRevista = new TelaRevista(controladorRevista, telaCaixa, controladorCaixa);
            //telaEmprestimo = new TelaEmprestimo(controladorEmprestimo, controladorAmigo,
            //    controladorRevista, telaRevista, telaAmigo);

            //PopularAplicacao();
        }

        private void PopularAplicacao()
        {
            //Amigo a1 = new Amigo("Helena", "Alexandre", "321", "Colégio");
            //controladorAmigo.AdicionarNovo(a1);

            //Caixa caixa = new Caixa("Azul", "xua-654");
            //controladorCaixa.AdicionarNovo(caixa);

            //Revista r = new Revista("Superman", "Trilogia", 10, caixa);
            //controladorRevista.AdicionarNovo(r);
        }

        public TelaBase ObterTela()
        {
            ConfigurarTela("Escolha uma opção: ");

            TelaBase telaSelecionada = null;
            string opcao;
            do
            {
                Console.WriteLine("Digite 1 para o Cadastro de Tarefas");
                Console.WriteLine("Digite 2 para o Cadastro de Contatos");
                
                Console.WriteLine("Digite S para Sair");
                Console.WriteLine();
                Console.Write("Opção: ");
                opcao = Console.ReadLine();

                if (opcao == "1")
                    telaSelecionada = telaTarefa;

                if (opcao == "2")
                    telaSelecionada = telaContato;

                else if (opcao.Equals("s", StringComparison.OrdinalIgnoreCase))
                    telaSelecionada = null;

            } while (OpcaoInvalida(opcao));

            return telaSelecionada;
        }

        private bool OpcaoInvalida(string opcao)
        {
            if (opcao != "1" && opcao != "2"  && opcao != "S" && opcao != "s")
            {
                ApresentarMensagem("Opção inválida", TipoMensagem.Erro);
                return true;
            }
            else
                return false;
        }
    }
}
