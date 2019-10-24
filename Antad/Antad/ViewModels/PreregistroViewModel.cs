using Antad.Views;
using GalaSoft.MvvmLight.Command;
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
                return new RelayCommand(AceptarRegistro);
            }

        }

        private  void AceptarRegistro()
        {
            MainViewModel.GetInstance().Register = new RegistroViewModel();
           // await Application.Current.MainPage.Navigation.PushAsync(new RegistroPage());
             Application.Current.MainPage = new NavigationPage(new RegistroPage());
        }
        #endregion

    }
}
