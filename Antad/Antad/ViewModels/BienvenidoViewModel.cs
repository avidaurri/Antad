using Antad.Helpers;
using Antad.Views;
using AntadComun.Models;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Antad.ViewModels
{
    public class BienvenidoViewModel :BaseViewModel
    {


        #region Properties

        public UserSession urr = JsonConvert.DeserializeObject<UserSession>(Settings.UserSession);

        public string Puesto
        {
            get
            {

                return urr.idPuesto.ToString();


            }
        }
        #endregion

        #region Contructors

        public BienvenidoViewModel()
        {
           
            //Direccionar();

        }

        #endregion

        #region Command

        public ICommand DireccionarCommand
        {
            get
            {

                return new RelayCommand(Direccionar);

            }
        }

        private async void Direccionar()
        {
            //await Task.Delay(1000);
            string roo = this.Puesto;
            if (roo.Equals("3"))
            {
                //intramuro
                MainViewModel.GetInstance().Intramuro = new IntramuroViewModel();
                //await Application.Current.MainPage.Navigation.PushAsync(new EditarUsuarioPage());
                await App.Navigator.PushAsync(new IntramuroPage());
            }
            else if (roo.Equals("1"))
            {
                //promotor
                MainViewModel.GetInstance().Promotor = new PromotorViewModel();
                //await Application.Current.MainPage.Navigation.PushAsync(new EditarUsuarioPage());
                await App.Navigator.PushAsync(new PromotorPage());
            }
        }

        #endregion
    }
}
