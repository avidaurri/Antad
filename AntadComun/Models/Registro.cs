using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntadComun.Models
{
    public class Registro
    {
        public string foto { get; set; }
        public byte[] ImageArray { get; set; }
        public string login { get; set; }
        public string password { get; set; }
                          

        public string nombre { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string curp { get; set; }
        public string email { get; set; }
        public string telefono { get; set; }
        public string descripcionTelefono { get; set; }
        public int clvEdoCivil { get; set; }
        //public string estadoCivil { get; set; }

        //public string gradoEstudios { get; set; }
        public int clvGradoEstudios { get; set; }
        public double estatura { get; set; }
        public double peso { get; set; }
        public string identificacion { get; set; }
        public string comprobanteDomiciliario { get; set; }

        public string estado { get; set; }
        public string delegacionMunicipio { get; set; }
        public int cp { get; set; }
        public string colonia { get; set; }
        public string calle { get; set; }
        public string noExterior { get; set; }
        public string noInterior { get; set; }


        public string noDeCuenta { get; set; }
        public string banco { get; set; }
        public string clabe { get; set; }
        public string tarjeta { get; set; }


        public string empresainteres { get; set; }
        public int clvPuesto { get; set; }



        public byte[] IdentificacionArray { get; set; }

        public byte[] ComprobanteArray { get; set; }
    }
}
