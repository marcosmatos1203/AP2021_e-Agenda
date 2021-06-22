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
    public class TelaContato : TelaCadastroBasico<Contato>, ICadastravel
    {
        public TelaContato(ControladorContato controlador)
           : base("Cadastro de Contatos", controlador)
        {
        }

        public override Contato AtualizarRegistro(TipoAcao tipoAcao)
        {
            return ObterRegistro(TipoAcao.Editando);
        }

        public override Contato ObterRegistro(TipoAcao tipoAcao)
        {
            Console.Write("Digite o nome: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o email: ");
            string email = Console.ReadLine();

            Console.Write("Digite o telefone: ");
            string telefone = Console.ReadLine();

            Console.Write("Digite o cargo: ");
            string cargo = Console.ReadLine();

            Console.Write("Digite a empresa: ");
            string empresa = Console.ReadLine();

            return new Contato(nome, email, telefone, cargo, empresa);
        }

        protected override void ConfiguraTela(List<Contato> registros)
        {
            string configuracaColunasTabela = "{0,-5} | {1,-15} | {2,-15} | {3,-15} | {4,-15} | {5,-15}";

            MontarCabecalhoTabela(configuracaColunasTabela, "Id", "Nome", "Email", "Telefone", "Cargo", "Empresa");

            foreach (Contato contato in registros)
            {
                Console.WriteLine(configuracaColunasTabela, contato.Id, contato.Nome, contato.Email, contato.Telefone, contato.Cargo, contato.Empresa);
            }
        }
    }
}
