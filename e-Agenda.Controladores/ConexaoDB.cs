using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Agenda.Controladores
{
    
    public  class ConexaoDB
    {

        static string enderecoDB = @"Data Source=(LocalDb)\MSSqlLocalDB;Initial Catalog=AP2021_Agenda;Integrated Security=True;Pooling=False";
        public SqlConnection conexaoComBanco = new SqlConnection(connectionString: enderecoDB);
        public SqlCommand Comando(string script)
        {
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexaoComBanco;
            comando.CommandText = script;
            return comando;
        }
        public void AbrirDB()
        {
            conexaoComBanco.Open();
        }
        public void FecharDB()
        {
            conexaoComBanco.Close();
        }
    }
}
