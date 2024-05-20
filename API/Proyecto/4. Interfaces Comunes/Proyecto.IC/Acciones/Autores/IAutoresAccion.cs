// ------------------------------------------------------------------------------------
// <copyright file="IAutoresAccion.cs" company="DIEGOMADRID26@GMAIL.COM">
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
	/// Interface que define las acciones que se implmentara en todas las capas de IAutoresAccion
	/// </summary>
	public interface IAutoresAccion
	{
		/// <summary>
		/// Metodo guardar autores
		/// </summary>
		/// <param name="autores">Entidad a guardar</param>
		/// <returns>Respuesta tipo Autores </returns>
		Task<Respuesta<IAutoresDTO>> GuardarAutoresAsync(IAutoresDTO autores);
		/// <summary>
		/// Metodo Guardar lista autores
		/// </summary>
		/// <param name="listaAutores">Lista de entidades a guardar</param>
		/// <returns>Respuesta tipo Autores </returns>
		Task<Respuesta<IAutoresDTO>> GuardarListaAutoresAsync(List<IAutoresDTO> listaAutores);
		/// <summary>
		/// Metodo editar autores
		/// </summary>
		/// <param name="autores">Entidad a editar</param>
		/// <returns>Respuesta tipo Autores </returns>
		Task<Respuesta<IAutoresDTO>> EditarAutoresAsync(IAutoresDTO autores);
		/// <summary>
		/// Metodo consultar lista autores
		/// </summary>
		/// <returns>Respuesta tipo Autores </returns>
		Task<Respuesta<IAutoresDTO>> ConsultarListaAutoresAsync();
		/// <summary>
		/// Metodo consultar por llave autores
		/// </summary>
		/// <param name="autores">Entidad a consultar</param>
		/// <returns>Respuesta tipo Autores </returns>
		Task<Respuesta<IAutoresDTO>> ConsultarAutoresFiltroAsync(IAutoresDTO autores);
		/// <summary>
		/// Metodo eliminar autores
		/// </summary>
		/// <param name="autores">Entidad a eliminar</param>
		/// <returns>Respuesta tipo Autores </returns>
		Task<Respuesta<IAutoresDTO>> EliminarAutoresAsync(Guid idAutor);
	}


	/// <summary>
	/// Interface que define las acciones de la capa de repositorioIAutoresAccion
	/// </summary>
	public interface IAutoresRepositorioAccion:IAutoresAccion
	{
		/// <summary>
		/// Metodo editar autores por query 
		/// </summary>
		/// <param name="filtro">Filtro de las entidades </param>
		/// <param name="valor">Entidad a mofificar</param>
		/// <param name="propiedades">Propiedades a modificar</param>
		/// <returns>Respuesta tipo Autores </returns>
		Task<Respuesta<IAutoresDTO>> EditarPorQueryAutoresAsync(Expression<Func<IAutoresDTO, bool>> filtro, IAutoresDTO valor, List<string> propiedades);
		/// <summary>
		/// Metodo consultar lista autores por filtro 
		/// </summary>
		/// <param name="filtro">Filtro de las entidades </param>
		/// <returns>Respuesta tipo Autores </returns>
		Task<Respuesta<IAutoresDTO>> ConsultarListaAutoresPorFiltroAsync(Expression<Func<IAutoresDTO, bool>> filtro);
		/// <summary>
		/// Metodo editar  autores por filtro 
		/// </summary>
		/// <param name="autores">Entidad con los datos a editar</param>
		/// <param name="propiedades">Propiedades a modificar</param>
		/// <returns>Respuesta tipo Autores </returns>
		Task<Respuesta<IAutoresDTO>> EditarAutoresPorFiltroAsync(IAutoresDTO autores, params string[] propiedades);
		/// <summary>
		/// Metodo eliminar lista autores 
		/// </summary>
		/// <param name="lista">Lista de entidad a eliminar</param>
		/// <returns>Respuesta tipo Autores </returns>
		Task<Respuesta<IAutoresDTO>> EliminarListaAutoresAsync(List<IAutoresDTO> lista);
		/// <summary>
		/// Metodo editar autores lista 
		/// </summary>
		/// <param name="lista"> Lista con entidades a modificar</param>
		/// <returns>Respuesta tipo Autores </returns>
		Task<Respuesta<IAutoresDTO>> EditarListaAutoresAsync(List<IAutoresDTO> lista);
	}


	/// <summary>
	/// Interface que define las acciones de la capa de negocioIAutoresAccion
	/// </summary>
	public interface IAutoresNegocioAccion:IAutoresAccion
	{
	}
}
