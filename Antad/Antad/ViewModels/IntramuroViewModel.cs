using Antad.Services;
using AntadComun.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Antad.ViewModels
{
    public class IntramuroViewModel:BaseViewModel
    {

        #region Attributes
        private ApiService apiService;
        private Intramuro sucursal { get; set; }

        #endregion


        #region Properties
        public Intramuro Sucursal
        {
            get { return this.sucursal; }
            set
            {
                sucursal = value;
                OnPropertyChanged();
            }
            // set => this.SetValue(ref this.usuarios, value);
        }
        #endregion

        #region Contructors
        public IntramuroViewModel()
        {
            this.CargarSucursal();
        }

        private void CargarSucursal()
        {
            //throw new NotImplementedException();
            // ok
            /*this.Sucursal = new Intramuro
            {                
                idUsuario = 1,
                empresa = "walmart",
                idSucursal = 1,
                nombreSucursal = "walmart",
                fotoSucursal = "https://compilacionweb.online/DemoAntad/FotosCC/soriana.jpg",
                distanciaSucursal = "6",
                existeSucursal = true,
                mostarMensajeSucursal = false,
                existeOperacion = true,
                mostrarMensajeOperacion = false,
                mensajeSucursal = "walmart",
                mensajeOperacion = "walmart",
                latitud = "walmart",
                longitud = "walmart",

            };*/

            // no sucursal
            this.Sucursal = new Intramuro
            {
                idUsuario = 1,
                empresa = "walmart",
                idSucursal = 1,
                nombreSucursal = "walmart",
                fotoSucursal = "https://compilacionweb.online/DemoAntad/FotosCC/soriana.jpg",
                distanciaSucursal = "6",
                existeSucursal = false,
                mostarMensajeSucursal = true,
                existeOperacion = false,
                mostrarMensajeOperacion = true,
                mensajeSucursal = "la sucursal no esta tu alcance",
                mensajeOperacion = "Ubicate en tu sucursalde trabajo",
                latitud = "walmart",
                longitud = "walmart",
                falloValidacion=true,

            };
        }
        #endregion
    }
}
