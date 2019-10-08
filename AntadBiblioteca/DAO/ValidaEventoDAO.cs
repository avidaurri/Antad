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
    public class ValidaEventoDAO
    {
        public ConexionDB conexion;
        public ValidaEventoDAO(string cadena)
        {
            conexion = new ConexionDB(cadena);
        }
        public ValidaEvento getValidacionEvento(string idusuario, string idEvento)
        {
            Utilidades ser = new Utilidades();
            string urlServidor = ser.getUrlServidor(conexion);

            ValidaEvento evento = new ValidaEvento();
            evento.validacionFinal = true;

            string select = "select ec.folio_evento as folioEvento, l.clv_emp as clv_Empleado, l.login as usuario, ct.logo_url as fotoSucursal, ct.folio_centro_trabajo as folioSucursal, " +
                "ct.nombre_comercial as nombreSucursal, ec.fecha_inicial as fechaInicio, ec.fecha_final as fechaFinal, ee.descripcion as estatusEvento, ec.clv_edo_evento as clvEstatusEvento, " +
                "l.reg_foto as fotoUsuario, (l.reg_nombre + ' ' + l.reg_apellido_paterno + ' ' + l.reg_apellido_materno) as nombreUsuario, SUBSTRING(l.reg_curp, 11, 1) as sexoUsuario, " +
                "DATEDIFF(year, CASE WHEN SUBSTRING(l.reg_curp, 5, 2) < 40 THEN '20' + SUBSTRING(l.reg_curp, 5, 2) ELSE '19' + SUBSTRING(l.reg_curp, 5, 2) end  " +
                "+ '-' + SUBSTRING(l.reg_curp, 7, 2) + '-' + SUBSTRING(l.reg_curp, 9, 2), getdate()) as edadUsuario, " +
                "(select CASE WHEN(select clv_empre from emp_requisitos_evento where clv_emp = l.clv_emp and clv_requisito_evento = 6)> 1 THEN 'True' ELSE 'False' end) as imssUsuario " +
                "from evento_cara ec left join login l on l.clv_emp = ec.clv_emp left join cat_tip_evento cte on cte.clv_tip_evento = ec.clv_tip_evento " +
                "left join edo_evento ee on ee.clv_edo_evento = ec.clv_edo_evento left join centro_trabajo ct on ct.folio_centro_trabajo = ec.folio_centro_trabajo left join " +
                "cadena_centro_trabajo cct on cct.clv_cadena = ct.clv_cadena where l.login = @usuario and ec.folio_evento = @evento";

            List<Parametro> parametros = new List<Parametro>();

            Parametro paramIdUsuario = new Parametro();
            paramIdUsuario.Nombre = "@usuario";
            paramIdUsuario.Valor = idusuario;
            parametros.Add(paramIdUsuario);

            Parametro paramIdEevento = new Parametro();
            paramIdEevento.Nombre = "@evento";
            paramIdEevento.Valor = idEvento;
            parametros.Add(paramIdEevento);

            SqlDataReader readerSuc = conexion.Consultar(select, parametros);



            if (readerSuc.Read())
            {
                evento.folioEvento = readerSuc["folioEvento"].ToString();
                evento.clv_Empleado = Convert.ToInt32(readerSuc["clv_Empleado"].ToString());
                evento.usuario = readerSuc["usuario"].ToString();
                evento.fotoSucursal = readerSuc["fotoSucursal"].ToString();
                evento.folioSucursal = readerSuc["folioSucursal"].ToString();
                evento.nombreSucursal = readerSuc["nombreSucursal"].ToString();
                evento.fechaInicio = readerSuc["fechaInicio"].ToString();
                evento.fechaFinal = readerSuc["fechaFinal"].ToString();
                evento.estatusEvento = readerSuc["estatusEvento"].ToString();
                evento.clvEstatusEvento = Convert.ToInt32(readerSuc["clvEstatusEvento"].ToString());
                evento.fotoUsuario = readerSuc["fotoUsuario"].ToString();
                evento.nombreUsuario = readerSuc["nombreUsuario"].ToString();
                evento.sexoUsuario = readerSuc["sexoUsuario"].ToString();
                evento.edadUsuario = readerSuc["edadUsuario"].ToString();
                evento.imssUsuario = Convert.ToBoolean(readerSuc["imssUsuario"].ToString());

            }

            /*


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

            }*/

            conexion.Cerrar();
            return evento;

        }
    }
}
