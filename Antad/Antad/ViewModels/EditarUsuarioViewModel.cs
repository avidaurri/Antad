
using Antad.Helpers;
using Antad.Services;
using AntadComun.Models;
using GalaSoft.MvvmLight.Command;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Antad.ViewModels
{
    public class EditarUsuarioViewModel:BaseViewModel
    {
        //EditarUsuario
        #region Attributes
        private Usuario usuario;
        private MediaFile file;
        private ImageSource imageSource;
        private ApiService apiService;
        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region Properties
        public Usuario Usuario
        {
            get { return this.usuario; }
            set
            {
                usuario = value;
                OnPropertyChanged();
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
        public EditarUsuarioViewModel(Usuario usuario)
        {
            this.usuario = usuario;
            this.apiService = new ApiService();
            this.isEnabled = true;
            this.ImageSource = usuario.foto;
        }
        #endregion


        #region Commands

        public ICommand DeleteCommand
        {
            get
            {
                return new RelayCommand(Delete);
            }
        }

        private async void Delete()
        {
            var answer = await Application.Current.MainPage.DisplayAlert("Desea eliminar", "confirmar", "si", "no");

            if (!answer)
            {
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

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlUsuarios"].ToString();
            var response = await this.apiService.Delete(url, prefix, controller, this.Usuario.idUsuario);
            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            var usuarioViewModel = UsuariosViewModel.GetInstance();
            //buscar el producto y borrarlode lalsita
            var borrarUsuario = usuarioViewModel.MyUsuarios.Where(p => p.idUsuario == this.Usuario.idUsuario).FirstOrDefault();
            if (borrarUsuario != null)
            {
                usuarioViewModel.MyUsuarios.Remove(borrarUsuario);
            }
            usuarioViewModel.RefreshList();
            this.IsRunning = false;
            this.IsEnabled = true;
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        public ICommand ChangeImageCommand
        {
            get
            {
                return new RelayCommand(ChangeImage);
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
                return new RelayCommand(Save);
            }
        }

        private async void Save()
        {
            if (string.IsNullOrEmpty(this.Usuario.nombre))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Introduce el nombre", "ok");
                return;
            }
            if (string.IsNullOrEmpty(this.Usuario.usuario))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Introduce el usaurio", "ok");
                return;
            }
            if (string.IsNullOrEmpty(this.Usuario.password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Introduce el password", "ok");
                return;
            }
            if (string.IsNullOrEmpty(this.Usuario.curp))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Introduce la curp", "ok");
                return;
            }
            if (string.IsNullOrEmpty(this.Usuario.rfc))
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
            if (this.file != null)
            {
                imageArray = FilesHelper.ReadFully(this.file.GetStream());
                this.Usuario.ImageArray = imageArray;
            }

  

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlUsuarios"].ToString();
            var response = await this.apiService.Put(url, prefix, controller, this.usuario, this.Usuario.idUsuario);

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
            }


            
            var newUser = (Usuario)response.Result;
            var usuariosViewModel = UsuariosViewModel.GetInstance();

            //borramos el usuario y lo volvemos a adicionar
            var oldUsuario = usuariosViewModel.MyUsuarios.Where(p => p.idUsuario == this.Usuario.idUsuario).FirstOrDefault();
            if(oldUsuario != null)
            {
                usuariosViewModel.MyUsuarios.Remove(oldUsuario);
            }
 
            usuariosViewModel.MyUsuarios.Add(newUser);
            usuariosViewModel.RefreshList();





            this.IsRunning = false;
            this.IsEnabled = true;
            //back por codigo
            await Application.Current.MainPage.Navigation.PopAsync();

        }
        #endregion
    }
}
