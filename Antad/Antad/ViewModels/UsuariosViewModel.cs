using Antad.Services;
using AntadComun.Models;
namespace Antad.ViewModels
{
    using Antad.Helpers;
    using GalaSoft.MvvmLight.Command;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Xamarin.Forms;
    public class UsuariosViewModel : BaseViewModel
    {
        #region Attributes
        private ApiService apiService;
        private bool isRefreshing;
        #endregion

        #region Properties
        private ObservableCollection<Usuario> usuarios { get; set; }
        public ObservableCollection<Usuario> Usuarios
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
            var list = (List<Usuario>)response.Result;
            this.Usuarios = new ObservableCollection<Usuario>(list);
           
        }
        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadUsuarios);
            }
        }
        #endregion
    }
}
