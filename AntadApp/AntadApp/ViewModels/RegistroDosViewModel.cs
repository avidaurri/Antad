using AntadApp.Services;
using AntadComun.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AntadApp.ViewModels
{
    public class RegistroDosViewModel : BaseViewModel
    {
        #region Attributes
        private ApiService apiService;
        private Registro registro;

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

        private string _estadocivilText { get; set; }
        public string EstadoCivilText
        {
            get
            {
                return _estadocivilText;
            }
            set
            {
                _estadocivilText = value;
            }
        }
        private string _gradoestudiosText { get; set; }
        public string GradoEstudiosText
        {
            get
            {
                return _gradoestudiosText;
            }
            set
            {
                _gradoestudiosText = value;
            }
        }
        public string Peso { get; set; }
        public string Altura { get; set; }

        #endregion

        #region Constructors

        public RegistroDosViewModel(Registro reg)
        {
            this.Registro = reg;
            //this.IsEnabled = true;
            this.apiService = new ApiService();

        }



        #endregion

        #region Commands



        #endregion

    }
}
