using System;
using System.Collections.Generic;
using System.Text;

namespace Antad.ViewModels
{
    public class MainViewModel
    {
        public MisEventosViewModel MisEventos { get; set; }

        public MainViewModel()
        {
            this.MisEventos = new MisEventosViewModel();
        }
    }
}
