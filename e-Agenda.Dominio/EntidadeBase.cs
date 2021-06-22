using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Agenda.Dominio
{
    public abstract class EntidadeBase
    {
        private int id;

        public int Id { get => id; set => id = value; }

        public abstract string Validar();
    }
}
