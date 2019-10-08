using AntadBiblioteca.DAO;
using AntadComun.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntadBiblioteca.Services
{
    public class EventoService
    {
        private EventoDAO loginAccesoDatos;
        public EventoService(string cadena)
        {
            loginAccesoDatos = new EventoDAO(cadena);
        }

        public List<Evento> getEventos(string usuario)
        {
            return loginAccesoDatos.getEventos(usuario);
            //return loginAccesoDatos.ValidarUsuario(usuario, password);

        }


    }
}
