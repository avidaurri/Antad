using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Antad.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Master : MasterDetailPage
    {
        public Master()
        {
            InitializeComponent();
           // this.Detail.Navigation.PushAsync(new PromotorPage());
            //this.Detail = new NavigationPage(new PromotorPage());
            //Detail =new IntramuroPage();

            //this.Detail=new IntramuroPage();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            App.Navigator = Navigator;

            
        }
    }
}