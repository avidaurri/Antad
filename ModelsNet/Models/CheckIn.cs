using System;
using System.Collections.Generic;
using System.Text;

namespace ModelsNet.Models
{
    public class CheckIn
    {
        public string mensajeCheckIn { get; set; }

        public int idEvento { get; set; }

        public int idUsuario { get; set; }
        public int estatus { get; set; }
    }
}
