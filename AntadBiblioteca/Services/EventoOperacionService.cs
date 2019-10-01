using AntadBiblioteca.DAO;
using AntadComun.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntadBiblioteca.Services
{
    public class EventoOperacionService
    {
        private EventoOperacionDAO evento;
        public EventoOperacionService(string cadena)
        {
            evento = new EventoOperacionDAO(cadena);
        }
        public List<EventoOperacion> getEventosDisponibles(int usuario)
        {

            return evento.getEventosDisponibles(usuario);
            //return loginAccesoDatos.ValidarUsuario(usuario, password);

        }
    }
}
