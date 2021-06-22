using e_Agenda.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Agenda.Controladores
{
    public abstract class Controlador<T> where T : EntidadeBase
    {
        public abstract string AdicionarNovo(T item);
        public abstract string Atualizar(int id, T item);
        public abstract bool ExisteItem(int id);
        public abstract bool ExcluirItem(int id);
        public abstract List<T> SelecionarTodos();
        public abstract T SelecionarPorId(int id);
        public abstract List<Tarefa> SelecionarTarefasFechadas();
        public abstract List<Tarefa> SelecionarTarefasAbertas();
    }
}
