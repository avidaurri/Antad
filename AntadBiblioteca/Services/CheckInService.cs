using AntadBiblioteca.DAO;
using ModelsNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntadBiblioteca.Services
{
    public class CheckInService
    {
        private CheckInDAO loginAccesoDatos;

        public CheckInService(string cadena)
        {
            loginAccesoDatos = new CheckInDAO(cadena);
        }
        public CheckIn hacerCheckIn(int usuaidEvento, int idUsuario)
        {

            return loginAccesoDatos.hacerCheckIn(usuaidEvento, idUsuario);
            //return loginAccesoDatos.ValidarUsuario(usuario, password);

        }
    }
}
