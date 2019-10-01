using Antad.Services;
using AntadComun.Models;
namespace Antad.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Xamarin.Forms;
    public class MisEventosViewModel : BaseViewModel
    {
        private ApiService apiService;
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
        public MisEventosViewModel()
        {
            this.apiService = new ApiService();
            this.LoadMisEventos();
        }

        private async void LoadMisEventos()
        {
            var response = await this.apiService.GetList<MiEvento>("http://apiantaddemo.trademetrix.online", "/api", "/MisEventos");
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("error", response.Message, "ok");
                return;
            }

            var list = (List<MiEvento>)response.Result;
            this.MisEventos = new ObservableCollection<MiEvento>(list);

        }
    }
}
