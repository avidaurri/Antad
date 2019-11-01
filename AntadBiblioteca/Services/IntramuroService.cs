using AntadBiblioteca.DAO;
using ModelsNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntadBiblioteca.Services
{
    public class IntramuroService
    {
        private IntramuroDAO loginAccesoDatos;

        public IntramuroService(string cadena)
        {
            loginAccesoDatos = new IntramuroDAO(cadena);
        }

        public Intramuro GetIntramuro(int usuario, double latitud, double longitud)
        {

            return loginAccesoDatos.GetIntramuro(usuario, latitud, longitud);
            //return loginAccesoDatos.ValidarUsuario(usuario, password);

        }
    }
}
