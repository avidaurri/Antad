using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntadComun.Models
{
    public class UserSession
    {
        public int idEmpleado { get; set; }
        public int edad { get; set; }
        public string genero { get; set; }
        public string nombre { get; set; }
        public string usuario { get; set; }
        public string password { get; set; }
        public string curp { get; set; }
        public string foto { get; set; }
        public int idPuesto { get; set; }
        public int idLogin { get; set; }
        public string puesto { get; set; }
        public bool activo { get; set; }
    }
}
