using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntadComun.Models
{
    public class MiEvento
    {
        public int idEvento { get; set; }
        public string fecha { get; set; }
        public override string ToString()
        {
            return this.fecha;
        }
    }
}
