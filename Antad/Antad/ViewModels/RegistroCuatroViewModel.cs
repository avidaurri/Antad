using Antad.Helpers;
using Antad.Services;
using AntadComun.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Antad.ViewModels
{
    public class RegistroCuatroViewModel:BaseViewModel
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

        private string _bancoText { get; set; }
        public string BancoText
        {
            get
            {
                return _bancoText;
            }
            set
            {
                _bancoText = value;
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

        public string Clabe { get; set; }
        public string NumeroCuenta { get; set; }
        public string NumeroTarjeta { get; set; }

        #endregion

        #region Constructors

        public RegistroCuatroViewModel(Registro reg)
        {
            this.Registro = reg;

        }



        #endregion

        #region Commands



        #endregion
    }
}
