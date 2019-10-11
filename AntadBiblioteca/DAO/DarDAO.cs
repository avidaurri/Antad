using AntadBiblioteca.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntadBiblioteca.DAO
{
    public class DarDAO
    {
        public ConexionDB conexion;
        public DarDAO(string cadena)
        {
            conexion = new ConexionDB(cadena);
        }
        public int Dar(string idEvento)
        {



            string select = "update evento_cara set clv_edo_evento=3 where  folio_evento= @evento";

            List<Parametro> parametros = new List<Parametro>();

            Parametro paramIdEevento = new Parametro();
            paramIdEevento.Nombre = "@evento";
            paramIdEevento.Valor = idEvento;
            parametros.Add(paramIdEevento);


            int readerSuc = conexion.ActualizarParametro(select, parametros);

       



            conexion.Cerrar();
            return readerSuc;

        }
    }
}
