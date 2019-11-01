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
    public class MisEventosController : ApiController
    {
        public string cadenaConexion = ConfigurationManager.ConnectionStrings["ConexionAntad"].ConnectionString;
        // GET: api/MisEventosC
        /*public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }*/

        // GET: api/MisEventosC/5
        public List<MiEvento> Get()
        {
            MiEventoService servicio = new MiEventoService(cadenaConexion);
            return servicio.getSucursalOperacion();
        }

        // POST: api/MisEventosC
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/MisEventosC/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/MisEventosC/5
        public void Delete(int id)
        {
        }
    }
}
