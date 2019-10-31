using Antad.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Antad.ViewModels
{
    public class PreregistroViewModel : BaseViewModel
    {


        #region Commands

       /* public ICommand RechazarRegistroCommand
        {
            get
            {
                return new RelayCommand(RechazarRegistro);
            }

        }

        private async void RechazarRegistro()
        {
           // await App.Navigator.PopAsync();
            //await Navigation.PopAsync();
        }*/

        public ICommand AceptarRegistroCommand
        {
            get
            {
                return new GalaSoft.MvvmLight.Command.RelayCommand(AceptarRegistro);
            }

        }

        private  void AceptarRegistro()
        {
            /*MainViewModel.GetInstance().Register = new RegistroViewModel();
            Application.Current.MainPage = new NavigationPage(new RegistroPage());*/

            MainViewModel.GetInstance().RegistroUno = new RegistroUnoViewModel();
            Application.Current.MainPage = new NavigationPage(new RegistroUnoPage());

        }
        #endregion

    }
}
