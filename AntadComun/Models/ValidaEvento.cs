using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntadComun.Models
{
    public class ValidaEvento
    {

        public int clv_Empleado { get; set; } //1
        public string usuario { get; set; } //1
        public string fotoUsuario { get; set; }//1  api
        public string nombreUsuario { get; set; }//1
        public string folioSucursal { get; set; } //1
        public string fotoSucursal { get; set; } //1

        public string folioEvento { get; set; } //1
        public string nombreSucursal { get; set; } //1

        public string fechaInicio { get; set; } //1
        public string fechaFinal { get; set; } //1

        public string estatusEvento { get; set; }//1  api
        public int clvEstatusEvento { get; set; } //1

        public string sexoSucursal { get; set; } //2
        public string sexoUsuario { get; set; } //1
        public bool validaSexo { get; set; } //2

        public string edadSucursal { get; set; } //2
        public string edadUsuario { get; set; } //1
        public bool validaEdad { get; set; } //2

        public bool imssSucursal { get; set; } //2
        public bool imssUsuario { get; set; } //1
        public bool validaImss { get; set; } //2


        public List<Rango> requisitosRangso { get; set; } //2

        public List<Fijo> requisitosFijos { get; set; } //2

        public List<Curso> requisitoCursos { get; set; } //2

        public class Rango
        {
            public int menor { get; set; }
            public int mayor { get; set; }
            public int valor { get; set; }
            public bool validado { get; set; }
        }

        public class Fijo
        {
            public string valorSuc { get; set; }
            public string valorUsuario { get; set; }
            public bool validado { get; set; }
        }

        public class Curso
        {
            public string nombre { get; set; }
            public int menor { get; set; }
            public int mayor { get; set; }

            public int valor { get; set; }
            public bool validado { get; set; }
        }
        
        public bool validacionFinal { get; set; }


    }
}
