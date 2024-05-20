// ------------------------------------------------------------------------------------
// <copyright file="IAutoresDTO.cs" company="DIEGOMADRID26@GMAIL.COM">
// Copyright (c) DIEGOMADRID26@GMAIL.COM. All rights reserved.
// </copyright>
// <author>DIEGO MADRID</author>
// ------------------------------------------------------------------------------------
namespace Proyecto.IC.DTO.Repositorio
{
	using System;

	/// <summary>
	/// Interface que define las propiedades de Autores
	/// </summary>
	public interface IAutoresDTO
	{
        /// <summary>
        /// Obtiene o establece el Autor I D
        /// </summary>
        Guid AutorID { get; set; }

		/// <summary>
		/// Obtiene o establece el Nombre
		/// </summary>
		string Nombre { get; set; }

		/// <summary>
		/// Obtiene o establece el Apellido
		/// </summary>
		string Apellido { get; set; }

		/// <summary>
		/// Obtiene o establece el Fecha Nacimiento
		/// </summary>
		DateTime FechaNacimiento { get; set; }

		/// <summary>
		/// Obtiene o establece el Nacionalidad
		/// </summary>
		string Nacionalidad { get; set; }
	}
}
