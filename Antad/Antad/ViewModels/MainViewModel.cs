

namespace Antad.ViewModels
{
    using Antad.Views;
    using AntadComun.Models;
    using GalaSoft.MvvmLight.Command;
    using Rg.Plugins.Popup.Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    public class MainViewModel
    {

        #region Properties
        public UserSession UserSession { get; set; }
        public ObservableCollection<MenuItemViewModel> Menu { get; set; }
        public LoginViewModel Login { get; set; }
        public EditarUsuarioViewModel EditarUsuario { get; set; }
        public RegistroViewModel Register { get; set; }
        public EventoOperacionViewModel EventoOperacion { get; set; }
        public UsuariosViewModel Usuarios { get; set; }

        public IntramuroViewModel Intramuro { get; set; }

        public PromotorViewModel Promotor { get; set; }

        public AgregarUsuarioViewModel AgregarUsuario { get; set; }

        public ValidacionAutorizarViewModel ValidarAutorizar { get; set; }

        public ValidacionActividadViewModel ValidacionActividad { get; set; }

        public BienvenidoViewModel Bienvenido { get; set; }
       // public List<string> listBank = new List<string>();
        public string UserFullName
        {
            get
            {
                if(this.UserSession !=null)
                {
                    return $"{this.UserSession.nombre}";
                }
                return null;
            }
        }
        public string UserImageFullPath
        {
            get
            {
                if (this.UserSession != null)
                {
                    return $"{this.UserSession.foto}";
                }
                return null;
            }
        }

        public string UserName
        {
            get
            {
                if (this.UserSession != null)
                {
                    return $"{this.UserSession.usuario}";
                }
                return null;
            }
        }

        #endregion


        #region Contructors
        public MainViewModel()
        {
            instance = this;
            this.LoadMenu();
            //this.Usuarios = new UsuariosViewModel();
           // CargarCatalogos();
        }

        /*private void CargarCatalogos()
        {
            var monkeyList = new List<string>();
            monkeyList.Add("Baboon");
            monkeyList.Add("Capuchin Monkey");
            monkeyList.Add("Blue Monkey");
            monkeyList.Add("Squirrel Monkey");
            monkeyList.Add("Golden Lion Tamarin");
            monkeyList.Add("Howler Monkey");
            monkeyList.Add("Japanese Macaque");

            //var picker = new Picker { Title = "Select a monkey", TitleColor = Color.Red };
            this.listBank = monkeyList;
        }*/
        #endregion

        #region Methods
        private void LoadMenu()
        {
            this.Menu = new ObservableCollection<MenuItemViewModel>();

            this.Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_action_user",
                PageName = "SetupPage",
                Title = "Perfil",
            });
            this.Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_action_report",
                PageName = "SetupPage",
                Title = "Incidencias",
            });
            this.Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_action_time",
                PageName = "SetupPage",
                Title = "Historial",
            });

            this.Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_phonelink_setup",
                PageName = "SetupPage",
                Title = "Configuración",
            });
            this.Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_info",
                PageName = "AboutPage",
                Title = "Acerca de",
            });
            this.Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_exit_to_app",
                PageName = "LoginPage",
                Title = "Salir",
            });
        }
        #endregion

        #region Singleton
        public static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }
            return instance;
        }
        #endregion

        #region Commands
        public ICommand AddProductoCommand
        {
            get
            {

                return new RelayCommand(IrAgregarUsuario);

            }
        } 
       

        private async void IrAgregarUsuario()
        {
            this.AgregarUsuario = new AgregarUsuarioViewModel();
            // await Application.Current.MainPage.Navigation.PushAsync(new AgregarUsuarioPage());
            await App.Navigator.PushAsync(new AgregarUsuarioPage());
           // await App.Navigator.PushAsync(new AgregarUsuarioPage());

        }
        #endregion

        #region Commands
        public ICommand SeeQRCommand
        {
            get
            {
                return new RelayCommand(SeeQR);
            }
        }

        private void SeeQR()
        {
            string estatus = this.EventoOperacion.Evento.clvEstatusEvento.ToString();
            string eventtoo = this.EventoOperacion.Evento.folioEvento;
            string usuario = this.EventoOperacion.Evento.usuario;
            PopupNavigation.Instance.PushAsync(new PopupView(usuario,eventtoo,estatus,"Escanea este código para supervisar tu evento"));

        }
        #endregion
    }
}
