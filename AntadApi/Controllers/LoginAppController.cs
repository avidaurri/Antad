using AntadBiblioteca.Services;
using AntadComun.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AntadApi.Controllers
{
    public class LoginAppController : ApiController
    {
        public string cadenaConexion = ConfigurationManager.ConnectionStrings["ConexionAntadd"].ConnectionString;
        // GET: api/LoginApp
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/LoginApp/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/LoginApp
        public UserSession Post([FromBody]UserSession usuario)
        {
            LoginAppService servicio = new LoginAppService(cadenaConexion);
            return servicio.LoginUsuario(usuario.usuario, usuario.password);
        }

        /*[HttpPost]
        [Authorize]
        [Route("GetUser")]
        public UserSession GetUser([FromBody]GetUserRequest usuario)
        {
            LoginAppService servicio = new LoginAppService(cadenaConexion);
            return servicio.GetUser(usuario.User);
        }*/

        // PUT: api/LoginApp/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/LoginApp/5
        public void Delete(int id)
        {
        }
    }
}
