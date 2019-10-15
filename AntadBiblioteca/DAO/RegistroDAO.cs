using AntadBiblioteca.Util;
using AntadComun.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        public Registro PostRegistro(Registro user)
        {
            Utilidades ser = new Utilidades();
            string urlServidor = ser.getUrlServidor(conexion);

            Registro respuesta = new Registro();
            int numeroFolio;
            bool folioConMesb;
            string folioEmpleado;
            int generoas;
            string verificarRepetido = "select * from login where login=@usuario";
                        
            List<Parametro> parametrosd = new List<Parametro>();

            Parametro paramUsuarioVer = new Parametro();
            paramUsuarioVer.Nombre = "@usuario";
            paramUsuarioVer.Valor = user.login.ToString();
            parametrosd.Add(paramUsuarioVer);

            SqlDataReader readerVerificarRepetido = conexion.Consultar(verificarRepetido, parametrosd);

            if (readerVerificarRepetido.Read())
            {
                //si esta repetido
                respuesta.usuarioRegistrado = false;
                respuesta.mensajeRegistro = "El usuario ya se encuentra registrado";
            }
            else
            {
                // no esta repetido
                //sacar folios con mes
                string st = "foliador_documentos";
                List<Parametro> parametrosdd = new List<Parametro>();
                Parametro paramLogind = new Parametro();
                paramLogind.Nombre = "@ano";
                paramLogind.Valor = "2019";
                parametrosdd.Add(paramLogind);

                Parametro paramLoginde = new Parametro();
                paramLoginde.Nombre = "@documento";
                paramLoginde.Valor = "tp";
                parametrosdd.Add(paramLoginde);

                Parametro paramLoginder = new Parametro();
                paramLoginder.Nombre = "@resultado";
                paramLoginder.Valor = "46";
                parametrosdd.Add(paramLoginder);

                DataSet leer = conexion.ConsultarSP(st, parametrosdd);

                //extraer el ultimo numero de la tabla.


                string ultimo = "select folio as folio from folios_documentos where ano=2019 and documento='tp'";


                SqlDataReader obtenerUltimo = conexion.Consultar(ultimo, parametrosdd);

                if (obtenerUltimo.Read())
                {
                    numeroFolio = Convert.ToInt32(obtenerUltimo["folio"]);
                }
                else
                {
                    numeroFolio = 5555;
                }

                string folioConMes = "select folios_con_mes as val from configuracion_sistema";

                SqlDataReader readerfolioConMes = conexion.Consultar(folioConMes, parametrosdd);

                if (readerfolioConMes.Read())
                {
                    folioConMesb = Convert.ToBoolean(readerfolioConMes["val"]);
                }
                else
                {
                    folioConMesb = false;
                }

                if (folioConMesb)
                {
                    string mes = Convert.ToString(DateTime.Now.ToString("MM"));
                    string ano = Convert.ToString(DateTime.Now.ToString("yy"));

                   // folioEmpleado = "TP" + mes + ano + "-" + numeroFolio;
                    folioEmpleado = numeroFolio.ToString();
                }
                else
                {
                    //folioEmpleado = "TP" + "-" + numeroFolio;
                    folioEmpleado = numeroFolio.ToString();
                }


                //fecha de nacimiento
                int anoo = Convert.ToInt32(user.curp.Substring(4, 2));
                string anooo;
                string mess = user.curp.Substring(6, 2);
                string diaa = user.curp.Substring(8, 2);

                if (anoo < 50)
                {
                    anooo = "20" + anoo;
                }
                else
                {
                    anooo = "19" + anoo;
                }
                //DateTime fech = new DateTime(Convert.ToInt32(anooo), Convert.ToInt32(mess), Convert.ToInt32(diaa));
                string fech = anooo+"-"+mess+"-"+diaa;
                //extraer genero 
                string generoprev = user.curp.Substring(10, 1);

                if (generoprev.Equals("H"))
                {
                    generoas = 1;
                }
                else if (generoprev.Equals("M"))
                {
                    generoas = 0;
                }
                else
                {
                    generoas = 0;
                }


                string sqlInsert = "insert into empleado(procesar_nomina,sicepo,clv_emp,clv_gen,fecha_naci,clv_puesto,nombre,apellido_paterno,apellido_materno," +
                    "email,curp,edo_civil,peso,estatura,clv_grado_estu,ciu_edo,del_mun,cp,calle_no,no_exterior,no_interior," +
                    "clv_banco,clabe,cuenta,tarjeta,tipo_empleado,foto_url) " +
        "values(0,0,@clv_emp,@clv_gen,@fecha_naci,@clv_puesto,@nombre,@apellido_paterno,@apellido_materno,@email,@curp,@edo_civil,@peso,@altura,@clv_gradoestu,@ciu_edo,@del_mun,@cp,@calle_no," +
        "@no_exterior,@no_interior, @clv_banco,@clabe,@cuenta,@tarjeta,2,@foto_url)";

                List<Parametro> parametrosInsert = new List<Parametro>();

                Parametro paramLogin = new Parametro();
                paramLogin.Nombre = "@login";
                paramLogin.Valor = user.login.ToString();
                parametrosInsert.Add(paramLogin);

                Parametro paramPassword = new Parametro();
                paramPassword.Nombre = "@password";
                paramPassword.Valor = user.password.ToString();
                parametrosInsert.Add(paramPassword);

                Parametro paramlvEmp = new Parametro();
                paramlvEmp.Nombre = "@clv_emp";
                paramlvEmp.Valor = folioEmpleado.ToString();
                parametrosInsert.Add(paramlvEmp);

                Parametro paramGen = new Parametro();
                paramGen.Nombre = "@clv_gen";
                paramGen.Valor = generoas.ToString();
                parametrosInsert.Add(paramGen);

                Parametro paramFechanac = new Parametro();
                paramFechanac.Nombre = "@fecha_naci";
                paramFechanac.Valor = fech.ToString();
                parametrosInsert.Add(paramFechanac);

                Parametro paramPuesto = new Parametro();
                paramPuesto.Nombre = "@clv_puesto";
                paramPuesto.Valor = user.clvPuesto.ToString();
                parametrosInsert.Add(paramPuesto);

                Parametro paramNombre = new Parametro();
                paramNombre.Nombre = "@nombre";
                paramNombre.Valor = user.nombre;
                parametrosInsert.Add(paramNombre);

                Parametro paramApellidoP = new Parametro();
                paramApellidoP.Nombre = "@apellido_paterno";
                paramApellidoP.Valor = user.apellidoPaterno;
                parametrosInsert.Add(paramApellidoP);

                Parametro paramApellidoM = new Parametro();
                paramApellidoM.Nombre = "@apellido_materno";
                paramApellidoM.Valor = user.apellidoMaterno;
                parametrosInsert.Add(paramApellidoM);

                Parametro paramEmail = new Parametro();
                paramEmail.Nombre = "@email";
                paramEmail.Valor = user.email.ToString();
                parametrosInsert.Add(paramEmail);
                Parametro paramCurp = new Parametro();
                paramCurp.Nombre = "@curp";
                paramCurp.Valor = user.curp.ToString();
                parametrosInsert.Add(paramCurp);

                Parametro paramFEdoCivil = new Parametro();
                paramFEdoCivil.Nombre = "@edo_civil";
                paramFEdoCivil.Valor = user.estadoCivil.ToString();
                parametrosInsert.Add(paramFEdoCivil);

                Parametro paramPeso = new Parametro();
                paramPeso.Nombre = "@peso";
                paramPeso.Valor = user.peso.ToString();
                parametrosInsert.Add(paramPeso);

                Parametro paramAltura = new Parametro();
                paramAltura.Nombre = "@altura";
                paramAltura.Valor = user.altura.ToString();
                parametrosInsert.Add(paramAltura);

                Parametro paramGradoEstu = new Parametro();
                paramGradoEstu.Nombre = "@clv_gradoestu";
                paramGradoEstu.Valor = user.clvGradoEstudios.ToString();
                parametrosInsert.Add(paramGradoEstu);

                Parametro paramEstado = new Parametro();
                paramEstado.Nombre = "@ciu_edo";
                paramEstado.Valor = user.estado;
                parametrosInsert.Add(paramEstado);


                Parametro paramMuni = new Parametro();
                paramMuni.Nombre = "@del_mun";
                paramMuni.Valor = user.municipio;
                parametrosInsert.Add(paramMuni);

                Parametro paramCodigoP = new Parametro();
                paramCodigoP.Nombre = "@cp";
                paramCodigoP.Valor = user.codigoPostal.ToString();
                parametrosInsert.Add(paramCodigoP);

                Parametro paramCalle = new Parametro();
                paramCalle.Nombre = "@calle_no";
                paramCalle.Valor = user.calle;
                parametrosInsert.Add(paramCalle);

                Parametro paramNoEterior = new Parametro();
                paramNoEterior.Nombre = "@no_exterior";
                paramNoEterior.Valor = user.numeroExterior;
                parametrosInsert.Add(paramNoEterior);

                Parametro paramNoInteiorr = new Parametro();
                paramNoInteiorr.Nombre = "@no_interior";
                paramNoInteiorr.Valor = user.numeroInterior;
                parametrosInsert.Add(paramNoInteiorr);

                Parametro paramBanco = new Parametro();
                paramBanco.Nombre = "@clv_banco";
                paramBanco.Valor = user.clvBanco.ToString();
                parametrosInsert.Add(paramBanco);

                Parametro paramClabe = new Parametro();
                paramClabe.Nombre = "@clabe";
                paramClabe.Valor = user.clabe;
                parametrosInsert.Add(paramClabe);


                Parametro paramCuenta = new Parametro();
                paramCuenta.Nombre = "@cuenta";
                paramCuenta.Valor = user.numeroCuenta.ToString();
                parametrosInsert.Add(paramCuenta);

                Parametro paramTarjeta = new Parametro();
                paramTarjeta.Nombre = "@tarjeta";
                paramTarjeta.Valor = user.numeroTarjeta.ToString();
                parametrosInsert.Add(paramTarjeta);
                               
                Parametro paramFotoUrl = new Parametro();
                paramFotoUrl.Nombre = "@foto_url";
                paramFotoUrl.Valor = urlServidor + "Content/Usuarios/" + user.foto;
                parametrosInsert.Add(paramFotoUrl);

                Parametro paramIdentificacion = new Parametro();
                paramIdentificacion.Nombre = "@identificacion";
                paramIdentificacion.Valor = urlServidor + user.identificacion;
                parametrosInsert.Add(paramIdentificacion);

                Parametro paramComprobante = new Parametro();
                paramComprobante.Nombre = "@comprobante";
                paramComprobante.Valor = urlServidor  + user.comprobanteDomiciliario;
                parametrosInsert.Add(paramComprobante);

                Parametro paramTelefonoUno = new Parametro();
                paramTelefonoUno.Nombre = "@telefonoUno";
                paramTelefonoUno.Valor = user.telefonoReferenciaUno.ToString();
                parametrosInsert.Add(paramTelefonoUno);

                Parametro paramNombreReferenciaUno = new Parametro();
                paramNombreReferenciaUno.Nombre = "@nombreReferenciaUno";
                paramNombreReferenciaUno.Valor = user.nombreReferenciaUno;
                parametrosInsert.Add(paramNombreReferenciaUno);

                Parametro paramTelefonoDos = new Parametro();
                paramTelefonoDos.Nombre = "@telefonoDos";
                paramTelefonoDos.Valor = user.telefonoReferenciaDos.ToString();
                parametrosInsert.Add(paramTelefonoDos);

                Parametro paramNombreReferenciaDos = new Parametro();
                paramNombreReferenciaDos.Nombre = "@nombreReferenciaDos";
                paramNombreReferenciaDos.Valor = user.nombreReferenciaDos;
                parametrosInsert.Add(paramNombreReferenciaDos);

                Parametro paramTelefonoTres = new Parametro();
                paramTelefonoTres.Nombre = "@telefonoTres";
                paramTelefonoTres.Valor = user.telefonoReferenciaTres.ToString();
                parametrosInsert.Add(paramTelefonoTres);

                Parametro paramNombreReferenciaTres = new Parametro();
                paramNombreReferenciaTres.Nombre = "@nombreReferenciaTres";
                paramNombreReferenciaTres.Valor = user.nombreReferenciaTres;
                parametrosInsert.Add(paramNombreReferenciaTres);
                //ImagePath
                int registradoEmpleado = conexion.ActualizarParametro(sqlInsert, parametrosInsert);

                if (registradoEmpleado != 1)
                {
                    respuesta.mensajeRegistro = "No se registro completo";
                    respuesta.usuarioRegistrado = false;
                }
                else
                {
                    //insertar en login

                    string sqlInsertLogin = "insert into login(clv_edo_reg_usr,estado_cuenta,administrador,login,password,clv_emp, reg_url_identificacion,reg_url_domicilio) " +
                        "values(5,0,0,@login,@password,@clv_emp,@identificacion,@comprobante)";

                    int registradoLogin = conexion.ActualizarParametro(sqlInsertLogin, parametrosInsert);

                    string sqlInsertReferenciaUno = "insert into emp_telefono(clv_emp,telefono,descripcion) " +
                        "values(@clv_emp,@telefonoUno,@nombreReferenciaUno)";

                    int registradoreferenciaUno = conexion.ActualizarParametro(sqlInsertReferenciaUno, parametrosInsert);

                    string sqlInsertReferenciaDos = "insert into emp_telefono(clv_emp,telefono,descripcion) " +
    "values(@clv_emp,@telefonoDos,@nombreReferenciaDos)";

                    int registradoreferenciaDos = conexion.ActualizarParametro(sqlInsertReferenciaDos, parametrosInsert);

                    string sqlInsertReferenciaTres = "insert into emp_telefono(clv_emp,telefono,descripcion) " +
    "values(@clv_emp,@telefonoTres,@nombreReferenciaTres)";

                    int registradoreferenciaTres = conexion.ActualizarParametro(sqlInsertReferenciaTres, parametrosInsert);


                    if (registradoLogin == 1)
                    {
                        respuesta.mensajeRegistro = "Tu registro fue exitoso";
                        respuesta.usuarioRegistrado = true;
                    }
                }


            }


            conexion.Cerrar();
            return respuesta;


        }
    }
}
