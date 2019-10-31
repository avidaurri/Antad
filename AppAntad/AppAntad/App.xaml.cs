﻿using AppAntad.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppAntad
{
    public partial class App : Application
    {
        public static NavigationPage Navigator { get; set; }
        public App()
        {
            InitializeComponent();
            MainPage = new SplashPageIndex();
            /*var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Login = new LoginViewModel();
            MainPage = new LoginPage();*/
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
