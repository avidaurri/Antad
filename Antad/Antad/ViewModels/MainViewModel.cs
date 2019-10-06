

namespace Antad.ViewModels
{
    using Antad.Views;
    using GalaSoft.MvvmLight.Command;
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    public class MainViewModel
    {

        #region Properties
        public ObservableCollection<MenuItemViewModel> Menu { get; set; }
        public LoginViewModel Login { get; set; }
        public EditarUsuarioViewModel EditarUsuario { get; set; }
        public RegistroViewModel Register { get; set; }

        public UsuariosViewModel Usuarios { get; set; }
        public AgregarUsuarioViewModel AgregarUsuario { get; set; }
        #endregion


        #region Contructors
        public MainViewModel()
        {
            instance = this;
            this.LoadMenu();
            //this.Usuarios = new UsuariosViewModel();

        }


        #endregion

        #region Methods
        private void LoadMenu()
        {
            this.Menu = new ObservableCollection<MenuItemViewModel>();

            this.Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_info",
                PageName = "AboutPage",
                Title = "Acerc de",
            });

            this.Menu.Add(new MenuItemViewModel
            {
                Icon = "ic_phonelink_setup",
                PageName = "SetupPage",
                Title = "Configuración",
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
    }
}
