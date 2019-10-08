using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntadComun.Models
{
    public class Evento
    {
        public string folioEvento { get; set; }
        public int clv_Empleado { get; set; }
        public string usuario { get; set; }
        public string fotoSucursal { get; set; }
        public string folioSucursal { get; set; }
        public string nombreSucursal { get; set; }
        public string fechaInicio { get; set; }
        public string fechaFinal { get; set; }
        public string estatusEvento { get; set; }
        public int clvEstatusEvento { get; set; }

        public string mensajeEvento { get; set; }
        public string descripcionMensajeEvento { get; set; }

        public bool seeQR { get; set; }
    }
}
