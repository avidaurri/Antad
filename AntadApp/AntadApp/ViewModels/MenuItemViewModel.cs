using AntadApp.Helpers;
using AntadApp.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AntadApp.ViewModels
{
    public class MenuItemViewModel
    {
        #region Properties
        public string Icon { get; set; }
        public string Title { get; set; }
        public string PageName { get; set; }
        #endregion

        #region Commands
        public ICommand GotoCommand
        {
            get
            {
                return new RelayCommand(GoTo);
            }

        }



        #endregion

        #region Methods
        private void GoTo()
        {

            if (this.PageName == "LoginPage")
            {
                Settings.Usuario = string.Empty;
                Settings.Password = string.Empty;
                Settings.IsRemembered = false;
                MainViewModel.GetInstance().Login = new LoginViewModel();
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
            if (this.PageName == "misucursal")
            {
                /*Settings.Usuario = string.Empty;
                Settings.Password = string.Empty;
                Settings.IsRemembered = false;*/
                MainViewModel.GetInstance().Intramuro = new IntramuroViewModel();
                Application.Current.MainPage = new Master(new IntramuroPage());
                //Application.Current.MainPage.Navigation.PushAsync(new IntramuroPage());
                //App.Navigator.PushAsync(new IntramuroPage());
                //
            }
            if (this.PageName == "Bienvenido")
            {
                /*Settings.Usuario = string.Empty;
                Settings.Password = string.Empty;
                Settings.IsRemembered = false;*/
                MainViewModel.GetInstance().Bienvenido = new BienvenidoViewModel();
                Application.Current.MainPage = new Master(new BienvenidoPage());
                //
            }
            if (this.PageName == "miseventos")
            {
                /*Settings.Usuario = string.Empty;
                Settings.Password = string.Empty;
                Settings.IsRemembered = false;*/
                MainViewModel.GetInstance().Promotor = new PromotorViewModel();
                Application.Current.MainPage = new Master(new PromotorPage());
                //
            }


        }
        #endregion
    }
}
