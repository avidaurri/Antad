using Antad.Helpers;
using Antad.Services;
using ModelsNet.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Antad.ViewModels
{
    public class RegistroTresViewModel:BaseViewModel
    {
        #region Attributes
        private ApiService apiService;
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

        private string _estadoText { get; set; }
        public string EstadoText
        {
            get
            {
                return _estadoText;
            }
            set
            {
                _estadoText = value;
            }
        }
        private string _municipioText { get; set; }
        public string MunicipioText
        {
            get
            {
                return _municipioText;
            }
            set
            {
                _municipioText = value;
            }
        }
        /*public List<CatalogoRegistro.Municipio> MunicipiosList
        {

            get { return this.municipiosList; }
            set
            {
                municipiosList = value;
                OnPropertyChanged();
            }
        }*/
       /* private async void cargaMun(string val)
        {
            //this.MunicipiosList.Clear();
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                return;
            }


            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlGetCatalogo"].ToString();// + "?idEstado=0";
            var response = await this.apiService.Post(url, prefix, controller, Convert.ToInt32(val));
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }
            CatalogoRegistro cat = new CatalogoRegistro();
            cat = (CatalogoRegistro)response.Result;

            //municipios
            this.MunicipiosList = null;
            this.MunicipiosList = cat.listaMunicipios;

        }
        private CatalogoRegistro.Municipio _selectedMunicipio;
        public CatalogoRegistro.Municipio SelectedMunicipio
        {
            get
            {
                return _selectedMunicipio;
            }
            set
            {
                _selectedMunicipio = value;
                // ????????
                if (_selectedMunicipio != null)
                {
                    MunicipioText = _selectedMunicipio.value.ToString();
                }
                //put here your code  

            }
        }
        */
        public string CodigoPostal { get; set; }
        public string Colonia { get; set; }
        public string Calle { get; set; }
        public string NumeroExterior { get; set; }
        public string NumeroInterior { get; set; }

        #endregion

        #region Constructors

        public RegistroTresViewModel(Registro reg)
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
