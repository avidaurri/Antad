using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AntadApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Master : MasterDetailPage
    {
        public Master(Page pagina)
        {
            //
            InitializeComponent();
            //this.Detail = new NavigationPage(pagina);
            App.Navigator = new NavigationPage(pagina);
            this.Detail = App.Navigator;
            //this.Detail = new NavigationPage(new Bienvenido());
            //this.Detail = (new Bienvenido());
            //App.Navigator.PushAsync(new Bienvenido());
        }
        /*protected override void OnAppearing()
        {
            base.OnAppearing();
            
           App.Navigator = Navigator;


        }*/
    }
}