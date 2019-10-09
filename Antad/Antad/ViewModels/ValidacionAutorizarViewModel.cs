using Antad.Helpers;
using Antad.Services;
using AntadComun.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Antad.ViewModels
{
    public class ValidacionAutorizarViewModel : BaseViewModel
    {


        #region Attributes
        private string evento;
        private ApiService apiService;
        private string usuario;
        private ValidaEvento validaEvento { get; set; }

        #endregion


        #region Properties
        public ValidaEvento ValidaEvento
        {

            get { return this.validaEvento; }
            set
            {
                validaEvento = value;
                OnPropertyChanged();
            }
        }

        #endregion


        #region Contructors
        public ValidacionAutorizarViewModel(string eventom, string usuariom)
        {
            this.apiService = new ApiService();
            this.evento = eventom;
            this.usuario = usuariom;
            this.traerEvento();
        }

        private async void traerEvento()
        {
            //this.IsRefreshing = true;
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                //this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                return;
            }

            var usser = new ParamValidarEvento
            {
                folioEvento = this.evento,
                usuario = this.usuario,

            };

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlValidaEvento"].ToString();
            var response = await this.apiService.GetWithPostVa(url, prefix, controller, usser);
            if (!response.IsSuccess)
            {
                //this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }
            this.ValidaEvento= (ValidaEvento)response.Result;

            /*var list = (List<Evento>)response.Result;
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
            this.IsRefreshing = false;*/
            //this.MyUsuarios = (List<Usuario>)response.Result;
            //this.RefreshList();

            //this.IsRefreshing = false;
        }


        #endregion

    }
}
