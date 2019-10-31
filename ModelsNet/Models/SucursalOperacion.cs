using System;
using System.Collections.Generic;
using System.Text;

namespace ModelsNet.Models
{
    public class SucursalOperacion
    {
        public int idUsuario { get; set; }
        public string empresa { get; set; }
        public int idSucursal { get; set; }
        public string nombreSucursal { get; set; }
        public string fotoSucursal { get; set; }
        public double distanciaSucursal { get; set; }
        public bool existeSucursal { get; set; }
        public bool mostarMensajeSucursal { get; set; }
        public bool existeOperacion { get; set; }
        public bool mostrarMensajeOperacion { get; set; }
        public string mensajeSucursal { get; set; }
        public string mensajeOperacion { get; set; }
        public double latitudSucursal { get; set; }
        public double longitudSucursal { get; set; }
        public double latitud { get; set; }
        public double longitud { get; set; }
    }
}
