// ------------------------------------------------------------------------------------
// <copyright file="IAutoresDTO.cs" company="DIEGOMADRID26@GMAIL.COM">
// Copyright (c) DIEGOMADRID26@GMAIL.COM. All rights reserved.
// </copyright>
// <author>DIEGO MADRID</author>
// ------------------------------------------------------------------------------------
namespace Proyecto.Datos.Contexto.Repositorio
{
	using System;

	/// <summary>
	/// Interface que define las propiedades de Autores
	/// </summary>
	public partial class Autores
	{
		/// <summary>
		/// Obtiene o establece el Autor I D
		/// </summary>
	public Guid AutorID { get; set; }

        /// <summary>
        /// Obtiene o establece el Nombre
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Obtiene o establece el Apellido
        /// </summary>
        public string Apellido { get; set; }

        /// <summary>
        /// Obtiene o establece el Fecha Nacimiento
        /// </summary>
        public DateTime FechaNacimiento { get; set; }

        /// <summary>
        /// Obtiene o establece el Nacionalidad
        /// </summary>
        public string Nacionalidad { get; set; }
	}
}
