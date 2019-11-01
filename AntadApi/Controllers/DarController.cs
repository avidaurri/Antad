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
    public class DarController : ApiController
    {

        public string cadenaConexion = ConfigurationManager.ConnectionStrings["ConexionAntadd"].ConnectionString;
        // GET: api/Dar
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Dar/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Dar
        public int Post([FromBody]ParamValidarEvento validacion)
        {
            DarService servicio = new DarService(cadenaConexion);
            return servicio.Dar(validacion.folioEvento);
        }

        // PUT: api/Dar/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Dar/5
        public void Delete(int id)
        {
        }
    }
}
