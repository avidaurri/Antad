

namespace Antad.ViewModels
{
    using Antad.Helpers;
    using Antad.Services;
    using ModelsNet.Models;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using System;
    using System.Windows.Input;
    using Xamarin.Forms;
    public class AgregarUsuarioViewModel : BaseViewModel
    {
        #region Attributes
        private MediaFile file;
        private ImageSource imageSource;
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

        public ImageSource ImageSource
        {
            get { return this.imageSource; }
            set
            {
                imageSource = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Contructors
        public AgregarUsuarioViewModel()
        {
            this.apiService = new ApiService();
            this.isEnabled = true;
            this.ImageSource = "avatar";
        }
        #endregion

        #region Commands

        public ICommand ChangeImageCommand
        {
            get
            {
                return new GalaSoft.MvvmLight.Command.RelayCommand(ChangeImage);
            }
        }

        private async void ChangeImage()
        {
            await CrossMedia.Current.Initialize();

            var source = await Application.Current.MainPage.DisplayActionSheet(
                Languages.ImageSource,
                Languages.Cancel,
                null,
                Languages.FromGallery,
                Languages.NewPicture);

            if (source == Languages.Cancel)
            {
                this.file = null;
                return;
            }

            if (source == Languages.NewPicture)
            {
                this.file = await CrossMedia.Current.TakePhotoAsync(
                    new StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = "test.jpg",
                        PhotoSize = PhotoSize.Small,
                    }
                );
            }
            else
            {
                this.file = await CrossMedia.Current.PickPhotoAsync();
            }

            if (this.file != null)
            {
                this.ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = this.file.GetStream();
                    return stream;
                });
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                return new GalaSoft.MvvmLight.Command.RelayCommand(Save);
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
                this.IsEnabled = true;
                
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                return;
            }

            byte[] imageArray = null;
            if(this.file != null)
            {
                imageArray = FilesHelper.ReadFully(this.file.GetStream());
            }

            var usuario = new Usuario
            {
                nombre=this.nombre,
                usuario=this.usuario,
                password=this.password,
                curp=this.curp,
                rfc=this.rfc,
                ImageArray=imageArray,

            };

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlUsuarios"].ToString();
            var response = await this.apiService.Post(url, prefix, controller, usuario);

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
            }


            /*
            var newUser = (Usuario)response.Result;
            var usuariosViewModel = UsuariosViewModel.GetInstance();
            usuariosViewModel.MyUsuarios.Add(newUser);
            usuariosViewModel.RefreshList();*/





            this.IsRunning = false;
            this.IsEnabled = true;
            //back por codigo
            //await Application.Current.MainPage.Navigation.PopAsync();
            await App.Navigator.PopAsync();

        }
        #endregion

    }
}
