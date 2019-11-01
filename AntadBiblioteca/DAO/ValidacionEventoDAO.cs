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
    public class ValidacionEventoDAO
    {
        public ConexionDB conexion;
        public ValidacionEventoDAO(string cadena)
        {
            conexion = new ConexionDB(cadena);
        }
        public ValidacionEvento getValidacionEvento(int idusuario, int idEvento, int idIntra)
        {
            Utilidades ser = new Utilidades();
            string urlServidor = ser.getUrlServidor(conexion);

            ValidacionEvento evento = new ValidacionEvento();
            evento.validacionFinal = true;

            string select = "select eu.id_usuario as idUsuario, u.foto as fotoUsuario, CONCAT(u.nombre, ' ', u.apellido_paterno, '', u.apellido_materno) as nombreUsuario," +
                " cs.id as idSucursal, cs.nombre as nombreSucursal, cs.foto_sucursal as fotoSucursal, cs.sexo as sexoRequisito, cs.mayor_edad as mayorEdadRequisito, u.sexo as sexoPromotor, " +
                "u.edad as edadPromotor, e.fecha as fechaEvento, e.hora_inicio as horaInicio, e.hora_fin as horaFin, cts.nombre as carta, cs.tipo_sucursal as idTipoSucursal, eu.id as idEvento " +
                "from evento_usuario eu left join evento e on e.id = eu.id_evento left join cat_sucursal cs on cs.id = e.id_sucursal " +
                "left join usuarios u on u.id = eu.id_usuario left join cat_tipo_sucursal cts on cts.id = cs.tipo_sucursal where eu.id_usuario =@usuario and eu.id_evento = @evento";

            List<Parametro> parametros = new List<Parametro>();

            Parametro paramIdUsuario = new Parametro();
            paramIdUsuario.Nombre = "@usuario";
            paramIdUsuario.Valor = idusuario.ToString();
            parametros.Add(paramIdUsuario);

            Parametro paramIdEevento = new Parametro();
            paramIdEevento.Nombre = "@evento";
            paramIdEevento.Valor = idEvento.ToString();
            parametros.Add(paramIdEevento);

            Parametro paramIntra = new Parametro();
            paramIntra.Nombre = "@intra";
            paramIntra.Valor = idIntra.ToString();
            parametros.Add(paramIntra);

            SqlDataReader readerSuc = conexion.Consultar(select, parametros);



            if (readerSuc.Read())
            {
                evento.idUsuario = Convert.ToInt32(readerSuc["idUsuario"].ToString());
                evento.fotoUsuario = urlServidor + readerSuc["fotoUsuario"].ToString();
                evento.nombreUsuario = readerSuc["nombreUsuario"].ToString();
                evento.idSucursal = Convert.ToInt32(readerSuc["idSucursal"].ToString());
                evento.idEvento = Convert.ToInt32(readerSuc["idEvento"].ToString());
                evento.nombreSucursal = readerSuc["nombreSucursal"].ToString();
                evento.fotoSucursal = urlServidor + readerSuc["fotoSucursal"].ToString();
                evento.sexoRequisito = readerSuc["sexoRequisito"].ToString();
                string may = readerSuc["mayorEdadRequisito"].ToString();
                if (readerSuc["mayorEdadRequisito"].ToString() == "True")
                {
                    evento.mayorEdadRequisito = "Mayor de edad";
                }
                else
                {
                    evento.mayorEdadRequisito = "Menor de edad";
                }
                evento.sexoPromotor = readerSuc["sexoPromotor"].ToString();
                evento.edadPromotor = Convert.ToInt32(readerSuc["edadPromotor"].ToString());
                evento.fechaEvento = readerSuc["fechaEvento"].ToString();
                evento.horaInicio = readerSuc["horaInicio"].ToString();
                evento.horaFin = readerSuc["horaFin"].ToString();
                evento.idTipoSucursal = Convert.ToInt32(readerSuc["idTipoSucursal"].ToString());
                string car = readerSuc["carta"].ToString();
                if (readerSuc["carta"].ToString() == "especial") { evento.cartaLaboral = true; } else { evento.cartaLaboral = false; }
                //evento.cartaLaboral = Convert.ToBoolean(readerSuc["cartaLaboral"]);

            }




            // documentos de sucursal
            string selectd = "select cd.id as idDocumento, cd.nombre as nombreDocumento " +
                "from evento e left join cat_sucursal cs on cs.id=e.id_sucursal left join cat_empresa ce on ce.id = cs.id_empresa " +
                "left join empresa_requisitos er on er.id_empresa = ce.id left join cat_documentos cd on cd.id = er.id_documento " +
                "where e.id = @evento";

            SqlDataReader readerDocSuc = conexion.Consultar(selectd, parametros);

            List<ValidacionEvento.DocumentoSucursal> ldocSuc = new List<ValidacionEvento.DocumentoSucursal>();

            while (readerDocSuc.Read())
            {
                ValidacionEvento.DocumentoSucursal docSuc = new ValidacionEvento.DocumentoSucursal();
                docSuc.idDocumento = Convert.ToInt32(readerDocSuc["idDocumento"].ToString());
                docSuc.nombreDocumento = readerDocSuc["nombreDocumento"].ToString();
                ldocSuc.Add(docSuc);
            }
            evento.documentosucursal = ldocSuc;

            // documentos promotor
            string selectp = "select ud.id_documento as idDocumento, cd.nombre as nombreDocumento,ud.documento as urlDocumento" +
                " from usuarios u left join usuario_documentos ud on ud.id_usuario=u.id left join cat_documentos cd on cd.id = ud.id_documento " +
                "where u.id = @usuario";

            SqlDataReader readerDocPro = conexion.Consultar(selectp, parametros);

            List<ValidacionEvento.DocumentoPromotor> ldocPro = new List<ValidacionEvento.DocumentoPromotor>();

            while (readerDocPro.Read())
            {
                ValidacionEvento.DocumentoPromotor docPro = new ValidacionEvento.DocumentoPromotor();
                docPro.idDocumento = Convert.ToInt32(readerDocPro["idDocumento"].ToString());
                docPro.nombreDocumento = readerDocPro["nombreDocumento"].ToString();
                docPro.urlDocumento = readerDocPro["urlDocumento"].ToString();
                ldocPro.Add(docPro);
            }
            evento.documentospromotor = ldocPro;


            //validacion de documentos
            //total de docuemntos de la sucursal
            string selectTotal = "select count(*) as Totals from evento e left join cat_sucursal cs on cs.id = e.id_sucursal left join cat_empresa ce on ce.id = cs.id_empresa " +
                "left join empresa_requisitos er on er.id_empresa = ce.id left join cat_documentos cd on cd.id = er.id_documento where e.id = @evento ";


            SqlDataReader readerToSuc = conexion.Consultar(selectTotal, parametros);
            int totalDoc;
            if (readerToSuc.Read())
            {

                totalDoc = Convert.ToInt32(readerToSuc["Totals"].ToString());

            }
            else
            {
                totalDoc = 0;
            }

            //total de cruce doc suscursal y docs promotor
            string selectTotalC = "select count(*) AS Totals from (select cd.id as idDocumento, cd.nombre as nombreDocumento from evento e left join " +
                "cat_sucursal cs on cs.id=e.id_sucursal left join cat_empresa ce on ce.id = cs.id_empresa left join empresa_requisitos er on er.id_empresa = ce.id " +
                "left join cat_documentos cd on cd.id = er.id_documento where e.id = @evento) as uno inner join " +
                "(select ud.id_documento as idDocumento, cd.nombre as nombreDocumento,ud.documento as urlDocumento from usuarios u " +
                "left join usuario_documentos ud on ud.id_usuario = u.id left join cat_documentos cd on cd.id = ud.id_documento where u.id = @usuario)as dos " +
                "on uno.idDocumento = dos.idDocumento ";


            SqlDataReader readerToSucC = conexion.Consultar(selectTotalC, parametros);
            int totalDocC;
            if (readerToSucC.Read())
            {

                totalDocC = Convert.ToInt32(readerToSucC["Totals"].ToString());

            }
            else
            {
                totalDocC = 0;
            }

            if (totalDocC.Equals(totalDoc))
            {
                evento.validacionDocumentos = true;
                evento.mensajeValidacionDocumentos = "Ningun documento faltante";
            }
            else
            {
                evento.validacionDocumentos = false;
                evento.mensajeValidacionDocumentos = "Faltan documentos";
                evento.validacionFinal = false;
            }


            //validacion carta laboral.

            if (evento.idTipoSucursal.Equals(2))
            {
                //verificar si promotor tiene carta laboral
                string verificarCarta = "select * from usuario_carta_laboral ucl left join carta_laboral cl on cl.id = ucl.id_carta " +
                    "left join cat_empresa ce on ce.id = cl.id_empresa  where ucl.id_usuario = @usuario and ucl.activo = 1 and ce.id = " +
                    "(select ce.id from evento e left join cat_sucursal cs on e.id_sucursal = cs.id left join cat_empresa ce on ce.id = cs.id_empresa where e.id = @evento) " +
                    "and cl.vigencia_final >= getdate() and cl.vigencia_inicial <= getdate()";

                SqlDataReader readerVerCarta = conexion.Consultar(verificarCarta, parametros);

                if (readerVerCarta.Read())
                {

                    evento.validacionCartaLaboral = true;
                    evento.mensajeValidacionCartaLaboral = "Carta laboral valida";
                }
                else
                {
                    evento.validacionCartaLaboral = false;
                    evento.mensajeValidacionCartaLaboral = "Tu carta laboral no es valida";
                    evento.validacionFinal = false;
                }

            }
            else
            {
                evento.validacionCartaLaboral = true;
                evento.mensajeValidacionCartaLaboral = "La sucursal no requiere carta laboral";
            }


            //VALIDACION PERSONAL
            string mensajeGenero;
            string mensajeEdad;
            evento.validacionPersonal = true;
            if (!evento.sexoRequisito.Equals(evento.sexoPromotor))
            {

                evento.validacionPersonal = false;
                mensajeGenero = "Promotor no cumple con el genero solicitado";
                evento.validacionFinal = false;
            }
            else
            {
                mensajeGenero = "Promotor si cumple con el genero solicitado";
            }

            if (evento.mayorEdadRequisito.Equals("Mayor de edad"))
            {
                if (evento.edadPromotor < 18)
                {
                    evento.validacionPersonal = false;
                    evento.validacionFinal = false;
                    mensajeEdad = "promotor no cumple con la edad solicitada";
                }
                else
                {
                    mensajeEdad = "promotor si cumple con la edad solicitada";
                }
            }
            else
            {
                if (evento.edadPromotor >= 18)
                {
                    evento.validacionPersonal = false;
                    evento.validacionFinal = false;
                    mensajeEdad = "promotor no cumple con la edad solicitada";
                }
                else
                {
                    mensajeEdad = "promotor si cumple con la edad solicitada";
                }
            }

            evento.mensajeValidacionPersonal = mensajeGenero + " y " + mensajeEdad;


            ///VALIDACION DEL HORARIO
            DateTime fechaF = Convert.ToDateTime(evento.fechaEvento).Date;
            DateTime FechAc = DateTime.Now.Date;
            if (fechaF == FechAc) // Si la fecha indicada es menor o igual a la fecha actual
            {
                evento.validacionHorario = true;
                evento.mensajeValidacionHorario = "La fecha del evento si coincide con la fecha de hoy";
            }
            else
            {
                evento.validacionHorario = false;
                evento.mensajeValidacionHorario = "La fecha del evento no coincide con la fecha de hoy";
                evento.validacionFinal = false;
            }

            //validacion de ubicacion

            string veri = "select id_sucursal as idSucursal from sucursal_usuario where id_usuario= @intra";

            SqlDataReader readerVeri = conexion.Consultar(veri, parametros);

            if (readerVeri.Read())
            {
                int rtrt = Convert.ToInt32(readerVeri["idSucursal"].ToString());
                if (rtrt == evento.idSucursal)
                {
                    evento.validacionUbicacion = true;
                    evento.mensajeValidacionUbicacion = "La sucursal de tu evento coincide con la sucursal en la que te encuentras";
                }
                else
                {
                    evento.validacionUbicacion = false;
                    evento.mensajeValidacionUbicacion = "La sucursal de tu evento no coincide con la sucursal en la que te encuentras";
                    evento.validacionFinal = false;
                }


            }
            else
            {

            }

            conexion.Cerrar();
            return evento;

        }
    }
}
