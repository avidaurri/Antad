using AntadBiblioteca.DAO;
using AntadComun.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntadBiblioteca.Services
{
    public class CatalogoRegistroService
    {
        private CatalogoRegistroDAO loginAccesoDatos;
        public CatalogoRegistroService(string cadena)
        {
            loginAccesoDatos = new CatalogoRegistroDAO(cadena);
        }
        public CatalogoRegistro getCatalogo(int idEstado)
        {

            return loginAccesoDatos.getCatalogo(idEstado);
            //return loginAccesoDatos.ValidarUsuario(usuario, password);

        }
    }
}
