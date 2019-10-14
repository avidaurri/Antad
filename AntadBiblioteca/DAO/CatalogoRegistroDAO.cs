using AntadBiblioteca.Util;
using AntadComun.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntadBiblioteca.DAO
{
    public class CatalogoRegistroDAO
    {
        public ConexionDB conexion;
        public CatalogoRegistroDAO(string cadena)
        {
            conexion = new ConexionDB(cadena);
        }
        public CatalogoRegistro getCatalogo()
        {
            CatalogoRegistro cat = new CatalogoRegistro();
            // get bancos
            string selectBancos = "select clv_banco as clave, descripcion as value from banco";
            List<Parametro> parametros = new List<Parametro>();

            Parametro paramIdEstado = new Parametro();
            paramIdEstado.Nombre = "@estado";
            paramIdEstado.Valor = "prueba";
            parametros.Add(paramIdEstado);

            SqlDataReader readerBancos = conexion.Consultar (selectBancos, parametros);

            List<CatalogoRegistro.Banco> ldocSuc = new List<CatalogoRegistro.Banco>();

            while (readerBancos.Read())
            {
                CatalogoRegistro.Banco docSuc = new CatalogoRegistro.Banco();
                docSuc.value = readerBancos["value"].ToString();
                docSuc.key = Convert.ToInt32(readerBancos["clave"].ToString());

                ldocSuc.Add(docSuc);
            }

            cat.listaBancos = ldocSuc;

            // get estado civil
            string selectEstadoCivil = "select clv_edo_civil as clave, descripcion as value from estado_civil";

            SqlDataReader readerEstadoCivil = conexion.Consultar(selectEstadoCivil, parametros);

            List<CatalogoRegistro.EstadoCivil> ldocEdoCivil = new List<CatalogoRegistro.EstadoCivil>();

            while (readerEstadoCivil.Read())
            {
                CatalogoRegistro.EstadoCivil docEdoCivil = new CatalogoRegistro.EstadoCivil();
                docEdoCivil.value = readerEstadoCivil["value"].ToString();
                docEdoCivil.key = Convert.ToInt32(readerEstadoCivil["clave"].ToString());

                ldocEdoCivil.Add(docEdoCivil);
            }

            cat.listaEdoCivil = ldocEdoCivil;


            // get grados de estudios
            string selectGradosEstudios = "select clv_grado_estu as clave, descripcion as value from grado_estudios";

            SqlDataReader readerGradosEstudios = conexion.Consultar(selectGradosEstudios, parametros);

            List<CatalogoRegistro.GradoEstudios> ldocGradoEstudios = new List<CatalogoRegistro.GradoEstudios>();

            while (readerGradosEstudios.Read())
            {
                CatalogoRegistro.GradoEstudios docGradoEstudio = new CatalogoRegistro.GradoEstudios();
                docGradoEstudio.value = readerGradosEstudios["value"].ToString();
                docGradoEstudio.key = Convert.ToInt32(readerGradosEstudios["clave"].ToString());

                ldocGradoEstudios.Add(docGradoEstudio);
            }

            cat.listaGradoEstudios = ldocGradoEstudios;


            // get estados
            string selectEstados = " select clv_edo as clave, nombre as value from estado";

            SqlDataReader readerEstados = conexion.Consultar(selectEstados, parametros);

            List<CatalogoRegistro.Estados> ldocEstados = new List<CatalogoRegistro.Estados>();

            while (readerEstados.Read())
            {
                CatalogoRegistro.Estados docEstado = new CatalogoRegistro.Estados();
                docEstado.value = readerEstados["value"].ToString();
                docEstado.key = Convert.ToInt32(readerEstados["clave"].ToString());

                ldocEstados.Add(docEstado);
            }

            cat.listaEstados = ldocEstados;





            // get regiones
            string selectRegion = " select region as clave, nombre as value from region";

            SqlDataReader readerRegion = conexion.Consultar(selectRegion, parametros);

            List<CatalogoRegistro.Region> ldocregion = new List<CatalogoRegistro.Region>();

            while (readerRegion.Read())
            {
                CatalogoRegistro.Region docRegion = new CatalogoRegistro.Region();
                docRegion.value = readerRegion["value"].ToString();
                docRegion.key = Convert.ToInt32(readerRegion["clave"].ToString());

                ldocregion.Add(docRegion);
            }

            cat.listaRegiones = ldocregion;


            // get puestos
            string selectPuestos = "select clv_puesto as clave, puesto as value from cat_puesto where app_funcion=1";

            SqlDataReader readerPuestos = conexion.Consultar(selectPuestos, parametros);

            List<CatalogoRegistro.Puesto> ldocPuestos = new List<CatalogoRegistro.Puesto>();

            while (readerPuestos.Read())
            {
                CatalogoRegistro.Puesto docPuesto = new CatalogoRegistro.Puesto();
                docPuesto.value = readerPuestos["value"].ToString();
                docPuesto.key = Convert.ToInt32(readerPuestos["clave"].ToString());

                ldocPuestos.Add(docPuesto);
            }

            cat.listaPuestos = ldocPuestos;




            conexion.Cerrar();
            return cat;

        }

        public CatalogoRegistro getMunicipios(int idEstado)
        {
            CatalogoRegistro cat = new CatalogoRegistro();
            // get municipios

            //if (idEstado != 0)
            //{
            string selectMunicipip = "  select clv_mun as clave, nombre as value from municipio where clv_edo=@estado";
            List<Parametro> parametros = new List<Parametro>();

            Parametro paramIdEstado = new Parametro();
            paramIdEstado.Nombre = "@estado";
            paramIdEstado.Valor = idEstado.ToString();
            parametros.Add(paramIdEstado);
            SqlDataReader readerMunicipios = conexion.Consultar(selectMunicipip, parametros);

            List<CatalogoRegistro.Municipio> ldocmunicipios = new List<CatalogoRegistro.Municipio>();

            while (readerMunicipios.Read())
            {
                CatalogoRegistro.Municipio docMunicipio = new CatalogoRegistro.Municipio();
                docMunicipio.value = readerMunicipios["value"].ToString();
                docMunicipio.key = Convert.ToInt32(readerMunicipios["clave"].ToString());

                ldocmunicipios.Add(docMunicipio);
            }

            cat.listaMunicipios = ldocmunicipios;
            // }



            conexion.Cerrar();
            return cat;

        }
    }   
}
