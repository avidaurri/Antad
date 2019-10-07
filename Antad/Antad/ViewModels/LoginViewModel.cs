

namespace Antad.ViewModels
{
    using Antad.Helpers;
    using Antad.Services;
    using Antad.Views;
    using AntadComun.Models;
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
            MainViewModel.GetInstance().Register = new RegistroViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new RegistroPage());
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


            var login = new LoginApp
            {
                login = this.Usuario,
                password = this.Password,


            };


            //consumir api para logear
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlLoginApp"].ToString();
            var response = await this.apiService.PostLogin(url, prefix, controller, login);

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
            if (response.Result == null)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                 "Aviso",
                 "Login y/o password incorrecto",
                    "Aceptar");
            }
            else
            {
                this.IsRunning = false;
                this.IsEnabled = true;

                login =(LoginApp)response.Result;

                if (login.idEdoRegUsuario != 5)
                {
                    await Application.Current.MainPage.DisplayAlert(
                    "Aviso",
                    login.mensajeLogin,
                     "Aceptar");
                }
                else
                {
                    if (this.IsRemembered)
                    {
                    Settings.Usuario = this.Usuario;
                    Settings.Password = this.Password;
                    Settings.IsRemembered = true;
                    }

                    //recuperar UserSession

                    //var prefixd = Application.Current.Resources["UrlPrefix"].ToString();
                    var controllerd = Application.Current.Resources["UrlUserSession"].ToString();
                    var responsed = await this.apiService.GetUser(url, prefix, controllerd, this.Usuario);
                    if (responsed.IsSuccess)
                    {
                        UserSession userASP = new UserSession();
                        userASP = (UserSession)responsed.Result;
                        MainViewModel.GetInstance().UserSession = userASP;
                        Settings.UserSession = JsonConvert.SerializeObject(userASP);

                        if (userASP.idPuesto.Equals(1))
                        {
                            //promotor

                        }else if (userASP.idPuesto.Equals(3))
                        {
                            //intramuro

                        }

                    }

                    // MainViewModel.GetInstance().Usuarios = new UsuariosViewModel();
                    MainViewModel.GetInstance().Intramuro = new IntramuroViewModel();
                    Application.Current.MainPage = new Master();


                }

                this.IsRunning = false;
                this.IsEnabled = true;




            }







        }
        #endregion
    }
}
