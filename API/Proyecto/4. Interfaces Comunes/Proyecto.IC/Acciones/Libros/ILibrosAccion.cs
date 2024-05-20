// ------------------------------------------------------------------------------------
// <copyright file="ILibrosAccion.cs" company="DIEGOMADRID26@GMAIL.COM">
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
	/// Interface que define las acciones que se implmentara en todas las capas de ILibrosAccion
	/// </summary>
	public interface ILibrosAccion
	{
		/// <summary>
		/// Metodo guardar libros
		/// </summary>
		/// <param name="libros">Entidad a guardar</param>
		/// <returns>Respuesta tipo Libros </returns>
		Task<Respuesta<ILibrosDTO>> GuardarLibrosAsync(ILibrosDTO libros);
		/// <summary>
		/// Metodo Guardar lista libros
		/// </summary>
		/// <param name="listaLibros">Lista de entidades a guardar</param>
		/// <returns>Respuesta tipo Libros </returns>
		Task<Respuesta<ILibrosDTO>> GuardarListaLibrosAsync(List<ILibrosDTO> listaLibros);
		/// <summary>
		/// Metodo editar libros
		/// </summary>
		/// <param name="libros">Entidad a editar</param>
		/// <returns>Respuesta tipo Libros </returns>
		Task<Respuesta<ILibrosDTO>> EditarLibrosAsync(ILibrosDTO libros);
		/// <summary>
		/// Metodo consultar lista libros
		/// </summary>
		/// <returns>Respuesta tipo Libros </returns>
		Task<Respuesta<ILibrosDTO>> ConsultarListaLibrosAsync();
		/// <summary>
		/// Metodo consultar por llave libros
		/// </summary>
		/// <param name="libros">Entidad a consultar</param>
		/// <returns>Respuesta tipo Libros </returns>
		Task<Respuesta<ILibrosDTO>> ConsultarLibrosFiltroAsync(ILibrosDTO libros);
		/// <summary>
		/// Metodo eliminar libros
		/// </summary>
		/// <param name="libros">Entidad a eliminar</param>
		/// <returns>Respuesta tipo Libros </returns>
		Task<Respuesta<ILibrosDTO>> EliminarLibrosAsync(Guid idLibro);
	}


	/// <summary>
	/// Interface que define las acciones de la capa de repositorioILibrosAccion
	/// </summary>
	public interface ILibrosRepositorioAccion:ILibrosAccion
	{
		/// <summary>
		/// Metodo editar libros por query 
		/// </summary>
		/// <param name="filtro">Filtro de las entidades </param>
		/// <param name="valor">Entidad a mofificar</param>
		/// <param name="propiedades">Propiedades a modificar</param>
		/// <returns>Respuesta tipo Libros </returns>
		Task<Respuesta<ILibrosDTO>> EditarPorQueryLibrosAsync(Expression<Func<ILibrosDTO, bool>> filtro, ILibrosDTO valor, List<string> propiedades);
		/// <summary>
		/// Metodo consultar lista libros por filtro 
		/// </summary>
		/// <param name="filtro">Filtro de las entidades </param>
		/// <returns>Respuesta tipo Libros </returns>
		Task<Respuesta<ILibrosDTO>> ConsultarListaLibrosPorFiltroAsync(Expression<Func<ILibrosDTO, bool>> filtro);
		/// <summary>
		/// Metodo editar  libros por filtro 
		/// </summary>
		/// <param name="libros">Entidad con los datos a editar</param>
		/// <param name="propiedades">Propiedades a modificar</param>
		/// <returns>Respuesta tipo Libros </returns>
		Task<Respuesta<ILibrosDTO>> EditarLibrosPorFiltroAsync(ILibrosDTO libros, params string[] propiedades);
		/// <summary>
		/// Metodo eliminar lista libros 
		/// </summary>
		/// <param name="lista">Lista de entidad a eliminar</param>
		/// <returns>Respuesta tipo Libros </returns>
		Task<Respuesta<ILibrosDTO>> EliminarListaLibrosAsync(List<ILibrosDTO> lista);
		/// <summary>
		/// Metodo editar libros lista 
		/// </summary>
		/// <param name="lista"> Lista con entidades a modificar</param>
		/// <returns>Respuesta tipo Libros </returns>
		Task<Respuesta<ILibrosDTO>> EditarListaLibrosAsync(List<ILibrosDTO> lista);
	}


	/// <summary>
	/// Interface que define las acciones de la capa de negocioILibrosAccion
	/// </summary>
	public interface ILibrosNegocioAccion:ILibrosAccion
	{
	}
}
