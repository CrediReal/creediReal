using System;
using System.Text;
using System.Web;

namespace SGA.Utilitarios
{
    /// <summary>
    /// Centraliza el manejo de errores
    /// </summary>
    public class clsManejoErrores
    {
        public enum IdEvento
        {
            ErrorInesperado = 0
        }

        public static int Procesar(Exception ex)
        {
            return ManejarExcepcionGeneral(ex);
        }


        private static int ManejarExcepcionGeneral(Exception ex)
        {
            clsManejoErrores mensaje = new clsManejoErrores();

            //LoggingUtil.AgregarInformacionException(mensaje, ex);

            //return LoggingUtil.RegistrarEvento(TraceEventType.Error
            //         , (int)IdEvento.ErrorInesperado, mensaje);
            return 1;
        }
    }
}
