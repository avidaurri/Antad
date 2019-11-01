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
    public class UsuarioDAO
    {
        public ConexionDB conexion;
        public UsuarioDAO(string cadena)
        {
            conexion = new ConexionDB(cadena);
        }
        public List<Usuario> getUsuarios()
        {
            Utilidades ser = new Utilidades();
            string urlServidor = ser.getUrlServidor(conexion);


            string select = "select u.id as idUsuario, u.numero_empleado as numeroEmpleado, CONCAT(u.nombre,' ',u.apellido_paterno,' ',u.apellido_materno) as nombre, " +
                "DATEDIFF(year, CONCAT(IIF(SUBSTRING(U.CURP, 5, 2) < 40, CONCAT('20', SUBSTRING(U.CURP, 5, 2)), CONCAT('19', SUBSTRING(U.CURP, 5, 2))), " +
                "'-', SUBSTRING(U.CURP, 7, 2), '-', SUBSTRING(U.CURP, 9, 2)),     getdate()) as edad, SUBSTRING(U.CURP, 11, 1) as genero, u.id_rol as idRol, " +
                "cr.rol as rol, u.activo as estatus, u.foto as foto     from usuarios u left     join cat_roles cr on cr.id = u.id_rol";

            List<Parametro> parametros = new List<Parametro>();

            Parametro paramIdUsuario = new Parametro();
            paramIdUsuario.Nombre = "@usuario";
            paramIdUsuario.Valor = "sdsdsd";
            parametros.Add(paramIdUsuario);

            SqlDataReader readerSuc = conexion.Consultar(select, parametros);

            List<Usuario> usuarios = new List<Usuario>();

            while (readerSuc.Read())
            {
                Usuario usuario = new Usuario();
                usuario.idUsuario = Convert.ToInt32(readerSuc["idUsuario"].ToString());
                usuario.numeroEmpleado = Convert.ToInt32(readerSuc["numeroEmpleado"].ToString());
                usuario.nombre = readerSuc["nombre"].ToString();
                usuario.edad = Convert.ToInt32(readerSuc["edad"].ToString());
                usuario.genero = readerSuc["genero"].ToString();
                usuario.idRol = Convert.ToInt32(readerSuc["idRol"].ToString());
                usuario.rol = readerSuc["rol"].ToString();
                usuario.estatus = Convert.ToBoolean(readerSuc["estatus"].ToString());
                usuario.foto = urlServidor + readerSuc["foto"].ToString();
                usuarios.Add(usuario);
                
            }

            conexion.Cerrar();
            return usuarios;
            
        }

        public int PostUsuario(Usuario user)
        {
           /* Utilidades ser = new Utilidades();
            string urlServidor = ser.getUrlServidor(conexion);*/

            string sql = "insert into usuarios(numero_empleado,usuario,nombre,clave,curp,id_rol,activo,foto,rfc)values(1,@usuario,@nombre,@password,@curp,1,1,@imagePath,@rfc)";

            List<Parametro> parametros = new List<Parametro>();

            Parametro paramNombre = new Parametro();
            paramNombre.Nombre = "@nombre";
            paramNombre.Valor = user.nombre.ToString();
            parametros.Add(paramNombre);

            Parametro paramUsuario = new Parametro();
            paramUsuario.Nombre = "@usuario";
            paramUsuario.Valor = user.usuario.ToString();
            parametros.Add(paramUsuario);

            Parametro paramPassword = new Parametro();
            paramPassword.Nombre = "@password";
            paramPassword.Valor = user.password.ToString();
            parametros.Add(paramPassword);

            Parametro paramCurp = new Parametro();
            paramCurp.Nombre = "@curp";
            paramCurp.Valor = user.curp.ToString();
            parametros.Add(paramCurp);

            Parametro paramRfc = new Parametro();
            paramRfc.Nombre = "@rfc";
            paramRfc.Valor = user.rfc.ToString();
            parametros.Add(paramRfc);

            Parametro paramImagePath = new Parametro();
            paramImagePath.Nombre = "@imagePath";
            paramImagePath.Valor = "Content/Usuarios/" +user.ImagePath.ToString();
            parametros.Add(paramImagePath);

            //ImagePath
            int registro = conexion.ActualizarParametro(sql, parametros);
            conexion.Cerrar();
            return registro;
        }
        public int BorrartUsuario(int id)
        {


            string sql = "delete from usuarios where id=@id";

            List<Parametro> parametros = new List<Parametro>();

            Parametro paramNombre = new Parametro();
            paramNombre.Nombre = "@id";
            paramNombre.Valor = id.ToString();
            parametros.Add(paramNombre);



            //ImagePath
            int registro = conexion.ActualizarParametro(sql, parametros);
            conexion.Cerrar();
            return registro;
        }
        public Usuario PustUsuario(int id,Usuario user)
        {

            Utilidades ser = new Utilidades();
            string urlServidor = ser.getUrlServidor(conexion);
            /* Utilidades ser = new Utilidades();
             string urlServidor = ser.getUrlServidor(conexion);*/

            /* string sql = "insert into usuarios(numero_empleado,usuario,nombre,clave,curp,id_rol,activo,foto,rfc)values " +
                 "(1,@usuario,@nombre,@password,@curp,1,1,@imagePath,@rfc)";*/

            string sql = "update usuarios set numero_empleado = 1, usuario=@usuario, nombre=@nombre, clave=@password, curp=@curp, id_rol=1, activo=1,foto=@imagePath " +
    " where id=@Id";

            List<Parametro> parametros = new List<Parametro>();

            Parametro paramId = new Parametro();
            paramId.Nombre = "@Id";
            paramId.Valor = id.ToString();
            parametros.Add(paramId);


            Parametro paramNombre = new Parametro();
            paramNombre.Nombre = "@nombre";
            paramNombre.Valor = user.nombre.ToString();
            parametros.Add(paramNombre);

            Parametro paramUsuario = new Parametro();
            paramUsuario.Nombre = "@usuario";
            paramUsuario.Valor = user.usuario.ToString();
            parametros.Add(paramUsuario);

            Parametro paramPassword = new Parametro();
            paramPassword.Nombre = "@password";
            paramPassword.Valor = user.password.ToString();
            parametros.Add(paramPassword);

            Parametro paramCurp = new Parametro();
            paramCurp.Nombre = "@curp";
            paramCurp.Valor = user.curp.ToString();
            parametros.Add(paramCurp);

            Parametro paramRfc = new Parametro();
            paramRfc.Nombre = "@rfc";
            paramRfc.Valor = user.rfc.ToString();
            parametros.Add(paramRfc);

            Parametro paramImagePath = new Parametro();
            paramImagePath.Nombre = "@imagePath";
            paramImagePath.Valor = "Content/Usuarios/" + user.ImagePath.ToString();
            parametros.Add(paramImagePath);

            //ImagePath
            int registro = conexion.ActualizarParametro(sql, parametros);

            Usuario newUsuario = new Usuario();

            if (registro > 0)
            {
                string select = "select u.id as idUsuario, u.numero_empleado as numeroEmpleado, CONCAT(u.nombre,' ',u.apellido_paterno,' ',u.apellido_materno) as nombre, " +
                    "cr.rol as rol, u.activo as estatus, u.foto as foto     from usuarios u left     join cat_roles cr on cr.id = u.id_rol where u.id= @Id";

                SqlDataReader readerSuc = conexion.Consultar(select, parametros);

                if (readerSuc.Read())
                {

                    newUsuario.idUsuario = Convert.ToInt32(readerSuc["idUsuario"].ToString());
                    newUsuario.numeroEmpleado = Convert.ToInt32(readerSuc["numeroEmpleado"].ToString());
                    newUsuario.nombre = readerSuc["nombre"].ToString();
                    newUsuario.estatus = Convert.ToBoolean(readerSuc["estatus"].ToString());
                    newUsuario.foto = urlServidor + readerSuc["foto"].ToString();
      

                }
            }


            conexion.Cerrar();
            return newUsuario;
        }
    }
}
