/// <summary>
/// Archivo para la clase de metodos de extension de utilidades comunes
/// </summary>
/// <remarks> 
/// Autor: Diego Madrid
/// </remarks>
namespace Proyecto.IC.Utilidades.Auxiliares.Comunes
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Proyecto.IC.Enumeraciones;
    using Proyecto.IC.Recursos;
    using Proyecto.IC.Utilidades.Auxiliares.Constantes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Clase de metodos de extension de utilidades comunes
    /// </summary>
    public static class ExtensionesBasicas
    {
      

        #region "VALIDACION"

        /// <summary>
        /// Indica si el objeto es nulo
        /// </summary>
        /// <param name="objeto">Objeto a evaluar</param>
        /// <returns>Un booleano que indica si el objeto es nulo</returns>
        public static bool EsNulo(this object objeto)
        {
            return objeto == null || objeto is System.DBNull;
        }

        /// <summary>
        /// Indica si la cadena es nula
        /// </summary>
        /// <param name="cadena">Cadena a evaluar</param>
        /// <returns>Un booleano que indica si la cadena es nula</returns>
        public static bool EsNuloOVacio(this string cadena)
        {
            return string.IsNullOrEmpty(cadena);
        }
 

        /// <summary>
        /// Indica si la lista es nula o no tiene elementos
        /// </summary>
        /// <param name="guid">Lista a evaluar</param>
        /// <returns>Un booleano que indica si la lista es nulo o no tiene elementos</returns>
        public static bool EsNuloOVacio<T>(this IEnumerable<T> lista)
        {
            return lista.EsNulo() || !lista.Any();
        }

       
     
        #endregion "VALIDACION"

        #region "COMUNES"

 
 
        /// <summary>
        /// Serializa Objeto a Json
        /// </summary>
        /// <param name="objeto"></param>
        /// <returns>String con el Json del Objeto</returns>
        public static string SerializarAJson(object objeto)
        {
            return JsonConvert.SerializeObject(objeto);
        }

        /// <summary>
        /// Deserializa de Json a un Objeto
        /// </summary>
        /// <typeparam name="T">Tipo de Objeto</typeparam>
        /// <param name="json">Strin con el Json</param>
        /// <returns>Objeto de tipo T</returns>
 

        /// <summary>
        /// Obtiene los detalles de la excepcion
        /// </summary>
        /// <param name="excepcion">Excepcion generada</param>
        /// <param name="mostrarTraza">Indicador para mostrar la traza</param>
        /// <returns>Una cadena con los detalles de la excepcion</returns>
        public static string ObtenerDetallesExcepcion(this Exception excepcion, bool mostrarTraza)
        {
            string mensajeError = string.Empty;

            if (!excepcion.EsNulo())
            {
                mensajeError = string.Concat(rcsUtilitarios.Mensaje, excepcion.Message);

                if (mostrarTraza)
                {
                    mensajeError += string.Concat(ConstantesComunes.NUEVALINEA, ObtenerInformacionTraza(excepcion));
                }

                if (!excepcion.InnerException.EsNulo())
                {
                    mensajeError += string.Concat(rcsUtilitarios.InnerException, excepcion.InnerException.ObtenerDetallesExcepcion(mostrarTraza));
                }
            }
            else
            {
                mensajeError = string.Empty;
            }

            return mensajeError;
        }

        /// <summary>
        /// Obtiene la informacion de la traza
        /// </summary>
        /// <param name="excepcion">Excepcion generada</param>
        /// <returns>Informacion de la traza</returns>
        private static string ObtenerInformacionTraza(Exception excepcion)
        {
            return string.Concat(rcsUtilitarios.StackTrace, (excepcion.StackTrace.EsNuloOVacio() ? ConstantesComunes.ESPACIOBLANCO : excepcion.StackTrace.ToString()));
        }
 

        #endregion "COMUNES"
  

        /// <summary>
        /// Valida la informacion de la expresion lambda
        /// </summary>
        /// <typeparam name="T">Tipo generico</typeparam>
        /// <typeparam name="TProperty">Propiedad generica</typeparam>
        /// <param name="expression">Expresion a evaluar</param>
        /// <returns>Una expresion</returns>
        private static MemberExpression ObtenerMiembroExpresion<T, TProperty>(Expression<Func<T, TProperty>> expression)
        {
            LambdaExpression lambda = expression as LambdaExpression;
            MemberExpression memberExpression;

            if (lambda.Body is UnaryExpression)
            {
                UnaryExpression unaryExpression = lambda.Body as UnaryExpression;
                memberExpression = unaryExpression.Operand as MemberExpression;
            }
            else
            {
                memberExpression = lambda.Body as MemberExpression;
            }

            return memberExpression;
        }

    

      
    }
}