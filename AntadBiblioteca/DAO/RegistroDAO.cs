using AntadBiblioteca.Util;
using AntadComun.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntadBiblioteca.DAO
{
    public class RegistroDAO
    {
        public ConexionDB conexion;

        public RegistroDAO(string cadena)
        {
            conexion = new ConexionDB(cadena);
        }

        public int PostRegistro(Registro user)
        {
            /* Utilidades ser = new Utilidades();
             string urlServidor = ser.getUrlServidor(conexion);*/

            string sql = "insert into login(login,password,clv_empre,clv_cuenta,clv_emp,fecha_cap,administrador,estado_cuenta," +
                "usar_fecha_servidor,clv_tema,timbra,contrasena_fija,reg_apellido_paterno,reg_apellido_materno,reg_nombre,reg_curp, " +
                "reg_foto,reg_url_identificacion,reg_url_domicilio,clv_edo_reg_usr,reg_email,reg_empresa_interes,clv_puesto) " +
                "values(@login,@password,1,1,0,getdate(),0,0,0,0,0,0,@reg_apellido_paterno,@reg_apellido_materno,@reg_nombre,@reg_curp, " +
                "@reg_foto,@reg_url_identificacion,@reg_url_domicilio,1,@reg_email,@reg_empresa_interes,@clv_puesto)";

            List<Parametro> parametros = new List<Parametro>();

            Parametro paramLogin = new Parametro();
            paramLogin.Nombre = "@login";
            paramLogin.Valor = user.login.ToString();
            parametros.Add(paramLogin);

            Parametro paramPassword = new Parametro();
            paramPassword.Nombre = "@password";
            paramPassword.Valor = user.password.ToString();
            parametros.Add(paramPassword);

            Parametro paramApellidoPaterno = new Parametro();
            paramApellidoPaterno.Nombre = "@reg_apellido_paterno";
            paramApellidoPaterno.Valor = user.apellidoPaterno.ToString();
            parametros.Add(paramApellidoPaterno);

            Parametro paramApellidoMaterno = new Parametro();
            paramApellidoMaterno.Nombre = "@reg_apellido_materno";
            paramApellidoMaterno.Valor = user.apellidoMaterno.ToString();
            parametros.Add(paramApellidoMaterno);

            Parametro paramNombre = new Parametro();
            paramNombre.Nombre = "@reg_nombre";
            paramNombre.Valor = user.nombre.ToString();
            parametros.Add(paramNombre);

            Parametro paramCurp = new Parametro();
            paramCurp.Nombre = "@reg_curp";
            paramCurp.Valor = user.curp.ToString();
            parametros.Add(paramCurp);

            Parametro paramFoto = new Parametro();
            paramFoto.Nombre = "@reg_foto";
            paramFoto.Valor = "Content/Usuarios/" + user.foto.ToString();
            parametros.Add(paramFoto);


            Parametro paramIdentificacion = new Parametro();
            paramIdentificacion.Nombre = "@reg_url_identificacion";
            paramIdentificacion.Valor = "Content/Identificaciones/" + user.identificacion.ToString();
            parametros.Add(paramIdentificacion);

            Parametro paramDomiciliario = new Parametro();
            paramDomiciliario.Nombre = "@reg_url_domicilio";
            paramDomiciliario.Valor = "Content/Comprobantes/" + user.comprobanteDomiciliario.ToString();
            parametros.Add(paramDomiciliario);

            Parametro paramEmail = new Parametro();
            paramEmail.Nombre = "@reg_email";
            paramEmail.Valor = user.email.ToString();
            parametros.Add(paramEmail);

            Parametro paramEmpresa = new Parametro();
            paramEmpresa.Nombre = "@reg_empresa_interes";
            paramEmpresa.Valor = user.empresainteres.ToString();
            parametros.Add(paramEmpresa);

            Parametro paramPuesto = new Parametro();
            paramPuesto.Nombre = "@clv_puesto";
            paramPuesto.Valor = user.clvPuesto.ToString();
            parametros.Add(paramPuesto);


            //ImagePath
            int registro = conexion.ActualizarParametro(sql, parametros);
            conexion.Cerrar();
            return registro;
        }
    }
}
