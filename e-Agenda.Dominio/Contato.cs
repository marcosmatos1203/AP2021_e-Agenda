using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace e_Agenda.Dominio
{
    public class Contato : EntidadeBase
    {
        private string nome;
        private string email;
        private string telefone;
        private string cargo;
        private string empresa;

        public Contato()
        {
        }

        public Contato(string nome, string email, string telefone, string cargo, string empresa)
        {
            this.Nome = nome;
            this.Email = email;
            this.Telefone = telefone;
            this.Cargo = cargo;
            this.Empresa = empresa;
        }

        public string Nome { get => nome; set => nome = value; }
        public string Email { get => email; set => email = value; }
        public string Telefone { get => telefone; set => telefone = value; }
        public string Cargo { get => cargo; set => cargo = value; }
        public string Empresa { get => empresa; set => empresa = value; }

        public override string Validar()
        {
            string resultadoValidacao = validaEmail(email);
            resultadoValidacao += validarNumTelefone(telefone);
            if (resultadoValidacao == "")
                resultadoValidacao = "ITEM_VALIDO";
            return resultadoValidacao;
        }
        private string validarNumTelefone(string telefone)
        { 

            if (telefone.Length>7)
            {
                return "";
            }
            else
            {
                return "Telefone Inválido!";
            }
        }
        private string validaEmail(string email)
        {
            
            Regex rg = new Regex(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");

            if (rg.IsMatch(email))
            {
               return "";
            }
            else
            {
                return "Email Inválido!";
            }
        }
    }
}
