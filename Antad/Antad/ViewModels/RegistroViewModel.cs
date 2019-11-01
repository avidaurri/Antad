using Antad.Helpers;
using Antad.Services;
using ModelsNet.Models;
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
        public string ConfirmarPassword { get; set; }
        public int ClvPuesto { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Email { get; set; }
        public string Curp { get; set; }
        public string Telefono { get; set; }
        public string DescripcionTelefono { get; set; }
        public int ClvEstadoCivil { get; set; }
        public string Peso { get; set; }
        public string Altura { get; set; }
        public int ClvGradoEstudios { get; set; }
        public int ClvEstado { get; set; }
        public int ClvMunicipio{ get; set; }
        public string CodigoPostal { get; set; }
        public string Colonia { get; set; }
        public string Calle { get; set; }
        public string NumeroExterior { get; set; }
        public string NumeroInterior { get; set; }
        public int ClvBanco { get; set; }
        public string Clabe { get; set; }
        public string NumeroCuenta { get; set; }
        public string NumeroTarjeta { get; set; }
        public string NombreReferenciaUno { get; set; }
        public string TelefonoReferenciaUno { get; set; }
        public string NombreReferenciaDos { get; set; }
        public string TelefonoReferenciaDos { get; set; }
        public string NombreReferenciaTres { get; set; }
        public string TelefonoReferenciaTres { get; set; }
        public string clabe { get; set; }
        public string foto { get; set; }
        public string identificacion { get; set; }
        public string comprobanteDomiciliario { get; set; }
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

                EstadoCivilText = _selectedEstadoCivil.value.ToString();

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
                EstadoText = _selectedEstado.value.ToString();
                cargaMun(_selectedEstado.key.ToString());
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
                // ????????
                if (_selectedMunicipio != null)
                {
                    MunicipioText = _selectedMunicipio.value.ToString();
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
            var response = await this.apiService.GetCatalogos(url, prefix, controller);
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
            //this.NombreReferenciaUno = "hola";
            //string prue = this.BancoText;

            //VALIDACIONES
            //validar foto perfil
            byte[] imageArray = null;
            if (this.fileFoto != null)
            {
                imageArray = FilesHelper.ReadFully(this.fileFoto.GetStream());
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Agrega tu foto de perfil", Languages.Accept);
                return;
            }


            //Login
            if (string.IsNullOrEmpty(this.Login))
            {
                await Application.Current.MainPage.DisplayAlert("Aviso","Escribe tu usuario",Languages.Accept);
                return;
            }

            //Password
            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Escribe tu contraseña", Languages.Accept);
                return;
            }
            if (this.Password.Length < 3)
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Escribe una contraseña mayor a 3 caracteres", Languages.Accept);
                return;
            }

            //ClvPuesto--  this.PuestoText
            if (this.PuestoText == null)
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Selecciona el puesto que te interesa", Languages.Accept);
                return;
            }
            //Nombre
            if (string.IsNullOrEmpty(this.Nombre))
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Escribe tu nombre", Languages.Accept);
                return;
            }
            //ApellidoPaterno
            if (string.IsNullOrEmpty(this.ApellidoPaterno))
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Escribe tu apellido paterno", Languages.Accept);
                return;
            }
            //ApellidoMaterno
            if (string.IsNullOrEmpty(this.ApellidoMaterno))
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Escribe tu apellido materno",Languages.Accept);
                return;
            }
            //Curp
            if (string.IsNullOrEmpty(this.Curp))
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Escribe tu CURP", Languages.Accept);
                return;
            }
            if (!RegexHelper.IsValidCurp(this.Curp))
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Escribe una CURP valida", Languages.Accept);
                return;
            }
            //Email
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Escribe tu email",Languages.Accept);
                return;
            }
            if (!RegexHelper.IsValidEmailAddress(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Escribe un email valido",Languages.Accept);
                return;
            }

            //validar identificacion
            byte[] imageArrayIdentificacion = null;
            if (this.fileIdentificacion != null)
            {
                imageArrayIdentificacion = FilesHelper.ReadFully(this.fileIdentificacion.GetStream());
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Agrega la foto de tu identificación", Languages.Accept);
                return;
            }
            //Telefono
            if (string.IsNullOrEmpty(this.Telefono))
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Escribe tu teléfono", Languages.Accept);
                return;
            }
            if (!RegexHelper.IsValidTel(this.Telefono) || this.Telefono.Length !=10)
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Escribe un teléfono de 10 dígitos", Languages.Accept);
                return;
            }
            //DescripcionTelefono
            if (string.IsNullOrEmpty(this.DescripcionTelefono))
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Escribe la descripción de tu teléfono", Languages.Accept);
                return;
            }
            //ClvEstadoCivil --this.EstadoCivilText
            if (this.EstadoCivilText == null)
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Selecciona tu estado civil", Languages.Accept);
                return;
            }
            //Peso
            double ejem = 0;
            if(double.TryParse(this.Peso, out ejem))
            {
                if (double.IsNaN(Convert.ToDouble(this.Peso))|| Convert.ToDouble(this.Peso)==0 || Convert.ToDouble(this.Peso) > 200 || Convert.ToDouble(this.Peso)<20)
                {
                    await Application.Current.MainPage.DisplayAlert("Aviso", "Escribe un peso valido en Kilogramos", Languages.Accept);
                    return;
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Introduce un Peso valido", Languages.Accept);
                return;
            }

            //Altura
            if (double.TryParse(this.Altura, out ejem))
            {
                if (double.IsNaN(Convert.ToDouble(this.Altura)) || Convert.ToDouble(this.Altura) == 0 || Convert.ToDouble(this.Altura) > 2 || Convert.ToDouble(this.Altura) < .5)
                {
                    await Application.Current.MainPage.DisplayAlert("Aviso", "Escribe una altura valida en Metros", Languages.Accept);
                    return;
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Introduce una Altura valida", Languages.Accept);
                return;
            }     

            //ClvGradoEstudios-- this.GradoEstudiosText
            if (this.GradoEstudiosText == null)
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Selecciona tu grado de estudios", Languages.Accept);
                return;
            }
            // ClvEstado -- this.EstadoText
            if (this.EstadoText == null)
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Selecciona tu estado de residencia", Languages.Accept);
                return;
            }
            // ClvMunicipio -- this.MunicipioText
            if (this.MunicipioText == null)
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Selecciona tu municipio de residencia", Languages.Accept);
                return;
            }
            //CodigoPostal
            if (string.IsNullOrEmpty(this.CodigoPostal))
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Escribe un código postal", Languages.Accept);
                return;
            }
            if (!RegexHelper.IsValidCP(this.CodigoPostal) || this.CodigoPostal.Length != 5)
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Código postal no valido", Languages.Accept);
                return;
            }
            //Colonia
            if (string.IsNullOrEmpty(this.Colonia))
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Escribe el nombre de la colonia donde resides", Languages.Accept);
                return;
            }
            //Calle
            if (string.IsNullOrEmpty(this.Calle))
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Escribe el nombre de la calle donde resides", Languages.Accept);
                return;
            }
            //NumeroExterior
            if (string.IsNullOrEmpty(this.NumeroExterior)|| !RegexHelper.IsValidCP(this.NumeroExterior))
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Escribe un numero exterior", Languages.Accept);
                return;
            }
            //NumeroInterior
            if (string.IsNullOrEmpty(this.NumeroInterior) || !RegexHelper.IsValidCP(this.NumeroInterior))
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Escribe un numero interior", Languages.Accept);
                return;
            }

            // validar comprobante
            byte[] imageArrayComprobante = null;
            if (this.fileComprobante != null)
            {
                imageArrayComprobante = FilesHelper.ReadFully(this.fileComprobante.GetStream());
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Agrega la foto de tu comprobante domiciliario", Languages.Accept);
                return;
            }
            //ClvBanco -- this.BancoText
            if (this.BancoText == null)
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Selecciona un banco", Languages.Accept);
                return;
            }
            //Clabe
            if (string.IsNullOrEmpty(this.Clabe))
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Escribe tu cuenta clabe", Languages.Accept);
                return;
            }
            if (!RegexHelper.IsValidCP(this.Clabe)|| this.Clabe.Length != 18)
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Escribe tu cuenta clabe de 18 números", Languages.Accept);
                return;
            }

            //NumeroCuenta
            if (string.IsNullOrEmpty(this.NumeroCuenta))
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Escribe tu número de cuenta", Languages.Accept);
                return;
            }
            if (!RegexHelper.IsValidCP(this.NumeroCuenta) || this.NumeroCuenta.Length >12)
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Escribe un número de cuenta no mayor a 12 números", Languages.Accept);
                return;
            }
            //NumeroTarjeta
            if (string.IsNullOrEmpty(this.NumeroTarjeta))
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Escribe tu número de tarjeta", Languages.Accept);
                return;
            }
            if (!RegexHelper.IsValidCP(this.NumeroTarjeta) || this.NumeroTarjeta.Length !=16)
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Escribe un número de tarjeta de 16 dígitos", Languages.Accept);
                return;
            }
            //NombreReferenciaUno
            if (string.IsNullOrEmpty(this.NombreReferenciaUno))
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Escribe el nombre de la referencia Uno", Languages.Accept);
                return;
            }
            //TelefonoReferenciaUno
            if (string.IsNullOrEmpty(this.TelefonoReferenciaUno)||this.TelefonoReferenciaUno.Length != 10)
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Escribe el teléfono de la referencia Uno", Languages.Accept);
                return;
            }
            if (!RegexHelper.IsValidTel(this.TelefonoReferenciaUno))
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Escribe un teléfono de 10 dígitos en la referencia Uno", Languages.Accept);
                return;
            }
            //NombreReferenciaDos
            if (string.IsNullOrEmpty(this.NombreReferenciaDos))
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Escribe el nombre de la referencia Dos", Languages.Accept);
                return;
            }
            //TelefonoReferenciaDos
            if (string.IsNullOrEmpty(this.TelefonoReferenciaDos) || this.TelefonoReferenciaDos.Length != 10)
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Escribe el teléfono de la referencia Dos", Languages.Accept);
                return;
            }
            if (!RegexHelper.IsValidTel(this.TelefonoReferenciaDos))
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Escribe un teléfono de 10 dígitos  en la referencia Dos", Languages.Accept);
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

            /*byte[] imageArray = null;
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
            }*/


            var registro = new Registro
            {
                login = this.Login,//
                password = this.Password,//
                clvPuesto = Convert.ToInt32(this.PuestoText),//
                nombre = this.Nombre, //
                apellidoPaterno = this.ApellidoPaterno, //
                apellidoMaterno = this.ApellidoMaterno, //
                email = this.Email, //
                curp = this.Curp, //
                telefono = this.Telefono, //
                descripcionTelefono = this.DescripcionTelefono,
                estadoCivil = this.EstadoCivilText,//
                peso = Convert.ToDouble(this.Peso),//
                altura = Convert.ToDouble(this.Altura),//
                clvGradoEstudios = Convert.ToInt32(this.GradoEstudiosText),//
                estado = this.EstadoText,//
                municipio = this.MunicipioText,//
                codigoPostal = this.CodigoPostal,//
                colonia = this.Colonia,//
                calle = this.Calle,//
                numeroExterior = this.NumeroExterior,//
                numeroInterior = this.NumeroInterior,//
                clvBanco = Convert.ToInt32(this.BancoText),//
                clabe=this.Clabe,//
                numeroCuenta=this.NumeroCuenta,//
                numeroTarjeta=this.NumeroTarjeta,//
                nombreReferenciaUno=this.NombreReferenciaUno,//
                telefonoReferenciaUno=this.TelefonoReferenciaUno,//
                nombreReferenciaDos=this.NombreReferenciaDos,
                telefonoReferenciaDos=this.TelefonoReferenciaDos,
                ImageArray = imageArray,//
                IdentificacionArray = imageArrayIdentificacion,//
                ComprobanteArray = imageArrayComprobante,//
                foto = "",
                identificacion="",
                comprobanteDomiciliario="",

            };

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlRegistro"].ToString();
            var response = await this.apiService.Post<Registro>(url, prefix, controller, registro);

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

            Registro respuesta = (Registro)response.Result;

                await Application.Current.MainPage.DisplayAlert(
                "Mensaje",
               respuesta.mensajeRegistro,
                "Aceptar");

            if (respuesta.usuarioRegistrado)
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            }

      
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
