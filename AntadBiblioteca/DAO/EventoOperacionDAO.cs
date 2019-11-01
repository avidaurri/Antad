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
    public class EventoOperacionDAO
    {
        public ConexionDB conexion;

        public EventoOperacionDAO(string cadena)
        {
            conexion = new ConexionDB(cadena);
        }

        public List<EventoOperacion> getEventosDisponibles(int idUsuario)
        {
            Utilidades ser = new Utilidades();
            string urlServidor = ser.getUrlServidor(conexion);

            string select = "select eu.id_evento as idEvento, eu.id_usuario as idUsuario, cs.foto_sucursal as fotoSucursal, cs.nombre as nombreSucursal, " +
                "e.fecha as fechaEvento, e.hora_inicio as horaInicioEvento, e.hora_fin as horaFinEvento, ee.nombre as estatusEvento, " +
                "ee.estatus as idEstatusEvento from evento_usuario eu left join evento e on e.id = eu.id_evento " +
                "left join estatus_evento ee on ee.estatus = e.estatus left join cat_sucursal cs on cs.id = e.id_sucursal " +
                " left join cat_empresa ce on ce.id = cs.id_empresa left join usuarios u on u.id = eu.id_usuario " +
                "where eu.id_usuario = @usuario and u.id_rol = 2 "; //and e.fecha >= getdate()

            List<Parametro> parametros = new List<Parametro>();

            Parametro paramUsuario = new Parametro();
            paramUsuario.Nombre = "@usuario";
            paramUsuario.Valor = idUsuario.ToString();
            parametros.Add(paramUsuario);

            SqlDataReader readerEve = conexion.Consultar(select, parametros);

            List<EventoOperacion> eventos = new List<EventoOperacion>();

            /*if (readerEve.Read())
            {*/
            while (readerEve.Read())
            {
                EventoOperacion evento = new EventoOperacion();

                evento.idEvento = Convert.ToInt32(readerEve["idEvento"].ToString());
                evento.idUsuario = Convert.ToInt32(readerEve["idUsuario"].ToString());
                evento.fotoSucursal = urlServidor + readerEve["fotoSucursal"].ToString();
                evento.nombreSucursal = readerEve["nombreSucursal"].ToString();
                evento.fechaEvento = readerEve["fechaEvento"].ToString();
                evento.horaInicioEvento = readerEve["horaInicioEvento"].ToString();
                evento.horaFinEvento = readerEve["horaFinEvento"].ToString();
                evento.estatusEvento = readerEve["estatusEvento"].ToString();
                evento.idEstatusEvento = Convert.ToInt32(readerEve["idEstatusEvento"].ToString());

                eventos.Add(evento);
            }


            // }

            conexion.Cerrar();
            return eventos;
        }
    }
}
