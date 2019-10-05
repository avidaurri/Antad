using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntadComun.Models
{
    public class Usuario
    {

        public int numeroEmpleado { get; set; }

        public int edad { get; set; }

        public string genero { get; set; }
        public string nombre { get; set; }
        public string usuario { get; set; }
        public string clave { get; set; }
        public string password { get; set; }
        public string curp { get; set; }

        public string rfc { get; set; }

        public string foto { get; set; }

        public int idRol { get; set; }
        public int idUsuario { get; set; }
        public string rol { get; set; }

        public bool EsIntramuro { get; set; }
        public bool estatus { get; set; }
        public override string ToString()
        {
            return this.rol;
        }

        public byte[] ImageArray { get; set; }

        public string ImagePath { get; set; }
    }
}
