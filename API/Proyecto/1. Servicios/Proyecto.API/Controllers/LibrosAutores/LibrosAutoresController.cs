// ------------------------------------------------------------------------------------
// <copyright file="LibrosAutoresController.cs" company="DIEGOMADRID26@GMAIL.COM">
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
	/// Clase con las capaciades Rest de la entidad LibrosAutores
	/// </summary>
	[Route("api/LibrosAutores")]
	public class LibrosAutoresController : AccesoComunAPI
	{


		/// <summary>
		/// Objeto para negocio LibrosAutores
		/// </summary>
		private Lazy<ILibrosAutoresNegocioAccion> negocioLibrosAutores;

		
		/// <summary>
		/// Inicializa una nueva instancia de la clase LibrosAutoresController
		/// </summary>
		public LibrosAutoresController()
		{
			this.negocioLibrosAutores = new Lazy<ILibrosAutoresNegocioAccion>(
										() => new LibrosAutoresBL());
		}
		
		/// <summary>
		/// Metodo consultar lista librosAutores
		/// </summary>
		/// <returns>Respuesta tipo LibrosAutores </returns>
		[HttpGet]
		[Route("ConsultarListaLibrosAutores")]
		public async Task<Respuesta<LibrosAutores>> ConsultarListaLibrosAutores()
		{
			return await this.EjecutarTransaccionAPI<Task<Respuesta<LibrosAutores>>, LibrosAutoresController>(async () =>
			{
				return Mapeador.MapearObjetoPorJson<Respuesta<LibrosAutores>>(await negocioLibrosAutores.Value.ConsultarListaLibrosAutoresAsync());
			});
		}
		
		/// <summary>
		/// Metodo consultar lista librosAutores
		/// </summary>
		/// <returns>Respuesta tipo LibrosAutores </returns>
		[HttpGet]
		[Route("ConsultarLibrosAutoresLinq")]
		public async Task<Respuesta<LibrosAutoresConsulta>> ConsultarLibrosAutoresLinq()
		{
			return await this.EjecutarTransaccionAPI<Task<Respuesta<LibrosAutoresConsulta>>, LibrosAutoresController>(async () =>
			{
				return Mapeador.MapearObjetoPorJson<Respuesta<LibrosAutoresConsulta>>(await negocioLibrosAutores.Value.ConsultarLibrosAutoresLinqAsync());
			});
		}

        /// <summary>
        /// Metodo consultar lista librosAutores
        /// </summary>
        /// <returns>Respuesta tipo LibrosAutores </returns>
        [HttpGet]
        [Route("ConsultarLibrosAutoresProcedimiento")]
        public async Task<Respuesta<LibrosAutoresConsulta>> ConsultarLibrosAutoresProcedimiento()
        {
            return await this.EjecutarTransaccionAPI<Task<Respuesta<LibrosAutoresConsulta>>, LibrosAutoresController>(async () =>
            {
                return Mapeador.MapearObjetoPorJson<Respuesta<LibrosAutoresConsulta>>(await negocioLibrosAutores.Value.ConsultarLibrosAutoresProcedimientoAsync());
            });
        }
 
		
		/// <summary>
		/// Metodo guardar librosAutores
		/// </summary>
		/// <param name="librosAutores">Entidad a guardar</param>
		/// <returns>Respuesta tipo LibrosAutores </returns>
		[HttpPost]
		[Route("GuardarLibrosAutores")]
		public async Task<Respuesta<LibrosAutores>> GuardarLibrosAutores([FromBody]LibrosAutores librosAutores)
		{
			return await this.EjecutarTransaccionAPI<Task<Respuesta<LibrosAutores>>, LibrosAutoresController>(async () =>
			{
				return Mapeador.MapearObjetoPorJson<Respuesta<LibrosAutores>>(await negocioLibrosAutores.Value.GuardarLibrosAutoresAsync(librosAutores));
			});
		}
		
		/// <summary>
		/// Metodo editar librosAutores
		/// </summary>
		/// <param name="librosAutores">Entidad a editar</param>
		/// <returns>Respuesta tipo LibrosAutores </returns>
		[HttpPut]
		[Route("EditarLibrosAutores")]
		public async Task<Respuesta<LibrosAutores>> EditarLibrosAutores([FromBody]LibrosAutores librosAutores)
		{
			return await this.EjecutarTransaccionAPI<Task<Respuesta<LibrosAutores>>, LibrosAutoresController>(async () =>
			{
				return Mapeador.MapearObjetoPorJson<Respuesta<LibrosAutores>>(await negocioLibrosAutores.Value.EditarLibrosAutoresAsync(librosAutores));
			});
		}
		
		/// <summary>
		/// Metodo eliminar librosAutores
		/// </summary>
		/// <param name="librosAutores">Entidad a eliminar</param>
		/// <returns>Respuesta tipo LibrosAutores </returns>
		[HttpDelete]
		[Route("EliminarLibrosAutores")]
		public async Task<Respuesta<LibrosAutores>> EliminarLibrosAutores(Guid idLibroAutor)
		{
			return await this.EjecutarTransaccionAPI<Task<Respuesta<LibrosAutores>>, LibrosAutoresController>(async () =>
			{
				return Mapeador.MapearObjetoPorJson<Respuesta<LibrosAutores>>(await negocioLibrosAutores.Value.EliminarLibrosAutoresAsync(idLibroAutor));
			});
		}
	}
}

