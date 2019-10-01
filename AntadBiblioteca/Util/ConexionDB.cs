using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntadBiblioteca.Util
{
    public class ConexionDB
    {
        private SqlConnection conexion;

        public ConexionDB(string cadena)
        {

            conexion = new SqlConnection(cadena);
            conexion.Open();

        }
        public SqlDataReader Consultar(string select, List<Parametro> parametros)
        {
            SqlCommand comando = new SqlCommand(select, conexion);
            comando.CommandTimeout = 0;///////////
            foreach (Parametro parametro in parametros)
            {
                comando.Parameters.AddWithValue(parametro.Nombre, parametro.Valor);
            }
            //Cerrar();
            return comando.ExecuteReader();

        }
        public DataSet ConsultarSP(String sp, List<Parametro> parametros)
        {
            SqlCommand comando = new SqlCommand(sp, conexion);
            comando.CommandTimeout = 0;///////////
            foreach (Parametro parametro in parametros)
            {
                comando.Parameters.AddWithValue(parametro.Nombre, parametro.Valor);
            }
            comando.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = comando;
            DataSet ds = new DataSet();
            da.Fill(ds);
            //Cerrar();
            return ds;
        }
        public int Actualizar(string sql)
        {
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.CommandTimeout = 0;///////////
            //Cerrar();
            return comando.ExecuteNonQuery();
        }


        //INSERT, UPDATE, DELETE
        public int ActualizarParametro(string sql, List<Parametro> parametros)
        {
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.CommandTimeout = 0;///////////
            foreach (Parametro parametro in parametros)
            {
                comando.Parameters.AddWithValue(parametro.Nombre, parametro.Valor);
            }
            //Cerrar();
            return comando.ExecuteNonQuery();

        }


        //INSERT, UPDATE, DELETE
        public int ActualizarD(string sql)
        {
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.CommandTimeout = 0;///////////
            return comando.ExecuteNonQuery();
        }

        public void Cerrar()
        {
            conexion.Close();
        }
    }
}
