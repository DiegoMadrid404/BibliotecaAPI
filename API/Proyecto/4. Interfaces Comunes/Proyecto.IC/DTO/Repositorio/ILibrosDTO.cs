// ------------------------------------------------------------------------------------
// <copyright file="ILibrosDTO.cs" company="DIEGOMADRID26@GMAIL.COM">
// Copyright (c) DIEGOMADRID26@GMAIL.COM. All rights reserved.
// </copyright>
// <author>DIEGO MADRID</author>
// ------------------------------------------------------------------------------------
namespace Proyecto.IC.DTO.Repositorio
{
	using System;

	/// <summary>
	/// Interface que define las propiedades de Libros
	/// </summary>
	public interface ILibrosDTO
	{
        /// <summary>
        /// Obtiene o establece el Libro I D
        /// </summary>
        Guid LibroID { get; set; }

		/// <summary>
		/// Obtiene o establece el Titulo
		/// </summary>
		string Titulo { get; set; }

		/// <summary>
		/// Obtiene o establece el Fecha Publicacion
		/// </summary>
		DateTime? FechaPublicacion { get; set; }

		/// <summary>
		/// Obtiene o establece el Genero
		/// </summary>
		string Genero { get; set; }

		/// <summary>
		/// Obtiene o establece el I S B N
		/// </summary>
		string ISBN { get; set; }
	}
}
