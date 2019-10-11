using AntadBiblioteca.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntadBiblioteca.Services
{
    public class DarService
    {
        private DarDAO loginAccesoDatos;
        public DarService(string cadena)
        {
            loginAccesoDatos = new DarDAO(cadena);
        }
        public int Dar(string idEvento)
        {

            return loginAccesoDatos.Dar(idEvento);
            //return loginAccesoDatos.ValidarUsuario(usuario, password);

        }
    }
}
