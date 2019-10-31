using Antad.Helpers;
using Antad.Services;
using AntadComun.Models;
using Newtonsoft.Json;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using ZXing;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;
using Antad.Views;

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
        private int estadodelevento { get; set; }
        #endregion


        #region Properties
        public int Estadodelevento
        {
            get { return this.estadodelevento; }
            set
            {
                estadodelevento = value;
                OnPropertyChanged();
            }
        }
        public Evento enviar = new Evento();
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
        public string ClvEmp
        {
            get
            {

                return urr.clvEmp.ToString();


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
             /*this.Sucursal.existeSucursal = false;
           this.Sucursal.existeOperacion = false;
            this.Sucursal.errorOperacion = true;
            this.Sucursal.errorSucursal = true;*/
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
                return new GalaSoft.MvvmLight.Command.RelayCommand(Scan);
            }
        }
        public ICommand RefreshCommand
        {
            get
            {
                return new GalaSoft.MvvmLight.Command.RelayCommand(CargarSucursal);
            }
        }
        private async void Scan()
        {
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

                    /*int estatus = Convert.ToInt32(evento.Split('&').Last());
                    string primera = evento.Split('&').First();*/

                    string eventom = evento.Split('/').Last();
                    int clvemp = Convert.ToInt32(evento.Split('/').First());

                    //inicializado
                    /*int estatus = 3;
                    string eventom = "e01";
                    string usuariom = "alexa";*/

                    // no inicializado
                    /*int estatus = 4;
                    string eventom = "e02";
                    string usuariom = "alexa";*/


                    var connection = await this.apiService.CheckConnection();

                    if (!connection.IsSuccess)
                    {

                        await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                        return;
                    }

                    var usser = new Evento
                    {
                        clvEmp = clvemp,
                        folioEvento = eventom,

                    };

                    var url = Application.Current.Resources["UrlAPI"].ToString();
                    var prefix = Application.Current.Resources["UrlPrefix"].ToString();
                    var controller = Application.Current.Resources["UrlEventoMensajeDetalle"].ToString();
                    var response = await this.apiService.GetDetalleEvento(url, prefix, controller, usser);
                    if (!response.IsSuccess)
                    {
                        await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                        return;
                    }

                    usser = (Evento)response.Result;
                    this.Estadodelevento = usser.clvEdoEvento;



                    if (this.Estadodelevento.Equals(3))
                    {
                        // evento inicializado
                       /* MainViewModel.GetInstance().ValidacionActividad = new ValidacionActividadViewModel(eventom, clvemp.ToString());
                        //await Application.Current.MainPage.Navigation.PushAsync(new EditarUsuarioPage());
                        await App.Navigator.PushAsync(new ValidacionActividadPage());*/


                    }
                    else if (this.Estadodelevento.Equals(4))
                    {
                        //evento no inicializado
                        MainViewModel.GetInstance().ValidarAutorizar = new ValidacionAutorizarViewModel(eventom, clvemp.ToString());
                        //await Application.Current.MainPage.Navigation.PushAsync(new EditarUsuarioPage());
                        await App.Navigator.PushAsync(new ValidacionAutorizarPage());
                    }

                });
            };

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

             var usser = new Intramuro
            {
                clvEmp = Convert.ToInt32(ClvEmp),
                latitud = -99.528970,
                longitud = -99.528970,


            };

            this.IsRunning = true;
            this.IsEnabled = false;

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlIntramuro"].ToString();
            var response = await this.apiService.Post<Intramuro>(url, prefix, controller, usser);
            //var response = await this.apiService.GetWithPost(url, prefix, controller, usser);
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
