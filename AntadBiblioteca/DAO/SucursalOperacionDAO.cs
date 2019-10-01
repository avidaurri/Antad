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
    public class SucursalOperacionDAO
    {
        public ConexionDB conexion;
        public SucursalOperacionDAO(string cadena)
        {
            conexion = new ConexionDB(cadena);
        }
        public SucursalOperacion getSucursalOperacion(int idusuario, double latitud, double longitud)
        {
            Utilidades ser = new Utilidades();
            string urlServidor = ser.getUrlServidor(conexion);

            latitud = 19.3981164;
            longitud = -99.5259474;
            string select = "select u.id as idUsuario, ce.nombre as empresa, su.id_sucursal as idSucursal, cs.nombre as nombreSucursal, " +
                " cs.foto_sucursal as fotoSucursal, cs.latitud as latitudSucursal, cs.longitud as longitudSucursal " +
                "  from sucursal_usuario su left join usuarios u on u.id = su.id_usuario " +
                "left join cat_sucursal cs on cs.id = su.id_sucursal left join cat_empresa ce on ce.id = cs.id_empresa where u.id= @usuario";

            List<Parametro> parametros = new List<Parametro>();

            Parametro paramIdUsuario = new Parametro();
            paramIdUsuario.Nombre = "@usuario";
            paramIdUsuario.Valor = idusuario.ToString();
            parametros.Add(paramIdUsuario);

            /*Parametro param = new Parametro();
            paramPassword.Nombre = "@password";
            paramPassword.Valor = latitud;
            parametros.Add(paramPassword);

            Parametro paramPassword = new Parametro();
            paramPassword.Nombre = "@password";
            paramPassword.Valor = latitud;
            parametros.Add(paramPassword);*/

            SqlDataReader readerSuc = conexion.Consultar(select, parametros);

            SucursalOperacion sucursal = new SucursalOperacion();
            sucursal.existeOperacion = true;
            sucursal.existeSucursal = true;
            if (readerSuc.Read())
            {
                sucursal.existeSucursal = true;
                sucursal.idUsuario = Convert.ToInt32(readerSuc["idUsuario"].ToString());
                sucursal.empresa = readerSuc["empresa"].ToString();
                sucursal.idSucursal = Convert.ToInt32(readerSuc["idSucursal"].ToString());
                sucursal.nombreSucursal = readerSuc["nombreSucursal"].ToString();
                sucursal.fotoSucursal = urlServidor + readerSuc["fotoSucursal"].ToString();
                Distancia dis = new Distancia();
                double lat1 = Convert.ToDouble(readerSuc["latitudSucursal"].ToString());
                double lon1 = Convert.ToDouble(readerSuc["longitudSucursal"].ToString());
                double distancia = dis.totalDistancia(lat1, lon1, Convert.ToDouble(latitud), Convert.ToDouble(longitud));
                sucursal.distanciaSucursal = distancia;

                if (distancia < .5)
                {
                    sucursal.existeOperacion = true;
                    sucursal.existeSucursal = true;
                    sucursal.latitudSucursal = Convert.ToDouble(readerSuc["latitudSucursal"].ToString());
                    sucursal.longitudSucursal = Convert.ToDouble(readerSuc["longitudSucursal"].ToString());
                    sucursal.mensajeSucursal = "ok";
                    sucursal.mensajeOperacion = "ok";
                }
                else
                {
                    sucursal.existeOperacion = false;
                    sucursal.existeSucursal = true;
                    sucursal.mensajeSucursal = "ok";
                    sucursal.mensajeOperacion = "Debes de estar cerca de tu sucursal asignada";
                }
                //string km = (distancia / 1000).ToString();
            }
            else
            {
                sucursal.existeSucursal = false;
                sucursal.existeOperacion = false;
                sucursal.mensajeSucursal = "No tienes sucursal asignada";
                sucursal.mensajeOperacion = "No tienes sucursal asignada";
            }

            sucursal.mostarMensajeSucursal = !sucursal.existeSucursal;
            sucursal.mostrarMensajeOperacion = !sucursal.existeOperacion;
            conexion.Cerrar();
            return sucursal;

            /*if (readerSuc.Read())
            {
                LoginAntad loginn = new LoginAntad();
                bool activo = Convert.ToBoolean(reader["activo"]);

                if (!activo)
                {
                    loginn.mensajeLogin = "Tu usuario esta desactivado";

                }
                else if (activo)
                {

                    loginn.mensajeLogin = "ok";
                    loginn.nombreUsuario = reader["nombreUsuario"].ToString();
                    loginn.usuario = reader["usuario"].ToString();
                    //Convert.ToBoolean(

                    loginn.fotoUsuario = reader["fotoUsuario"].ToString();
                    loginn.rolUsuario = reader["rolUsuario"].ToString();
                    loginn.idUsuario = Convert.ToInt32(reader["idUsuario"].ToString());
                    loginn.idRolUsuario = Convert.ToInt32(reader["idRolUsuario"].ToString());

                }

                conexion.Cerrar();
                return loginn;
            }
            conexion.Cerrar();
            return null;*/
        }
    }
}
