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
    public class EventoDetalleController : ApiController
    {
        public string cadenaConexion = ConfigurationManager.ConnectionStrings["ConexionAntadd"].ConnectionString;
        // GET: api/EventoDetalle
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/EventoDetalle/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/EventoDetalle
        public Evento Post([FromBody]Evento value)
        {

            EventoService servicio = new EventoService(cadenaConexion);
            return servicio.getDetalleEvento(value.clvEmp, value.folioEvento);
        }

        // PUT: api/EventoDetalle/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/EventoDetalle/5
        public void Delete(int id)
        {
        }
    }
}
