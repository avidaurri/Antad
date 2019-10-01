using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntadBiblioteca.Util
{
    public class Utilidades
    {
        public ConexionDB conexion;
        string urlSer;
        /*public Util(string cadena)
        {
            conexion = new ConexionDB(cadena);
        }*/

        public string getUrlServidor(ConexionDB conexion)
        {

            string select = "select urlserver as UrlServer from appconfig";
            // return ""Server = tcp:198.38.83.200,1433; Initial Catalog = rhados_antad; Persist Security Info = False; User ID = rhados_user_antad; Password = Al3jandr0; MultipleActiveResultSets = True; Encrypt = False; TrustServerCertificate = False; Connection Timeout = 30; "";

            List<Parametro> parametros = new List<Parametro>();

            Parametro paramIdUsuario = new Parametro();
            paramIdUsuario.Nombre = "@usuario";
            paramIdUsuario.Valor = "prueba";
            parametros.Add(paramIdUsuario);

            SqlDataReader readerSuc = conexion.Consultar(select, parametros);
            urlSer = "";
            if (readerSuc.Read())
            {
                urlSer = readerSuc["UrlServer"].ToString();

            }
            return urlSer;

        }
    }
}
