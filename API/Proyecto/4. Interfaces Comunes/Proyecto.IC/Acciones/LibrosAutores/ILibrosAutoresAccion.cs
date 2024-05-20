// ------------------------------------------------------------------------------------
// <copyright file="ILibrosAutoresAccion.cs" company="DIEGOMADRID26@GMAIL.COM">
// Copyright (c) DIEGOMADRID26@GMAIL.COM. All rights reserved.
// </copyright>
// <author>DIEGO MADRID</author>
// ------------------------------------------------------------------------------------
namespace Proyecto.IC.Acciones.Repositorio
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Proyecto.IC.DTO.Repositorio;
    using Proyecto.IC.Utilidades.Genericos;
    using Proyecto.IC.Utilidades.Auxiliares;

    /// <summary>
    /// Interface que define las acciones que se implmentara en todas las capas de ILibrosAutoresAccion
    /// </summary>
    public interface ILibrosAutoresAccion
    {
        /// <summary>
        /// Metodo guardar librosAutores
        /// </summary>
        /// <param name="librosAutores">Entidad a guardar</param>
        /// <returns>Respuesta tipo LibrosAutores </returns>
        Task<Respuesta<ILibrosAutoresDTO>> GuardarLibrosAutoresAsync(ILibrosAutoresDTO librosAutores);
        /// <summary>
        /// Metodo Guardar lista librosAutores
        /// </summary>
        /// <param name="listaLibrosAutores">Lista de entidades a guardar</param>
        /// <returns>Respuesta tipo LibrosAutores </returns>
        Task<Respuesta<ILibrosAutoresDTO>> GuardarListaLibrosAutoresAsync(List<ILibrosAutoresDTO> listaLibrosAutores);
        /// <summary>
        /// Metodo editar librosAutores
        /// </summary>
        /// <param name="librosAutores">Entidad a editar</param>
        /// <returns>Respuesta tipo LibrosAutores </returns>
        Task<Respuesta<ILibrosAutoresDTO>> EditarLibrosAutoresAsync(ILibrosAutoresDTO librosAutores);
        /// <summary>
        /// Metodo consultar lista librosAutores
        /// </summary>
        /// <returns>Respuesta tipo LibrosAutores </returns>
        Task<Respuesta<ILibrosAutoresDTO>> ConsultarListaLibrosAutoresAsync();
        /// <summary>
        /// Metodo consultar por llave librosAutores
        /// </summary>
        /// <param name="librosAutores">Entidad a consultar</param>
        /// <returns>Respuesta tipo LibrosAutores </returns>
        Task<Respuesta<ILibrosAutoresDTO>> ConsultarLibrosAutoresLlaveAsync(ILibrosAutoresDTO librosAutores);
        /// <summary>
        /// Metodo eliminar librosAutores
        /// </summary>
        /// <param name="librosAutores">Entidad a eliminar</param>
        /// <returns>Respuesta tipo LibrosAutores </returns>
        Task<Respuesta<ILibrosAutoresDTO>> EliminarLibrosAutoresAsync(Guid idLibroAutor);

        Task<Respuesta<ILibrosAutoresConsultaDTO>> ConsultarLibrosAutoresProcedimientoAsync();

    }


    /// <summary>
    /// Interface que define las acciones de la capa de repositorioILibrosAutoresAccion
    /// </summary>
    public interface ILibrosAutoresRepositorioAccion : ILibrosAutoresAccion
    {
        /// <summary>
        /// Metodo editar librosAutores por query 
        /// </summary>
        /// <param name="filtro">Filtro de las entidades </param>
        /// <param name="valor">Entidad a mofificar</param>
        /// <param name="propiedades">Propiedades a modificar</param>
        /// <returns>Respuesta tipo LibrosAutores </returns>
        Task<Respuesta<ILibrosAutoresDTO>> EditarPorQueryLibrosAutoresAsync(Expression<Func<ILibrosAutoresDTO, bool>> filtro, ILibrosAutoresDTO valor, List<string> propiedades);
        /// <summary>
        /// Metodo consultar lista librosAutores por filtro 
        /// </summary>
        /// <param name="filtro">Filtro de las entidades </param>
        /// <returns>Respuesta tipo LibrosAutores </returns>
        Task<Respuesta<ILibrosAutoresDTO>> ConsultarListaLibrosAutoresPorFiltroAsync(Expression<Func<ILibrosAutoresDTO, bool>> filtro);
        /// <summary>
        /// Metodo editar  librosAutores por filtro 
        /// </summary>
        /// <param name="librosAutores">Entidad con los datos a editar</param>
        /// <param name="propiedades">Propiedades a modificar</param>
        /// <returns>Respuesta tipo LibrosAutores </returns>
        Task<Respuesta<ILibrosAutoresDTO>> EditarLibrosAutoresPorFiltroAsync(ILibrosAutoresDTO librosAutores, params string[] propiedades);
        /// <summary>
        /// Metodo eliminar lista librosAutores 
        /// </summary>
        /// <param name="lista">Lista de entidad a eliminar</param>
        /// <returns>Respuesta tipo LibrosAutores </returns>
        Task<Respuesta<ILibrosAutoresDTO>> EliminarListaLibrosAutoresAsync(List<ILibrosAutoresDTO> lista);
        /// <summary>
        /// Metodo editar librosAutores lista 
        /// </summary>
        /// <param name="lista"> Lista con entidades a modificar</param>
        /// <returns>Respuesta tipo LibrosAutores </returns>
        Task<Respuesta<ILibrosAutoresDTO>> EditarListaLibrosAutoresAsync(List<ILibrosAutoresDTO> lista);


    }


    /// <summary>
    /// Interface que define las acciones de la capa de negocioILibrosAutoresAccion
    /// </summary>
    public interface ILibrosAutoresNegocioAccion : ILibrosAutoresAccion
    {
        Task<Respuesta<ILibrosAutoresConsultaDTO>> ConsultarLibrosAutoresLinqAsync();

    }
}
