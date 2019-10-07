using Antad.Helpers;
using Antad.ViewModels;
using Antad.Views;
using AntadComun.Models;
using Newtonsoft.Json;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Antad
{
    public partial class App : Application
    {
        public static NavigationPage Navigator { get; internal set; }

        public App()
        {
            InitializeComponent();

            /*if(Settings.IsRemembered && !string.IsNullOrEmpty(Settings.Usuario))
            {
                MainViewModel.GetInstance().Usuarios = new UsuariosViewModel();
                MainPage = new Master();
            }
            else
            {
             MainViewModel.GetInstance().Login = new LoginViewModel();
             MainPage =new NavigationPage(new LoginPage());
            }*/
            var mainViewModel = MainViewModel.GetInstance();

            if (Settings.IsRemembered)
            {

                if (!string.IsNullOrEmpty(Settings.UserSession))
                {
                    mainViewModel.UserSession = JsonConvert.DeserializeObject<UserSession>(Settings.UserSession);
                }

                //mainViewModel.Usuarios = new UsuariosViewModel();
                mainViewModel.Intramuro = new IntramuroViewModel();
                this.MainPage = new Master();
            }
            else
            {
                mainViewModel.Login = new LoginViewModel();
                this.MainPage = new NavigationPage(new LoginPage());
            }


        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
