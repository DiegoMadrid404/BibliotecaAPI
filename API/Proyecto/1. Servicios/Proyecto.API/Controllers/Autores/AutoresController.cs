// ------------------------------------------------------------------------------------
// <copyright file="AutoresController.cs" company="DIEGOMADRID26@GMAIL.COM">
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
	/// Clase con las capaciades Rest de la entidad Autores
	/// </summary>
	[Route("api/Autores")]
	public class AutoresController : AccesoComunAPI
	{


		/// <summary>
		/// Objeto para negocio Autores
		/// </summary>
		private Lazy<IAutoresNegocioAccion> negocioAutores;

		
		/// <summary>
		/// Inicializa una nueva instancia de la clase AutoresController
		/// </summary>
		public AutoresController()
		{
			this.negocioAutores = new Lazy<IAutoresNegocioAccion>(
										() => new AutoresBL());
		}
		
		/// <summary>
		/// Metodo consultar lista autores
		/// </summary>
		/// <returns>Respuesta tipo Autores </returns>
		[HttpGet]
		[Route("ConsultarListaAutores")]
		public async Task<Respuesta<Autores>> ConsultarListaAutores()
		{
			return await this.EjecutarTransaccionAPI<Task<Respuesta<Autores>>, AutoresController>(async () =>
			{
				return Mapeador.MapearObjetoPorJson<Respuesta<Autores>>(await negocioAutores.Value.ConsultarListaAutoresAsync());
			});
		}
		
		/// <summary>
		/// Metodo consultar por llave autores
		/// </summary>
		/// <param name="autores">Entidad a consultar</param>
		/// <returns>Respuesta tipo Autores </returns>
		[HttpPost]
		[Route("ConsultarAutoresFiltro")]
		public async Task<Respuesta<Autores>> ConsultarAutoresFiltro([FromBody]Autores autores)
		{
			return await this.EjecutarTransaccionAPI<Task<Respuesta<Autores>>, AutoresController>(async () =>
			{
				return Mapeador.MapearObjetoPorJson<Respuesta<Autores>>(await negocioAutores.Value.ConsultarAutoresFiltroAsync(autores));
			});
		}
		
		/// <summary>
		/// Metodo guardar autores
		/// </summary>
		/// <param name="autores">Entidad a guardar</param>
		/// <returns>Respuesta tipo Autores </returns>
		[HttpPost]
		[Route("GuardarAutores")]
		public async Task<Respuesta<Autores>> GuardarAutores([FromBody]Autores autores)
		{
			return await this.EjecutarTransaccionAPI<Task<Respuesta<Autores>>, AutoresController>(async () =>
			{
				return Mapeador.MapearObjetoPorJson<Respuesta<Autores>>(await negocioAutores.Value.GuardarAutoresAsync(autores));
			});
		}
		
		/// <summary>
		/// Metodo editar autores
		/// </summary>
		/// <param name="autores">Entidad a editar</param>
		/// <returns>Respuesta tipo Autores </returns>
		[HttpPut]
		[Route("EditarAutores")]
		public async Task<Respuesta<Autores>> EditarAutores([FromBody]Autores autores)
		{
			return await this.EjecutarTransaccionAPI<Task<Respuesta<Autores>>, AutoresController>(async () =>
			{
				return Mapeador.MapearObjetoPorJson<Respuesta<Autores>>(await negocioAutores.Value.EditarAutoresAsync(autores));
			});
		}
		
		/// <summary>
		/// Metodo eliminar autores
		/// </summary>
		/// <param name="autores">Entidad a eliminar</param>
		/// <returns>Respuesta tipo Autores </returns>
		[HttpDelete]
		[Route("EliminarAutores")]
		public async Task<Respuesta<Autores>> EliminarAutores( Guid idAutor)
		{
			return await this.EjecutarTransaccionAPI<Task<Respuesta<Autores>>, AutoresController>(async () =>
			{
				return Mapeador.MapearObjetoPorJson<Respuesta<Autores>>(await negocioAutores.Value.EliminarAutoresAsync(idAutor));
			});
		}
	}
}

