using AntadBiblioteca.DAO;
using ModelsNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntadBiblioteca.Services
{
    public class ValidaEventoService
    {
        private ValidaEventoDAO loginAccesoDatos;
        public ValidaEventoService(string cadena)
        {
            loginAccesoDatos = new ValidaEventoDAO(cadena);
        }
        public ValidaEvento getValidacionEvento(int idusuario, string idEvento)
        {

            return loginAccesoDatos.getValidacionEvento(idusuario, idEvento);
            //return loginAccesoDatos.ValidarUsuario(usuario, password);

        }
    }
}
