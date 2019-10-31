using Antad.Helpers;
using Antad.Services;
using Antad.Views;
using ModelsNet.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Antad.ViewModels
{
    public class EventoDetalleViewModel :BaseViewModel
    {
        #region Attributes
        private ApiService apiService;
        private Evento eventodetalle { get; set; }
        private int clvemp;
        private string folioevento;
        #endregion

        #region Properties
        public Evento Eventodetalle
        {
            get { return this.eventodetalle; }
            set
            {
                eventodetalle = value;
                OnPropertyChanged();
            }
        }
        public int Clvemp
        {
            get { return this.clvemp; }
            set
            {
                clvemp = value;
                OnPropertyChanged();
            }
        }
        public string Folioevento
        {
            get { return this.folioevento; }
            set
            {
                folioevento = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Contructors
        public EventoDetalleViewModel(int clvEmp, string folioEvento)
        {
            this.apiService = new ApiService();
            this.folioevento = folioEvento;
            this.clvemp = clvEmp;
            getDetalleEvento(clvEmp, folioEvento);
        }


        #region Commands
        public ICommand UpdateDetailCommand
        {
            get
            {
                return new GalaSoft.MvvmLight.Command.RelayCommand(UpdateDetail);
            }
        }

        public ICommand ViewQrCommand
        {
            get
            {
                return new GalaSoft.MvvmLight.Command.RelayCommand(ViewQr);
            }
        }

        private void ViewQr()
        {
            string usuario = this.clvemp.ToString();
            string folioEvento = this.folioevento.ToString();

            PopupNavigation.Instance.PushAsync(new PopupView(usuario, folioEvento));
        }

        private void UpdateDetail()
        {
            int usuario = this.clvemp;
            string folioEvento = this.folioevento.ToString();
            getDetalleEvento(usuario, folioEvento);
        }
        #endregion

        private async void getDetalleEvento(int clvEmp, string folioEvento)
        {
         /*   string usuario = this.clvEmp.ToString();
            string folioEvento = this.folioEvento;*/

            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {

                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                return;
            }

            var usser = new Evento
            {
                clvEmp = clvEmp,
                folioEvento = folioEvento,

            };

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlEventoMensajeDetalle"].ToString();
            var response = await this.apiService.GetDetalleEvento(url, prefix, controller, usser);
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }

            this.Eventodetalle = (Evento)response.Result;

            /*if (this.eventodetalle.clvEdoEvento.Equals(3))
            {
                //autorizado- ir a pantalla de operacion
                MainViewModel.GetInstance().EventoOperacion = new EventoOperacionViewModel(this);
                //await Application.Current.MainPage.Navigation.PushAsync(new EditarUsuarioPage());
                await App.Navigator.PushAsync(new EventoOperacionPage());

            }
            else if (this.Eventt.clvEdoEvento.Equals(20))
            {
                // evento cancelado
                await Application.Current.MainPage.DisplayAlert("Mensaje", "Tu evento esta cancelado", "Aceptar");
                return;

            }
            else if (this.Eventt.seeQR)
            {
                // mostrar qr
                PopupNavigation.Instance.PushAsync(new PopupView(this.Eventt.clvEmp.ToString(), this.Eventt.folioEvento, this.Eventt.clvEdoEvento.ToString(), "Pide autorización a través de este código"));
            }
            else if (!this.Eventt.seeQR && this.Eventt.clvEdoEvento.Equals(4))
            {
                // mostrar descripcion de mensaje
                await Application.Current.MainPage.DisplayAlert("Mensaje", this.Eventt.descripcionMensajeEvento, "Aceptar");
                return;
            }*/
        }
        #endregion


        #region Commands

        #endregion
    }
}
