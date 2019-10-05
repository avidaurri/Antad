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
        #region Properties
        public EditarUsuarioViewModel EditarUsuario { get; set; }

        public UsuariosViewModel Usuarios { get; set; }
        public AgregarUsuarioViewModel AgregarUsuario { get; set; }
        #endregion


        #region Contructors
        public MainViewModel()
        {
            instance = this;
            this.Usuarios = new UsuariosViewModel();

        }
        #endregion

        #region Singleton
        public static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }
            return instance;
        }
        #endregion

        #region Commands
        public ICommand AddProductoCommand
        {
            get
            {

                return new RelayCommand(IrAgregarUsuario);

            }
        } 
       

        private async void IrAgregarUsuario()
        {
            this.AgregarUsuario = new AgregarUsuarioViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new AgregarUsuarioPage());

        } 
        #endregion
    }
}
