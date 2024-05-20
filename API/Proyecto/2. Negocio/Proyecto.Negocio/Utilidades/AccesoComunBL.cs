// *********************************************************************** Assembly :
// Utilitarios.TransaccionalNet Author : Diego Madrid Created : 11-01-2017
//
// Last Modified By : Diego Madrid Last Modified On : 05-05-2021
// <copyright file="AccesoComunBL.cs" company="Utilitarios.TransaccionalNet">
//     Copyright (c) Diego Madrid. All rights reserved.
// </copyright>
// <summary>
// Archivo para la clase de acceso a la base de datos y transaccionalidad
// </summary>
// ***********************************************************************
namespace Proyecto.Negocio.Utilidades
{
    using Microsoft.EntityFrameworkCore;
    using Proyecto.IC.Utilidades.Auxiliares.Comunes;
    using Proyecto.IC.Utilidades.Auxiliares.Ejecucion;
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using System.Transactions;

    /// <summary>
    /// Clase AccesoComunBL.
    /// </summary>
    public abstract class AccesoComunBL
    {
        #region "MIEMBROS"

        /// <summary>
        /// Ejecutar transaccion bd.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <param name="nivelAislamiento">The nivel aislamiento.</param>
        /// <param name="cuerpoEjecutar">The cuerpo ejecutar.</param>
        /// <param name="hacerEnErrorDeIntegridad">
        /// Cuerpo a ejecutar cuando se encuentra unerror de integridad de datos
        /// </param>
        /// <param name="generaExcepcion">if set to <c>true</c> [genera excepcion].</param>
        /// <returns>T.</returns>
        /// <exception cref="Exception"></exception>
        public T EjecutarTransaccionBD<T, C>(IsolationLevel nivelAislamiento, Func<T> cuerpoEjecutar, Func<T> hacerEnErrorDeIntegridad = null, bool generaExcepcion = true) where C : class
        {
            T retorno = default(T);

            using (TransactionScope transaccion = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = nivelAislamiento }))
            {
                try
                {
                    if (generaExcepcion == true)
                    {
                        retorno = UtilitariosEjecucion.EjecutarTransaccion<T, C>(() =>
                        {
                            return cuerpoEjecutar();
                        }, null, null);
                    }
                    else
                    {
                        retorno = cuerpoEjecutar();
                    }

                    transaccion.Complete();
                }
                catch (DbUpdateException ex)
                {
                    if (!hacerEnErrorDeIntegridad.EsNulo())
                    {
                        retorno = UtilitariosEjecucion.EjecutarTransaccion<T, C>(() =>
                        {
                            return hacerEnErrorDeIntegridad();
                        }, null, null);
                        transaccion.Complete();
                    }
                    else
                    {
                        var mensajeExcepcion = new StringBuilder();
                        mensajeExcepcion.AppendLine($"DbUpdateException - {ex?.InnerException?.InnerException?.Message}");

                        foreach (var eve in ex.Entries)
                        {
                            mensajeExcepcion.AppendLine($"La Entidad {eve.Entity.GetType().Name} en estado {eve.State} no se puede afectar.");
                        }

                        throw new Exception(mensajeExcepcion.ToString());
                    }
                }
                finally
                {
                    transaccion.Dispose();
                }
            }

            return retorno;
        }

        /// <summary>
        /// Envoltorio asincrono para la administracion de transacciones hacia la base de datos.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <param name="nivelAislamiento">Nivel de aislamiento.</param>
        /// <param name="cuerpoEjecutar">Cuerpo a ejecutar.</param>
        /// <param name="hacerEnErrorDeIntegridad">
        /// Cuerpo a ejecutar cuando se encuentra un error de integridad de datos
        /// </param>
        /// <param name="generaExcepcion">Si se [genera excepcion].</param>
        /// <returns>Representa una transacción asíncrona</returns>
        /// <exception cref="Exception"></exception>
        public async Task<T> EjecutarTransaccionBDAsync<T, C>(System.Transactions.IsolationLevel nivelAislamiento, Func<Task<T>> cuerpoEjecutar, Func<Task<T>> hacerEnErrorDeIntegridad = null, bool generaExcepcion = true) where C : class
        {
            T retorno = default(T);

            using (TransactionScope transaccion = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = nivelAislamiento }, TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    if (generaExcepcion == true)
                    {
                        retorno = await UtilitariosEjecucion.EjecutarAsync<T, C>(async () =>
                       {
                           return await cuerpoEjecutar();
                       }, null, null);
                    }
                    else
                    {
                        retorno = await cuerpoEjecutar();
                    }

                    transaccion.Complete();
                }
                catch (DbUpdateException ex)
                {
                    if (!hacerEnErrorDeIntegridad.EsNulo())
                    {
                        retorno = await UtilitariosEjecucion.EjecutarAsync<T, C>(async () =>
                        {
                            return await hacerEnErrorDeIntegridad();
                        }, null, null);
                        transaccion.Complete();
                    }
                    else
                    {
                        var mensajeExcepcion = new StringBuilder();
                        mensajeExcepcion.AppendLine($"DbUpdateException - {ex?.InnerException?.InnerException?.Message}");

                        foreach (var eve in ex.Entries)
                        {
                            mensajeExcepcion.AppendLine($"La Entidad {eve.Entity.GetType().Name} en estado {eve.State} no se puede afectar.");
                        }

                        throw new Exception(mensajeExcepcion.ToString());
                    }
                }
                finally
                {
                    transaccion.Dispose();
                }
            }

            return retorno;
        }

        #endregion "MIEMBROS"
    }
}