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
    public class LoginAntadDAO
    {
        public ConexionDB conexion;
        public LoginAntadDAO(string cadena)
        {
            conexion = new ConexionDB(cadena);
        }
        public LoginAntad LoginUsuario(string usuario, string password)
        {
            Utilidades ser = new Utilidades();
            string urlServidor = ser.getUrlServidor(conexion);


            string select = "select top 1 u.id as idUsuario, u.usuario, u.id_rol as idRolUsuario, " +
                "CONCAT(u.nombre,' ',u.apellido_paterno,'', u.apellido_materno) as nombreUsuario, u.edad as edad, u.sexo as genero, " +
                " u.foto as fotoUsuario, cr.rol as rolUsuario, u.numero_empleado as numeroEmpleado, u.activo as activo " +
                "from usuarios u left join cat_roles cr on cr.id = u.id_rol " +
                     " where u.usuario=@usuario and u.clave=@password";

            List<Parametro> parametros = new List<Parametro>();

            Parametro paramUsuario = new Parametro();
            paramUsuario.Nombre = "@usuario";
            paramUsuario.Valor = usuario;
            parametros.Add(paramUsuario);

            Parametro paramPassword = new Parametro();
            paramPassword.Nombre = "@password";
            paramPassword.Valor = password;
            parametros.Add(paramPassword);

            SqlDataReader reader = conexion.Consultar(select, parametros);

            if (reader.Read())
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
                    loginn.edad = Convert.ToInt32(reader["edad"].ToString());
                    loginn.genero = reader["genero"].ToString();
                    //loginn.genero = reader["genero"].ToString();
                    loginn.fotoUsuario = urlServidor + reader["fotoUsuario"].ToString();
                    loginn.rolUsuario = reader["rolUsuario"].ToString();
                    loginn.idUsuario = Convert.ToInt32(reader["idUsuario"].ToString());
                    loginn.idRolUsuario = Convert.ToInt32(reader["idRolUsuario"].ToString());

                }

                conexion.Cerrar();
                return loginn;
            }
            conexion.Cerrar();
            return null;
        }
    }
}
