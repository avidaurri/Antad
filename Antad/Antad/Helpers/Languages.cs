using Antad.Interfaces;
using Antad.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Antad.Helpers
{
    public static class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Accept
        {
            get { return Resource.Accept; }
        }

        public static string Error
        {
            get { return Resource.Error; }
        }

        public static string NoInternet
        {
            get { return Resource.NoInternet; }
        }

        public static string TurnOnInternet
        {
            get { return Resource.TurnOnInternet; }
        }

        public static string Users
        {
            get { return Resource.Users; }
        }
        public static string AddUsers
        {
            get { return Resource.Users; }
        }
    }
}
