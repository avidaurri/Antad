using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntadComun.Models
{
    public class Registro
    {

        public string login { get; set; }
        public string password { get; set; }
        public string nombre { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string curp { get; set; }
        public string foto { get; set; }
        public string identificacion { get; set; }
        public string comprobanteDomiciliario { get; set; }
        public string email { get; set; }
        public string empresainteres { get; set; }
        public int clvPuesto { get; set; }

        public byte[] ImageArray { get; set; }

        public byte[] IdentificacionArray { get; set; }

        public byte[] ComprobanteArray { get; set; }
    }
}
