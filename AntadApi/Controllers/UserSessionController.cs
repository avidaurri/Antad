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
    public class UserSessionController : ApiController
    {
        public string cadenaConexion = ConfigurationManager.ConnectionStrings["ConexionAntadd"].ConnectionString;
        // GET: api/UserSession
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/UserSession/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/UserSession
        public UserSession Post([FromBody]GetUserRequest usuario)
        {
            LoginAppService servicio = new LoginAppService(cadenaConexion);
            return servicio.GetUser(usuario.User);
        }

        // PUT: api/UserSession/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/UserSession/5
        public void Delete(int id)
        {
        }
    }
}