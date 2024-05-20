// ------------------------------------------------------------------------------------
// <copyright file="LibrosBL.cs" company="DIEGOMADRID26@GMAIL.COM">
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
    using Proyecto.Negocio.Clases.BO.Repositorio;
    using System.Linq;

    /// <summary>
    /// Clase con las acciones de negocio de la entidad Libros
    /// </summary>
    public class LibrosBL : AccesoComunBL, ILibrosNegocioAccion
    {

        #region ATRIBUTOS
        /// <summary>
        /// Objeto para repositorio Libros
        /// <summary>
        /// Objeto para acciones Libros
        /// </summary>
        private Lazy<ILibrosRepositorioAccion> librosRepositorioAccion;
        private Lazy<ILibrosAutoresRepositorioAccion> librosAutoresRepositorioAccion;

        /// <summary>
        /// Objeto para entidad respuesta
        /// </summary>
        Respuesta<ILibrosDTO> respuesta;

        #endregion ATRIBUTOS
        #region CONSTRUCTORES
        /// <summary>
        /// Inicializa una nueva instancia de la clase LibrosBL
        /// </summary>
        /// <param name="argLibrosRepositorioAccion">Acciones entidad Libros</param>
        public LibrosBL(Lazy<ILibrosRepositorioAccion> argLibrosRepositorioAccion = null, Lazy<ILibrosAutoresRepositorioAccion> arglibrosAutoresRepositorioAccion = null)
        {
            this.respuesta = new Respuesta<ILibrosDTO>();
            this.librosRepositorioAccion = argLibrosRepositorioAccion ?? new Lazy<ILibrosRepositorioAccion>(() => new LibrosDAL());
             this.librosAutoresRepositorioAccion = arglibrosAutoresRepositorioAccion ?? new Lazy<ILibrosAutoresRepositorioAccion>(() => new LibrosAutoresDAL());

        }

        #endregion CONSTRUCTORES
        #region METODOS PUBLICOS
        /// <summary>
        /// Metodo consultar lista libros
        /// </summary>
        /// <returns>Respuesta tipo Libros </returns>
        public async Task<Respuesta<ILibrosDTO>> ConsultarListaLibrosAsync()
        {

            return await this.EjecutarTransaccionBDAsync<Respuesta<ILibrosDTO>, LibrosBL>(
            System.Transactions.IsolationLevel.ReadUncommitted,
            async () =>
            {
                try
                {
                    respuesta = await this.librosRepositorioAccion.Value.ConsultarListaLibrosAsync();
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
        /// Metodo consultar por llave libros
        /// </summary>
        /// <param name="libros">Entidad a consultar</param>
        /// <returns>Respuesta tipo Libros </returns>
        public async Task<Respuesta<ILibrosDTO>> ConsultarLibrosFiltroAsync(ILibrosDTO libros)
        {
            return await this.EjecutarTransaccionBDAsync<Respuesta<ILibrosDTO>, LibrosBL>(
            System.Transactions.IsolationLevel.ReadUncommitted,
            async () =>
            {
                try
                {
                    respuesta = await this.librosRepositorioAccion.Value.ConsultarLibrosFiltroAsync(libros);
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
        /// Metodo editar libros
        /// </summary>
        /// <param name="libros">Entidad a editar</param>
        /// <returns>Respuesta tipo Libros </returns>
        public async Task<Respuesta<ILibrosDTO>> EditarLibrosAsync(ILibrosDTO libros)
        {
            return await this.EjecutarTransaccionBDAsync<Respuesta<ILibrosDTO>, LibrosBL>(
            System.Transactions.IsolationLevel.ReadUncommitted,
            async () =>
            {
                try
                {
                    bool existeId = await ValidarExistenciaLibroAsync(libros.LibroID);
                    if (!existeId)
                    {
                        respuesta.Resultado = false;
                        respuesta.TipoNotificacion = TipoNotificacion.Error;
                        respuesta.Mensajes.Add("No existe el Id ingresado, verifique");

                        return respuesta;
                    }
                    respuesta = await this.librosRepositorioAccion.Value.EditarLibrosAsync(libros);
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


        /// <summary>
        /// Metodo eliminar libros
        /// </summary>
        /// <param name="libros">Entidad a eliminar</param>
        /// <returns>Respuesta tipo Libros </returns>
        public async Task<Respuesta<ILibrosDTO>> EliminarLibrosAsync(Guid idLibro)
        {
            return await this.EjecutarTransaccionBDAsync<Respuesta<ILibrosDTO>, LibrosBL>(
            System.Transactions.IsolationLevel.ReadUncommitted,
            async () =>
            {
                try
                {
                    Task<bool> existeId = this.ValidarExistenciaLibroAsync(idLibro);
                    Task<bool> tieneIdTbLibrosAutor = this.ValidarEstaEnTbLibrosAutor(idLibro);
                    Task.WaitAll(tieneIdTbLibrosAutor, existeId);
                    if (!existeId.Result || tieneIdTbLibrosAutor.Result)
                    {
                        respuesta.Resultado = false;
                        respuesta.TipoNotificacion = TipoNotificacion.Error;
                        if (!existeId.Result)
                            respuesta.Mensajes.Add("No existe el Id ingresado, verifique");
                        if (tieneIdTbLibrosAutor.Result)
                            respuesta.Mensajes.Add("El registro esta asociado a un autor, verifique");

                        return respuesta;
                    }

                     
                    respuesta = await this.librosRepositorioAccion.Value.EliminarLibrosAsync(idLibro);
                    respuesta.Resultado = true;
                    respuesta.TipoNotificacion = TipoNotificacion.Exitoso;
                    respuesta.Mensajes.Add(rcsMensajesComunes.MensajeEntidadEliminadaConExito);
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
        /// Metodo guardar libros
        /// </summary>
        /// <param name="libros">Entidad a guardar</param>
        /// <returns>Respuesta tipo Libros </returns>
        public async Task<Respuesta<ILibrosDTO>> GuardarLibrosAsync(ILibrosDTO libros)
        {
            return await this.EjecutarTransaccionBDAsync<Respuesta<ILibrosDTO>, LibrosBL>(
            System.Transactions.IsolationLevel.ReadUncommitted,
            async () =>
            {
                try
                {
                    respuesta = await this.librosRepositorioAccion.Value.GuardarLibrosAsync(libros);
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

        /// <summary>
        /// Metodo Guardar lista libros
        /// </summary>
        /// <param name="listaLibros">Lista de entidades a guardar</param>
        /// <returns>Respuesta tipo Libros </returns>
        public async Task<Respuesta<ILibrosDTO>> GuardarListaLibrosAsync(List<ILibrosDTO> listaLibros)
        {
            return await this.EjecutarTransaccionBDAsync<Respuesta<ILibrosDTO>, LibrosBL>(
            System.Transactions.IsolationLevel.ReadUncommitted,
            async () =>
            {
                try
                { 
                        respuesta = await this.librosRepositorioAccion.Value.GuardarListaLibrosAsync(listaLibros);
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
        public async Task<bool> ValidarEstaEnTbLibrosAutor(Guid libroID)
        {
            return this.librosAutoresRepositorioAccion.Value.ConsultarListaLibrosAutoresPorFiltroAsync(x => x.LibroID == libroID).Result.Entidades.Any();

        }
        public async Task<bool> ValidarExistenciaLibroAsync(Guid libroID)
        {
            return this.librosRepositorioAccion.Value.ConsultarListaLibrosPorFiltroAsync(x => x.LibroID == libroID).Result.Entidades.Any();
             
        }
        #endregion METODOS PUBLICOS
        #region METODOS PRIVADOS

        #endregion METODOS PUBLICOS
        #region METODOS PRIVADOS
        #endregion METODOS PRIVADOS
    }
}

