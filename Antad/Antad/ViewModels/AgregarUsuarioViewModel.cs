

namespace Antad.ViewModels
{
    using Antad.Helpers;
    using Antad.Services;
    using AntadComun.Models;
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Xamarin.Forms;
    public class AgregarUsuarioViewModel : BaseViewModel
    {
        #region Attributes
        private ApiService apiService;
        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region Properties
        public string nombre { get; set; }
        public string usuario { get; set; }
        public string password { get; set; }
        public string curp { get; set; }
        public string rfc { get; set; }

        public bool IsRunning {
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

        #endregion

        #region Contructors
        public AgregarUsuarioViewModel()
        {
            this.apiService = new ApiService();
            this.isEnabled = true;
        }
        #endregion

        #region Commands
        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }

        private async void Save()
        {
            if (string.IsNullOrEmpty(this.nombre))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Introduce el nombre", "ok");
                return;
            }
            if (string.IsNullOrEmpty(this.usuario))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Introduce el usaurio", "ok");
                return;
            }
            if (string.IsNullOrEmpty(this.password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Introduce el password", "ok");
                return;
            }
            if (string.IsNullOrEmpty(this.curp))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Introduce la curp", "ok");
                return;
            }
            if (string.IsNullOrEmpty(this.rfc))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Introduce el rfc", "ok");
                return;
            }

            this.IsRunning = true;
            this.isEnabled = false;

            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.isEnabled = true;
                
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                return;
            }

            var usuario = new Usuario
            {
                nombre=this.nombre,
                usuario=this.usuario,
                password=this.password,
                curp=this.curp,
                rfc=this.rfc,

            };

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlUsuarios"].ToString();
            var response = await this.apiService.Post(url, prefix, controller, usuario);

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.isEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
            }

           /* var newUser = (Usuario)response.Result;
            var viewModel = UsuariosViewModel.GetInstance();
            viewModel.Usuarios.Add(newUser);*/



            this.IsRunning = false;
            this.isEnabled = true;
            //back por codigo
            await Application.Current.MainPage.Navigation.PopAsync();

        }
        #endregion

    }
}
