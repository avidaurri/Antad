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
    public class ValidaEventoDAO
    {
        public ConexionDB conexion;
        public ValidaEventoDAO(string cadena)
        {
            conexion = new ConexionDB(cadena);
        }
        public ValidaEvento getValidacionEvento(int idusuario, string idEvento)
        {
            Utilidades ser = new Utilidades();
            string urlServidor = ser.getUrlServidor(conexion);

            ValidaEvento evento = new ValidaEvento();
            evento.validacionFinal = true;

            string select = "select ec.folio_evento as folioEvento, e.clv_emp as clv_Empleado, ct.logo_url as fotoSucursal, ct.folio_centro_trabajo as folioSucursal, " +
                "ct.nombre_comercial as nombreSucursal, ec.fecha_inicial as fechaInicio, ec.fecha_final as fechaFinal, ee.descripcion as estatusEvento, " +
                "ee.clv_edo_evento as clvEstatusEvento, cte.descripcion as tipoEvento, cte.clv_tip_evento as clvTipoEvento, e.foto_url as fotoUsuario, " +
                "(e.nombre + ' ' + e.apellido_paterno + ' ' + e.apellido_materno) as nombreUsuario, e.genero as sexoUsuario, " +
                "DATEDIFF(year, CASE WHEN SUBSTRING(e.curp, 5, 2) < 40 THEN '20' + SUBSTRING(e.curp, 5, 2) ELSE '19' + SUBSTRING(e.curp, 5, 2) " +
                "end + '-' + SUBSTRING(e.curp, 7, 2) + '-' + SUBSTRING(e.curp, 9, 2), " +
                "getdate()) as edadUsuario, e.nu_seguro as imssUsuario, e.estatura as altura from evento_cara ec left join evento_personal ep on ep.folio_evento = ec.folio_evento left join " +
                "empleado e on e.clv_emp = ep.clv_emp left join cat_tip_evento cte on cte.clv_tip_evento = ec.clv_tip_evento left join edo_evento ee " +
                "on ee.clv_edo_evento = ec.clv_edo_evento left join centro_trabajo ct on ct.folio_centro_trabajo = ec.folio_centro_trabajo " +
                "left join cadena_centro_trabajo cct on cct.clv_cadena = ct.clv_cadena where e.clv_emp = @usuario and ec.folio_evento = @evento";

            List<Parametro> parametros = new List<Parametro>();

            Parametro paramIdUsuario = new Parametro();
            paramIdUsuario.Nombre = "@usuario";
            paramIdUsuario.Valor = idusuario.ToString();
            parametros.Add(paramIdUsuario);

            Parametro paramIdEevento = new Parametro();
            paramIdEevento.Nombre = "@evento";
            paramIdEevento.Valor = idEvento;
            parametros.Add(paramIdEevento);

            Parametro paramClvReq = new Parametro();
            paramClvReq.Nombre = "@req";
            paramClvReq.Valor = "prueba";
            parametros.Add(paramClvReq);

            SqlDataReader readerSuc = conexion.Consultar(select, parametros);

            evento.validacionFinal = true;

            if (readerSuc.Read())
            {
                evento.folioEvento = readerSuc["folioEvento"].ToString();
                evento.clv_Empleado = Convert.ToInt32(readerSuc["clv_Empleado"].ToString());
                evento.fotoSucursal = "https://compilacionweb.online/DemoAntad/"+readerSuc["fotoSucursal"].ToString();
                evento.folioSucursal = readerSuc["folioSucursal"].ToString();
                evento.nombreSucursal = readerSuc["nombreSucursal"].ToString();
                evento.fechaInicio = readerSuc["fechaInicio"].ToString();
                evento.fechaFinal = readerSuc["fechaFinal"].ToString();
                evento.estatusEvento = readerSuc["estatusEvento"].ToString();
                evento.clvEstatusEvento = Convert.ToInt32(readerSuc["clvEstatusEvento"].ToString());
                evento.tipoEvento = readerSuc["tipoEvento"].ToString();
                evento.clvTipoEvento = Convert.ToInt32(readerSuc["clvTipoEvento"].ToString());
                evento.fotoUsuario = readerSuc["fotoUsuario"].ToString();
                evento.nombreUsuario = readerSuc["nombreUsuario"].ToString();
                evento.sexoUsuario = readerSuc["sexoUsuario"].ToString();
                evento.edadUsuario = Convert.ToInt32(readerSuc["edadUsuario"].ToString());
                evento.imssUsuario = readerSuc["imssUsuario"].ToString();
                evento.altura = readerSuc["altura"].ToString();
            }





            string tipoDosSuc = "select cre.clv_requisito_evento as clvreq, cre.descripcion, cre.clv_tipo_requisito as tipo, per.rango_inferior as inferior, per.rango_superior as superior, " +
                "ere.valor as cal, ere.caducidad from evento_requisitos er left join cat_requisito_evento cre on cre.clv_requisito_evento = er.clv_requisito_evento " +
                "left join proyecto_evento_requisitos per on per.clv_requisito_evento = cre.clv_requisito_evento " +
                "left join emp_requisitos_evento ere on ere.clv_requisito_evento = cre.clv_requisito_evento and ere.clv_emp = @usuario where er.folio_evento =@evento";


            SqlDataReader readertipoDos = conexion.Consultar(tipoDosSuc, parametros);

            List<ValidaEvento.Rango> ldocSuc = new List<ValidaEvento.Rango>();
            List<ValidaEvento.Curso> ldocSucC = new List<ValidaEvento.Curso>();
            double bajo;
            double alto;
            int clvReqEven;
            int tipoReq;
            evento.generoValidado = true;
            evento.errorGenero = false;
            evento.imssValidado = true;
            evento.errorIms = false;
            evento.sexoSucursal = "";
            evento.mensajevalidacionsexo = "cumple con requisitos de genero";
            evento.mensajevalidacionimss = "cumple con el requisito de imss";
            bool fem = false;
            bool mas = false;


            while (readertipoDos.Read())
            {
                tipoReq = Convert.ToInt32(readertipoDos["tipo"].ToString());
                clvReqEven = Convert.ToInt32(readertipoDos["clvreq"].ToString());
                if (tipoReq.Equals(1))
                {
                    if (clvReqEven.Equals(2))
                    {//genero femenino
                        evento.sexoSucursal = evento.sexoSucursal + "Genero femenino";
                        fem = true;
                    }
                    else if (clvReqEven.Equals(3))
                    {//genero masculino
                        evento.sexoSucursal = evento.sexoSucursal + "Genero masculino";
                        mas = true;
                    }
                    else if (clvReqEven.Equals(6))
                    {//imss activo
                        if(evento.imssUsuario=="" || evento.imssUsuario == null)
                        {
                            evento.imssValidado = false;
                            evento.errorIms = true;
                            evento.mensajevalidacionimss = "no cumple con el requisito del imss";
                            evento.validacionFinal = false;
                        }

                    }

                    //requsitos tipo dos rango
                } else if (tipoReq.Equals(2)|| tipoReq.Equals(3))
                {
                    ValidaEvento.Rango docSuc = new ValidaEvento.Rango();
                    docSuc.nombreRequisito = readertipoDos["descripcion"].ToString();
      
                    if (readertipoDos["inferior"].ToString() == "")
                    {
                        docSuc.menor = 0;
                    }
                    else
                    {
                        docSuc.menor = Convert.ToDouble(readertipoDos["inferior"].ToString());
                    }
                    if (readertipoDos["superior"].ToString() == "")
                    {
                        docSuc.mayor = 0;
                    }
                    else
                    {
                        docSuc.mayor = Convert.ToDouble(readertipoDos["superior"].ToString());
                    }
                    if (readertipoDos["cal"].ToString() == "")
                    {
                        docSuc.valor = 0;
                    }
                    else
                    {
                        docSuc.valor = Convert.ToDouble(readertipoDos["cal"].ToString());
                    }
                    bajo = docSuc.menor;
                    alto = docSuc.mayor;

                    if (tipoReq.Equals(2))
                    {
                       
                        if (docSuc.valor >= bajo && docSuc.valor <= alto)
                        {
                            docSuc.validado = true;
                            docSuc.errorvaidado = false;
                        }
                        else
                        {
                            docSuc.validado = false;
                            docSuc.errorvaidado = true;
                            evento.validacionFinal = false;
                        }
                    }
                    else if (tipoReq.Equals(3))
                    {
                        //extraer los rangos del empleado y comprar

                        if (docSuc.valor == bajo )
                        {
                            docSuc.validado = true;
                            docSuc.errorvaidado = false;
                        }
                        else
                        {
                            docSuc.validado = false;
                            docSuc.errorvaidado = true;
                            evento.validacionFinal = false;
                        }
                    }


                    ldocSuc.Add(docSuc);


                } else if (tipoReq.Equals(4))
                {
                    /////////////////////////////////

                    ValidaEvento.Curso docSucC = new ValidaEvento.Curso();
                    docSucC.nombreCurso = readertipoDos["descripcion"].ToString();

                    if (readertipoDos["inferior"].ToString() == "")
                    {
                        docSucC.menor = 0;
                    }
                    else
                    {
                        docSucC.menor = Convert.ToDouble(readertipoDos["inferior"].ToString());
                    }
                    if (readertipoDos["superior"].ToString() == "")
                    {
                        docSucC.mayor = 0;
                    }
                    else
                    {
                        docSucC.mayor = Convert.ToDouble(readertipoDos["superior"].ToString());
                    }
                    if (readertipoDos["cal"].ToString() == "")
                    {
                        docSucC.valor = 0;
                    }
                    else
                    {
                        docSucC.valor = Convert.ToDouble(readertipoDos["cal"].ToString());
                    }
                    bajo = docSucC.menor;
                    alto = docSucC.mayor;

                    if (docSucC.valor >= bajo && docSucC.valor <= alto)
                    {
                        docSucC.validado = true;
                        docSucC.errorvaidado = false;


                    }
                    else
                    {
                        docSucC.validado = false;
                        docSucC.errorvaidado = true;
                        evento.validacionFinal = false;
                    }
          



                    ldocSucC.Add(docSucC);


                    ///////////////////////////////////
                }





            }



            evento.requisitoCursos = ldocSucC;
            evento.requisitosRangos = ldocSuc;

            //validar sexo
            if(fem && !mas)
            {
                if (evento.sexoUsuario.Equals("Masculino"))
                {
                    evento.generoValidado = false;
                    evento.errorGenero = true;
                    evento.validacionFinal = false;
                    evento.mensajevalidacionsexo = "Se solicita genero femenino";
                }
            }else if(!fem && mas){
                if (evento.sexoUsuario.Equals("Femenino"))
                {
                    evento.generoValidado = false;
                    evento.errorGenero = true;
                    evento.validacionFinal = false;
                    evento.mensajevalidacionsexo = "Se solicita genero masculino";
                }
            }



            conexion.Cerrar();
            return evento;

        }
    }
}
