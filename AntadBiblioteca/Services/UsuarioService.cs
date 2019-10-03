using AntadBiblioteca.DAO;
using AntadComun.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntadBiblioteca.Services
{
    public class UsuarioService
    {
        private UsuarioDAO loginAccesoDatos;
        public UsuarioService(string cadena)
        {
            loginAccesoDatos = new UsuarioDAO(cadena);
        }

        public List<Usuario> getUsuarios()
        {
            return loginAccesoDatos.getUsuarios();
            //return loginAccesoDatos.ValidarUsuario(usuario, password);

        }

        public int PostUsuario(Usuario user)
        {
            return loginAccesoDatos.PostUsuario(user);
        }
        public int BorrarUsuario(int id)
        {
            return loginAccesoDatos.BorrartUsuario(id);
        }

    }
}
