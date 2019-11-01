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
    public class EventoDAO
    {
        public ConexionDB conexion;
        public EventoDAO(string cadena)
        {
            conexion = new ConexionDB(cadena);
        }
        public List<Evento> getEventos(int usuario)
        {
            Utilidades ser = new Utilidades();
            string urlServidor = ser.getUrlServidor(conexion);


            /*string select = "select ec.folio_evento as folioEvento, l.clv_emp as clv_Empleado, l.login as usuario, ct.logo_url as fotoSucursal, " +
                "ct.folio_centro_trabajo as folioSucursal, ct.nombre_comercial as nombreSucursal, ec.fecha_inicial as fechaInicio, ec.fecha_final as fechaFinal, " +
                "ee.descripcion as estatusEvento, ec.clv_edo_evento as clvEstatusEvento from evento_personal ep left join login l on l.clv_emp = ep.clv_emp " +
                "left join evento_cara ec on ec.folio_evento = ep.folio_evento left join cat_tip_evento cte on cte.clv_tip_evento = ec.clv_tip_evento left join " +
                "edo_evento ee on ee.clv_edo_evento = ec.clv_edo_evento left join centro_trabajo ct on ct.folio_centro_trabajo = ec.folio_centro_trabajo " +
                "left join cadena_centro_trabajo cct on cct.clv_cadena = ct.clv_cadena where l.login =@usuario ";*/

            /*string select = "select ec.folio_evento as folioEvento, ec.fecha_inicial as fechaInicio, " +
                "ec.fecha_final as fechaFinal, ec.clv_edo_evento as clvEdoEvento, ee.descripcion as estadoEvento, " +
                "ec.clv_tip_evento as clvTipoEvento, cte.descripcion as tipoEvento, ep.clv_emp as clvEmp, ct.logo_url as fotoCentroTrabajo, " +
                "ct.folio_centro_trabajo as folioCentroTrabajo, ct.nombre_comercial as nombreCentroTrabajo, cct.clv_cadena " +
                "as clvCadenaCentroTrabajo, cct.nombre as cadenaCentroTrabajo, cct.url_logo as fotoCadenaCentroTrabajo from evento_cara ec " +
                "left join evento_personal ep on ep.folio_evento = ec.folio_evento left join centro_trabajo ct on " +
                "ct.folio_centro_trabajo = ec.folio_centro_trabajo left join cadena_centro_trabajo cct on cct.clv_cadena = ct.clv_cadena left " +
                "join cat_tip_evento cte on cte.clv_tip_evento = ec.clv_tip_evento left join edo_evento ee on ee.clv_edo_evento = ec.clv_edo_evento " +
                "where ep.clv_emp = @usuario order by ec.fecha_inicial asc";*/

            string select = "select ec.folio_evento as folioEvento, ec.fecha_inicial as fechaInicio, ec.fecha_final as fechaFinal, ec.clv_edo_evento as clvEdoEvento, ee.descripcion as estadoEvento," +
                " ec.clv_tip_evento as clvTipoEvento, cte.descripcion as tipoEvento, ep.clv_emp as clvEmp, ct.logo_url as fotoCentroTrabajo,  ct.folio_centro_trabajo as folioCentroTrabajo, " +
                "ct.nombre_comercial as nombreCentroTrabajo, cct.clv_cadena as clvCadenaCentroTrabajo, cct.nombre as cadenaCentroTrabajo, cct.url_logo as fotoCadenaCentroTrabajo,	" +
                "(select nombre from adscripcion where adscripcion = (SUBSTRING(bpa.adscripcion, 0, 3) + '000')) as agencia, ec.folio_proyecto as folioProyecto, cp.puesto as puesto, " +
                "cp.clv_puesto as clvPuesto from evento_cara ec left join evento_personal ep on ep.folio_evento = ec.folio_evento left join centro_trabajo ct on " +
                "ct.folio_centro_trabajo = ec.folio_centro_trabajo left join cadena_centro_trabajo cct on cct.clv_cadena = ct.clv_cadena left join cat_tip_evento cte " +
                "on cte.clv_tip_evento = ec.clv_tip_evento left join edo_evento ee on ee.clv_edo_evento = ec.clv_edo_evento left join bitacora_proyecto_adscripcion bpa " +
                "on bpa.folio_proyecto = ec.folio_proyecto left join empleado e on e.clv_emp = ep.clv_emp left join cat_puesto cp on cp.clv_puesto = e.clv_puesto " +
                "where ep.clv_emp = @usuario order by ec.fecha_inicial asc";

            List<Parametro> parametros = new List<Parametro>();

            Parametro paramIdUsuario = new Parametro();
            paramIdUsuario.Nombre = "@usuario";
            paramIdUsuario.Valor = usuario.ToString();
            parametros.Add(paramIdUsuario);

            SqlDataReader readerSuc = conexion.Consultar(select, parametros);

            List<Evento> eventos = new List<Evento>();

            while (readerSuc.Read())
            {
                Evento evento = new Evento();
                evento.folioEvento = readerSuc["folioEvento"].ToString();
                evento.fechaInicio = readerSuc["fechaInicio"].ToString();
                evento.fechaFinal = readerSuc["fechaFinal"].ToString();
                evento.clvEdoEvento = Convert.ToInt32(readerSuc["clvEdoEvento"].ToString());
                evento.estadoEvento = readerSuc["estadoEvento"].ToString();
                evento.clvTipoEvento = Convert.ToInt32(readerSuc["clvTipoEvento"].ToString());
                evento.tipoEvento = readerSuc["tipoEvento"].ToString();
                evento.clvEmp = Convert.ToInt32(readerSuc["clvEmp"].ToString());
                evento.fotoCentroTrabajo = "https://compilacionweb.online/DemoAntad/" + readerSuc["fotoCentroTrabajo"].ToString();
                evento.folioCentroTrabajo = readerSuc["folioCentroTrabajo"].ToString();
                evento.nombreCentroTrabajo = readerSuc["nombreCentroTrabajo"].ToString();
                evento.clvCadenaCentroTrabajo = Convert.ToInt32(readerSuc["clvCadenaCentroTrabajo"].ToString());
                evento.cadenaCentroTrabajo = readerSuc["cadenaCentroTrabajo"].ToString();
                evento.fotoCadenaCentroTrabajo = readerSuc["fotoCadenaCentroTrabajo"].ToString();

                evento.clvPuesto = Convert.ToInt32(readerSuc["clvPuesto"].ToString());
                evento.agencia = readerSuc["agencia"].ToString();
                evento.folioProyecto = readerSuc["folioProyecto"].ToString();
                evento.puesto = readerSuc["puesto"].ToString();
                




                eventos.Add(evento);

            }

            conexion.Cerrar();
            return eventos;

        }

        public Evento getDetalleEvento(int clvEmp, string eve)
        {

            string select = "select ec.folio_evento as folioEvento, ec.fecha_inicial as fechaInicio, " +
                "ec.fecha_final as fechaFinal, ec.clv_edo_evento as clvEdoEvento, ee.descripcion as estadoEvento, " +
                "ec.clv_tip_evento as clvTipoEvento, cte.descripcion as tipoEvento, ep.clv_emp as clvEmp, ct.logo_url as fotoCentroTrabajo, " +
                "ct.folio_centro_trabajo as folioCentroTrabajo, ct.nombre_comercial as nombreCentroTrabajo, cct.clv_cadena " +
                "as clvCadenaCentroTrabajo, cct.nombre as cadenaCentroTrabajo, cct.url_logo as fotoCadenaCentroTrabajo from evento_cara ec " +
                "left join evento_personal ep on ep.folio_evento = ec.folio_evento left join centro_trabajo ct on " +
                "ct.folio_centro_trabajo = ec.folio_centro_trabajo left join cadena_centro_trabajo cct on cct.clv_cadena = ct.clv_cadena left " +
                "join cat_tip_evento cte on cte.clv_tip_evento = ec.clv_tip_evento left join edo_evento ee on ee.clv_edo_evento = ec.clv_edo_evento " +
                "where ep.clv_emp = @clvEmp and ec.folio_evento=@evento";

            List<Parametro> parametros = new List<Parametro>();

            Parametro paramIdUsuario = new Parametro();
            paramIdUsuario.Nombre = "@clvEmp";
            paramIdUsuario.Valor = clvEmp.ToString();
            parametros.Add(paramIdUsuario);

            Parametro paramIdEvento = new Parametro();
            paramIdEvento.Nombre = "@evento";
            paramIdEvento.Valor = eve;
            parametros.Add(paramIdEvento);

            SqlDataReader readerSuc = conexion.Consultar(select, parametros);

            Evento evento = new Evento();

            if (readerSuc.Read())
            {
                evento.folioEvento = readerSuc["folioEvento"].ToString();
                evento.fechaInicio = readerSuc["fechaInicio"].ToString();
                evento.fechaFinal = readerSuc["fechaFinal"].ToString();
                evento.clvEdoEvento = Convert.ToInt32(readerSuc["clvEdoEvento"].ToString());
                evento.estadoEvento = readerSuc["estadoEvento"].ToString();
                evento.clvTipoEvento = Convert.ToInt32(readerSuc["clvTipoEvento"].ToString());
                evento.tipoEvento = readerSuc["tipoEvento"].ToString();
                evento.clvEmp = Convert.ToInt32(readerSuc["clvEmp"].ToString());
                evento.fotoCentroTrabajo = "https://compilacionweb.online/DemoAntad/" + readerSuc["fotoCentroTrabajo"].ToString();
                evento.folioCentroTrabajo = readerSuc["folioCentroTrabajo"].ToString();
                evento.nombreCentroTrabajo = readerSuc["nombreCentroTrabajo"].ToString();
                evento.clvCadenaCentroTrabajo = Convert.ToInt32(readerSuc["clvCadenaCentroTrabajo"].ToString());
                evento.cadenaCentroTrabajo = readerSuc["cadenaCentroTrabajo"].ToString();
                evento.fotoCadenaCentroTrabajo = readerSuc["fotoCadenaCentroTrabajo"].ToString();

                if (evento.clvEdoEvento.Equals(4))
                {
                    //agendado

                    DateTime fechaF = Convert.ToDateTime(evento.fechaInicio).Date;
                    DateTime FechAc = DateTime.Now.Date;

                    String date = DateTime.Now.Date.ToString();
                    String Month = DateTime.Now.Month.ToString();
                    String Year = DateTime.Now.Year.ToString();

                    String dated = fechaF.Date.ToString();
                    String Monthd = fechaF.Month.ToString();
                    String Yeard = fechaF.Year.ToString();

                    if (Yeard.Equals(Year))
                    {
                        if (Monthd.Equals(Month))
                        {
                            int nombredidad = Convert.ToInt32(FechAc.ToString("dd"));
                            int nombredida = Convert.ToInt32(fechaF.ToString("dd"));

                            if (nombredidad.Equals(nombredida))
                            {

                                    evento.seeQR = true;
                                    evento.mensajeEvento = "Mensaje";
                                    evento.descripcionMensajeEvento = "El evento no esta autorizado, porfavor muestra el código QR para solicitar autorización";
                                    evento.checkIn = false;
                                     evento.seeUpdate = true;
                            }
                            else
                            {
                                evento.mensajeEvento = "Mensaje";
                                evento.descripcionMensajeEvento = "El evento no corresponde al día de hoy";
                                evento.seeQR = false;
                                evento.seeUpdate = true;
                                evento.checkIn = false;
                            }

                        }
                        else
                        {
                            evento.mensajeEvento = "Mensaje";
                            evento.descripcionMensajeEvento = "El evento no corresponde al día de hoy";
                            evento.seeQR = false;
                            evento.seeUpdate = true;
                            evento.checkIn = false;
                        }
                    }
                    else
                    {
                        evento.mensajeEvento = "Mensaje";
                        evento.descripcionMensajeEvento = "El evento no corresponde al día de hoy";
                        evento.seeQR = false;
                        evento.seeUpdate = true;
                        evento.checkIn = false;
                    }

                    if (fechaF != FechAc) // Si la fecha indicada es menor o igual a la fecha actual
                    {
                        evento.mensajeEvento = "Mensaje";
                        evento.descripcionMensajeEvento = "El evento no corresponde al día de hoy";
                        evento.seeQR = false;
                        evento.seeUpdate = true;
                        evento.checkIn = false;
                    }
                    else
                    {
                        evento.seeQR = true;
                        if (fechaF != FechAc)
                        {

                        }
                        else
                        {

                        }
                        //Operación 2
                    }
                }else if (evento.clvEdoEvento.Equals(3))
                {
                    //evento autorizado.
                    evento.mensajeEvento = "Mensaje";
                    evento.descripcionMensajeEvento = "El evento esta autorizado, ya puedes hacer checkIn";
                    evento.seeQR = false;
                    evento.seeUpdate = true;
                    evento.checkIn = true;

                }
                else if (evento.clvEdoEvento.Equals(20))
                {
                    //evento autorizado.
                    evento.mensajeEvento = "Mensaje";
                    evento.descripcionMensajeEvento = "El evento se encuentra cancelado";
                    evento.seeQR = false;
                    evento.seeUpdate = false;
                    evento.checkIn = false;

                }



            }
            else
            {
                string selectd = "select ec.folio_evento as folioEvento, ec.fecha_inicial as fechaInicio, " +
                    "ec.fecha_final as fechaFinal, ec.clv_edo_evento as clvEdoEvento, ee.descripcion as estadoEvento, " +
                    "ec.clv_tip_evento as clvTipoEvento, cte.descripcion as tipoEvento, ep.clv_emp as clvEmp, ct.logo_url as fotoCentroTrabajo, " +
                    "ct.folio_centro_trabajo as folioCentroTrabajo, ct.nombre_comercial as nombreCentroTrabajo, cct.clv_cadena " +
                    "as clvCadenaCentroTrabajo, cct.nombre as cadenaCentroTrabajo, cct.url_logo as fotoCadenaCentroTrabajo from evento_cara ec " +
                    "left join evento_personal ep on ep.folio_evento = ec.folio_evento left join centro_trabajo ct on " +
                    "ct.folio_centro_trabajo = ec.folio_centro_trabajo left join cadena_centro_trabajo cct on cct.clv_cadena = ct.clv_cadena left " +
                    "join cat_tip_evento cte on cte.clv_tip_evento = ec.clv_tip_evento left join edo_evento ee on ee.clv_edo_evento = ec.clv_edo_evento " +
                    "where ec.folio_evento=@evento";

                    SqlDataReader readerSucd = conexion.Consultar(selectd, parametros);


                if (readerSucd.Read())
                {

                    evento.folioEvento = readerSucd["folioEvento"].ToString();
                    evento.fechaInicio = readerSucd["fechaInicio"].ToString();
                    evento.fechaFinal = readerSucd["fechaFinal"].ToString();
                    evento.clvEdoEvento = Convert.ToInt32(readerSucd["clvEdoEvento"].ToString());
                    evento.estadoEvento = readerSucd["estadoEvento"].ToString();
                    evento.clvTipoEvento = Convert.ToInt32(readerSucd["clvTipoEvento"].ToString());
                    evento.tipoEvento = readerSucd["tipoEvento"].ToString();
                    evento.clvEmp = Convert.ToInt32(readerSucd["clvEmp"].ToString());
                    evento.fotoCentroTrabajo = "https://compilacionweb.online/DemoAntad/" + readerSucd["fotoCentroTrabajo"].ToString();
                    evento.folioCentroTrabajo = readerSucd["folioCentroTrabajo"].ToString();
                    evento.nombreCentroTrabajo = readerSucd["nombreCentroTrabajo"].ToString();
                    evento.clvCadenaCentroTrabajo = Convert.ToInt32(readerSucd["clvCadenaCentroTrabajo"].ToString());
                    evento.cadenaCentroTrabajo = readerSucd["cadenaCentroTrabajo"].ToString();
                    evento.fotoCadenaCentroTrabajo = readerSucd["fotoCadenaCentroTrabajo"].ToString();

                    evento.mensajeEvento = "Mensaje";
                    evento.descripcionMensajeEvento = "El evento no esta disponible para ti, porfavor solicita tu asignación";
                    evento.seeQR = false;
                    evento.seeUpdate = true;
                    evento.checkIn = false;
                }

            }

            conexion.Cerrar();
            return evento;

        }


    }
}
