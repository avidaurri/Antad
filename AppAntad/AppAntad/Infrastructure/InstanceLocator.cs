using AppAntad.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppAntad.Infrastructure
{
    public class InstanceLocator
    {
        public MainViewModel Main { get; set; }

        public InstanceLocator()
        {
            this.Main = new MainViewModel();
        }
    }
}
