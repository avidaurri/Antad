using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntadComun.Models
{
    public class GetUserRequest
    {
        public string User { get; set; }

        public double latitud { get; set; }
        public double longitud { get; set; }
    }
}
