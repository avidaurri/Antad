

namespace Antad.ViewModels
{
    using Antad.Helpers;
    using Antad.Services;
    using AntadComun.Models;
    using GalaSoft.MvvmLight.Command;
    using System.Linq;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class UsuarioItemViewModel : Usuario
    {
        #region Attributes
        private ApiService apiService;
        #endregion

        #region Constructors
        public UsuarioItemViewModel()
        {
            this.apiService = new ApiService();
        }
        #endregion

        #region Command
        public ICommand EliminarUsuarioCommand {
            get
            {
                return new RelayCommand(EliminarUsuario);
            }
        }

        private async void EliminarUsuario()
        {
            var answer = await Application.Current.MainPage.DisplayAlert("Desea eliminar","confirmar", "si","no");

            if (!answer)
            {
                return;
            }

            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                return;
            }

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlUsuarios"].ToString();
            var response = await this.apiService.Delete(url, prefix, controller,this.idUsuario);
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            var usuarioViewModel = UsuariosViewModel.GetInstance();
            //buscar el producto y borrarlode lalsita
            var borrarUsuario = usuarioViewModel.Usuarios.Where(p => p.idUsuario == this.idUsuario).FirstOrDefault();
            if (borrarUsuario != null)
            {
                usuarioViewModel.Usuarios.Remove(borrarUsuario);
            }


        }
        #endregion
    }
}
