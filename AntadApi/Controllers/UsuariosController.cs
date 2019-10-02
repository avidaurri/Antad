

namespace AntadApi.Controllers
{
    using AntadBiblioteca.Services;
    using AntadComun.Models;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Web.Http;
    public class UsuariosController : ApiController
    {
        public string cadenaConexion = ConfigurationManager.ConnectionStrings["ConexionAntad"].ConnectionString;
        // GET: api/Usuarios
        /* public IEnumerable<string> Get()
         {
             return new string[] { "value1", "value2" };
         }*/

        // GET: api/Usuarios/5
        public List<Usuario> Get()
        {
            UsuarioService servicio = new UsuarioService(cadenaConexion);
            return servicio.getUsuarios();
        }

        // POST: api/Usuarios
        public int Post([FromBody]Usuario value)
        {
            UsuarioService servicio = new UsuarioService(cadenaConexion);
            return servicio.PostUsuario(value);
        }

        // PUT: api/Usuarios/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Usuarios/5
        public void Delete(int id)
        {
        }
    }
}
