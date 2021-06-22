using e_Agenda.ConsoleApp.shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Agenda.ConsoleApp
{
    class Program
    {
        static TelaPrincipal telaPrincipal = new TelaPrincipal();
        static void Main(string[] args)
        {
            while (true)
            {
                TelaBase telaSelecionada = telaPrincipal.ObterTela();

                if (telaSelecionada == null)
                    break;

                Console.Clear();

                if (telaSelecionada is TelaBase)
                    Console.WriteLine(telaSelecionada.Titulo); Console.WriteLine();

                string opcao = telaSelecionada.ObterOpcao();

                if (opcao.Equals("s", StringComparison.OrdinalIgnoreCase))
                    continue;

                if (telaSelecionada is ICadastravel)
                {
                    ICadastravel tela = (ICadastravel)telaSelecionada;

                    if (opcao == "1")
                        tela.InserirNovoRegistro();

                    else if (opcao == "2")
                    {
                        bool temRegistros = tela.VisualizarRegistros(TipoVisualizacao.VisualizandoTela);
                        if (temRegistros)
                            Console.ReadLine();
                    }

                    else if (opcao == "3")
                        tela.EditarRegistro();

                    else if (opcao == "4")
                        tela.ExcluirRegistro();
                }
                else if (telaSelecionada is TelaTarefa)
                {
                    TelaTarefa telaTarefa = (TelaTarefa)telaSelecionada;

                    if (opcao == "1")
                        telaTarefa.RegistrarTarefa();

                    else if (opcao == "2")
                        telaTarefa.EditarTarefa();

                    else if (opcao == "3")
                    {
                        telaTarefa.ExcluirTarefa(); 
                    }

                    else if (opcao == "4")
                    {
                        bool temRegistros = telaTarefa.VisualizarTarefasPendentes();
                        if (temRegistros)
                            Console.ReadLine();
                    }
                    else if (opcao == "5")
                    {
                        bool temRegistros = telaTarefa.VisualizarTarefasFechadas();
                        if (temRegistros)
                            Console.ReadLine();
                    }

                }
                Console.Clear();
            }
        }
    }
}
