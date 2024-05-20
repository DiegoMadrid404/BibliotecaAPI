/// <summary>
/// Archivo para la clase de Utilitarios de ejecucion
/// </summary>
/// <remarks>
/// Autor: Diego Madrid
/// </remarks>
namespace Proyecto.IC.Utilidades.Auxiliares.Ejecucion
{
    using Proyecto.IC.Utilidades.Auxiliares.Comunes;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Clase de Utilitarios de ejecucion
    /// </summary>
    public static class UtilitariosEjecucion
    {
        #region "METODOS PUBLICOS"

        /// <summary>
        /// Envoltorio para la ejecucion de una funcion
        /// </summary>
        /// <typeparam name="T">Tipo de dato</typeparam>
        /// <typeparam name="C">Clase que esta ejecutando el envoltorio</typeparam>
        /// <param name="cuerpoEjecutar">Cuerpo a ejecutar</param>
        /// <param name="hacerEnError">Hacer en error</param>
        /// <param name="hacerEnFinalizacion">Hacer en finalizacion</param>
        /// <returns>Una funcion</returns>
        public static T EjecutarTransaccion<T, C>(Func<T> cuerpoEjecutar, Func<Exception, T> hacerEnError, Func<T, T> hacerEnFinalizacion) where C : class
        {
            T retorno = default(T);

            try
            {
                retorno = cuerpoEjecutar();
            }
            catch (Exception ex)
            {
                if (!hacerEnError.EsNulo())
                {
                    retorno = hacerEnError(ex);
                }
                else
                {
                    throw ex;
                }
            }
            finally
            {
                if (!hacerEnFinalizacion.EsNulo())
                {
                    retorno = hacerEnFinalizacion(retorno);
                }
            }

            return retorno;
        }

        /// <summary>
        /// Envoltorio para la ejecucion de una funcion
        /// </summary>
        /// <typeparam name="T">Tipo de dato</typeparam>
        /// <typeparam name="C">Clase que esta ejecutando el envoltorio</typeparam>
        /// <param name="cuerpoEjecutar">Cuerpo a ejecutar</param>
        /// <param name="hacerEnError">Hacer en error</param>
        /// <returns>Una funcion</returns>
        public static T EjecutarTransaccion<T, C>(Func<T> cuerpoEjecutar, Func<Exception, T> hacerEnError) where C : class
        {
            return EjecutarTransaccion<T, C>(cuerpoEjecutar, hacerEnError, null);
        }

        /// <summary>
        /// Envoltorio para la ejecucion de una funcion
        /// </summary>
        /// <typeparam name="T">Tipo de dato</typeparam>
        /// <typeparam name="C">Clase que esta ejecutando el envoltorio</typeparam>
        /// <param name="cuerpoEjecutar">Cuerpo a ejecutar</param>
        /// <returns>Una funcion</returns>
        public static T EjecutarTransaccion<T, C>(Func<T> cuerpoEjecutar) where C : class
        {
            return EjecutarTransaccion<T, C>(cuerpoEjecutar, null, null);
        }

        /// <summary>
        /// Envoltorio para la ejecucion de un metodo
        /// </summary>
        /// <typeparam name="C">Clase que esta ejecutando el envoltorio</typeparam>
        /// <param name="cuerpoEjecutar">Cuerpo a ejecutar</param>
        public static void EjecutarTransaccion<C>(Action cuerpoEjecutar) where C : class
        {
            bool nada = EjecutarTransaccion<bool, C>(() =>
             {
                 cuerpoEjecutar();
                 return false;
             });
        }

        /// <summary>
        /// Envoltorio para la ejecucion de un metodo asincrono
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <param name="cuerpoEjecutar"></param>
        /// <param name="hacerEnError"></param>
        /// <param name="hacerEnFinalizacion"></param>
        /// <returns></returns>
            public static async Task<T> EjecutarAsync<T, C>(Func<Task<T>> cuerpoEjecutar, Func<Exception, Task<T>> hacerEnError, Func<T, Task<T>> hacerEnFinalizacion) where C : class
            {
                T retorno = default(T);

                try
                {
                    retorno = await cuerpoEjecutar();
                }
                catch (Exception ex)
                {
                    if (!hacerEnError.EsNulo())
                    {
                        retorno = await hacerEnError(ex);
                    }
                    else
                    {
                        throw ex;
                    }
                }
                finally
                {
                    if (!hacerEnFinalizacion.EsNulo())
                    {
                        retorno = await hacerEnFinalizacion(retorno);
                    }
                }

                return retorno;
        }

        /// <summary>
        /// Ejecución de un metodo async
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <param name="cuerpoEjecutar"></param>
        /// <param name="hacerEnError"></param>
        /// <returns></returns>
        public static async Task<T> EjecutarAsync<T, C>(Func<Task<T>> cuerpoEjecutar, Func<Exception, Task<T>> hacerEnError) where C : class
        {
            return await EjecutarAsync<T, C>(cuerpoEjecutar, hacerEnError, null);
        }

        /// <summary>
        /// Ejecución de un metodo async
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <param name="cuerpoEjecutar"></param>
        /// <returns></returns>
        public static async Task<T> EjecutarAsync<T, C>(Func<Task<T>> cuerpoEjecutar) where C : class
        {
            return await EjecutarAsync<T, C>(cuerpoEjecutar, null, null);
        }

        #endregion "METODOS PUBLICOS"
    }
}