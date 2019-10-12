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
    public partial class RegistroPage : CarouselPage
    {
        public RegistroPage()
        {
            InitializeComponent();
            //cargarAnimaciones();
        }

        /*private async void cargarAnimaciones()
        {
            await cargarAni.FadeTo(1,700);
            await cargarAni.FadeTo(.3, 700);
            await cargarAni.FadeTo(1, 700);
            await cargarAni.FadeTo(.3, 700);
            await cargarAni.FadeTo(1, 700);
            await cargarAni.FadeTo(.3, 700);
            await cargarAni.FadeTo(1, 700);
            await cargarAni.FadeTo(.3, 700);
            await cargarAni.FadeTo(1, 700);
            await cargarAni.FadeTo(.3, 700);
            await cargarAni.FadeTo(1, 700);
            await cargarAni.FadeTo(.3, 700);
            await cargarAni.FadeTo(1, 700);
            await cargarAni.FadeTo(.3, 700);
            await cargarAni.FadeTo(1, 700);
            await cargarAni.FadeTo(.3, 700);
            await cargarAni.FadeTo(1, 700);
        }*/
    }
}