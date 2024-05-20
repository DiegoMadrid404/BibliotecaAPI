// ------------------------------------------------------------------------------------
// <copyright file="ILibrosAutoresDTO.cs" company="DIEGOMADRID26@GMAIL.COM">
// Copyright (c) DIEGOMADRID26@GMAIL.COM. All rights reserved.
// </copyright>
// <author>DIEGO MADRID</author>
// ------------------------------------------------------------------------------------
namespace Proyecto.IC.DTO.Repositorio
{
	using System;

	/// <summary>
	/// Interface que define las propiedades de Libros Autores
	/// </summary>
	public interface ILibrosAutoresDTO
    {

        /// <summary>
        /// Obtiene o establece e lLibro Autor ID
        /// </summary>
        public Guid LibroAutorID { get; set; }
        /// <summary>
        /// Obtiene o establece el Libro I D
        /// </summary>
        Guid LibroID { get; set; }

        /// <summary>
        /// Obtiene o establece el Autor I D
        /// </summary>
        Guid AutorID { get; set; }
	}
}
