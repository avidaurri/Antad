using AntadComun.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AntadApp.ViewModels
{
    public class RegistroCincoViewModel : BaseViewModel
    {
        #region Attributes
        private Registro registro;
        // private List<CatalogoRegistro.Municipio> municipiosList { get; set; }
        #endregion

        #region Properties

        public Registro Registro
        {
            get
            {
                return registro;
            }
            set
            {
                registro = value;
            }
        }


        public string NombreReferenciaUno { get; set; }
        public string TelefonoReferenciaUno { get; set; }

        #endregion

        #region Constructors

        public RegistroCincoViewModel(Registro reg)
        {
            this.Registro = reg;

        }



        #endregion

        #region Commands



        #endregion
    }
}
