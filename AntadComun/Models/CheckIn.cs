﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntadComun.Models
{
    public class CheckIn
    {
        public string mensajeCheckIn { get; set; }

        public int idEvento { get; set; }

        public int idUsuario { get; set; }
        public int estatus { get; set; }
    }
}
