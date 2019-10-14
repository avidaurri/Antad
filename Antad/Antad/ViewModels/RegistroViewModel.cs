using Antad.Helpers;
using Antad.Services;
using AntadComun.Models;
using GalaSoft.MvvmLight.Command;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Antad.ViewModels
{
    public class RegistroViewModel :BaseViewModel
    {

        #region Attributes

        private MediaFile fileFoto;
        private MediaFile fileIdentificacion;
        private MediaFile fileComprobante;
        private ImageSource imageSourceFoto;
        private ImageSource imageSourceComprobante;
        private ImageSource imageSourceIdentificacion;
      
        private ApiService apiService;
        private bool isRunning;
        private bool isEnabled;
        private List<CatalogoRegistro.Banco> bancoList { get; set; }
        private List<CatalogoRegistro.EstadoCivil> estadoCivilList { get; set; }
        private List<CatalogoRegistro.GradoEstudios> gradoEstudiosList { get; set; }
        private List<CatalogoRegistro.Estados> estadosList { get; set; }
        private List<CatalogoRegistro.Municipio> municipiosList { get; set; }
        private List<CatalogoRegistro.Region> regionList { get; set; }
        private List<CatalogoRegistro.Puesto> puestoList { get; set; }
        #endregion

        #region Properties
        public string Login { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Curp { get; set; }
        public string clabe { get; set; }
        public string foto { get; set; }
        public string identificacion { get; set; }
        public string comprobanteDomiciliario { get; set; }
        public string Email { get; set; }
        public string EmpresaInteres { get; set; }
        public int Puesto { get; set; }
        public string banco { get; set; }
        private string _puestoText{ get; set; }

        public string PuestoText
        {
            get
            {
                return _puestoText;
            }
            set
            {
                _puestoText = value;
            }
        }
        private string _bancoText { get; set; }
        public string BancoText
        {
            get
            {
                return _bancoText;
            }
            set
            {
               _bancoText=value;
            }
        }
        private string _estadocivilText { get; set; }
        public string EstadoCivilText
        {
            get
            {
                return _estadocivilText;
            }
            set
            {
                _estadocivilText = value;
            }
        }
        private string _gradoestudiosText { get; set; }
        public string GradoEstudiosText
        {
            get
            {
                return _gradoestudiosText;
            }
            set
            {
                _gradoestudiosText = value;
            }
        }
        private string _estadoText { get; set; }
        public string EstadoText
        {
            get
            {
                return _estadoText;
            }
            set
            {
                _estadoText = value;
            }
        }
        private string _municipioText { get; set; }
        public string MunicipioText
        {
            get
            {
                return _municipioText;
            }
            set
            {
                _municipioText = value;
            }
        }

        private string _regionText { get; set; }
        public string RegionText
        {
            get
            {
                return _regionText;
            }
            set
            {
                _regionText = value;
            }
        }
        //public List<string> listBank = new List<string>();
        private CatalogoRegistro.Puesto _selectedPuesto;
        public CatalogoRegistro.Puesto SelectedPuesto
        {
            get
            {
                return _selectedPuesto;
            }
            set
            {
                _selectedPuesto = value;
                //put here your code  
                PuestoText = _selectedPuesto.key.ToString();
            }
        }


        private CatalogoRegistro.Banco _selectedBanco;
        public CatalogoRegistro.Banco SelectedBanco
        {
            get
            {
                return _selectedBanco;
            }
            set
            {
                _selectedBanco = value;
                //put here your code  
                BancoText = _selectedBanco.key.ToString();
            }
        }
        private CatalogoRegistro.EstadoCivil _selectedEstadoCivil;
        public CatalogoRegistro.EstadoCivil SelectedEstadoCivil
        {

            get
            {
                return _selectedEstadoCivil;
            }
            set
            {
                _selectedEstadoCivil = value;
                //put here your code  

                EstadoCivilText = _selectedEstadoCivil.key.ToString();

            }
        }



        private CatalogoRegistro.GradoEstudios _selectedGradoEstudios;
        public CatalogoRegistro.GradoEstudios SelectedGradoEstudios
        {
            get
            {
                return _selectedGradoEstudios;
            }
            set
            {
                _selectedGradoEstudios = value;
                //put here your code  
                GradoEstudiosText = _selectedGradoEstudios.key.ToString();
            }
        }
        private CatalogoRegistro.Estados _selectedEstado;
        public CatalogoRegistro.Estados SelectedEstado
        {
            get
            {
                return _selectedEstado;
            }
            set
            {
                _selectedEstado = value;
               
                //put here your code  
                EstadoText = _selectedEstado.key.ToString();
                cargaMun(EstadoText);
            }
        }
        private async void cargaMun(string val)
        {
            //this.MunicipiosList.Clear();
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                return;
            }


            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlGetCatalogo"].ToString();// + "?idEstado=0";
            var response = await this.apiService.Post(url, prefix, controller, Convert.ToInt32(val));
            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }
            CatalogoRegistro cat = new CatalogoRegistro();
            cat = (CatalogoRegistro)response.Result;

            //municipios
            this.MunicipiosList =null;
            this.MunicipiosList = cat.listaMunicipios;

        }
        private CatalogoRegistro.Municipio _selectedMunicipio;
        public CatalogoRegistro.Municipio SelectedMunicipio
        {
            get
            {
                return _selectedMunicipio;
            }
            set
            {
                _selectedMunicipio = value;
                if (_selectedMunicipio != null)
                {
 MunicipioText = _selectedMunicipio.key.ToString();
                }
                //put here your code  
               
            }
        }
        private CatalogoRegistro.Region _selectedRegion;
        public CatalogoRegistro.Region SelectedRegion
        {
            get
            {
                return _selectedRegion;
            }
            set
            {
                _selectedRegion = value;
                //put here your code  
               RegionText = _selectedRegion.key.ToString();
            }
        }
        public List<CatalogoRegistro.Puesto> PuestoList
        {

            get { return this.puestoList; }
            set
            {
                puestoList = value;
                OnPropertyChanged();
            }
        }
        public List<CatalogoRegistro.Banco> BancoList
        {

            get { return this.bancoList; }
            set
            {
                bancoList = value;
                OnPropertyChanged();
            }
        }
        public List<CatalogoRegistro.EstadoCivil> EstadoCivilList
        {

            get { return this.estadoCivilList; }
            set
            {
                estadoCivilList = value;
                OnPropertyChanged();
            }
        }
        public List<CatalogoRegistro.GradoEstudios> GradoEstudiosList
        {

            get { return this.gradoEstudiosList; }
            set
            {
                gradoEstudiosList = value;
                OnPropertyChanged();
            }
        }
        public List<CatalogoRegistro.Estados> EstadosList
        {

            get { return this.estadosList; }
            set
            {
                estadosList = value;
                OnPropertyChanged();
            }
        }
        public List<CatalogoRegistro.Municipio> MunicipiosList
        {

            get { return this.municipiosList; }
            set
            {
                municipiosList = value;
                OnPropertyChanged();
            }
        }
        public List<CatalogoRegistro.Region> RegionList
        {

            get { return this.regionList; }
            set
            {
                regionList = value;
                OnPropertyChanged();
            }
        }
        public bool IsRunning
        {
            get { return this.isRunning; }
            set
            {
                isRunning = value;
                OnPropertyChanged();
            }
        }
        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set
            {
                isEnabled = value;
                OnPropertyChanged();
            }
        }

        public ImageSource ImageSourceFoto
        {
            get { return this.imageSourceFoto; }
            set
            {
                imageSourceFoto = value;
                OnPropertyChanged();
            }
        }

        public ImageSource ImageSourceComprobante
        {
            get { return this.imageSourceComprobante; }
            set
            {
                imageSourceComprobante = value;
                OnPropertyChanged();
            }
        }

        public ImageSource ImageSourceIdentificacion
        {
            get { return this.imageSourceIdentificacion; }
            set
            {
                imageSourceIdentificacion = value;
                OnPropertyChanged();
            }
        }
        #endregion


        #region Contructors
        public RegistroViewModel()
        {
            this.IsEnabled = true;
            this.apiService = new ApiService();
            this.ImageSourceFoto = "user_large";
            this.ImageSourceComprobante = "ic_launcher_domici";
            this.ImageSourceIdentificacion = "ic_launcher_ine";
            bancoList = new List<CatalogoRegistro.Banco>();
            CargarCatalogos();

        }

        private async void CargarCatalogos()
        {
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                return;
            }


            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlGetCatalogo"].ToString();// + "?idEstado=0";
            var response = await this.apiService.Get(url, prefix, controller);
            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
                return;
            }
            CatalogoRegistro cat = new CatalogoRegistro();
            cat= (CatalogoRegistro)response.Result;

            // bancos
            this.BancoList = cat.listaBancos;
            // estados civil
            this.EstadoCivilList = cat.listaEdoCivil;
            // grados de estudios
            this.GradoEstudiosList = cat.listaGradoEstudios;
            //estados
            this.EstadosList = cat.listaEstados;
            //municipios
            //this.MunicipiosList = cat.listaMunicipios;
            //regiones
            this.RegionList = cat.listaRegiones;
            //puesto
            this.PuestoList = cat.listaPuestos;


        }
        #endregion

        #region Commands


        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }

        private async void Save()
        {
            string prue = this.BancoText;

            if (string.IsNullOrEmpty(this.Curp))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Escribe la curp",
                    Languages.Accept);
                return;
            }

            /*if (!RegexHelper.IsValidCurp(this.Curp))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Escribe una curp valida",
                    Languages.Accept);
                return;
            }



            if (string.IsNullOrEmpty(this.Nombre))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Escribe el nombre",
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.ApellidoPaterno))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Escribe el apellido paterno",
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.ApellidoMaterno))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Escribe el apellido materno",
                    Languages.Accept);
                return;
            }
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Escribe el email",
                    Languages.Accept);
                return;
            }
            if (!RegexHelper.IsValidEmailAddress(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Escribe un email valido",
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.Login))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Escribe el login",
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Escribe el password",
                    Languages.Accept);
                return;
            }

            if (this.Password.Length < 3)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Escribe un password mayor a 3 digitos",
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.Curp))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Escribe la curp",
                    Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.EmpresaInteres))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Escribe la empresa de interes",
                    Languages.Accept);
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    Languages.Accept);
                return;
            }

            byte[] imageArray = null;
            if (this.fileFoto != null)
            {
                imageArray = FilesHelper.ReadFully(this.fileFoto.GetStream());
            }
            byte[] imageArrayIdentificacion = null;
            if (this.fileIdentificacion != null)
            {
                imageArrayIdentificacion = FilesHelper.ReadFully(this.fileIdentificacion.GetStream());
            }
            byte[] imageArrayComprobante = null;
            if (this.fileComprobante != null)
            {
                imageArrayComprobante = FilesHelper.ReadFully(this.fileComprobante.GetStream());
            }


            var registro = new Registro
            {
                login = this.Login,
                password = this.Password,
                nombre = this.Nombre,
                apellidoPaterno = this.ApellidoPaterno,
                apellidoMaterno = this.ApellidoMaterno,
                curp = this.Curp,
                email = this.Email,
                empresainteres = this.EmpresaInteres,
                clvPuesto = Convert.ToInt32(this.Puesto)+1,
                ImageArray = imageArray,
                IdentificacionArray = imageArrayIdentificacion,
                ComprobanteArray = imageArrayComprobante,
                foto = "",
                identificacion="",
                comprobanteDomiciliario="",

            };

            

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlRegistro"].ToString();
            var response = await this.apiService.Post(url, prefix, controller, registro);

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    Languages.Accept);
                return;
            }

            this.IsRunning = false;
            this.IsEnabled = true;

            await Application.Current.MainPage.DisplayAlert(
                "confirmación",
               "Registro exitoso",
                "Aceptar");

            await Application.Current.MainPage.Navigation.PopAsync();*/
        }

        public ICommand CargarImagenCommand
        {
            get
            {
                return new RelayCommand(CargarImagen);
            }
        }

        private async void CargarImagen()
        {
            await CrossMedia.Current.Initialize();

            var source = await Application.Current.MainPage.DisplayActionSheet(
                Languages.ImageSource,
                Languages.Cancel,
                null,
                Languages.FromGallery,
                Languages.NewPicture);

            if (source == Languages.Cancel)
            {
                this.fileFoto = null;
                return;
            }

            if (source == Languages.NewPicture)
            {
                this.fileFoto = await CrossMedia.Current.TakePhotoAsync(
                    new StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = "test.jpg",
                        PhotoSize = PhotoSize.Small,
                    }
                );
            }
            else
            {
                this.fileFoto = await CrossMedia.Current.PickPhotoAsync();
            }

            if (this.fileFoto != null)
            {
                this.ImageSourceFoto = ImageSource.FromStream(() =>
                {
                    var stream = this.fileFoto.GetStream();
                    return stream;
                });
            }
        }

        public ICommand CargarIdentificacionCommand
        {
            get
            {
                return new RelayCommand(CargarIdentificacion);
            }
        }

        private async void CargarIdentificacion()
        {
            await CrossMedia.Current.Initialize();

            var source = await Application.Current.MainPage.DisplayActionSheet(
                Languages.ImageSource,
                Languages.Cancel,
                null,
                Languages.FromGallery,
                Languages.NewPicture);

            if (source == Languages.Cancel)
            {
                this.fileIdentificacion = null;
                return;
            }

            if (source == Languages.NewPicture)
            {
                this.fileIdentificacion = await CrossMedia.Current.TakePhotoAsync(
                    new StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = "test.jpg",
                        PhotoSize = PhotoSize.Small,
                    }
                );
            }
            else
            {
                this.fileIdentificacion = await CrossMedia.Current.PickPhotoAsync();
            }

            if (this.fileIdentificacion != null)
            {
                this.ImageSourceIdentificacion = ImageSource.FromStream(() =>
                {
                    var stream = this.fileIdentificacion.GetStream();
                    return stream;
                });
            }
        }

        public ICommand CargarComprobanteCommand
        {
            get
            {
                return new RelayCommand(CargarComprobante);
            }
        }

        private async void CargarComprobante()
        {
            await CrossMedia.Current.Initialize();

            var source = await Application.Current.MainPage.DisplayActionSheet(
                Languages.ImageSource,
                Languages.Cancel,
                null,
                Languages.FromGallery,
                Languages.NewPicture);

            if (source == Languages.Cancel)
            {
                this.fileComprobante = null;
                return;
            }

            if (source == Languages.NewPicture)
            {
                this.fileComprobante = await CrossMedia.Current.TakePhotoAsync(
                    new StoreCameraMediaOptions
                    {
                        Directory = "Sample",
                        Name = "test.jpg",
                        PhotoSize = PhotoSize.Small,
                    }
                );
            }
            else
            {
                this.fileComprobante = await CrossMedia.Current.PickPhotoAsync();
            }

            if (this.fileComprobante != null)
            {
                this.ImageSourceComprobante = ImageSource.FromStream(() =>
                {
                    var stream = this.fileComprobante.GetStream();
                    return stream;
                });
            }
        }
        #endregion
    }
}
