﻿

namespace Antad.ViewModels
{
    using Antad.Helpers;
    using Antad.Services;
    using Antad.Views;
    using ModelsNet.Models;
    using GalaSoft.MvvmLight.Command;
    using Newtonsoft.Json;
    using System;
    using System.Windows.Input;
    using Xamarin.Forms;
    public class LoginViewModel:BaseViewModel
    {

        #region Atributes
        private bool isRunning;
        private bool isEnabled;
        private ApiService apiService;
        #endregion

        #region Properties
        public string Usuario { get; set; }
        public string ClvEmp { get; set; }
        public string Password { get; set; }

        public bool IsRemembered { get; set; }

        public bool IsRunning
        {
            get { return this.isRunning; }
            set
            {
                isRunning = value;
                OnPropertyChanged();
            }
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
        #endregion}

        #region Contructors
        public LoginViewModel()
        {
            this.apiService = new ApiService();
            this.IsEnabled = true;
            this.IsRemembered = true;
        }
        #endregion

        #region Commands

        
        public ICommand RegisterCommand
        {
            get
            {
                return new RelayCommand(Register);
            }

        }

        private async void Register()
        {
            /*MainViewModel.GetInstance().Register = new RegistroViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new RegistroPage());*/
            MainViewModel.GetInstance().Preregistro = new PreregistroViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new PreregistroPage());
        }

        public ICommand LoginCommand {
            get
            {
                return new RelayCommand(Login);
            }
   
        }

        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Usuario))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Digita el usuario",
                    "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Digita el password",
                    "Aceptar");
                return;
            }
            this.IsRunning = true;
            this.IsEnabled = false;
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                return;
            }


            var login = new UserSession
            {
                usuario = this.Usuario,
                password = this.Password,


            };


            //consumir api para logear
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlLoginApp"].ToString();
            var response = await this.apiService.Post<UserSession>(url, prefix, controller, login);

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    Languages.Accept);
                return;
            }
            this.IsRunning = false;
            this.IsEnabled = true;

            login = (UserSession)response.Result;



            if (!login.seLogeo)
            {
                await Application.Current.MainPage.DisplayAlert(
                "Mensaje",
               login.mensajeLogin,
                "Aceptar");
                return;
            }
            else
            {

                if (this.IsRemembered)
                {
                Settings.Clvemp = login.clvEmp.ToString();
                Settings.Usuario = this.Usuario;
                Settings.Password = this.Password;
                Settings.IsRemembered = true;
                }

                //recuperar UserSession
                MainViewModel.GetInstance().UserSession = login;
                Settings.UserSession = JsonConvert.SerializeObject(login);
                if (login.clvPuesto.Equals(1))
                {
                    //promotor

                }else if (login.clvPuesto.Equals(3))
                {
                    //intramuro

                }

     

                // MainViewModel.GetInstance().Usuarios = new UsuariosViewModel();
                // MainViewModel.GetInstance().Intramuro = new IntramuroViewModel();
                // MainViewModel.GetInstance().Promotor = new PromotorViewModel();
                MainViewModel.GetInstance().Bienvenido = new BienvenidoViewModel();
                Application.Current.MainPage = new Master(new Bienvenido());
                //

                /*this.IsRunning = false;
                this.IsEnabled = true;*/




            }







        }
        #endregion
    }
}
