using System;
using System.Collections.Generic;
using System.Text;

namespace ModelsNet.Models
{
    public class ValidacionEvento
    {
        public int idUsuario { get; set; } //1
        public string fotoUsuario { get; set; }//1
        public string nombreUsuario { get; set; }//1
        public int idSucursal { get; set; } //1

        public int idEvento { get; set; } //1

        public int idTipoSucursal { get; set; } //1
        public string nombreSucursal { get; set; } //1
        public string fotoSucursal { get; set; } //1

        public string sexoRequisito { get; set; } //1
        public string mayorEdadRequisito { get; set; } //1
        public string sexoPromotor { get; set; } //1
        public int edadPromotor { get; set; } //1

        public List<DocumentoPromotor> documentospromotor { get; set; } //2

        public List<DocumentoSucursal> documentosucursal { get; set; } //3

        public string fechaEvento { get; set; } //1
        public string horaInicio { get; set; } //1
        public string horaFin { get; set; } //1
        public bool cartaLaboral { get; set; } //1

        public bool validacionPersonal { get; set; }
        public string mensajeValidacionPersonal { get; set; }
        public bool validacionHorario { get; set; }
        public string mensajeValidacionHorario { get; set; }

        public bool validacionUbicacion { get; set; }
        public string mensajeValidacionUbicacion { get; set; }

        public bool validacionDocumentos { get; set; }
        public string mensajeValidacionDocumentos { get; set; }
        public bool validacionCartaLaboral { get; set; }
        public string mensajeValidacionCartaLaboral { get; set; }
        public bool validacionFinal { get; set; }
        public class DocumentoPromotor
        {
            public int idDocumento { get; set; }
            public string nombreDocumento { get; set; }
            public string urlDocumento { get; set; }
        }

        public class DocumentoSucursal
        {
            public int idDocumento { get; set; }
            public string nombreDocumento { get; set; }

        }
    }
}
