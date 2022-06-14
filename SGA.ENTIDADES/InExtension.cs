using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGA.ENTIDADES
{
    /// <summary>
    /// Extension en el tipo de dato
    /// </summary>
    public static class InExtension
    {
        /// <summary>
        /// validacion de existencia de un valor en el conjunto de parametros
        /// </summary>
        /// <typeparam name="T">tipo de dato</typeparam>
        /// <param name="x">valor a buscar</param>
        /// <param name="args">valores a buscar</param>
        /// <returns>retorna verdadero o falso</returns>
        public static bool In<T>(this T x, params T[] args)
        {
            return args.Contains(x);
        }
    }
}
