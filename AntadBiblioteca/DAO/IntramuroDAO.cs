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
    public class IntramuroDAO
    {
        public ConexionDB conexion;
        public IntramuroDAO(string cadena)
        {
            conexion = new ConexionDB(cadena);
        }
        public Intramuro GetIntramuro(string usuario, double latitud, double longitud)
        {
            /* Utilidades ser = new Utilidades();
             string urlServidor = ser.getUrlServidor(conexion);*/

            string urlServidor = "https://compilacionweb.online/DemoAntad/";

            string select = "select l.reg_id_provisional as idUsuario, cct.nombre as empresa, ct.folio_centro_trabajo as idSucursal, " +
                 "ct.nombre_comercial as nombreSucursal, ct.logo_url as fotoSucursal, ct.latitud as latitudSucursal, ct.longitud as longitudSucursal " +
                 "from login l left join centro_trabajo ct on l.folio_centro_trabajo = ct.folio_centro_trabajo left join cadena_centro_trabajo cct " +
                 "on cct.clv_cadena = ct.clv_cadena where l.login =@usuario ";

            List<Parametro> parametros = new List<Parametro>();

            Parametro paramUsuario = new Parametro();
            paramUsuario.Nombre = "@usuario";
            paramUsuario.Valor = usuario;
            parametros.Add(paramUsuario);


            SqlDataReader readerSuc = conexion.Consultar(select, parametros);

            Intramuro intramuro = new Intramuro();
            intramuro.existeOperacion = true;
            intramuro.existeSucursal = true;
            intramuro.falloValidacion = false;
            
            if (readerSuc.Read())
            {

                intramuro.existeSucursal = true;
                intramuro.idUsuario = Convert.ToInt32(readerSuc["idUsuario"].ToString());
                intramuro.empresa = readerSuc["empresa"].ToString();
                intramuro.idSucursal = readerSuc["idSucursal"].ToString();
                intramuro.nombreSucursal = readerSuc["nombreSucursal"].ToString();
                intramuro.fotoSucursal = urlServidor + readerSuc["fotoSucursal"].ToString();
                Distancia dis = new Distancia();
                double lat1 = Convert.ToDouble(readerSuc["latitudSucursal"].ToString());
                double lon1 = Convert.ToDouble(readerSuc["longitudSucursal"].ToString());
                double distancia = dis.totalDistancia(lat1, lon1, Convert.ToDouble(latitud), Convert.ToDouble(longitud));
                intramuro.distanciaSucursal = distancia.ToString();

                if (distancia < .5)
                {
                    intramuro.existeOperacion = true;
                    intramuro.existeSucursal = true;
                    intramuro.latitudSucursal = Convert.ToDouble(readerSuc["latitudSucursal"].ToString());
                    intramuro.longitudSucursal = Convert.ToDouble(readerSuc["longitudSucursal"].ToString());
                    intramuro.mensajeSucursal = "ok";
                    intramuro.mensajeOperacion = "ok";
                }
                else
                {
                    intramuro.existeOperacion = false;
                    intramuro.existeSucursal = true;
                    intramuro.mensajeSucursal = "ok";
                    intramuro.mensajeOperacion = "Debes de estar cerca de tu sucursal asignada";
                    intramuro.falloValidacion = true;
                }
                //string km = (distancia / 1000).ToString();
            }
            else
            {
                intramuro.existeSucursal = false;
                intramuro.existeOperacion = false;
                intramuro.falloValidacion = true;
                intramuro.mensajeSucursal = "No tienes sucursal asignada";
                intramuro.mensajeOperacion = "No tienes sucursal asignada";
            }
            intramuro.mostarMensajeSucursal = !intramuro.existeSucursal;
            intramuro.mostrarMensajeOperacion = !intramuro.existeOperacion;
            conexion.Cerrar();
            return intramuro;
        }
    }
}
