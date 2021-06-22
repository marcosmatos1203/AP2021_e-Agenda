using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Agenda.Dominio
{
    public class Tarefa : EntidadeBase
    {
        private int percentualConcluido;
        private string titulo;
        private DateTime dataConclusao;
        private int prioridade;
        private DateTime dataCriacao;
        public Tarefa(string titulo, int prioridade, DateTime dataCriacao)
        {
            this.Titulo = titulo;
            this.Prioridade = prioridade;
            this.DataCriacao = dataCriacao;
            this.dataConclusao = new DateTime(1900,01, 01);
        }

        public Tarefa(int percentualConcluido, string titulo, DateTime dataConclusao, int prioridade)
        {
            this.percentualConcluido = percentualConcluido;
            this.titulo = titulo;
            this.dataConclusao = dataConclusao;
            this.prioridade = prioridade;
        }

        public Tarefa(int percentualConcluido, string titulo, DateTime dataConclusao, int prioridade, DateTime dataCriacao)
        {
            this.PercentualConcluido = percentualConcluido;
            this.Titulo = titulo;
            this.DataConclusao = dataConclusao;
            this.Prioridade = prioridade;
            this.DataCriacao = dataCriacao;
        }

        public int PercentualConcluido { get => percentualConcluido; set => percentualConcluido = value; }
        public string Titulo { get => titulo; set => titulo = value; }
        public DateTime DataConclusao { get => dataConclusao; set => dataConclusao = value; }
        public int Prioridade { get => prioridade; set => prioridade = value; }
        public DateTime DataCriacao { get => dataCriacao; set => dataCriacao = value; }

        public override string Validar()
        {
            return "ITEM_VALIDO";
        }
    }
}
