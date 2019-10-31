using ModelsNet.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Antad.ViewModels
{
    public class EventoOperacionViewModel : BaseViewModel
    {

        #region Attributes
        private Evento evento;
        #endregion

        #region Properties
        public Evento Evento
        {
            get { return this.evento; }
            set
            {
                evento = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Contructors
        public EventoOperacionViewModel(Evento evento)
        {
            this.evento = evento;
        }
        #endregion


        #region Commands
 
        #endregion
    }
    } 
