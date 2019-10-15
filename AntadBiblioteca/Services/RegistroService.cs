using AntadBiblioteca.DAO;
using AntadComun.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntadBiblioteca.Services
{
    public class RegistroService
    {
        private RegistroDAO loginAccesoDatos;

        public RegistroService(string cadena)
        {
            loginAccesoDatos = new RegistroDAO(cadena);
        }

        public Registro PostRegistro(Registro user)
        {
            return loginAccesoDatos.PostRegistro(user);
        }
    }
}
