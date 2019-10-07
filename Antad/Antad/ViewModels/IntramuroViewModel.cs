using Antad.Helpers;
using Antad.Services;
using AntadComun.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Antad.ViewModels
{
    public class IntramuroViewModel : BaseViewModel
    {

        #region Attributes
        private ApiService apiService;
        //public UserSession urr = new UserSession();
        private Intramuro sucursal { get; set; }

        private bool isEnabled;
        #endregion


        #region Properties

       /* public UserSession UserSession { get; set; }

        public string UserName
        {
            get
            {

                    return $"{this.UserSession.usuario}";
             
            }
        }*/

      
        public UserSession urr = JsonConvert.DeserializeObject<UserSession>(Settings.UserSession);
        //public string ddd = urr.usuario;
        /*public UserSession UserSession { get; set; }
        */
        public string UserName
        {
            get
            {
              
                    return urr.usuario;
              
              
            }
        }

        public Intramuro Sucursal
        {
            get { return this.sucursal; }
            set
            {
                sucursal = value;
                OnPropertyChanged();
            }
            // set => this.SetValue(ref this.usuarios, value);
        }

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set
            {
                isEnabled = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Contructors
        public IntramuroViewModel()
        {
            this.IsEnabled = true;
            this.apiService = new ApiService();
            this.CargarSucursal();
        }

        private async void CargarSucursal()
        {

            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
               await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                return;
            }

            var usser = new GetUserRequest
            {
                User = UserName,
                latitud = 1.4008161,
                longitud = -9.528970,


            };

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlIntramuro"].ToString();
            //var response = await this.apiService.GetList<Intramuro>(url, prefix, controller);
            //var response = await this.apiService.GetWithPost(url, prefix, controller, registro);
            var response = await this.apiService.GetWithPost(url, prefix, controller, usser);
            if (!response.IsSuccess)
            {
              
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            this.Sucursal = (Intramuro)response.Result;


            //throw new NotImplementedException();
            // ok
            /*this.Sucursal = new Intramuro
            {                
                idUsuario = 1,
                empresa = "walmart",
                idSucursal = 1,
                nombreSucursal = "walmart",
                fotoSucursal = "https://compilacionweb.online/DemoAntad/FotosCC/soriana.jpg",
                distanciaSucursal = "6",
                existeSucursal = true,
                mostarMensajeSucursal = false,
                existeOperacion = true,
                mostrarMensajeOperacion = false,
                mensajeSucursal = "walmart",
                mensajeOperacion = "walmart",
                latitud = "walmart",
                longitud = "walmart",

            };



            // no sucursal
            this.Sucursal = new Intramuro
            {
                idUsuario = 1,
                empresa = "walmart",
                idSucursal = 1,
                nombreSucursal = "walmart",
                fotoSucursal = "https://compilacionweb.online/DemoAntad/FotosCC/soriana.jpg",
                distanciaSucursal = "6",
                existeSucursal = false,
                mostarMensajeSucursal = true,
                existeOperacion = false,
                mostrarMensajeOperacion = true,
                mensajeSucursal = "la sucursal no esta tu alcance",
                mensajeOperacion = "Ubicate en tu sucursalde trabajo",
                latitud = "walmart",
                longitud = "walmart",
                falloValidacion=true,

            };*/
        }
        #endregion
    }
}
