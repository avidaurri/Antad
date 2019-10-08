using Antad.Helpers;
using Antad.Services;
using AntadComun.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace Antad.ViewModels
{
    public class PromotorViewModel:BaseViewModel
    {
        #region Attributes
        private ApiService apiService;
        private Promotor Promotor { get; set; }
        private ObservableCollection<Evento> eventos { get; set; }
        #endregion


        #region Properties

        public UserSession urr = JsonConvert.DeserializeObject<UserSession>(Settings.UserSession);

        public string UserName
        {
            get
            {

                return urr.usuario;


            }
        }
        public ObservableCollection<Evento> Eventos {

            get { return this.eventos; }
            set
            {
                eventos = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Contructors
        public PromotorViewModel()
        {
            this.apiService = new ApiService();
            this.CargarEventos();
        }

         private async void CargarEventos()
         {
            //this.IsRefreshing = true;
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
               // this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                return;
            }

            var usser = new GetUserRequest
            {
                User = UserName,
                latitud = 1.2,
                longitud = 1.2,


            };

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlEvento"].ToString();
            var response = await this.apiService.PostList<Evento>(url, prefix, controller, usser);
            if (!response.IsSuccess)
            {
                //this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            var list = (List<Evento>)response.Result;
            this.Eventos = new ObservableCollection<Evento>(list);
            //this.IsRefreshing = false;
            //this.MyUsuarios = (List<Usuario>)response.Result;
            //this.RefreshList();

            //this.IsRefreshing = false;
        }
        #endregion
    }
}
