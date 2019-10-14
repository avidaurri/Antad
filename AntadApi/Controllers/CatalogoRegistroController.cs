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
    public class CatalogoRegistroController : ApiController
    {
        public string cadenaConexion = ConfigurationManager.ConnectionStrings["ConexionAntadd"].ConnectionString;
        // GET: api/CatalogoRegistro
        /*public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }*/

        // GET: api/CatalogoRegistro/5
        public CatalogoRegistro Get(int idEstado)
        {
            CatalogoRegistroService servicio = new CatalogoRegistroService(cadenaConexion);
            return servicio.getCatalogo(idEstado);
        }

        // POST: api/CatalogoRegistro
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/CatalogoRegistro/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/CatalogoRegistro/5
        public void Delete(int id)
        {
        }
    }
}
