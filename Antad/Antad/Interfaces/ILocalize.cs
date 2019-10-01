using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Antad.Interfaces
{
    public interface ILocalize
    {
        //idioma del telefono
        CultureInfo GetCurrentCultureInfo();

        //configuracio del telefono
        void SetLocale(CultureInfo ci);
    }
}
