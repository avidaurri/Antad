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
        public LoginApp LoginUsuario(string usuario, string password)
        {

            return loginAccesoDatos.LoginUsuario(usuario, password);
            //return loginAccesoDatos.ValidarUsuario(usuario, password);

        }

        public UserSession GetUser(string usuario)
        {

            return loginAccesoDatos.GetUser(usuario);
            //return loginAccesoDatos.ValidarUsuario(usuario, password);

        }

    }
}
