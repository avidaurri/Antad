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
    public class ValidaEventoDAO
    {
        public ConexionDB conexion;
        public ValidaEventoDAO(string cadena)
        {
            conexion = new ConexionDB(cadena);
        }
        public ValidaEvento getValidacionEvento(string idusuario, string idEvento)
        {
            Utilidades ser = new Utilidades();
            string urlServidor = ser.getUrlServidor(conexion);

            ValidaEvento evento = new ValidaEvento();
            evento.validacionFinal = true;

            string select = "select ec.folio_evento as folioEvento, l.clv_emp as clv_Empleado, l.login as usuario, ct.logo_url as fotoSucursal, ct.folio_centro_trabajo as folioSucursal, " +
                "ct.nombre_comercial as nombreSucursal, ec.fecha_inicial as fechaInicio, ec.fecha_final as fechaFinal, ee.descripcion as estatusEvento, ec.clv_edo_evento as clvEstatusEvento, " +
                "l.reg_foto as fotoUsuario, (l.reg_nombre + ' ' + l.reg_apellido_paterno + ' ' + l.reg_apellido_materno) as nombreUsuario, SUBSTRING(l.reg_curp, 11, 1) as sexoUsuario, " +
                "DATEDIFF(year, CASE WHEN SUBSTRING(l.reg_curp, 5, 2) < 40 THEN '20' + SUBSTRING(l.reg_curp, 5, 2) ELSE '19' + SUBSTRING(l.reg_curp, 5, 2) end  " +
                "+ '-' + SUBSTRING(l.reg_curp, 7, 2) + '-' + SUBSTRING(l.reg_curp, 9, 2), getdate()) as edadUsuario, " +
                "(select CASE WHEN(select clv_empre from emp_requisitos_evento where clv_emp = l.clv_emp and clv_requisito_evento = 6)> 1 THEN 'True' ELSE 'False' end) as imssUsuario " +
                "from evento_cara ec left join login l on l.clv_emp = ec.clv_emp left join cat_tip_evento cte on cte.clv_tip_evento = ec.clv_tip_evento " +
                "left join edo_evento ee on ee.clv_edo_evento = ec.clv_edo_evento left join centro_trabajo ct on ct.folio_centro_trabajo = ec.folio_centro_trabajo left join " +
                "cadena_centro_trabajo cct on cct.clv_cadena = ct.clv_cadena where l.login = @usuario and ec.folio_evento = @evento";

            List<Parametro> parametros = new List<Parametro>();

            Parametro paramIdUsuario = new Parametro();
            paramIdUsuario.Nombre = "@usuario";
            paramIdUsuario.Valor = idusuario;
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
                evento.usuario = readerSuc["usuario"].ToString();
                evento.fotoSucursal = readerSuc["fotoSucursal"].ToString();
                evento.folioSucursal = readerSuc["folioSucursal"].ToString();
                evento.nombreSucursal = readerSuc["nombreSucursal"].ToString();
                evento.fechaInicio = readerSuc["fechaInicio"].ToString();
                evento.fechaFinal = readerSuc["fechaFinal"].ToString();
                evento.estatusEvento = readerSuc["estatusEvento"].ToString();
                evento.clvEstatusEvento = Convert.ToInt32(readerSuc["clvEstatusEvento"].ToString());
                evento.fotoUsuario = readerSuc["fotoUsuario"].ToString();
                evento.nombreUsuario = readerSuc["nombreUsuario"].ToString();
                evento.sexoUsuario = readerSuc["sexoUsuario"].ToString();
                evento.edadUsuario = Convert.ToInt32(readerSuc["edadUsuario"].ToString());
                evento.imssUsuario = Convert.ToBoolean(readerSuc["imssUsuario"].ToString());

            }

            //sacar sexo sucursal e imss sucursal

            string sexoSuc = "  select clv_requisito as requisito from requisitos_cadena where clv_cadena=( " +
                "select top 1 cct.clv_cadena from evento_cara ec left join centro_trabajo ct on ct.folio_centro_trabajo = ec.folio_centro_trabajo " +
                "  left join cadena_centro_trabajo cct on cct.clv_cadena = ct.clv_cadena where folio_evento = @evento   )";


            SqlDataReader readersexoSuc = conexion.Consultar(sexoSuc, parametros);

            evento.imssSucursal = false;
            bool feneminoSuc = false;
            bool masculinoSuc = false;
            int requisito;
            while (readersexoSuc.Read())
            {
                requisito = Convert.ToInt32(readersexoSuc["requisito"].ToString());

                if (requisito.Equals(6))
                {
                    evento.imssSucursal = true;
                    
                }

                if (requisito.Equals(2))
                {
                    feneminoSuc = true;
                }

                if (requisito.Equals(3))
                {
                    masculinoSuc = true;
                }
            }

            //valida imss
            if (evento.imssSucursal)
            {
                if (!evento.imssUsuario)
                {
                    evento.validaImss = false;
                }
            }
            else
            {
                evento.validaImss = true;
            }


            // llenar sexo sucursal
            evento.validaSexo = true;
            if(feneminoSuc && masculinoSuc)
            {
                evento.sexoSucursal = "Ambos Sexos";
            }
            else if(!feneminoSuc && masculinoSuc)
            {

                evento.sexoSucursal = "Sexo masculino";
                if (evento.sexoUsuario.Equals("H"))
                {
                    
                    evento.validacionFinal = false;
                    evento.validaSexo = false;
                }
                    
            }
            else if (feneminoSuc && !masculinoSuc)
            {
                evento.sexoSucursal = "Sexo femenino";
                if (evento.sexoUsuario.Equals("M"))
                {
                    
                    evento.validacionFinal = false;
                    evento.validaSexo = false;
                }
            }


            // extraer los requisitos de tipo 2 y 3 y validarlos


            string tipoDosSuc = "select per.clv_requisito_evento as clvRequisito, per.rango_inferior as menor, per.rango_superior as mayor, " +
                "ccc.descripcion as nombreRequisito, ccc.clv_tipo_requisito as tipoReq from proyecto_evento_requisitos per left join cat_requisito_evento ccc on ccc.clv_requisito_evento = per.clv_requisito_evento " +
                "where per.clv_requisito_evento in( select clv_requisito from requisitos_cadena rc left join cat_requisito_evento cre on " +
                "rc.clv_requisito = cre.clv_requisito_evento where clv_tipo_requisito in(2,3,4) and clv_cadena = " +
                "( select top 1 cct.clv_cadena from evento_cara ec " +
                " left join centro_trabajo ct on ct.folio_centro_trabajo = ec.folio_centro_trabajo  " +
                " left join cadena_centro_trabajo cct on cct.clv_cadena = ct.clv_cadena   where folio_evento = @evento ) )";


            SqlDataReader readertipoDos = conexion.Consultar(tipoDosSuc, parametros);

            List<ValidaEvento.Rango> ldocSuc = new List<ValidaEvento.Rango>();
            double bajo;
            double alto;
            int clvReqEven;
            //double valll = 0;
            while (readertipoDos.Read())
            {
                clvReqEven = Convert.ToInt32(readertipoDos["clvRequisito"].ToString());
                ValidaEvento.Rango docSuc = new ValidaEvento.Rango();
                docSuc.nombreRequisito = readertipoDos["nombreRequisito"].ToString();
                docSuc.menor = Convert.ToDouble(readertipoDos["menor"].ToString());
                docSuc.mayor = Convert.ToDouble(readertipoDos["mayor"].ToString());
                bajo = docSuc.menor;
                alto = docSuc.mayor;
                if (clvReqEven.Equals(1))
                {
                    docSuc.valor = evento.edadUsuario;
                    if (evento.edadUsuario>=bajo && evento.edadUsuario <= alto)
                    {
                        docSuc.validado = true;
                    }
                    else
                    {
                        docSuc.validado = false;
                    }
                }
                else
                {
                   //extraer los rangos del empleado y comprar

               
           
                    paramClvReq.Valor = readertipoDos["clvRequisito"].ToString();
              


                    string vall = "select erc.valor as valll from emp_requisitos_evento erc where erc.clv_requisito_evento = @req and erc.clv_emp = " +
                        "(  select top 1 clv_emp from login where login = @usuario ) ";

                    SqlDataReader readerVal= conexion.Consultar(vall, parametros);

                    if (readerVal.Read())
                    {

                        if (clvReqEven.Equals(2))
                        {
                            docSuc.valor = Convert.ToDouble(readerVal["valll"].ToString());
                            if (docSuc.valor >= bajo && docSuc.valor <= alto)
                            {
                                docSuc.validado = true;
                            }
                            else
                            {
                                docSuc.mens = "No esta dentro del rango requerido";
                                docSuc.validado = false;
                            }
                        }else if (clvReqEven.Equals(3))
                        {
                            docSuc.valor = Convert.ToDouble(readerVal["valll"].ToString());
                            if (docSuc.valor >= bajo )
                            {
                                docSuc.validado = true;
                            }
                            else
                            {
                                docSuc.mens = "No alcanza el valor requerido";
                                docSuc.validado = false;
                            }
                        }
                        else if (clvReqEven.Equals(4))
                        {
                            docSuc.valor = Convert.ToDouble(readerVal["valll"].ToString());
                            docSuc.validado = true;

                        }



                    }
                    else
                    {
                        if (clvReqEven.Equals(4))
                        {
                        
                            docSuc.validado = false;
                            docSuc.mens = "no cuenta cuenta con curso";
                        }
                        else
                        {

                            docSuc.validado = false;
                            docSuc.mens = "no tiene registro";


                        }


                    }
                }


                ldocSuc.Add(docSuc);

            }



            evento.requisitosRangos = ldocSuc;





            conexion.Cerrar();
            return evento;

        }
    }
}
