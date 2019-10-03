

namespace AntadApi.Controllers
{
    using AntadApi.Helpers;
    using AntadBiblioteca.Services;
    using AntadComun.Models;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Web;
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

            //var path = Path.Combine(HttpContext.Current.Server.MapPath("foede"), "name");
            UsuarioService servicio = new UsuarioService(cadenaConexion);

            //subir iagen
            if (value.ImageArray != null && value.ImageArray.Length > 0)
            {
                var stream = new MemoryStream(value.ImageArray);
                var guid = Guid.NewGuid().ToString();
                var file = $"{guid}.jpg";
                var folder = "~/Content/Usuarios";
                var fullPath = $"{folder}/{file}";
                var response = FilesHelper.UploadPhoto(stream, folder, file);

                if (response)
                {
                    value.ImagePath = file;
                }
            }

            return servicio.PostUsuario(value);
        }

        // PUT: api/Usuarios/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Usuarios/5
        public int Delete(int id)
        {
            UsuarioService servicio = new UsuarioService(cadenaConexion);
            return servicio.BorrarUsuario(id);
        }
    }
}
