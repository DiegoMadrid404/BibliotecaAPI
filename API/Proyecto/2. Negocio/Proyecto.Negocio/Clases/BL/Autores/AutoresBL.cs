// ------------------------------------------------------------------------------------
// <copyright file="AutoresBL.cs" company="DIEGOMADRID26@GMAIL.COM">
// Copyright (c) DIEGOMADRID26@GMAIL.COM. All rights reserved.
// </copyright>
// <author>DIEGO MADRID</author>
// ------------------------------------------------------------------------------------
namespace Proyecto.Negocio.Clases.BL
{
    using System;
    using System.Collections.Generic;
    using Proyecto.Negocio.Utilidades;

    using System.Threading.Tasks;
    using Proyecto.Datos.Clases.DAL.Repositorio;
    using Proyecto.IC.Acciones.Repositorio;
    using Proyecto.IC.DTO.Repositorio;
    using Proyecto.IC.Recursos;
    using Proyecto.IC.Utilidades.Genericos;
    using Proyecto.IC.Enumeraciones;
    using System.Linq.Expressions;
    using Proyecto.Datos.Contexto.Repositorio;
    using Proyecto.Negocio.Clases.BO.Repositorio;
    using System.Linq;

    /// <summary>
    /// Clase con las acciones de negocio de la entidad Autores
    /// </summary>
    public class AutoresBL : AccesoComunBL, IAutoresNegocioAccion
    {

        #region ATRIBUTOS
        /// <summary>
        /// Objeto para repositorio Autores
        /// <summary>
        /// Objeto para acciones Autores
        /// </summary>
        private Lazy<IAutoresRepositorioAccion> autoresRepositorioAccion;
        private Lazy<ILibrosAutoresRepositorioAccion> librosAutoresRepositorioAccion;

        /// <summary>
        /// Objeto para entidad respuesta
        /// </summary>
        Respuesta<IAutoresDTO> respuesta;

        #endregion ATRIBUTOS
        #region CONSTRUCTORES
        /// <summary>
        /// Inicializa una nueva instancia de la clase AutoresBL
        /// </summary>
        /// <param name="argAutoresRepositorioAccion">Acciones entidad Autores</param>
        public AutoresBL(Lazy<IAutoresRepositorioAccion> argAutoresRepositorioAccion = null,
                          Lazy<ILibrosAutoresRepositorioAccion> arglibrosAutoresRepositorioAccion = null)
        {
            this.respuesta = new Respuesta<IAutoresDTO>();
            this.autoresRepositorioAccion = argAutoresRepositorioAccion ?? new Lazy<IAutoresRepositorioAccion>(() => new AutoresDAL());
            this.librosAutoresRepositorioAccion = arglibrosAutoresRepositorioAccion ?? new Lazy<ILibrosAutoresRepositorioAccion>(() => new LibrosAutoresDAL());
        }

        #endregion CONSTRUCTORES
        #region METODOS PUBLICOS
        /// <summary>
        /// Metodo consultar lista autores
        /// </summary>
        /// <returns>Respuesta tipo Autores </returns>
        public async Task<Respuesta<IAutoresDTO>> ConsultarListaAutoresAsync()
        {

            return await this.EjecutarTransaccionBDAsync<Respuesta<IAutoresDTO>, AutoresBL>(
            System.Transactions.IsolationLevel.ReadUncommitted,
            async () =>
            {
                try
                {
                    respuesta = await this.autoresRepositorioAccion.Value.ConsultarListaAutoresAsync();
                    respuesta.Resultado = true;
                    respuesta.TipoNotificacion = TipoNotificacion.Exitoso;

                    //throw new InvalidOperationException("Logfile cannot be read-only");

                    return respuesta;
                }

                catch (Exception ex)
                {
                    respuesta.Mensajes = new List<string> { $"ERROR GENERADO: {ex}" };
                    respuesta.Resultado = false;
                    respuesta.TipoNotificacion = TipoNotificacion.Error;


                    return respuesta;

                }
            });

        }
        /// <summary>
        /// Metodo consultar por llave autores
        /// </summary>
        /// <param name="autores">Entidad a consultar</param>
        /// <returns>Respuesta tipo Autores </returns>
        public async Task<Respuesta<IAutoresDTO>> ConsultarAutoresFiltroAsync(IAutoresDTO autores)
        {
            return await this.EjecutarTransaccionBDAsync<Respuesta<IAutoresDTO>, AutoresBL>(
            System.Transactions.IsolationLevel.ReadUncommitted,
            async () =>
            {
                try
                {
                    respuesta = await this.autoresRepositorioAccion.Value.ConsultarAutoresFiltroAsync(autores);
                    respuesta.Resultado = true;
                    respuesta.TipoNotificacion = TipoNotificacion.Exitoso;
                    return respuesta;
                }

                catch (Exception ex)
                {
                    respuesta.Mensajes = new List<string> { $"ERROR GENERADO: {ex}" };
                    respuesta.Resultado = false;
                    respuesta.TipoNotificacion = TipoNotificacion.Error;


                    return respuesta;

                }
            });
        }

        /// <summary>
        /// Metodo editar autores
        /// </summary>
        /// <param name="autores">Entidad a editar</param>
        /// <returns>Respuesta tipo Autores </returns>
        public async Task<Respuesta<IAutoresDTO>> EditarAutoresAsync(IAutoresDTO autores)
        {
            try
            {
                return await this.EjecutarTransaccionBDAsync<Respuesta<IAutoresDTO>, AutoresBL>(
                System.Transactions.IsolationLevel.ReadUncommitted,
                async () =>
                {
                    bool existeId = await this.ValidarExistenciaAutorAsync(autores.AutorID);

                    if (!existeId)
                    {
                        respuesta.Resultado = false;
                        respuesta.TipoNotificacion = TipoNotificacion.Error;
                        respuesta.Mensajes.Add("No existe el Id ingresado, verifique");

                        return respuesta;
                    }
                    respuesta = await this.autoresRepositorioAccion.Value.EditarAutoresAsync(autores);
                    respuesta.Resultado = true;
                    respuesta.TipoNotificacion = TipoNotificacion.Exitoso;
                    respuesta.Mensajes.Add(rcsMensajesComunes.MensajeCreacionEdicionExitosa);
                    return respuesta;

                });

            }

            catch (Exception ex)
            {
                respuesta.Mensajes = new List<string> { $"ERROR GENERADO: {ex}" };
                respuesta.Resultado = false;
                respuesta.TipoNotificacion = TipoNotificacion.Error;


                return respuesta;

            }
        }


        /// <summary>
        /// Metodo eliminar autores
        /// </summary>
        /// <param name="autores">Entidad a eliminar</param>
        /// <returns>Respuesta tipo Autores </returns>
        public async Task<Respuesta<IAutoresDTO>> EliminarAutoresAsync(Guid idAutor)
        {
            try
            {
                return await this.EjecutarTransaccionBDAsync<Respuesta<IAutoresDTO>, AutoresBL>(
                System.Transactions.IsolationLevel.ReadUncommitted,
                async () =>
                {
                    Task<bool> existeId = this.ValidarExistenciaAutorAsync(idAutor);
                    Task<bool> tieneIdTbLibrosAutor = this.ValidarEstaEnTbLibrosAutor(idAutor);
                    Task.WaitAll(tieneIdTbLibrosAutor, existeId);
                    if (!existeId.Result || tieneIdTbLibrosAutor.Result)
                    {
                        respuesta.Resultado = false;
                        respuesta.TipoNotificacion = TipoNotificacion.Error;
                        if (!existeId.Result)
                            respuesta.Mensajes.Add("No existe el Id ingresado, verifique");
                        if (tieneIdTbLibrosAutor.Result)
                            respuesta.Mensajes.Add("El registro esta asociado a un libro, verifique");

                        return respuesta;
                    }

                    respuesta = await this.autoresRepositorioAccion.Value.EliminarAutoresAsync(idAutor);
                    respuesta.Resultado = true;
                    respuesta.TipoNotificacion = TipoNotificacion.Exitoso;
                    respuesta.Mensajes.Add(rcsMensajesComunes.MensajeEntidadEliminadaConExito);

                    return respuesta;

                });
            }

            catch (Exception ex)
            {
                respuesta.Mensajes = new List<string> { $"ERROR GENERADO: {ex}" };
                respuesta.Resultado = false;
                respuesta.TipoNotificacion = TipoNotificacion.Error;


                return respuesta;

            }
        }

        /// <summary>
        /// Metodo guardar autores
        /// </summary>
        /// <param name="autores">Entidad a guardar</param>
        /// <returns>Respuesta tipo Autores </returns>
        public async Task<Respuesta<IAutoresDTO>> GuardarAutoresAsync(IAutoresDTO autores)
        {
            try
            {
                return await this.EjecutarTransaccionBDAsync<Respuesta<IAutoresDTO>, AutoresBL>(
                System.Transactions.IsolationLevel.ReadUncommitted,
                async () =>
                {
                    respuesta = await this.autoresRepositorioAccion.Value.GuardarAutoresAsync(autores);
                    respuesta.Resultado = true;
                    respuesta.TipoNotificacion = TipoNotificacion.Exitoso;
                    respuesta.Mensajes.Add(rcsMensajesComunes.MensajeCreacionEdicionExitosa);
                    return respuesta;

                });

            }

            catch (Exception ex)
            {
                respuesta.Mensajes = new List<string> { $"ERROR GENERADO: {ex}" };
                respuesta.Resultado = false;
                respuesta.TipoNotificacion = TipoNotificacion.Error;


                return respuesta;

            }
        }

        /// <summary>
        /// Metodo Guardar lista autores
        /// </summary>
        /// <param name="listaAutores">Lista de entidades a guardar</param>
        /// <returns>Respuesta tipo Autores </returns>
        public async Task<Respuesta<IAutoresDTO>> GuardarListaAutoresAsync(List<IAutoresDTO> listaAutores)
        {
            return await this.EjecutarTransaccionBDAsync<Respuesta<IAutoresDTO>, AutoresBL>(
            System.Transactions.IsolationLevel.ReadUncommitted,
            async () =>
            {
                try
                {
                    respuesta = await this.autoresRepositorioAccion.Value.GuardarListaAutoresAsync(listaAutores);
                    respuesta.Resultado = true;
                    respuesta.TipoNotificacion = TipoNotificacion.Exitoso;
                    respuesta.Mensajes.Add(rcsMensajesComunes.MensajeCreacionEdicionExitosa);
                    return respuesta;
                }
                catch (Exception ex)
                {
                    respuesta.Mensajes = new List<string> { $"ERROR GENERADO: {ex}" };
                    respuesta.Resultado = false;
                    respuesta.TipoNotificacion = TipoNotificacion.Error;


                    return respuesta;

                }
            });
        }
        public async Task<bool> ValidarEstaEnTbLibrosAutor(Guid autorId)
        {
            return this.librosAutoresRepositorioAccion.Value.ConsultarListaLibrosAutoresPorFiltroAsync(x => x.AutorID == autorId).Result.Entidades.Any();

        }
        public async Task<bool> ValidarExistenciaAutorAsync(Guid autorId)
        {
            return this.autoresRepositorioAccion.Value.ConsultarListaAutoresPorFiltroAsync(x => x.AutorID == autorId).Result.Entidades.Any();

        }
        #endregion METODOS PUBLICOS
        #region METODOS PRIVADOS


        #endregion METODOS PRIVADOS
    }
}

