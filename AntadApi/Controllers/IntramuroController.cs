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
    public class IntramuroController : ApiController
    {
        public string cadenaConexion = ConfigurationManager.ConnectionStrings["ConexionAntadd"].ConnectionString;
        // GET: api/Intramuro
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Intramuro/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Intramuro
        public Intramuro Post([FromBody]Intramuro usuario)
        {
            IntramuroService servicio = new IntramuroService(cadenaConexion);
            return servicio.GetIntramuro(usuario.clvEmp, usuario.latitud, usuario.longitud);
        }

        // PUT: api/Intramuro/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Intramuro/5
        public void Delete(int id)
        {
        }
    }
}
