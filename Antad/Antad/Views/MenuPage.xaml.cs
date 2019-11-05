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
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();
            this.BackgroundImageSource = ImageSource.FromFile("menudetail");
           // this.BackgroundImageSource = FileImageSource.FromFile("drawable/patternwide");
        }
    }
}