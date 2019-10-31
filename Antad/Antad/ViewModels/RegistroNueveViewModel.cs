using ModelsNet.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Antad.ViewModels
{
    public class RegistroNueveViewModel:BaseViewModel
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


        public string Login { get; set; }
        public string Password { get; set; }
        public string ConfirmarPassword { get; set; }

        #endregion

        #region Constructors

        public RegistroNueveViewModel(Registro reg)
        {
            this.Registro = reg;

        }



        #endregion

        #region Commands



        #endregion
    }
}
