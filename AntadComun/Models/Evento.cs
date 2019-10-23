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
        public string fechaInicio { get; set; }
        public string fechaFinal { get; set; }
        public int clvEdoEvento { get; set; }
        public string estadoEvento { get; set; }
        public int clvTipoEvento { get; set; }
        public string tipoEvento { get; set; }
        public int clvEmp { get; set; }
        public string fotoCentroTrabajo { get; set; }
        public string folioCentroTrabajo { get; set; }
        public string nombreCentroTrabajo { get; set; }
        public int clvCadenaCentroTrabajo { get; set; }
        public string cadenaCentroTrabajo { get; set; }
        public string fotoCadenaCentroTrabajo { get; set; }
        public string mensajeEvento { get; set; }
        public string descripcionMensajeEvento { get; set; }
        public bool seeQR { get; set; }
        public bool checkIn { get; set; }
        public bool seeUpdate { get; set; }
  
    }
}
