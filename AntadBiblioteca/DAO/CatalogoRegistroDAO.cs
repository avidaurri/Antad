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
        public CatalogoRegistro getCatalogo(int idEstado)
        {
            CatalogoRegistro cat = new CatalogoRegistro();
            // get bancos
            string selectBancos = "select clv_banco as clave, descripcion as value from banco";
            List<Parametro> parametros = new List<Parametro>();

            Parametro paramIdEstado = new Parametro();
            paramIdEstado.Nombre = "@estado";
            paramIdEstado.Valor = idEstado.ToString();
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

            while (readerBancos.Read())
            {
                CatalogoRegistro.EstadoCivil docEdoCivil = new CatalogoRegistro.EstadoCivil();
                docEdoCivil.value = readerBancos["value"].ToString();
                docEdoCivil.key = Convert.ToInt32(readerBancos["clave"].ToString());

                ldocEdoCivil.Add(docEdoCivil);
            }

            cat.listaEdoCivil = ldocEdoCivil;


            // get grados de estudios
            string selectGradosEstudios = "select clv_edo_civil as clave, descripcion as value from estado_civil";

            SqlDataReader readerGradosEstudios = conexion.Consultar(selectGradosEstudios, parametros);

            List<CatalogoRegistro.GradoEstudios> ldocGradoEstudios = new List<CatalogoRegistro.GradoEstudios>();

            while (readerGradosEstudios.Read())
            {
                CatalogoRegistro.GradoEstudios docGradoEstudio = new CatalogoRegistro.GradoEstudios();
                docGradoEstudio.value = readerBancos["value"].ToString();
                docGradoEstudio.key = Convert.ToInt32(readerBancos["clave"].ToString());

                ldocGradoEstudios.Add(docGradoEstudio);
            }

            cat.listaGradoEstudios = ldocGradoEstudios;


            // get estados
            string selectEstados = "select clv_edo_civil as clave, descripcion as value from estado_civil";

            SqlDataReader readerEstados = conexion.Consultar(selectEstados, parametros);

            List<CatalogoRegistro.Estados> ldocEstados = new List<CatalogoRegistro.Estados>();

            while (readerEstados.Read())
            {
                CatalogoRegistro.Estados docEstado = new CatalogoRegistro.Estados();
                docEstado.value = readerBancos["value"].ToString();
                docEstado.key = Convert.ToInt32(readerBancos["clave"].ToString());

                ldocEstados.Add(docEstado);
            }

            cat.listaEstados = ldocEstados;


            // get municipios

            if (idEstado != 0)
            {
                string selectMunicipip = "  select clv_mun as clave, nombre as value from municipio where clv_edo=@estado";

                SqlDataReader readerMunicipios = conexion.Consultar(selectMunicipip, parametros);

                List<CatalogoRegistro.Estados> ldocmunicipios = new List<CatalogoRegistro.Estados>();

                while (readerMunicipios.Read())
                {
                    CatalogoRegistro.Estados docMunicipio = new CatalogoRegistro.Estados();
                    docMunicipio.value = readerBancos["value"].ToString();
                    docMunicipio.key = Convert.ToInt32(readerBancos["clave"].ToString());

                    ldocmunicipios.Add(docMunicipio);
                }

                cat.listaMunicipios = ldocmunicipios;
            }






            conexion.Cerrar();
            return cat;

        }
    }   
}
