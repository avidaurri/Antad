using Antad.Helpers;
using Antad.Services;
using AntadComun.Models;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Antad.ViewModels
{
    public class PromotorViewModel:BaseViewModel
    {
        #region Attributes
        private bool isRefreshing;
        private ApiService apiService;
        private Promotor Promotor { get; set; }
        private ObservableCollection<EventoItemViewModel> eventos { get; set; }
        #endregion


        #region Properties
        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set
            {
                isRefreshing = value;
                OnPropertyChanged();
            }
        }
        public UserSession urr = JsonConvert.DeserializeObject<UserSession>(Settings.UserSession);

        public string UserName
        {
            get
            {

                return urr.usuario;


            }
        }
        public ObservableCollection<EventoItemViewModel> Eventos {

            get { return this.eventos; }
            set
            {
                eventos = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(CargarEventos);
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
            this.IsRefreshing = true;
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
               this.IsRefreshing = false;
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
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            var list = (List<Evento>)response.Result;
            var myList = list.Select(p => new EventoItemViewModel
            {
                folioEvento = p.folioEvento,
                clv_Empleado = p.clv_Empleado,
                usuario = p.usuario,
                fotoSucursal = p.fotoSucursal,
                folioSucursal = p.folioSucursal,
                nombreSucursal = p.nombreSucursal,
                fechaInicio = p.fechaInicio,
                fechaFinal = p.fechaFinal,
                estatusEvento = p.estatusEvento,
                clvEstatusEvento = p.clvEstatusEvento,

            });

            this.Eventos = new ObservableCollection<EventoItemViewModel>(myList);
            this.IsRefreshing = false;
            //this.MyUsuarios = (List<Usuario>)response.Result;
            //this.RefreshList();

            //this.IsRefreshing = false;
        }
        #endregion
    }
}
