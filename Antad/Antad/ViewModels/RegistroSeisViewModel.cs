using ModelsNet.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Antad.ViewModels
{
    public class RegistroSeisViewModel:BaseViewModel
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

        public RegistroSeisViewModel(Registro reg)
        {
            this.Registro = reg;
         

        }



        #endregion

        #region Commands



        #endregion
    }
}
