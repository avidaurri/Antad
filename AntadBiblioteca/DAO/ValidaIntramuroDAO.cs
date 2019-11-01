using AntadBiblioteca.Util;
using ModelsNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntadBiblioteca.DAO
{
    public class ValidaIntramuroDAO
    {
        public ConexionDB conexion;
        public ValidaIntramuroDAO(string cadena)
        {
            conexion = new ConexionDB(cadena);
        }
        public ValidaIntramuro validarIntramuro(int idEvento)
        {
            ValidaIntramuro verr = new ValidaIntramuro();

            string actu = "update evento set estatus=1 where estatus=0 and id=@idEvento";


            List<Parametro> parametros = new List<Parametro>();

            Parametro paramEvento = new Parametro();
            paramEvento.Nombre = "@idEvento";
            paramEvento.Valor = idEvento.ToString();
            parametros.Add(paramEvento);

            //autorizar el evento ActualizarParametro


            int readera = conexion.ActualizarParametro(actu, parametros);

            if (readera.Equals(1))
            {
                verr.mensajeValidaIntramuro = "ok";
                verr.estatus = 1;
            }
            else
            {
                verr.mensajeValidaIntramuro = "error al actualizar";
            }



            conexion.Cerrar();
            return verr;


        }
    }
}
