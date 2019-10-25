using AntadBiblioteca.DAO;
using AntadComun.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntadBiblioteca.Services
{
    public class LoginAppService
    {
        private LoginAppDAO loginAccesoDatos;

        public LoginAppService(string cadena)
        {
            loginAccesoDatos = new LoginAppDAO(cadena);
        }
        public UserSession LoginUsuario(string usuario, string password)
        {

            return loginAccesoDatos.LoginUsuario(usuario, text_to_ascii(password));
            //return loginAccesoDatos.ValidarUsuario(usuario, password);

        }

        /*public UserSession GetUser(string usuario, string password)
        {

            return loginAccesoDatos.GetUser(usuario, password);
            //return loginAccesoDatos.ValidarUsuario(usuario, password);

        }*/
        public static string text_to_ascii(string password)
        {
            //pasar cadena a arreglo de caracteres
            char[] arr = password.ToCharArray();
            //invertir el orden de los caracteres
            Array.Reverse(arr);

            string invertida = new string(arr);
            string caracter;
            string g = "";
            string a = "";
            string cad = "";
            string compuesta = "";

            //recorrer la cadena de carateres
            for (int i = 0; i < invertida.Length; i++)
            {
                caracter = invertida[i].ToString();
                //convertir a codigo ascii el caracter y concatenarlo en un string separado por comas
                g = Encoding.ASCII.GetBytes(caracter)[0].ToString();

                if (Convert.ToInt32(g) < 128)
                {

                    g = (Convert.ToInt32(g) + 128).ToString();

                }
                else
                {

                    g = (Convert.ToInt32(g) - 128).ToString();

                }

                cad = cad + g;
                cad = cad + ',';

            }
            //eliminar la ultima coma de la cadena
            compuesta = cad.Substring(0, cad.Length - 1);
            //Console.WriteLine(cad);
            //Console.WriteLine(compuesta);
            a = ascii_to_text(compuesta);

            return a;
        }
        public static string ascii_to_text(string cad)
        {
            string myString = "";
            string[] valores;

            valores = cad.Split(',');
            //recorrer los valores separados por comas
            foreach (string item in valores)
            {
                //codificar caracteres a secuencia de bytes
                // byte[] bytes = Encoding.Default.GetBytes(item);
                myString = myString + (char)Convert.ToInt32(item);
            }
            Console.WriteLine(myString);
            return myString;

        }
    }
}
