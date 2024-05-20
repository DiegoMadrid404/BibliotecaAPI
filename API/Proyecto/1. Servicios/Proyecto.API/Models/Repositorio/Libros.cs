// ------------------------------------------------------------------------------------
// <copyright file="Libros.cs" company="DIEGOMADRID26@GMAIL.COM">
// Copyright (c) DIEGOMADRID26@GMAIL.COM. All rights reserved.
// </copyright>
// <author>DIEGO MADRID</author>
// ------------------------------------------------------------------------------------
namespace Proyecto.API.Models.Repositorio
{
	using Proyecto.IC.DTO.Repositorio;
	using System;

	/// <summary>
	/// clase para las propiedades de la entidad Libros
	/// </summary>
	public class Libros : ILibrosDTO
	{
		/// <summary>
		/// Obtiene o establece el Libro I D
		/// </summary>
		public Guid LibroID { get; set; }

		/// <summary>
		/// Obtiene o establece el Titulo
		/// </summary>
		public string Titulo { get; set; }

		/// <summary>
		/// Obtiene o establece el Fecha Publicacion
		/// </summary>
		public DateTime? FechaPublicacion { get; set; }

		/// <summary>
		/// Obtiene o establece el Genero
		/// </summary>
		public string Genero { get; set; }

		/// <summary>
		/// Obtiene o establece el I S B N
		/// </summary>
		public string ISBN { get; set; }

	}
}
