// ------------------------------------------------------------------------------------
// <copyright file="AutoresDAL.cs" company="DIEGOMADRID26@GMAIL.COM">
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

    /// <summary>
    /// Clase con las acciones de repositorio para la entidad Autores
    /// </summary>
    public class AutoresDAL : AccesoComunDAL<ContextoProyecto>, IAutoresRepositorioAccion
    {
        /// <summary>
        /// Objeto para entidad respuesta
        /// </summary>
        Respuesta<IAutoresDTO> respuesta;

        /// <summary>
        /// Objeto para repositorio Autores
        /// </summary>
        RepositorioGenerico<Autores> repositorio;

        /// <summary>
        /// Inicializa una nueva instancia de la clase AutoresDAL
        /// </summary>
        public AutoresDAL()
        {
            this.respuesta = new Respuesta<IAutoresDTO>();
            this.repositorio = new RepositorioGenerico<Autores>(ContextoBD);
        }

        /// <summary>
        /// Metodo editar autores
        /// </summary>
        /// <param name="autores">Entidad a editar</param>
        /// <returns>Respuesta tipo Autores </returns>
        public async Task<Respuesta<IAutoresDTO>> EditarAutoresAsync(IAutoresDTO autores)
        {
            return await this.EjecutarTransaccionAsync<Respuesta<IAutoresDTO>, AutoresDAL>(async () =>
            {
                Autores AutoresEntidad = repositorio.BuscarPor(entidad => entidad
                    .AutorID == autores.AutorID).FirstOrDefault();
                Mapeador.MapearObjetosPorPropiedad(autores, AutoresEntidad);
                repositorio.Editar(AutoresEntidad);
                repositorio.Guardar();
                return respuesta;
            });
        }

        /// <summary>
        /// Metodo editar  autores por filtro 
        /// </summary>
        /// <param name="autores">Entidad con los datos a editar</param>
        /// <param name="propiedades">Propiedades a modificar</param>
        /// <returns>Respuesta tipo Autores </returns>
        public async Task<Respuesta<IAutoresDTO>> EditarAutoresPorFiltroAsync(IAutoresDTO autores, params string[] propiedades)
        {
            return await this.EjecutarTransaccionAsync<Respuesta<IAutoresDTO>, AutoresDAL>(async () =>
            {
                Autores autoresEntidad = repositorio.BuscarPor(entidad => entidad
                .AutorID == autores.AutorID).FirstOrDefault();
                Mapeador.MapearObjetosPorPropiedadPorFiltro(autores, autoresEntidad, propiedades);
                repositorio.Editar(autoresEntidad);
                repositorio.Guardar();
                return respuesta;
            });
        }


        /// <summary>
        /// Metodo editar autores por query 
        /// </summary>
        /// <param name="filtro">Filtro de las entidades </param>
        /// <param name="valor">Entidad a mofificar</param>
        /// <param name="propiedades">Propiedades a modificar</param>
        /// <returns>Respuesta tipo Autores </returns>
        public async Task<Respuesta<IAutoresDTO>> EditarPorQueryAutoresAsync(Expression<Func<IAutoresDTO, bool>> filtro, IAutoresDTO valor, List<string> propiedades)
        {
            return await this.EjecutarTransaccionAsync<Respuesta<IAutoresDTO>, AutoresDAL>(async () =>
            {
                Autores AutoresEntidad = Mapeador.MapearEntidadDTO(valor, new Autores());
                Expression<Func<Autores, bool>> filtros = Mapeador.MapearExpresion<IAutoresDTO, Autores>(filtro);
                await repositorio.EditarPorQueryAsync(filtros, AutoresEntidad, propiedades);
                await repositorio.GuardarAsync();
                return respuesta;
            });
        }


        /// <summary>
        /// Metodo editar autores lista 
        /// </summary>
        /// <param name="lista"> Lista con entidades a modificar</param>
        /// <returns>Respuesta tipo Autores </returns>
        public async Task<Respuesta<IAutoresDTO>> EditarListaAutoresAsync(List<IAutoresDTO> lista)
        {
            return await this.EjecutarTransaccionAsync<Respuesta<IAutoresDTO>, AutoresDAL>(async () =>
            {
                List<Autores> listaAutores = new List<Autores>();
                listaAutores = Mapeador.MapearALista<IAutoresDTO, Autores>(lista);
                await repositorio.EditarListaAsync(listaAutores);
                await repositorio.GuardarAsync();
                return respuesta;
            });
        }

        /// <summary>
        /// Metodo eliminar autores
        /// </summary>
        /// <param name="autores">Entidad a eliminar</param>
        /// <returns>Respuesta tipo Autores </returns>
        public async Task<Respuesta<IAutoresDTO>> EliminarAutoresAsync(Guid idAutor)
        {
            return await this.EjecutarTransaccionAsync<Respuesta<IAutoresDTO>, AutoresDAL>(async () =>
            {
                Autores AutoresEntidad = repositorio.BuscarPor(entidad => entidad
                    .AutorID == idAutor).FirstOrDefault();
                repositorio.Eliminar(AutoresEntidad);
                repositorio.Guardar();
                return respuesta;
            });
        }


        /// <summary>
        /// Metodo eliminar lista autores 
        /// </summary>
        /// <param name="lista">Lista de entidad a eliminar</param>
        /// <returns>Respuesta tipo Autores </returns>
        public async Task<Respuesta<IAutoresDTO>> EliminarListaAutoresAsync(List<IAutoresDTO> lista)
        {
            return await this.EjecutarTransaccionAsync<Respuesta<IAutoresDTO>, AutoresDAL>(async () =>
            {
                List<Autores> listaAutores = new List<Autores>();
                listaAutores = Mapeador.MapearALista<IAutoresDTO, Autores>(lista);
                await repositorio.EliminarListaAsync(listaAutores);
                await repositorio.GuardarAsync();
                return respuesta;
            });
        }

        /// <summary>
        /// Metodo guardar autores
        /// </summary>
        /// <param name="autores">Entidad a guardar</param>
        /// <returns>Respuesta tipo Autores </returns>
        public async Task<Respuesta<IAutoresDTO>> GuardarAutoresAsync(IAutoresDTO autores)
        {
            return await this.EjecutarTransaccionAsync<Respuesta<IAutoresDTO>, AutoresDAL>(async () =>
            {
                Autores AutoresEntidad = Mapeador.MapearEntidadDTO(autores, new Autores());
                await repositorio.AgregarAsync(AutoresEntidad);
                await repositorio.GuardarAsync();
                respuesta.Entidades.Add(AutoresEntidad);
                return respuesta;

            });
        }

        /// <summary>
        /// Metodo Guardar lista autores
        /// </summary>
        /// <param name="listaAutores">Lista de entidades a guardar</param>
        /// <returns>Respuesta tipo Autores </returns>
        public async Task<Respuesta<IAutoresDTO>> GuardarListaAutoresAsync(List<IAutoresDTO> listaAutores)
        {
            return await this.EjecutarTransaccionAsync<Respuesta<IAutoresDTO>, AutoresDAL>(async () =>
            {
                List<Autores> listaAutoresEntidad = Mapeador.MapearALista<IAutoresDTO, Autores>(listaAutores);
                await repositorio.AgregarListaAsync(listaAutoresEntidad);
                respuesta.Entidades.AddRange(listaAutoresEntidad);
                return respuesta;
            });
        }

        /// <summary>
        /// Metodo consultar lista autores
        /// </summary>
        /// <returns>Respuesta tipo Autores </returns>
        public async Task<Respuesta<IAutoresDTO>> ConsultarListaAutoresAsync()
        {
            return await this.EjecutarTransaccionAsync<Respuesta<IAutoresDTO>, AutoresDAL>(async () =>
            {
                respuesta.Entidades = repositorio.BuscarTodos().ToList<IAutoresDTO>();
                return respuesta;

            });
        }

        /// <summary>
        /// Metodo consultar lista autores por filtro 
        /// </summary>
        /// <param name="autores">Entidad con los datos a filtrar</param>
        /// <param name="filtro">Filtro de las entidades </param>
        /// <returns>Respuesta tipo Autores </returns>
        public async Task<Respuesta<IAutoresDTO>> ConsultarListaAutoresPorFiltroAsync(Expression<Func<IAutoresDTO, bool>> filtro)
        {
            return await this.EjecutarTransaccionAsync<Respuesta<IAutoresDTO>, AutoresDAL>(async () =>
            {
                respuesta.Entidades = repositorio.BuscarPor(Mapeador.MapearExpresion<IAutoresDTO, Autores>(filtro)).ToList<IAutoresDTO>();
                return respuesta;

            });
        }

        /// <summary>
        /// Metodo consultar por llave autores
        /// </summary>
        /// <param name="autores">Entidad a consultar</param>
        /// <returns>Respuesta tipo Autores </returns>
        public async Task<Respuesta<IAutoresDTO>> ConsultarAutoresFiltroAsync(IAutoresDTO autores)
        {
            return await this.EjecutarTransaccionAsync<Respuesta<IAutoresDTO>, AutoresDAL>(async () =>
            {
                respuesta.Entidades = (from entidad in ContextoBD.Autores
                                       where entidad.AutorID == autores.AutorID
                                       || entidad.Nombre.Contains(autores.Nombre)
                                       || entidad.Apellido.Contains(autores.Nombre)
                                       || entidad.Nacionalidad.Contains(autores.Nacionalidad)
                                       || entidad.FechaNacimiento == (autores.FechaNacimiento)
                                       select entidad).ToList<IAutoresDTO>();

                return respuesta;

            });
        }
    }
}

