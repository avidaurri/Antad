using System;
using System.Collections.Generic;
using System.Text;

namespace ModelsNet.Models
{
    public class MiEvento
    {
        public int idEvento { get; set; }
        public string fecha { get; set; }
        public string fotoSucursal { get; set; }
        public override string ToString()
        {
            return this.fecha;
        }
    }
}
