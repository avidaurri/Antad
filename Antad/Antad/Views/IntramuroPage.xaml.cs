using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace Antad.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IntramuroPage : ContentPage
    {
        public IntramuroPage()
        {
            InitializeComponent();
        }

       /* private async Task Button_ClickedAsync(object sender, EventArgs e)
        {
            var scannerPage = new ZXingScannerPage();
            scannerPage.Title = "Lector QR";
            scannerPage.OnScanResult += (result) =>
            {
                scannerPage.IsScanning = false;
                Device.BeginInvokeOnMainThread(() =>
                {
                    Navigation.PopAsync();
                    string evento = result.Text;

                });
            };

            await Navigation.PushAsync(scannerPage);
        }*/

 
    }
}