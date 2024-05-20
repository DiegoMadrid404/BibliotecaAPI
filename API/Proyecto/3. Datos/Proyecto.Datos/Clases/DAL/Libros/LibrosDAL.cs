// ------------------------------------------------------------------------------------
// <copyright file="LibrosDAL.cs" company="DIEGOMADRID26@GMAIL.COM">
// Copyright (c) DIEGOMADRID26@GMAIL.COM. All rights reserved.
// </copyright>
// <author>DIEGO MADRID</author>
// ------------------------------------------------------------------------------------
namespace Proyecto.Datos.Clases.DAL.Repositorio
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Proyecto.Datos.Contexto;
    using Proyecto.Datos.Contexto.Repositorio;
    using Proyecto.IC.Acciones.Repositorio;
    using Proyecto.IC.DTO.Repositorio;
    using Proyecto.Datos.Utilidades;
    using Proyecto.IC.Utilidades.Genericos;
    using Proyecto.IC.Utilidades.Auxiliares;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Clase con las acciones de repositorio para la entidad Libros
    /// </summary>
    public class LibrosDAL : AccesoComunDAL<ContextoProyecto>, ILibrosRepositorioAccion
    {
        /// <summary>
        /// Objeto para entidad respuesta
        /// </summary>
		Respuesta<ILibrosDTO> respuesta;

        /// <summary>
        /// Objeto para repositorio Libros
        /// </summary>
        RepositorioGenerico<Libros> repositorio;

        /// <summary>
        /// Inicializa una nueva instancia de la clase LibrosDAL
        /// </summary>
        public LibrosDAL()
        {
            this.respuesta = new Respuesta<ILibrosDTO>();
            this.repositorio = new RepositorioGenerico<Libros>(ContextoBD);
        }

        /// <summary>
        /// Metodo editar libros
        /// </summary>
        /// <param name="libros">Entidad a editar</param>
        /// <returns>Respuesta tipo Libros </returns>
        public async Task<Respuesta<ILibrosDTO>> EditarLibrosAsync(ILibrosDTO libros)
        {
            return await this.EjecutarTransaccionAsync<Respuesta<ILibrosDTO>, LibrosDAL>(async () =>
            {
                Libros LibrosEntidad = repositorio.BuscarPor(entidad => entidad
                    .LibroID == libros.LibroID).FirstOrDefault();
                Mapeador.MapearObjetosPorPropiedad(libros, LibrosEntidad);
                repositorio.Editar(LibrosEntidad);
                repositorio.Guardar();
                return respuesta;
            });
        }

        /// <summary>
        /// Metodo editar  libros por filtro 
        /// </summary>
        /// <param name="libros">Entidad con los datos a editar</param>
        /// <param name="propiedades">Propiedades a modificar</param>
        /// <returns>Respuesta tipo Libros </returns>
        public async Task<Respuesta<ILibrosDTO>> EditarLibrosPorFiltroAsync(ILibrosDTO libros, params string[] propiedades)
        {
            return await this.EjecutarTransaccionAsync<Respuesta<ILibrosDTO>, LibrosDAL>(async () =>
            {
                Libros librosEntidad = repositorio.BuscarPor(entidad => entidad
                .LibroID == libros.LibroID).FirstOrDefault();
                Mapeador.MapearObjetosPorPropiedadPorFiltro(libros, librosEntidad, propiedades);
                repositorio.Editar(librosEntidad);
                repositorio.Guardar();
                return respuesta;
            });
        }


        /// <summary>
        /// Metodo editar libros por query 
        /// </summary>
        /// <param name="filtro">Filtro de las entidades </param>
        /// <param name="valor">Entidad a mofificar</param>
        /// <param name="propiedades">Propiedades a modificar</param>
        /// <returns>Respuesta tipo Libros </returns>
        public async Task<Respuesta<ILibrosDTO>> EditarPorQueryLibrosAsync(Expression<Func<ILibrosDTO, bool>> filtro, ILibrosDTO valor, List<string> propiedades)
        {
            return await this.EjecutarTransaccionAsync<Respuesta<ILibrosDTO>, LibrosDAL>(async () =>
            {
                Libros LibrosEntidad = Mapeador.MapearEntidadDTO(valor, new Libros());
                Expression<Func<Libros, bool>> filtros = Mapeador.MapearExpresion<ILibrosDTO, Libros>(filtro);
                await repositorio.EditarPorQueryAsync(filtros, LibrosEntidad, propiedades);
                await repositorio.GuardarAsync();
                return respuesta;
            });
        }


        /// <summary>
        /// Metodo editar libros lista 
        /// </summary>
        /// <param name="lista"> Lista con entidades a modificar</param>
        /// <returns>Respuesta tipo Libros </returns>
        public async Task<Respuesta<ILibrosDTO>> EditarListaLibrosAsync(List<ILibrosDTO> lista)
        {
            return await this.EjecutarTransaccionAsync<Respuesta<ILibrosDTO>, LibrosDAL>(async () =>
            {
                List<Libros> listaLibros = new List<Libros>();
                listaLibros = Mapeador.MapearALista<ILibrosDTO, Libros>(lista);
                await repositorio.EditarListaAsync(listaLibros);
                await repositorio.GuardarAsync();
                return respuesta;
            });
        }

        /// <summary>
        /// Metodo eliminar libros
        /// </summary>
        /// <param name="libros">Entidad a eliminar</param>
        /// <returns>Respuesta tipo Libros </returns>
        public async Task<Respuesta<ILibrosDTO>> EliminarLibrosAsync(Guid idLibro)
        {
            return await this.EjecutarTransaccionAsync<Respuesta<ILibrosDTO>, LibrosDAL>(async () =>
            {
                Libros LibrosEntidad = repositorio.BuscarPor(entidad => entidad
                    .LibroID == idLibro).FirstOrDefault();
                repositorio.Eliminar(LibrosEntidad);
                repositorio.Guardar();
                return respuesta;
            });
        }


        /// <summary>
        /// Metodo eliminar lista libros 
        /// </summary>
        /// <param name="lista">Lista de entidad a eliminar</param>
        /// <returns>Respuesta tipo Libros </returns>
        public async Task<Respuesta<ILibrosDTO>> EliminarListaLibrosAsync(List<ILibrosDTO> lista)
        {
            return await this.EjecutarTransaccionAsync<Respuesta<ILibrosDTO>, LibrosDAL>(async () =>
            {
                List<Libros> listaLibros = new List<Libros>();
                listaLibros = Mapeador.MapearALista<ILibrosDTO, Libros>(lista);
                await repositorio.EliminarListaAsync(listaLibros);
                await repositorio.GuardarAsync();
                return respuesta;
            });
        }

        /// <summary>
        /// Metodo guardar libros
        /// </summary>
        /// <param name="libros">Entidad a guardar</param>
        /// <returns>Respuesta tipo Libros </returns>
        public async Task<Respuesta<ILibrosDTO>> GuardarLibrosAsync(ILibrosDTO libros)
        {
            return await this.EjecutarTransaccionAsync<Respuesta<ILibrosDTO>, LibrosDAL>(async () =>
            {
                Libros LibrosEntidad = Mapeador.MapearEntidadDTO(libros, new Libros());

                // Verifica si la entidad ya está siendo rastreada
                var trackedEntity = ContextoBD.ChangeTracker.Entries<Libros>()
                    .FirstOrDefault(e => e.Entity.LibroID == LibrosEntidad.LibroID);

                if (trackedEntity != null)
                {
                    ContextoBD.Entry(trackedEntity.Entity).State = EntityState.Detached;
                }

                await repositorio.AgregarAsync(LibrosEntidad);
                await repositorio.GuardarAsync();
                respuesta.Entidades.Add(LibrosEntidad);
                return respuesta;
            });
            //return await this.EjecutarTransaccionAsync<Respuesta<ILibrosDTO>, LibrosDAL>(async () =>
            //{
            //    Libros LibrosEntidad = Mapeador.MapearEntidadDTO(libros, new Libros());
            //   await repositorio.AgregarAsync(LibrosEntidad);
            //   await repositorio.GuardarAsync();
            //    respuesta.Entidades.Add(LibrosEntidad);
            //    return respuesta;

            //});
        }

        /// <summary>
        /// Metodo Guardar lista libros
        /// </summary>
        /// <param name="listaLibros">Lista de entidades a guardar</param>
        /// <returns>Respuesta tipo Libros </returns>
        public async Task<Respuesta<ILibrosDTO>> GuardarListaLibrosAsync(List<ILibrosDTO> listaLibros)
        {
            return await this.EjecutarTransaccionAsync<Respuesta<ILibrosDTO>, LibrosDAL>(async () =>
            {
                List<Libros> listaLibrosEntidad = Mapeador.MapearALista<ILibrosDTO, Libros>(listaLibros);
                await repositorio.AgregarListaAsync(listaLibrosEntidad);
                respuesta.Entidades.AddRange(listaLibrosEntidad);
                return respuesta;
            });
        }

        /// <summary>
        /// Metodo consultar lista libros
        /// </summary>
        /// <returns>Respuesta tipo Libros </returns>
        public async Task<Respuesta<ILibrosDTO>> ConsultarListaLibrosAsync()
        {
            return await this.EjecutarTransaccionAsync<Respuesta<ILibrosDTO>, LibrosDAL>(async () =>
            {
                respuesta.Entidades = repositorio.BuscarTodos().ToList<ILibrosDTO>();
                return respuesta;

            });
        }

        /// <summary>
        /// Metodo consultar lista libros por filtro 
        /// </summary>
        /// <param name="libros">Entidad con los datos a filtrar</param>
        /// <param name="filtro">Filtro de las entidades </param>
        /// <returns>Respuesta tipo Libros </returns>
        public async Task<Respuesta<ILibrosDTO>> ConsultarListaLibrosPorFiltroAsync(Expression<Func<ILibrosDTO, bool>> filtro)
        {
            return await this.EjecutarTransaccionAsync<Respuesta<ILibrosDTO>, LibrosDAL>(async () =>
            {
                respuesta.Entidades = repositorio.BuscarPor(Mapeador.MapearExpresion<ILibrosDTO, Libros>(filtro)).ToList<ILibrosDTO>();
                return respuesta;

            });
        }

        /// <summary>
        /// Metodo consultar por llave libros
        /// </summary>
        /// <param name="libros">Entidad a consultar</param>
        /// <returns>Respuesta tipo Libros </returns>
        public async Task<Respuesta<ILibrosDTO>> ConsultarLibrosFiltroAsync(ILibrosDTO libros)
        {
            return await this.EjecutarTransaccionAsync<Respuesta<ILibrosDTO>, LibrosDAL>(async () =>
            {
                respuesta.Entidades = (from entidad in ContextoBD.Libros
                                       where entidad.LibroID == libros.LibroID
                                       || entidad.FechaPublicacion == libros.FechaPublicacion
                                       || entidad.ISBN.Contains(libros.ISBN)
                                       || entidad.Genero.Contains(libros.Genero)
                                       || entidad.Titulo.Contains(libros.Titulo)
                                       select entidad).ToList<ILibrosDTO>();

                return respuesta;

            });
        }
    }
}

