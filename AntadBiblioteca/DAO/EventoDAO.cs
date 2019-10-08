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
    public class EventoDAO
    {
        public ConexionDB conexion;
        public EventoDAO(string cadena)
        {
            conexion = new ConexionDB(cadena);
        }
        public List<Evento> getEventos(string usuario)
        {
            Utilidades ser = new Utilidades();
            string urlServidor = ser.getUrlServidor(conexion);


            string select = "select ec.folio_evento as folioEvento, l.clv_emp as clv_Empleado, l.login as usuario, " +
                "ct.logo_url as fotoSucursal, ct.folio_centro_trabajo as folioSucursal, ct.nombre_comercial as nombreSucursal, " +
                "ec.fecha_inicial as fechaInicio, ec.fecha_final as fechaFinal, ee.descripcion as estatusEvento, ec.clv_edo_evento as clvEstatusEvento " +
                "from evento_cara ec left join login l on l.clv_emp = ec.clv_emp left join cat_tip_evento cte on cte.clv_tip_evento = ec.clv_tip_evento " +
                "left join edo_evento ee on ee.clv_edo_evento = ec.clv_edo_evento left join centro_trabajo ct on ct.folio_centro_trabajo = ec.folio_centro_trabajo " +
                "left join cadena_centro_trabajo cct on cct.clv_cadena = ct.clv_cadena where l.login = @usuario"; 

            List<Parametro> parametros = new List<Parametro>();

            Parametro paramIdUsuario = new Parametro();
            paramIdUsuario.Nombre = "@usuario";
            paramIdUsuario.Valor = usuario;
            parametros.Add(paramIdUsuario);

            SqlDataReader readerSuc = conexion.Consultar(select, parametros);

            List<Evento> eventos = new List<Evento>();

            while (readerSuc.Read())
            {
                Evento evento = new Evento();
                evento.folioEvento = readerSuc["folioEvento"].ToString();
                evento.clv_Empleado = Convert.ToInt32(readerSuc["clv_Empleado"].ToString());
                evento.usuario = readerSuc["usuario"].ToString();
                evento.fotoSucursal = "https://compilacionweb.online/DemoAntad/" + readerSuc["fotoSucursal"].ToString();
                evento.folioSucursal = readerSuc["folioSucursal"].ToString();
                evento.nombreSucursal = readerSuc["nombreSucursal"].ToString();
                evento.fechaInicio = readerSuc["fechaInicio"].ToString();
                evento.fechaFinal = readerSuc["fechaFinal"].ToString();
                evento.estatusEvento = readerSuc["estatusEvento"].ToString();
                evento.clvEstatusEvento = Convert.ToInt32(readerSuc["clvEstatusEvento"].ToString());

                eventos.Add(evento);

            }

            conexion.Cerrar();
            return eventos;

        }

        public Evento getDetalleEvento(string usuario, string eve)
        {
            /*Utilidades ser = new Utilidades();
             string urlServidor = ser.getUrlServidor(conexion);*/


            string select = "select ec.folio_evento as folioEvento, l.clv_emp as clv_Empleado, l.login as usuario, " +
                "ct.logo_url as fotoSucursal, ct.folio_centro_trabajo as folioSucursal, ct.nombre_comercial as nombreSucursal, " +
                "ec.fecha_inicial as fechaInicio, ec.fecha_final as fechaFinal, ee.descripcion as estatusEvento, ec.clv_edo_evento as clvEstatusEvento " +
                "from evento_cara ec left join login l on l.clv_emp = ec.clv_emp left join cat_tip_evento cte on cte.clv_tip_evento = ec.clv_tip_evento " +
                "left join edo_evento ee on ee.clv_edo_evento = ec.clv_edo_evento left join centro_trabajo ct on ct.folio_centro_trabajo = ec.folio_centro_trabajo " +
                "left join cadena_centro_trabajo cct on cct.clv_cadena = ct.clv_cadena where l.login = @usuario and ec.folio_evento=@evento ";

            List<Parametro> parametros = new List<Parametro>();

            Parametro paramIdUsuario = new Parametro();
            paramIdUsuario.Nombre = "@usuario";
            paramIdUsuario.Valor = usuario;
            parametros.Add(paramIdUsuario);

            Parametro paramIdEvento = new Parametro();
            paramIdEvento.Nombre = "@evento";
            paramIdEvento.Valor = eve;
            parametros.Add(paramIdEvento);

            SqlDataReader readerSuc = conexion.Consultar(select, parametros);

            Evento evento = new Evento();

            if (readerSuc.Read())
            {
                evento.seeQR = false;
                evento.folioEvento = readerSuc["folioEvento"].ToString();
                evento.clv_Empleado = Convert.ToInt32(readerSuc["clv_Empleado"].ToString());
                evento.usuario = readerSuc["usuario"].ToString();
                evento.fotoSucursal = "https://compilacionweb.online/DemoAntad/" + readerSuc["fotoSucursal"].ToString();
                evento.folioSucursal = readerSuc["folioSucursal"].ToString();
                evento.nombreSucursal = readerSuc["nombreSucursal"].ToString();
                evento.fechaInicio = readerSuc["fechaInicio"].ToString();
                evento.fechaFinal = readerSuc["fechaFinal"].ToString();
                evento.estatusEvento = readerSuc["estatusEvento"].ToString();
                evento.clvEstatusEvento = Convert.ToInt32(readerSuc["clvEstatusEvento"].ToString());

                if (evento.clvEstatusEvento.Equals(4))
                {
                    /*string dt = "1/3/2011";
                    DateTime myDateTime = DateTime.Parse(dt);
                    string dia = Convert.ToString(myDateTime.Day);
                    string mes = Convert.ToString(myDateTime.Month);
                    string año = Convert.ToString(myDateTime.Year);*/

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
                            //int nombredidadt = Convert.ToInt32(FechAc.ToString("hh"));
                            //int nombredidat = Convert.ToInt32(evento.fechaInicio.ToString("hh"));
                 
                            //string nombrediadd = fechaF.ToString("d");

                            if (nombredidad.Equals(nombredida))
                            {

                                    evento.seeQR = true;
                                    evento.mensajeEvento = "Mensaje";
                                    evento.descripcionMensajeEvento = "Escanea tu código para obtener autorización";
                 
                            }
                            else
                            {
                                evento.mensajeEvento = "Mensaje";
                                evento.descripcionMensajeEvento = "El evento no corresponde al día de hoy";
                            }

                        }
                        else
                        {
                            evento.mensajeEvento = "Mensaje";
                            evento.descripcionMensajeEvento = "El evento no corresponde al día de hoy";
                        }
                    }
                    else
                    {
                        evento.mensajeEvento = "Mensaje";
                        evento.descripcionMensajeEvento = "El evento no corresponde al día de hoy";
                    }

                    if (fechaF != FechAc) // Si la fecha indicada es menor o igual a la fecha actual
                    {
                        evento.mensajeEvento = "Mensaje";
                        evento.descripcionMensajeEvento = "El evento no corresponde al día de hoy";
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
                }



            }

            conexion.Cerrar();
            return evento;

        }


    }
}
