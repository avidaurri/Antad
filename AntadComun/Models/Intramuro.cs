using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntadComun.Models
{
    public class Intramuro
    {
        public int idUsuario { get; set; }
        public string empresa { get; set; }
        public int idSucursal { get; set; }
        public string nombreSucursal { get; set; }
        public string fotoSucursal { get; set; }
        public string distanciaSucursal { get; set; }
        public bool existeSucursal { get; set; }
        public bool mostarMensajeSucursal { get; set; }
        public bool existeOperacion { get; set; }
        public bool mostrarMensajeOperacion { get; set; }
        public string mensajeSucursal { get; set; }
        public string mensajeOperacion { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }
        public bool falloValidacion { get; set; }
    }
}
