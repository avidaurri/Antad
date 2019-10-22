using AntadBiblioteca.DAO;
using AntadComun.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntadBiblioteca.Services
{
    public class LoginAppService
    {
        private LoginAppDAO loginAccesoDatos;

        public LoginAppService(string cadena)
        {
            loginAccesoDatos = new LoginAppDAO(cadena);
        }
        public UserSession LoginUsuario(string usuario, string password)
        {

            return loginAccesoDatos.LoginUsuario(usuario, password);
            //return loginAccesoDatos.ValidarUsuario(usuario, password);

        }

        /*public UserSession GetUser(string usuario, string password)
        {

            return loginAccesoDatos.GetUser(usuario, password);
            //return loginAccesoDatos.ValidarUsuario(usuario, password);

        }*/

    }
}
