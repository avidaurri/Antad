using AntadBiblioteca.Util;
using ModelsNet.Models;
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
        public Intramuro GetIntramuro(int clvemp, double latitud, double longitud)
        {
            /* Utilidades ser = new Utilidades();
             string urlServidor = ser.getUrlServidor(conexion);*/

            string urlServidor = "https://compilacionweb.online/DemoAntad/";

            /*string select = "select l.reg_id_provisional as idUsuario, cct.nombre as empresa, ct.folio_centro_trabajo as idSucursal, " +
                 "ct.nombre_comercial as nombreSucursal, ct.logo_url as fotoSucursal, ct.latitud as latitudSucursal, ct.longitud as longitudSucursal " +
                 "from login l left join centro_trabajo ct on l.folio_centro_trabajo = ct.folio_centro_trabajo left join cadena_centro_trabajo cct " +
                 "on cct.clv_cadena = ct.clv_cadena where l.login =@clvemp ";*/

            string select = "select e.clv_emp as clvEmp, cct.nombre as empresa, cct.url_logo as fotoEmpresa, " +
                "ct.folio_centro_trabajo as folioCentroTrabajo, ct.nombre_comercial as nombreCentroTrabajo, " +
                "ct.logo_url as fotoCentroTrabajo, ct.latitud as latitud, ct.longitud as longitud from empleado e " +
                "left join login l on l.clv_emp = e.clv_emp left join centro_trabajo ct on ct.folio_centro_trabajo = " +
                "l.folio_centro_trabajo left join cadena_centro_trabajo cct on cct.clv_cadena = ct.clv_cadena " +
                "where e.clv_emp = @clvemp and e.clv_puesto = 3";

            List<Parametro> parametros = new List<Parametro>();

            Parametro paramUsuario = new Parametro();
            paramUsuario.Nombre = "@clvemp";
            paramUsuario.Valor = clvemp.ToString();
            parametros.Add(paramUsuario);


            SqlDataReader readerSuc = conexion.Consultar(select, parametros);

            Intramuro intramuro = new Intramuro();
            intramuro.existeSucursal = true;
            intramuro.errorSucursal = false;
            
            if (readerSuc.Read())
            {
                intramuro.empresa = readerSuc["empresa"].ToString();
                if (intramuro.empresa !="")
                {
                    intramuro.existeSucursal = true;
                    intramuro.errorSucursal = false;
                    intramuro.clvEmp = Convert.ToInt32(readerSuc["clvEmp"].ToString());
                    intramuro.fotoEmpresa = readerSuc["fotoEmpresa"].ToString();
                    intramuro.folioCentroTrabajo = readerSuc["folioCentroTrabajo"].ToString();
                    intramuro.nombreCentroTrabajo = readerSuc["nombreCentroTrabajo"].ToString();
                    intramuro.fotoCentroTrabajo = urlServidor+readerSuc["fotoCentroTrabajo"].ToString();

                    Distancia dis = new Distancia();
                    double lat1 = Convert.ToDouble(readerSuc["latitud"].ToString());
                    double lon1 = Convert.ToDouble(readerSuc["longitud"].ToString());
                    double distancia = dis.totalDistancia(lat1, lon1, latitud, longitud);
                    intramuro.distanciaIntramuroCTrabajo = distancia.ToString();

                    if (distancia < .5)
                    {
                        intramuro.existeOperacion = true;
                        intramuro.errorOperacion = false;
                        intramuro.latitudSucursal = Convert.ToDouble(readerSuc["latitud"].ToString());
                        intramuro.longitudSucursal = Convert.ToDouble(readerSuc["longitud"].ToString());
                        intramuro.mensajeSucursal = "ok";
  
                    }
                    else
                    {
                        intramuro.existeOperacion = false;
                        intramuro.errorOperacion = true;
                        intramuro.mensajeSucursal = "Tu sucursal no está a tu alcance, posiciónate dentro de tu sucursal y presiona el botón actualizar";
 
                    }

                }
                else
                {
                    intramuro.existeOperacion = false;
                    intramuro.errorOperacion = true;
                    intramuro.existeSucursal = false;
                    intramuro.errorSucursal = true;
                    intramuro.mensajeSucursal = "No tienes sucursal asignada, solicita la asignación y posteriormente selecciona el botón actualizar";
                }
               
                //string km = (distancia / 1000).ToString();
            }
            else
            {
                intramuro.existeOperacion = false;
                intramuro.errorOperacion = true;
                intramuro.existeSucursal = false;
                intramuro.errorSucursal = true;
                intramuro.mensajeSucursal = "No tienes sucursal asignada, solicita la asignación y posteriormente selecciona el botón actualizar";
            }

            conexion.Cerrar();
            return intramuro;
        }
    }
}
