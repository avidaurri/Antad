﻿using Antad.Helpers;
using Antad.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Antad.ViewModels
{
    public class MenuItemViewModel
    {
        #region Properties
        public string Icon { get; set; }
        public string Title { get; set; }
        public string PageName { get; set; }
        #endregion

        #region Commands
        public ICommand GotoCommand {
            get
            {
                return new RelayCommand(GoTo);
            }
  
        }



        #endregion

        #region Methods
        private void GoTo()
        {

            if (this.PageName == "LoginPage")
            {
                Settings.Usuario = string.Empty;
                Settings.Password = string.Empty;
                Settings.IsRemembered = false;
                MainViewModel.GetInstance().Login = new LoginViewModel();
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
        }
        #endregion
    }
}