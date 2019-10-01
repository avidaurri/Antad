using Antad.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Antad.ViewModels
{
    public class MainViewModel
    {
        public MisEventosViewModel MisEventos { get; set; }
        public AddProductViewModel AddProduct { get; set; }

        public MainViewModel()
        {
            this.MisEventos = new MisEventosViewModel();
        }
        public ICommand AddProductoCommand {
            get
            {

                return new  RelayCommand(GoToAddProduct);

            }
        }

        private async void GoToAddProduct()
        {
            this.AddProduct = new AddProductViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new AddProductPage());

        }
    }
}
