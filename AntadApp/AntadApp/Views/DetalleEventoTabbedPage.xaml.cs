using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace AntadApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalleEventoTabbedPage : Xamarin.Forms.TabbedPage
    {
        public DetalleEventoTabbedPage()
        {
            InitializeComponent();
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
        }
    }
}