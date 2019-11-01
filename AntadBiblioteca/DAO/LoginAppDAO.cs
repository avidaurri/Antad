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
    public class LoginAppDAO
    {
        public ConexionDB conexion;
        public LoginAppDAO(string cadena)
        {
            conexion = new ConexionDB(cadena);
        }

        public UserSession LoginUsuario(string usuario, string password)
        {
            /*Utilidades ser = new Utilidades();
            string urlServidor = ser.getUrlServidor(conexion);*/


            /*string select = "select l.login as login, l.password as password, l.reg_id_provisional as idLogin, " +
                "eru.descripcion as estadoLogin,eru.texto as mensajeLogin, l.clv_edo_reg_usr as idEdoRegUsuario, l.clv_puesto as idPuesto  " +
                "from login l    left join edo_reg_usr eru on eru.clv_edo_reg_usr = l.clv_edo_reg_usr " +
                "where l.login =@usuario and l.password =@password";*/

            string select = "select l.clv_emp as clvEmp, DATEDIFF(year, CASE WHEN SUBSTRING(e.curp, 5, 2) < 40 " +
                "THEN '20' + SUBSTRING(e.curp, 5, 2) ELSE '19' + SUBSTRING(e.curp, 5, 2) end + '-' + SUBSTRING(e.curp, 7, 2) + '-' + SUBSTRING(e.curp, 9, 2), " +
                "getdate()) as edad, e.genero as genero, e.clv_gen as clvGen, " +
                "(e.nombre + ' ' + e.apellido_paterno + ' ' + e.apellido_materno) as nombre, l.login as usuario, e.foto_url as foto, " +
                "e.clv_puesto as clvPuesto, cp.puesto as puesto, l.clv_edo_reg_usr as edoReg, eru.descripcion as estadoRegistro " +
                "from login l left  join empleado e on e.clv_emp = l.clv_emp left join cat_puesto cp on cp.clv_puesto = e.clv_puesto " +
                "left join edo_reg_usr eru on eru.clv_edo_reg_usr = l.clv_edo_reg_usr where l.login = @usuario and l.password = @password";

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

            UserSession loginn = new UserSession();
            loginn.seLogeo = true;

            if (reader.Read())
            {
                
               
                int estado = Convert.ToInt32(reader["edoReg"]);

                if (estado==5)
                {
                   

                    loginn.clvEmp = Convert.ToInt32(reader["clvEmp"]);
                    loginn.edad = Convert.ToInt32(reader["edad"]);
                    loginn.clvGen = Convert.ToInt32(reader["clvGen"]);
                    loginn.genero = reader["genero"].ToString();
                    loginn.nombre = reader["nombre"].ToString();
                    loginn.usuario = reader["usuario"].ToString();
                    loginn.foto = reader["foto"].ToString();
                    loginn.clvPuesto = Convert.ToInt32(reader["clvPuesto"]);
                    loginn.puesto = reader["puesto"].ToString();
                    //loginn.mensajeLogin = reader["mensajeLogin"].ToString();

                }
                else
                {

                    loginn.mensajeLogin = reader["estadoRegistro"].ToString();
                    loginn.seLogeo = false;

                }

            }
            else
            {
                loginn.mensajeLogin = "Usuario y/o contraseña incorrectos";
                loginn.seLogeo = false;
            }
            conexion.Cerrar();
            return loginn;
        }

        /*public UserSession GetUser(string usuario, string password)
        {
            Utilidades ser = new Utilidades();
            string urlServidor = ser.getUrlServidor(conexion);
            
            string select = "select clv_emp as idEmpleado, (l.reg_nombre+' '+l.reg_apellido_paterno+' '+l.reg_apellido_materno) as nombre, " +
                "DATEDIFF(year, CASE WHEN SUBSTRING(l.reg_curp, 5, 2) < 40 THEN '20' + SUBSTRING(l.reg_curp, 5, 2) ELSE '19' + SUBSTRING(l.reg_curp, 5, 2) end " +
                " + '-' + SUBSTRING(l.reg_curp, 7, 2) + '-' + SUBSTRING(l.reg_curp, 9, 2), getdate()) as edad, SUBSTRING(l.reg_curp, 11, 1) as genero, " +
                "l.login as usuario, l.password as password, l.reg_curp as curp, l.reg_foto as foto, l.clv_puesto as idPuesto, l.reg_id_provisional as idLogin," +
                " cp.puesto as puesto from login l left join cat_puesto cp on cp.clv_puesto = l.clv_puesto where l.login = @usuario ";

            List<Parametro> parametros = new List<Parametro>();

            Parametro paramUsuario = new Parametro();
            paramUsuario.Nombre = "@usuario";
            paramUsuario.Valor = usuario;
            parametros.Add(paramUsuario);


            SqlDataReader reader = conexion.Consultar(select, parametros);

            UserSession loginn = new UserSession();


            if (reader.Read())
            {


                    loginn.idEmpleado = Convert.ToInt32(reader["idEmpleado"]);
                    loginn.edad = Convert.ToInt32(reader["edad"]);
                    loginn.genero = reader["genero"].ToString();
                    loginn.nombre = reader["nombre"].ToString();
                    loginn.usuario = reader["usuario"].ToString();
                    loginn.password = reader["password"].ToString();
                    loginn.curp = reader["curp"].ToString();
                    loginn.foto = urlServidor+reader["foto"].ToString();
                    loginn.idPuesto = Convert.ToInt32(reader["idPuesto"]);
                    loginn.idLogin = Convert.ToInt32(reader["idLogin"]);
                    loginn.puesto = reader["puesto"].ToString();


                conexion.Cerrar();
                return loginn;
            }
            conexion.Cerrar();
            return null;
        }*/

    }
}
