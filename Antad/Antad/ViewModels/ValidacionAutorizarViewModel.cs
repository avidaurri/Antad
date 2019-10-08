using System;
using System.Collections.Generic;
using System.Text;

namespace Antad.ViewModels
{
    public class ValidacionAutorizarViewModel : BaseViewModel
    {


        #region Attributes
        private string evento;

        private string usuario;
        #endregion


        #region Properties


        #endregion


        #region Contructors
        public ValidacionAutorizarViewModel(string eventom, string usuariom)
        {
            this.evento = eventom;
            this.usuario = usuariom;
        }


        #endregion

    }
}
