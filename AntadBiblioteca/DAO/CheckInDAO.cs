using AntadBiblioteca.Util;
using AntadComun.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntadBiblioteca.DAO
{
    public class CheckInDAO
    {
        public ConexionDB conexion;
        public CheckInDAO(string cadena)
        {
            conexion = new ConexionDB(cadena);
        }
        public CheckIn hacerCheckIn(int idEvento, int idUsuario)
        {
            /*Utilidades ser = new Utilidades();
            string urlServidor = ser.getUrlServidor(conexion);*/

            //obtener el estatus del evento

            string select = "select estatus as estatus from evento where id=@idEvento";

            List<Parametro> parametros = new List<Parametro>();

            Parametro paramEvento = new Parametro();
            paramEvento.Nombre = "@idEvento";
            paramEvento.Valor = idEvento.ToString();
            parametros.Add(paramEvento);

            /* Parametro paramPassword = new Parametro();
             paramPassword.Nombre = "@password";
             paramPassword.Valor = password;
             parametros.Add(paramPassword);*/

            SqlDataReader reader = conexion.Consultar(select, parametros);

            if (reader.Read())
            {
                CheckIn chec = new CheckIn();

                int estatus = Convert.ToInt32(reader["estatus"].ToString());
                chec.estatus = estatus;
                chec.idUsuario = idUsuario;
                if (estatus.Equals(0))
                {
                    chec.mensajeCheckIn = "El evento no tiene autorización";
                }
                else if (estatus.Equals(1))
                {
                    //autorizar el evento ActualizarParametro
                    string actu = "update evento set estatus=2 where estatus=1 and id=@idEvento";

                    int readera = conexion.ActualizarParametro(actu, parametros);

                    if (readera.Equals(1))
                    {
                        chec.mensajeCheckIn = "ok";
                        chec.estatus = 2;
                    }
                    else
                    {
                        chec.mensajeCheckIn = "error al hacer CheckIn";
                    }
                }


                conexion.Cerrar();
                return chec;
            }
            conexion.Cerrar();
            return null;
        }
    }
}
