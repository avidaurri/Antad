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
    public class SucursalOperacionController : ApiController
    {
        public string cadenaConexion = ConfigurationManager.ConnectionStrings["ConexionAntad"].ConnectionString;
        // GET: api/SucursalOperacion
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/SucursalOperacion/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SucursalOperacion
        public SucursalOperacion Post([FromBody]SucursalOperacion usuario)
        {
            SucursalOperacionService servicio = new SucursalOperacionService(cadenaConexion);
            return servicio.getSucursalOperacion(usuario.idUsuario, usuario.latitud, usuario.longitud);
        }

        // PUT: api/SucursalOperacion/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/SucursalOperacion/5
        public void Delete(int id)
        {
        }
    }
}
