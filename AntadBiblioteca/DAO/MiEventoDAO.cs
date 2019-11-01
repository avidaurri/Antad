using AntadBiblioteca.Util;
using ModelsNet.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntadBiblioteca.DAO
{
    public class MiEventoDAO
    {
        public ConexionDB conexion;
        public MiEventoDAO(string cadena)
        {
            conexion = new ConexionDB(cadena);
        }
        public List<MiEvento> getSucursalOperacion()
        {
            Utilidades ser = new Utilidades();
            string urlServidor = ser.getUrlServidor(conexion);


            string select = "select e.id as idEvento, e.fecha as fecha, cs.foto_sucursal as fotoSucursal " +
                "from evento e left join cat_sucursal cs on cs.id=e.id_sucursal";

            List<Parametro> parametros = new List<Parametro>();

            Parametro paramIdUsuario = new Parametro();
            paramIdUsuario.Nombre = "@usuario";
            paramIdUsuario.Valor = "sdsdsd";
            parametros.Add(paramIdUsuario);

            SqlDataReader readerSuc = conexion.Consultar(select, parametros);

            List<MiEvento> sucursales = new List<MiEvento>();

            while (readerSuc.Read())
            {
                MiEvento sucursal = new MiEvento();
                sucursal.idEvento = Convert.ToInt32(readerSuc["idEvento"].ToString());
                sucursal.fecha = readerSuc["fecha"].ToString();
                sucursal.fotoSucursal = urlServidor + readerSuc["fotoSucursal"].ToString();
                sucursales.Add(sucursal);
                //string km = (distancia / 1000).ToString();
            }

            conexion.Cerrar();
            return sucursales;


        }
    }
}
