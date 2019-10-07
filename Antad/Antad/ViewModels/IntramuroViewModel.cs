using Antad.Services;
using AntadComun.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Antad.ViewModels
{
    public class IntramuroViewModel:BaseViewModel
    {

        #region Attributes
        private ApiService apiService;
        private Intramuro Intramuro { get; set; }

        #endregion


        #region Properties

        #endregion

        #region Contructors
        public IntramuroViewModel()
        {
            //this.CargarSucursal();
        }

       /* private void CargarSucursal()
        {
            throw new NotImplementedException();
        }*/
        #endregion
    }
}
