// ------------------------------------------------------------------------------------
// <copyright file="LibrosAutores.cs" company="DIEGOMADRID26@GMAIL.COM">
// Copyright (c) DIEGOMADRID26@GMAIL.COM. All rights reserved.
// </copyright>
// <author>DIEGO MADRID</author>
// ------------------------------------------------------------------------------------
namespace Proyecto.API.Models.Repositorio
{
    using Proyecto.IC.DTO.Repositorio;
    using System;

    /// <summary>
    /// clase para las propiedades de la entidad LibrosAutores
    /// </summary>
    public class LibrosAutoresConsulta : ILibrosAutoresConsultaDTO
    {

        /// <summary>
        /// Obtiene o establece e lLibro Autor ID
        /// </summary>
        public Guid LibroAutorID { get; set; }
        /// <summary>
        /// Obtiene o establece el Libro I D
        /// </summary>
        public Guid LibroID { get; set; }

        /// <summary>
        /// Obtiene o establece el Autor I D
        /// </summary>
        public Guid AutorID { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
    }
}
