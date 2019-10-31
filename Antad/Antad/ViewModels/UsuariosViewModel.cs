using Antad.Services;
using AntadComun.Models;
namespace Antad.ViewModels
{
    using Antad.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Xamarin.Forms;
    public class UsuariosViewModel : BaseViewModel
    {
        #region Attributes
        public string filtro { get; set; }
        private ApiService apiService;
        private bool isRefreshing;
        public List<Usuario> MyUsuarios { get; set; }
        private ObservableCollection<UsuarioItemViewModel> usuarios { get; set; }
        #endregion

        #region Properties
        public string Filtro {
            get { return this.filtro; }
            set {
                this.filtro = value;
                this.RefreshList();
            }
        }
        public ObservableCollection<UsuarioItemViewModel> Usuarios
        {
            get { return this.usuarios; }
            set
            {
                usuarios = value;
                OnPropertyChanged();
            }
            // set => this.SetValue(ref this.usuarios, value);
        }
        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set
            {
                isRefreshing = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Constructors
        public UsuariosViewModel()
        {
            instance = this;
            this.apiService = new ApiService();
            this.LoadUsuarios();
        }
        #endregion

        #region Singleton
        public static UsuariosViewModel instance;

        public static UsuariosViewModel GetInstance()
        {
            if (instance == null)
            {
                return new UsuariosViewModel();
            }
            return instance;
        }
        #endregion

        #region Methods
        private async void LoadUsuarios()
        {
            this.IsRefreshing = true;
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                return;
            }
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlUsuarios"].ToString();
            var response = await this.apiService.GetList<Usuario>(url, prefix, controller);
            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }
            this.IsRefreshing = false;
            this.MyUsuarios = (List<Usuario>)response.Result;
            this.RefreshList();

            this.IsRefreshing = false;
           
        }

        public void RefreshList()
        {
            if (string.IsNullOrEmpty(this.Filtro))
            {
                var mylistUsuarioItemViewModel = this.MyUsuarios.Select(p => new UsuarioItemViewModel
                {
                    foto = p.foto,
                    nombre = p.nombre,
                    rol = p.rol,
                    idUsuario = p.idUsuario,
                    usuario = p.usuario,
                    rfc = p.rfc,
                    curp = p.curp,
                });
                this.Usuarios = new ObservableCollection<UsuarioItemViewModel>(mylistUsuarioItemViewModel.OrderBy(p => p.idUsuario));
            }
            else
            {
                var mylistUsuarioItemViewModel = this.MyUsuarios.Select(p => new UsuarioItemViewModel
                {
                    foto = p.foto,
                    nombre = p.nombre,
                    rol = p.rol,
                    idUsuario = p.idUsuario,
                    usuario = p.usuario,
                    rfc = p.rfc,
                    curp = p.curp,
                }).Where(p=>p.usuario.ToLower().Contains(this.Filtro.ToLower())).ToList();
                this.Usuarios = new ObservableCollection<UsuarioItemViewModel>(mylistUsuarioItemViewModel.OrderBy(p => p.idUsuario));
            }

        }
        #endregion

        #region Commands

        public ICommand SearchCommand
        {
            get
            {
                return new GalaSoft.MvvmLight.Command.RelayCommand(RefreshList);
            }
        }
        public ICommand RefreshCommand
        {
            get
            {
                return new GalaSoft.MvvmLight.Command.RelayCommand(LoadUsuarios);
            }
        }
        #endregion
    }
}
