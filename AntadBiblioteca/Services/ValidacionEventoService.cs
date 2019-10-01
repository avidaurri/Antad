using AntadBiblioteca.DAO;
using AntadComun.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntadBiblioteca.Services
{
    public class ValidacionEventoService
    {
        private ValidacionEventoDAO loginAccesoDatos;
        public ValidacionEventoService(string cadena)
        {
            loginAccesoDatos = new ValidacionEventoDAO(cadena);
        }
        public ValidacionEvento getValidacionEvento(int idusuario, int idEvento, int idIntra)
        {

            return loginAccesoDatos.getValidacionEvento(idusuario, idEvento, idIntra);
            //return loginAccesoDatos.ValidarUsuario(usuario, password);

        }
    }
}
