using System;
using System.Collections.Generic;
using System.Text;

namespace ModelsNet.Models
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
