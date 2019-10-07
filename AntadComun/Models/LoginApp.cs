using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntadComun.Models
{
    public class LoginApp
    {
        public int idLogin { get; set; }

        public int idEdoRegUsuario { get; set; }

        public int idPuesto { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string estadoLogin { get; set; }
        public string mensajeLogin { get; set; }

    }
}
