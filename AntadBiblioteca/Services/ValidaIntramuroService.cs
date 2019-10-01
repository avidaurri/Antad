using AntadBiblioteca.DAO;
using AntadComun.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntadBiblioteca.Services
{
    public class ValidaIntramuroService
    {
        private ValidaIntramuroDAO loginAccesoDatos;

        public ValidaIntramuroService(string cadena)
        {
            loginAccesoDatos = new ValidaIntramuroDAO(cadena);
        }
        public ValidaIntramuro validarIntramuro(int idEvento)
        {

            return loginAccesoDatos.validarIntramuro(idEvento);
            //return loginAccesoDatos.ValidarUsuario(usuario, password);

        }
    }
}
