using Antad.Helpers;
using Antad.Services;
using Antad.Views;
using GalaSoft.MvvmLight.Command;
using ModelsNet.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Antad.ViewModels
{
    public class PreregistroViewModel : BaseViewModel
    {
        #region attributes
        private bool isEnabled;
        private ApiService apiService;
        #endregion

        #region Properties
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
        public PreregistroViewModel()
        {
            this.IsEnabled = true;
            this.apiService = new ApiService();
        }
        #endregion


        #region Commands


        public ICommand AceptarRegistroCommand
        {
            get
            {
                return new RelayCommand(AceptarRegistroAsync);
            }

        }

        private async void AceptarRegistroAsync()
        {
            this.IsEnabled = false;
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                return;
            }


            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlGetCatalogo"].ToString();// + "?idEstado=0";
            var response = await this.apiService.GetCatalogos(url, prefix, controller);
            this.IsEnabled = true;
            if (!response.IsSuccess)
            {
                /*this.IsRunning = false;
                this.IsEnabled = true;*/
                //await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                await Application.Current.MainPage.DisplayAlert(Languages.Error, "Hubo un problema con su conexión a internet, por favor intentalo nuevamente", Languages.Accept);
                /*MainViewModel.GetInstance().Login = new LoginViewModel();
                Application.Current.MainPage = new NavigationPage(new LoginPage());*/
                return;




            }
            else
            {
                CatalogoRegistro cat = new CatalogoRegistro();
                cat = (CatalogoRegistro)response.Result;
                
                MainViewModel.GetInstance().RegistroUno = new RegistroUnoViewModel(cat);
                Application.Current.MainPage = new NavigationPage(new RegistroUnoPage());
            }


            //////////////////////////////////////////////////
            ///

            //MainViewModel.GetInstance().Register = new RegistroViewModel();
            //Application.Current.MainPage = new NavigationPage(new RegistroPage());

            /*MainViewModel.GetInstance().RegistroUno = new RegistroUnoViewModel();
            Application.Current.MainPage = new NavigationPage(new RegistroUnoPage());*/

        }
        #endregion

    }
}
