﻿using AntadBiblioteca.Services;
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
    public class MisEventosController : ApiController
    {
        public string cadenaConexion = ConfigurationManager.ConnectionStrings["ConexionAntad"].ConnectionString;
        // GET: api/MisEventos
        /*public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }*/

        // GET: api/MisEventos/5
        public List<MiEvento> Get()
        {
            MiEventoService servicio = new MiEventoService(cadenaConexion);
            return servicio.getSucursalOperacion();
        }

        // POST: api/MisEventos
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/MisEventos/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/MisEventos/5
        public void Delete(int id)
        {
        }
    }
}