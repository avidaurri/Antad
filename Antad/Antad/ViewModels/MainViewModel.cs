using Antad.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Antad.ViewModels
{
    public class MainViewModel
    {
      
        public UsuariosViewModel Usuarios { get; set; }
        public AgregarUsuarioViewModel AgregarUsuario { get; set; }

        public MainViewModel()
        {
              this.Usuarios = new UsuariosViewModel();
        
        }
        public ICommand AddProductoCommand {
            get
            {

                return new  RelayCommand(IrAgregarUsuario);

            }
        }

        private async void IrAgregarUsuario()
        {
            this.AgregarUsuario = new AgregarUsuarioViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new AgregarUsuarioPage());

        }
    }
}
