// ------------------------------------------------------------------------------------
// <copyright file="LibrosController.cs" company="DIEGOMADRID26@GMAIL.COM">
// Copyright (c) DIEGOMADRID26@GMAIL.COM. All rights reserved.
// </copyright>
// <author>DIEGO MADRID</author>
// ------------------------------------------------------------------------------------
namespace Proyecto.API.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Proyecto.IC.Utilidades.Auxiliares;
    using Proyecto.IC.Utilidades.Genericos;
    using Proyecto.API.Models.Repositorio;
    using Proyecto.IC.Acciones.Repositorio;
    using Proyecto.Negocio.Clases.BL;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Clase con las capaciades Rest de la entidad Libros
    /// </summary>
    [Route("api/Libros")]
    public class LibrosController : AccesoComunAPI
    {


        /// <summary>
        /// Objeto para negocio Libros
        /// </summary>
        private Lazy<ILibrosNegocioAccion> negocioLibros;


        /// <summary>
        /// Inicializa una nueva instancia de la clase LibrosController
        /// </summary>
        public LibrosController()
        {
            this.negocioLibros = new Lazy<ILibrosNegocioAccion>(
                                        () => new LibrosBL());
        }

        /// <summary>
        /// Metodo consultar lista libros
        /// </summary>
        /// <returns>Respuesta tipo Libros </returns>
        [HttpGet]
        [Route("ConsultarListaLibros")]
        public async Task<Respuesta<Libros>> ConsultarListaLibros()
        {
            return await this.EjecutarTransaccionAPI<Task<Respuesta<Libros>>, LibrosController>(async () =>
            {
                return Mapeador.MapearObjetoPorJson<Respuesta<Libros>>(await negocioLibros.Value.ConsultarListaLibrosAsync());
            });
        }

        /// <summary>
        /// Metodo consultar por llave libros
        /// </summary>
        /// <param name="libros">Entidad a consultar</param>
        /// <returns>Respuesta tipo Libros </returns>
        [HttpPost]
        [Route("ConsultarLibrosLlave")]
        public async Task<Respuesta<Libros>> ConsultarLibrosFiltro([FromBody] Libros libros)
        {
            return await this.EjecutarTransaccionAPI<Task<Respuesta<Libros>>, LibrosController>(async () =>
            {
                return Mapeador.MapearObjetoPorJson<Respuesta<Libros>>(await negocioLibros.Value.ConsultarLibrosFiltroAsync(libros));
            });
        }

        /// <summary>
        /// Metodo guardar libros
        /// </summary>
        /// <param name="libros">Entidad a guardar</param>
        /// <returns>Respuesta tipo Libros </returns>
        [HttpPost]
        [Route("GuardarLibros")]
        public async Task<Respuesta<Libros>> GuardarLibros([FromBody] Libros libros)
        {
            return await this.EjecutarTransaccionAPI<Task<Respuesta<Libros>>, LibrosController>(async () =>
            {
                return Mapeador.MapearObjetoPorJson<Respuesta<Libros>>(await negocioLibros.Value.GuardarLibrosAsync(libros));
            });
        }

        /// <summary>
        /// Metodo editar libros
        /// </summary>
        /// <param name="libros">Entidad a editar</param>
        /// <returns>Respuesta tipo Libros </returns>
        [HttpPut]
        [Route("EditarLibros")]
        public async Task<Respuesta<Libros>> EditarLibros([FromBody] Libros libros)
        {
            return await this.EjecutarTransaccionAPI<Task<Respuesta<Libros>>, LibrosController>(async () =>
            {
                return Mapeador.MapearObjetoPorJson<Respuesta<Libros>>(await negocioLibros.Value.EditarLibrosAsync(libros));
            });
        }

        /// <summary>
        /// Metodo eliminar libros
        /// </summary>
        /// <param name="libros">Entidad a eliminar</param>
        /// <returns>Respuesta tipo Libros </returns>
        [HttpDelete]
        [Route("EliminarLibros")]
        public async Task<Respuesta<Libros>> EliminarLibros(Guid idLibro)
        {
            return await this.EjecutarTransaccionAPI<Task<Respuesta<Libros>>, LibrosController>(async () =>
            {
                return Mapeador.MapearObjetoPorJson<Respuesta<Libros>>(await negocioLibros.Value.EliminarLibrosAsync(idLibro));
            });
        }
    }
}

