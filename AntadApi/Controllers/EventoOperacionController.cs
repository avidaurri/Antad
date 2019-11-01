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
    public class EventoOperacionController : ApiController
    {
        public string cadenaConexion = ConfigurationManager.ConnectionStrings["ConexionAntad"].ConnectionString;
        // GET: api/EventoOperacion
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/EventoOperacion/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/EventoOperacion
        public List<EventoOperacion> Post([FromBody]EventoOperacion usuario)
        {
            EventoOperacionService servicio = new EventoOperacionService(cadenaConexion);
            return servicio.getEventosDisponibles(usuario.idUsuario);
        }

        // PUT: api/EventoOperacion/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/EventoOperacion/5
        public void Delete(int id)
        {
        }
    }
}
