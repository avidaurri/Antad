﻿using Antad.Helpers;
using Antad.Services;
using AntadComun.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Antad.ViewModels
{
    public class EventoItemViewModel: Evento
    {

        #region Attributes
        private ApiService apiService;
       // private Evento eventt { get; set; }
        #endregion

        #region Properties
        public Evento Eventt
        { get; set; }
        #endregion

        #region Commands
        public ICommand DetalleEventoCommand
        {
            get
            {
                return new RelayCommand(DetalleEvento);
            }
        }

        private async void DetalleEvento()
        {
            string usuario = this.usuario;
            string folioEvento = this.folioEvento;

            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                return;
            }

            var usser = new Evento
            {
                usuario = usuario,
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

            this.Eventt = (Evento)response.Result;

            if (this.Eventt.clvEstatusEvento.Equals(3))
            {
                //autorizado- ir a pantalla de operacion


            }else if (this.Eventt.clvEstatusEvento.Equals(20))
            {
                // evento cancelado
                await Application.Current.MainPage.DisplayAlert("Mensaje", "Tu evento esta cancelado", "Aceptar");
                return;

            }
            else if (this.Eventt.seeQR)
            {
                // mostrar qr

            }
            else if (!this.Eventt.seeQR && this.Eventt.clvEstatusEvento.Equals(4))
            {
                // mostrar descripcion de mensaje
                await Application.Current.MainPage.DisplayAlert("Mensaje", this.Eventt.descripcionMensajeEvento, "Aceptar");
                return;
            }
        }
        #endregion


        #region Contructors
        public EventoItemViewModel()
        {
            this.apiService = new ApiService();
        }
        #endregion
    }
}
