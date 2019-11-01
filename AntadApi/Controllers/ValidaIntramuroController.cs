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
    public class ValidaIntramuroController : ApiController
    {
        public string cadenaConexion = ConfigurationManager.ConnectionStrings["ConexionAntad"].ConnectionString;
        // GET: api/ValidaIntramuro
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ValidaIntramuro/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ValidaIntramuro
        public ValidaIntramuro Post([FromBody]ValidaIntramuro usuario)
        {
            ValidaIntramuroService servicio = new ValidaIntramuroService(cadenaConexion);
            return servicio.validarIntramuro(usuario.idEvento);
        }

        // PUT: api/ValidaIntramuro/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ValidaIntramuro/5
        public void Delete(int id)
        {
        }
    }
}
