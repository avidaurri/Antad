﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntadComun.Models
{
    public class ParamValidarEvento
    {
        public int idUsuario { get; set; }
        public int idIntramuro { get; set; }
        public int idEvento { get; set; }

        public string usuario { get; set; }

        public string folioEvento { get; set; }
    }
}
