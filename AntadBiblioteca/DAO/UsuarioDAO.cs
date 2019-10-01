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


            string select = "select u.numero_empleado as numeroEmpleado, CONCAT(u.nombre,' ',u.apellido_paterno,' ',u.apellido_materno) as nombre, " +
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
    }
}
