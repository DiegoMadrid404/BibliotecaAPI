// ------------------------------------------------------------------------------------
// <copyright file="LibrosAutoresBL.cs" company="DIEGOMADRID26@GMAIL.COM">
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
    using System.Linq;
    using Proyecto.Negocio.Clases.BO.Repositorio;
    using Proyecto.Datos.Contexto.Repositorio;

    /// <summary>
    /// Clase con las acciones de negocio de la entidad LibrosAutores
    /// </summary>
    public class LibrosAutoresBL : AccesoComunBL, ILibrosAutoresNegocioAccion
    {

        #region ATRIBUTOS
        /// <summary>
        /// Objeto para repositorio LibrosAutores
        /// <summary>
        /// Objeto para acciones LibrosAutores
        /// </summary>
        private Lazy<ILibrosAutoresRepositorioAccion> librosAutoresRepositorioAccion;


        private Lazy<IAutoresRepositorioAccion> autoresRepositorioAccion;
        private Lazy<ILibrosRepositorioAccion> librosRepositorioAccion;


        /// <summary>
        /// Objeto para entidad respuesta
        /// </summary>
        Respuesta<ILibrosAutoresDTO> respuesta;

        #endregion ATRIBUTOS
        #region CONSTRUCTORES
        /// <summary>
        /// Inicializa una nueva instancia de la clase LibrosAutoresBL
        /// </summary>
        /// <param name="argLibrosAutoresRepositorioAccion">Acciones entidad LibrosAutores</param>
        public LibrosAutoresBL(Lazy<ILibrosAutoresRepositorioAccion> argLibrosAutoresRepositorioAccion = null,
                               Lazy<IAutoresRepositorioAccion> argautoresRepositorioAccion = null,
                               Lazy<ILibrosRepositorioAccion> argILibrosRepositorioAccion = null)
        {
            this.respuesta = new Respuesta<ILibrosAutoresDTO>();
            this.librosAutoresRepositorioAccion = argLibrosAutoresRepositorioAccion ?? new Lazy<ILibrosAutoresRepositorioAccion>(() => new LibrosAutoresDAL());
            this.autoresRepositorioAccion = argautoresRepositorioAccion ?? new Lazy<IAutoresRepositorioAccion>(() => new AutoresDAL());
            this.librosRepositorioAccion = argILibrosRepositorioAccion ?? new Lazy<ILibrosRepositorioAccion>(() => new LibrosDAL());
        }

        #endregion CONSTRUCTORES
        #region METODOS PUBLICOS
        /// <summary>
        /// Metodo consultar lista librosAutores
        /// </summary>
        /// <returns>Respuesta tipo LibrosAutores </returns>
        public async Task<Respuesta<ILibrosAutoresDTO>> ConsultarListaLibrosAutoresAsync()
        {

            return await this.EjecutarTransaccionBDAsync<Respuesta<ILibrosAutoresDTO>, LibrosAutoresBL>(
            System.Transactions.IsolationLevel.ReadUncommitted,
            async () =>
            {
                try
                {
                    respuesta = await this.librosAutoresRepositorioAccion.Value.ConsultarListaLibrosAutoresAsync();
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
        /// Metodo consultar por llave librosAutores
        /// </summary>
        /// <param name="librosAutores">Entidad a consultar</param>
        /// <returns>Respuesta tipo LibrosAutores </returns>
        public async Task<Respuesta<ILibrosAutoresDTO>> ConsultarLibrosAutoresLlaveAsync(ILibrosAutoresDTO librosAutores)
        {
            return await this.EjecutarTransaccionBDAsync<Respuesta<ILibrosAutoresDTO>, LibrosAutoresBL>(
            System.Transactions.IsolationLevel.ReadUncommitted,
            async () =>
            {
                try
                {
                    respuesta = await this.librosAutoresRepositorioAccion.Value.ConsultarLibrosAutoresLlaveAsync(librosAutores);
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
        /// Metodo consultar por llave librosAutores
        /// </summary>
        /// <param name="librosAutores">Entidad a consultar</param>
        /// <returns>Respuesta tipo LibrosAutores </returns>
        public async Task<Respuesta<ILibrosAutoresConsultaDTO>> ConsultarLibrosAutoresLinqAsync()
        {
            return await this.EjecutarTransaccionBDAsync<Respuesta<ILibrosAutoresConsultaDTO>, LibrosAutoresBL>(
            System.Transactions.IsolationLevel.ReadUncommitted,
            async () =>
            {
                try
                {
                    Task<Respuesta<IAutoresDTO>> autores = this.autoresRepositorioAccion.Value.ConsultarListaAutoresAsync();
                    Task<Respuesta<ILibrosDTO>> libros = this.librosRepositorioAccion.Value.ConsultarListaLibrosAsync();
                    Task<Respuesta<ILibrosAutoresDTO>> librosAutores = this.ConsultarListaLibrosAutoresAsync();
                    Task.WaitAll(autores, libros, librosAutores);

                    Respuesta<ILibrosAutoresConsultaDTO> respuestaLibrosAutores = new Respuesta<ILibrosAutoresConsultaDTO>();



                    respuestaLibrosAutores.Entidades = this.ConsultarLibrosAutores(librosAutores.Result.Entidades, autores.Result.Entidades, libros.Result.Entidades);

                    respuestaLibrosAutores.Resultado = true;
                    respuestaLibrosAutores.TipoNotificacion = TipoNotificacion.Exitoso;
                    return respuestaLibrosAutores;
                }

                catch (Exception ex)
                {
                    Respuesta<ILibrosAutoresConsultaDTO> respuestaLibrosAutores = new Respuesta<ILibrosAutoresConsultaDTO>();

                    respuestaLibrosAutores.Mensajes = new List<string> { $"ERROR GENERADO: {ex}" };
                    respuestaLibrosAutores.Resultado = false;
                    respuestaLibrosAutores.TipoNotificacion = TipoNotificacion.Error;


                    return respuestaLibrosAutores;

                }

            });
        }

        public List<ILibrosAutoresConsultaDTO> ConsultarLibrosAutores(List<ILibrosAutoresDTO> librosAutores, List<IAutoresDTO> autores, List<ILibrosDTO> libros)
        {
            return (from libroAutor in librosAutores
                    from autor in autores.Where(x => x.AutorID == libroAutor.AutorID)
                    from libro in libros.Where(x => x.LibroID == libroAutor.LibroID)
                    select new LibrosAutoresConsultaBO
                    {
                        LibroID = libroAutor.LibroID,
                        AutorID = libroAutor.AutorID,
                        LibroAutorID = libroAutor.LibroID,
                        Autor = $"{autor.Nombre} {autor.Apellido}",
                        Titulo = libro.Titulo

                    }).ToList<ILibrosAutoresConsultaDTO>();

        }

        public async Task<Respuesta<ILibrosAutoresConsultaDTO>> ConsultarLibrosAutoresProcedimientoAsync()
        {
            try
            {
                return await this.EjecutarTransaccionBDAsync<Respuesta<ILibrosAutoresConsultaDTO>, LibrosAutoresBL>(
                System.Transactions.IsolationLevel.ReadUncommitted,
                async () =>
                {
                    Respuesta<ILibrosAutoresConsultaDTO> respuestalibrosAutores = new Respuesta<ILibrosAutoresConsultaDTO>();
                    respuestalibrosAutores = await this.librosAutoresRepositorioAccion.Value.ConsultarLibrosAutoresProcedimientoAsync();
                    respuestalibrosAutores.Resultado = true;
                    respuestalibrosAutores.TipoNotificacion = TipoNotificacion.Exitoso;
                    return respuestalibrosAutores;

                });
            }

            catch (Exception ex)
            {
                Respuesta<ILibrosAutoresConsultaDTO> respuestalibrosAutores = new Respuesta<ILibrosAutoresConsultaDTO>();

                respuestalibrosAutores.Mensajes = new List<string> { $"ERROR GENERADO: {ex}" };
                respuestalibrosAutores.Resultado = false;
                respuestalibrosAutores.TipoNotificacion = TipoNotificacion.Error;


                return respuestalibrosAutores;

            }
        }

        /// <summary>
        /// Metodo editar librosAutores
        /// </summary>
        /// <param name="librosAutores">Entidad a editar</param>
        /// <returns>Respuesta tipo LibrosAutores </returns>
        public async Task<Respuesta<ILibrosAutoresDTO>> EditarLibrosAutoresAsync(ILibrosAutoresDTO librosAutores)
        {
            return await this.EjecutarTransaccionBDAsync<Respuesta<ILibrosAutoresDTO>, LibrosAutoresBL>(
            System.Transactions.IsolationLevel.ReadUncommitted,
            async () =>
            {
                try
                {
                    Task<bool> existelibro = this.ValidarExistenciaLibroAsync(librosAutores.LibroID);

                    Task<bool> existeAutor = this.ValidarExistenciaAutorAsync(librosAutores.AutorID);

                    Task<bool> existeAutorLibro = this.ValidarExistenciaLibrosAutoresAsync(librosAutores.AutorID);


                    Task.WaitAll(existeAutor, existelibro, existeAutorLibro);
                    if (!existeAutor.Result || !existelibro.Result)
                    {
                        respuesta.Resultado = false;
                        respuesta.TipoNotificacion = TipoNotificacion.Error;
                        if (!existeAutor.Result)
                            respuesta.Mensajes.Add("No existe el Id ingresado del autor, verifique");
                        if (!existelibro.Result)
                            respuesta.Mensajes.Add("No existe el Id ingresado del libro, verifique");                      
                        if (!existeAutorLibro.Result)
                            respuesta.Mensajes.Add("No existe el Id ingresado del registro, verifique");

                        return respuesta;
                    }

                    respuesta = await this.librosAutoresRepositorioAccion.Value.EditarLibrosAutoresAsync(librosAutores);
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
        /// Metodo eliminar librosAutores
        /// </summary>
        /// <param name="librosAutores">Entidad a eliminar</param>
        /// <returns>Respuesta tipo LibrosAutores </returns>
        public async Task<Respuesta<ILibrosAutoresDTO>> EliminarLibrosAutoresAsync(Guid idLibroAutor)
        {
            return await this.EjecutarTransaccionBDAsync<Respuesta<ILibrosAutoresDTO>, LibrosAutoresBL>(
            System.Transactions.IsolationLevel.ReadUncommitted,
            async () =>
            {
                try
                {
               
                     bool  existeAutorLibro = await this.ValidarExistenciaLibrosAutoresAsync(idLibroAutor);


                     if (!existeAutorLibro)
                    {
                        respuesta.Resultado = false;
                        respuesta.TipoNotificacion = TipoNotificacion.Error;
                             respuesta.Mensajes.Add("No existe el Id ingresado, verifique");
                        
                        return respuesta;
                    }

                    respuesta = await this.librosAutoresRepositorioAccion.Value.EliminarLibrosAutoresAsync(idLibroAutor);
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
        /// Metodo guardar librosAutores
        /// </summary>
        /// <param name="librosAutores">Entidad a guardar</param>
        /// <returns>Respuesta tipo LibrosAutores </returns>
        public async Task<Respuesta<ILibrosAutoresDTO>> GuardarLibrosAutoresAsync(ILibrosAutoresDTO librosAutores)
        {
            return await this.EjecutarTransaccionBDAsync<Respuesta<ILibrosAutoresDTO>, LibrosAutoresBL>(
            System.Transactions.IsolationLevel.ReadUncommitted,
            async () =>
            {
                try
                { 
                    Task<bool> existelibro = this.ValidarExistenciaLibroAsync(librosAutores.LibroID);                   
                    Task<bool> existeAutor = this.ValidarExistenciaAutorAsync(librosAutores.AutorID);
                    Task.WaitAll(existeAutor, existelibro);
                    if (!existeAutor.Result || !existelibro.Result)
                    {
                        respuesta.Resultado = false;
                        respuesta.TipoNotificacion = TipoNotificacion.Error;
                        if (!existeAutor.Result)
                            respuesta.Mensajes.Add("No existe el Id ingresado del autor, verifique");
                        if (!existelibro.Result)
                            respuesta.Mensajes.Add("No existe el Id ingresado del libro, verifique");

                        return respuesta;
                    }
                    respuesta = await this.librosAutoresRepositorioAccion.Value.GuardarLibrosAutoresAsync(librosAutores);
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
        /// Metodo Guardar lista librosAutores
        /// </summary>
        /// <param name="listaLibrosAutores">Lista de entidades a guardar</param>
        /// <returns>Respuesta tipo LibrosAutores </returns>
        public async Task<Respuesta<ILibrosAutoresDTO>> GuardarListaLibrosAutoresAsync(List<ILibrosAutoresDTO> listaLibrosAutores)
        {
            return await this.EjecutarTransaccionBDAsync<Respuesta<ILibrosAutoresDTO>, LibrosAutoresBL>(
            System.Transactions.IsolationLevel.ReadUncommitted,
            async () =>
            {

                try
                {
                    respuesta = await this.librosAutoresRepositorioAccion.Value.GuardarListaLibrosAutoresAsync(listaLibrosAutores);
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

        public async Task<bool> ValidarExistenciaAutorAsync(Guid autorId)
        {
            return this.autoresRepositorioAccion.Value.ConsultarListaAutoresPorFiltroAsync(x => x.AutorID == autorId).Result.Entidades.Any();

        }
        public async Task<bool> ValidarExistenciaLibroAsync(Guid libroID)
        {
            return this.librosRepositorioAccion.Value.ConsultarListaLibrosPorFiltroAsync(x => x.LibroID == libroID).Result.Entidades.Any();

        }
        public async Task<bool> ValidarExistenciaLibrosAutoresAsync(Guid libroAutorID)
        {
            return this.librosAutoresRepositorioAccion.Value.ConsultarListaLibrosAutoresPorFiltroAsync(x => x.LibroAutorID == libroAutorID).Result.Entidades.Any();
        }
        #endregion METODOS PUBLICOS
        #region METODOS PRIVADOS
        #endregion METODOS PRIVADOS
    }
}

