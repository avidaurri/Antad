using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntadComun.Models
{
    public class EventoOperacion
    {
        public int idEvento { get; set; }

        public int idUsuario { get; set; }

        public string fotoSucursal { get; set; }

        public string nombreSucursal { get; set; }

        public string fechaEvento { get; set; }

        public string horaInicioEvento { get; set; }

        public string horaFinEvento { get; set; }

        public string estatusEvento { get; set; }

        public int idEstatusEvento { get; set; }

        //public string mensajeOperacion { get; set; }

        public string codigoQR { get; set; }

        //public bool existenEventos { get; set; }
    }
}
