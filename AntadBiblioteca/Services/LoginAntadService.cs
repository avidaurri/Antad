using AntadBiblioteca.DAO;
using ModelsNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntadBiblioteca.Services
{
    public class LoginAntadService
    {
        private LoginAntadDAO loginAccesoDatos;

        public LoginAntadService(string cadena)
        {
            loginAccesoDatos = new LoginAntadDAO(cadena);
        }
        public LoginAntad LoginUsuario(string usuario, string password)
        {

            return loginAccesoDatos.LoginUsuario(usuario, password);
            //return loginAccesoDatos.ValidarUsuario(usuario, password);

        }
    }
}
