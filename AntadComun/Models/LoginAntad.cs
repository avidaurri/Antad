using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntadComun.Models
{
    public class LoginAntad
    {
        public int idUsuario { get; set; }
        public string usuario { get; set; }
        public string password { get; set; }
        public int idRolUsuario { get; set; }
        public string nombreUsuario { get; set; }
        public int edad { get; set; }
        public string genero { get; set; }
        public string numeroEmpleado { get; set; }

        public string fotoUsuario { get; set; }
        public string rolUsuario { get; set; }
        public string mensajeLogin { get; set; }
    }
}
