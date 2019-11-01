using AntadBiblioteca.Services;
using ModelsNet.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AntadApi.Controllers
{
    public class CheckInController : ApiController
    {
        public string cadenaConexion = ConfigurationManager.ConnectionStrings["ConexionAntad"].ConnectionString;
        // GET: api/CheckIn
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/CheckIn/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/CheckIn
        public CheckIn Post([FromBody]CheckIn usuario)
        {
            CheckInService servicio = new CheckInService(cadenaConexion);
            return servicio.hacerCheckIn(usuario.idEvento, usuario.idUsuario);
        }

        // PUT: api/CheckIn/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/CheckIn/5
        public void Delete(int id)
        {
        }
    }
}
