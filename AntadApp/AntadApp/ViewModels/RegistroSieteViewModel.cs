using AntadComun.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AntadApp.ViewModels
{
    public class RegistroSieteViewModel : BaseViewModel
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




        #endregion

        #region Constructors

        public RegistroSieteViewModel(Registro reg)
        {
            this.Registro = reg;


        }



        #endregion

        #region Commands



        #endregion
    }
}
