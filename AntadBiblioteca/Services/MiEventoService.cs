using AntadBiblioteca.DAO;
using AntadComun.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntadBiblioteca.Services
{
    public class MiEventoService
    {
        private MiEventoDAO loginAccesoDatos;
        public MiEventoService(string cadena)
        {
            loginAccesoDatos = new MiEventoDAO(cadena);
        }

        public List<MiEvento> getSucursalOperacion()
        {
            return loginAccesoDatos.getSucursalOperacion();
            //return loginAccesoDatos.ValidarUsuario(usuario, password);

        }
    }
}
