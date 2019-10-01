using Antad.Services;
using AntadComun.Models;
namespace Antad.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Xamarin.Forms;
    public class MisEventosViewModel : BaseViewModel
    {
        private ApiService apiService;
        private bool isRefreshing;
        private ObservableCollection<MiEvento> miseventos { get; set; }
        public ObservableCollection<MiEvento> MisEventos
        {
            get { return this.miseventos; }
            set
            {
                miseventos = value;
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
        public MisEventosViewModel()
        {
            this.apiService = new ApiService();
            this.LoadMisEventos();
        }

        private async void LoadMisEventos()
        {
            this.IsRefreshing = true;
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("error", connection.Message, "ok");
                return;
            }
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetList<MiEvento>(url, "/api", "/MisEventos");
            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("error", response.Message, "ok");
                return;
            }
            this.IsRefreshing = false;
            var list = (List<MiEvento>)response.Result;
            this.MisEventos = new ObservableCollection<MiEvento>(list);
           
        }
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadMisEventos);
            }
        }
    }
}
