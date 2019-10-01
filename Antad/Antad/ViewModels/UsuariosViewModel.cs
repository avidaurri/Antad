using Antad.Helpers;
using Antad.Services;
using AntadComun.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Antad.ViewModels
{
    public class UsuariosViewModel : BaseViewModel
    {
        private ApiService apiService;
        private bool isRefreshing;
        private ObservableCollection<Usuario> usuarios { get; set; }
        public ObservableCollection<Usuario> Usuarios
        {
            get { return this.usuarios; }
            set
            {
                usuarios = value;
                OnPropertyChanged();
            }
            // set => this.SetValue(ref this.miseventos, value);
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
        public UsuariosViewModel()
        {
            this.apiService = new ApiService();
            this.LoadUsuarios();
        }

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
            var list = (List<Usuario>)response.Result;
            this.Usuarios = new ObservableCollection<Usuario>(list);

        }
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadUsuarios);
            }
        }
    }
}
