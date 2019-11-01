using AntadBiblioteca.DAO;
using ModelsNet.Models;
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
        public CatalogoRegistro getCatalogo()
        {

            return loginAccesoDatos.getCatalogo();

        }
        public CatalogoRegistro getMunicipios(int idEstado)
        {

            return loginAccesoDatos.getMunicipios(idEstado);

        }
    }
}
