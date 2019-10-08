using Antad.Helpers;
using Antad.Services;
using AntadComun.Models;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace Antad.ViewModels
{
    public class IntramuroViewModel : BaseViewModel
    {

        #region Attributes
        private ApiService apiService;
        private Intramuro sucursal { get; set; }
        private bool isRunning;
        private bool isEnabled;
        private string _result;
        #endregion


        #region Properties
        public string Result
        {
            get => _result;
            set
            {
                _result = value;
                OnPropertyChanged(nameof(Result));
            }
        }
        public bool IsRunning
        {
            get { return this.isRunning; }
            set
            {
                isRunning = value;
                OnPropertyChanged();
            }
        }


        public UserSession urr = JsonConvert.DeserializeObject<UserSession>(Settings.UserSession);

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
            //SaveCommand = new Command(Scan);
        }
        #endregion


        #region Commands
        //public ICommand SaveCommand { get; private set; }
        public ICommand ScanCommand
        {
            get
            {
                return new RelayCommand(Scan);
            }
        }
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(CargarSucursal);
            }
        }
        private async  void Scan()
        {
            /*  await Application.Current.MainPage.DisplayAlert(
         "Error",
         "Escribe una curp valida",
         Languages.Accept);*/

            var scannerPage = new ZXingScannerPage();
            scannerPage.Title = "Lector QR";
            await App.Navigator.PushAsync(scannerPage);
            scannerPage.OnScanResult += (result) =>
            {
                scannerPage.IsScanning = false;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await App.Navigator.PopAsync();
                    string evento = result.Text;

                });
            };
            // Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(page), true);
        }
        #endregion
        #region Methods

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
                latitud = -99.528970,
                longitud = -99.528970,


            };

            this.IsRunning = true;
            this.IsEnabled = false;

          var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlIntramuro"].ToString();
            var response = await this.apiService.GetWithPost(url, prefix, controller, usser);
            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }
            this.IsRunning = false;
            this.IsEnabled = true;
            this.Sucursal = (Intramuro)response.Result;


            /////////////////////////////////////////////
            /*var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;

            if (locator.IsGeolocationAvailable)// devuuelve si el servicio existe en el dispositivo
            {
                if (locator.IsGeolocationEnabled) // devuelve si el gps esta activado
                {
                    if (!locator.IsListening) // comprueba si el campo esta escuchando el servicio
                    {
                        await locator.StartListeningAsync(new TimeSpan(0, 0, 5), 20);
                    }

                    locator.PositionChanged += async (cambio, args) =>
                    {
                        var loc = args.Position;

                        var usser = new GetUserRequest
                        {
                            User = UserName,
                            latitud = loc.Latitude,
                            longitud = loc.Longitude,


                        };

                        await Task.Delay(1000);

                        this.IsRunning = true;
                        this.IsEnabled = false;

                        var url = Application.Current.Resources["UrlAPI"].ToString();
                        var prefix = Application.Current.Resources["UrlPrefix"].ToString();
                        var controller = Application.Current.Resources["UrlIntramuro"].ToString();
                        var response = await this.apiService.GetWithPost(url, prefix, controller, usser);
                        if (!response.IsSuccess)
                        {
                            this.IsRunning = false;
                            this.IsEnabled = true;
                            await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                            return;
                        }
                        this.IsRunning = false;
                        this.IsEnabled = true;
                        this.Sucursal = (Intramuro)response.Result;



                    };
                }


            }*/




            ////////////////////////////////////////////

        }
        #endregion
    }
}
