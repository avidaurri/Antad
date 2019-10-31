using System;
using System.Collections.Generic;
using System.Text;

namespace ModelsNet.Models
{
    public class ValidaEvento
    {

        public int clv_Empleado { get; set; } //1
        public string usuario { get; set; } //1
        public string fotoUsuario { get; set; }//1  api
        public string nombreUsuario { get; set; }//1
        public string folioSucursal { get; set; } //1
        public string fotoSucursal { get; set; } //1
        public string nombreSucursal { get; set; } //1
        public string folioEvento { get; set; } //1
        public string fechaInicio { get; set; } //1
        public string fechaFinal { get; set; } //1
        public string estatusEvento { get; set; }//1  api
        public int clvEstatusEvento { get; set; } //1
        public string tipoEvento { get; set; }//1  api
        public int clvTipoEvento { get; set; } //1

        public string sexoSucursal { get; set; } //2
        public string sexoUsuario { get; set; } //1
        public string mensajevalidacionsexo { get; set; } //1

        public string mensajevalidacionimss { get; set; } //1
        public bool generoValidado { get; set; } //3
        public bool errorGenero { get; set; } //3
        public bool imssSucursal { get; set; } //2
        public string imssUsuario { get; set; } //1
        public bool imssValidado { get; set; } //2
        public bool errorIms { get; set; } //2
        public int edadSucursal { get; set; } //2
        public int edadUsuario { get; set; } //1
        public bool validaEdad { get; set; } //2
        public string altura { get; set; } //1

        public List<Rango> requisitosRangos { get; set; } //2

        public List<Curso> requisitoCursos { get; set; } //2

        public class Rango
        {
            public string nombreRequisito { get; set; }
            public double menor { get; set; }
            public double mayor { get; set; }
            public double valor { get; set; }
            public bool validado { get; set; }
            public bool errorvaidado { get; set; }

            public bool mensaje { get; set; }
        }

        public class Curso
        {
            public string nombreCurso { get; set; }
            /*public int menor { get; set; }
            public int mayor { get; set; }*/
            public int valorrequerido { get; set; }
            public double valor { get; set; }
            public double menor { get; set; }
            public double mayor { get; set; }
            public bool validado { get; set; }
            public bool errorvaidado { get; set; }
            public string mens { get; set; }
        }

        public bool validacionFinal { get; set; }


    }
}
