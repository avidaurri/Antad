using Antad.Services;
using AntadComun.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Antad.ViewModels
{
    public class PromotorViewModel:BaseViewModel
    {
        #region Attributes
        private ApiService apiService;
        private Promotor Promotor { get; set; }

        #endregion


        #region Properties

        #endregion

        #region Contructors
        public PromotorViewModel()
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
