// ------------------------------------------------------------------------------------
// <copyright file="ILibrosDTO.cs" company="DIEGOMADRID26@GMAIL.COM">
// Copyright (c) DIEGOMADRID26@GMAIL.COM. All rights reserved.
// </copyright>
// <author>DIEGO MADRID</author>
// ------------------------------------------------------------------------------------
namespace Proyecto.Datos.Contexto.Repositorio
{
	using System;

	/// <summary>
	/// Interface que define las propiedades de Libros
	/// </summary>
	public partial class Libros
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
