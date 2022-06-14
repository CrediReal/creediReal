using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.Utilitarios
{
    /// <summary>
    /// Enumeración que contiene los ids de los tipos de ApplicationDataUtilException que puede lanzar
    /// </summary>
    public enum AplicationDataUtilExceptionIds
    {
        /// <summary>
        /// Cuando el número de ítems deserializados supera el número correcto de elementos contenidos
        /// en una línea del archivo CSV
        /// </summary>
        DemasiadosElementosDeserializados = 1,
        /// <summary>
        /// Cuando el número de ítems deserializados es menor al número correcto de elementos contenidos
        /// en una línea del archivo CSV
        /// </summary>
        PocosElementosDeserializados,
        /// <summary>
        /// Cuando el valor de la llave, requerido por el algoritmo, es Nulo
        /// </summary>
        ValorDeLlaveNulo,
        /// <summary>
        /// Cuando el valor del password es nulo
        /// </summary>
        ValorDePasswordNulo,
        /// <summary>
        /// Cuando el valor del vector de inicialización, requerido por el algoritmo, es nulo
        /// </summary>
        ValorDeVectorNulo,
        /// <summary>
        /// Cuando el valor de la llave y del password son nulos
        /// </summary>
        ValorDePasswordLlaveNulos
    }

    /// <summary>
    /// Clase que engloba el conjunto de excepciones que puede generar la librería
    /// </summary>
    [Serializable]
    public class ApplicationDataUtilException : Exception
    {
        /// <summary>
        /// Obtiene o establece el id del ApplicationDataUtilException lanzado
        /// </summary>
        public AplicationDataUtilExceptionIds Id { get; set; }

        /// <summary>
        /// Obtiene o establece el arreglo de datos adicionales que se pueden agregar al detalle de la excepción
        /// </summary>
        public object[] Datos { get; set; }

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public ApplicationDataUtilException() { }

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="id">Tipo de excepción</param>
        /// <param name="message">Mensaje informativo de la excepción</param>
        /// <param name="datos">Datos adicionales de la excepción</param>
        public ApplicationDataUtilException(AplicationDataUtilExceptionIds id, string message, object[] datos) : this(id, message, datos, null) { }

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="id">Tipo de excepción</param>
        /// <param name="message">Mensaje informativo de la excepción</param>
        /// <param name="datos">Datos adicionales de la excepción</param>
        /// <param name="inner">Excepción base generada</param>
        public ApplicationDataUtilException(AplicationDataUtilExceptionIds id, string message, object[] datos, Exception inner)
            : base(message, inner)
        {
            Id = id;
            Datos = datos;
        }

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="info">SerializationInfo</param>
        /// <param name="context">StreamingContext</param>
        protected ApplicationDataUtilException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
