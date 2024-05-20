/// <summary>
/// Archivo para la clase de acceso a las entidades de la base de datos
/// </summary>
/// <remarks>
/// Autor: Diego Madrid
/// </remarks>
namespace Proyecto.Datos.Utilidades
{
    using IC.Recursos;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading.Tasks; 

    /// <summary>
    /// Clase de acceso a las entidades de la base de datos
    /// </summary>
    /// <typeparam name="TDBContexto">Contexto de la base de datos</typeparam>
    public abstract class AccesoComunDAL<TDBContexto> where TDBContexto : DbContext, new()
    {
        public Func<TDBContexto> FabricaContexto = new Func<TDBContexto>(() => { return new TDBContexto(); });

        public TDBContexto ContextoBD;

        public AccesoComunDAL()
        {
            ContextoBD = FabricaContexto.Invoke();
        }

        /// <summary>
        /// Envoltorio para la ejecucion de una funcion
        /// </summary>
        /// <typeparam name="T">Tipo de dato de la funcion</typeparam>
        /// <typeparam name="C">Clase que esta ejecutando el envoltorio</typeparam>
        /// <param name="cuerpoEjecutar">Cuerpo a ejecutar</param>
        /// <returns>Una funcion tipo T</returns>
        public T EjecutarTransaccion<T, C>(Func<T> cuerpoEjecutar) where C : class
        {
            T retorno = default(T);
            try
            {
                using (ContextoBD = FabricaContexto.Invoke())
                {
                    retorno = cuerpoEjecutar();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new ExcepcionControladaException(string.Empty, rcsUtilitariosTransaccional.MensajeDbUpdateConcurrencyException, false);
            }
            catch (Exception)
            {
                throw;
            }

            return retorno;
        }

        /// <summary>
        /// Envoltorio asincrono para la ejecucion de una funcion
        /// </summary>
        /// <typeparam name="T">Tipo de dato de la funcion</typeparam>
        /// <typeparam name="C">Clase que esta ejecutando el envoltorio</typeparam>
        /// <param name="cuerpoEjecutar">Cuerpo a ejecutar</param>
        /// <returns>Una funcion tipo T</returns>
        public async Task<T> EjecutarTransaccionAsync<T, C>(Func<Task<T>> cuerpoEjecutar) where C : class
        {
            T retorno = default(T);
            try
            {
                using (ContextoBD = FabricaContexto.Invoke())
                {
                    retorno = await cuerpoEjecutar();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new ExcepcionControladaException(string.Empty, rcsUtilitariosTransaccional.MensajeDbUpdateConcurrencyException, false);
            }
            catch (Exception)
            {
                throw;
            }

            return retorno;
        }
    }
}