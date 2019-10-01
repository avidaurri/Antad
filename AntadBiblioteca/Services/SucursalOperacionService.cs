using AntadBiblioteca.DAO;
using AntadComun.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntadBiblioteca.Services
{
    public class SucursalOperacionService
    {
        private SucursalOperacionDAO loginAccesoDatos;
        public SucursalOperacionService(string cadena)
        {
            loginAccesoDatos = new SucursalOperacionDAO(cadena);
        }

        public SucursalOperacion getSucursalOperacion(int idusuario, double latitud, double longitud)
        {

            return loginAccesoDatos.getSucursalOperacion(idusuario, latitud, longitud);
            //return loginAccesoDatos.ValidarUsuario(usuario, password);

        }
    }
}
