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
    public class ValidacionEventoController : ApiController
    {
        public string cadenaConexion = ConfigurationManager.ConnectionStrings["ConexionAntad"].ConnectionString;
        // GET: api/ValidacionEvento
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ValidacionEvento/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ValidacionEvento
        public ValidacionEvento Post([FromBody]ParamValidarEvento validacion)
        {
            ValidacionEventoService servicio = new ValidacionEventoService(cadenaConexion);
            return servicio.getValidacionEvento(validacion.idUsuario, validacion.idEvento, validacion.idIntramuro);
        }

        // PUT: api/ValidacionEvento/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ValidacionEvento/5
        public void Delete(int id)
        {
        }
    }
}
