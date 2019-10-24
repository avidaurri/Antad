

namespace Antad.ViewModels
{
    using Antad.Helpers;
    using Antad.Views;
    using AntadComun.Models;
    using GalaSoft.MvvmLight.Command;
    using Newtonsoft.Json;
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

        public PreregistroViewModel Preregistro { get; set; }
        public EventoOperacionViewModel EventoOperacion { get; set; }

        public EventoDetalleViewModel EventoDeta { get; set; }

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
            //this.LoadMenu();
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
             UserSession urr = JsonConvert.DeserializeObject<UserSession>(Settings.UserSession);
            int clvPu = urr.clvPuesto;
            this.Menu = new ObservableCollection<MenuItemViewModel>();

            this.Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_action_user",
                PageName = "Bienvenido",
                Title = "Bienvenido",
            });


            if (clvPu.Equals(3))
            {

                this.Menu.Add(new MenuItemViewModel
                {
                    Icon = "ic_misucursal",
                    PageName = "misucursal",
                    Title = "Mi Sucursal",
                });
                this.Menu.Add(new MenuItemViewModel
                {
                    Icon = "ic_misusuarios",
                    PageName = "misusuarios",
                    Title = "Mis Usuarios",
                });

                this.Menu.Add(new MenuItemViewModel
                {
                    Icon = "ic_misincidencias",
                    PageName = "misincidencias",
                    Title = "Incidencias",
                });
                this.Menu.Add(new MenuItemViewModel
                {
                    Icon = "ic_misautorizaciones",
                    PageName = "misautorizaciones",
                    Title = "Mis Autorizaciones",
                });
                this.Menu.Add(new MenuItemViewModel
                {
                    Icon = "ic_misrechazos",
                    PageName = "misrechazos",
                    Title = "Mis Rechazos",
                });
            }
            else if(clvPu.Equals(1)){

            }

    


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
            string estatus = this.EventoOperacion.Evento.clvEdoEvento.ToString();
            string eventtoo = this.EventoOperacion.Evento.folioEvento;
            string usuario = this.EventoOperacion.Evento.clvEmp.ToString();
            PopupNavigation.Instance.PushAsync(new PopupView(usuario,eventtoo));

        }
        #endregion
    }
}
