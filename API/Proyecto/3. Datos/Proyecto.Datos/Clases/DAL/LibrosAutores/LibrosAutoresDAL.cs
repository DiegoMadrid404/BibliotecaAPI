// ------------------------------------------------------------------------------------
// <copyright file="LibrosAutoresDAL.cs" company="DIEGOMADRID26@GMAIL.COM">
// Copyright (c) DIEGOMADRID26@GMAIL.COM. All rights reserved.
// </copyright>
// <author>DIEGO MADRID</author>
// ------------------------------------------------------------------------------------
namespace Proyecto.Datos.Clases.DAL.Repositorio
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Proyecto.Datos.Contexto;
    using Proyecto.Datos.Contexto.Repositorio;
    using Proyecto.IC.Acciones.Repositorio;
    using Proyecto.IC.DTO.Repositorio;
    using Proyecto.Datos.Utilidades;
    using Proyecto.IC.Utilidades.Genericos;
    using Proyecto.IC.Utilidades.Auxiliares;
    using Dapper;
    using Proyecto.Datos.Clases.DO.Consulta;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Clase con las acciones de repositorio para la entidad LibrosAutores
    /// </summary>
    public class LibrosAutoresDAL : AccesoComunDAL<ContextoProyecto>, ILibrosAutoresRepositorioAccion
    {
        /// <summary>
        /// Objeto para entidad respuesta
        /// </summary>
		Respuesta<ILibrosAutoresDTO> respuesta;

        /// <summary>
        /// Objeto para repositorio LibrosAutores
        /// </summary>
        RepositorioGenerico<LibrosAutores> repositorio;
        ProcedimientoGenerico procedimientoGenerico;
        ContextoProyecto dbContext;

        /// <summary>
        /// Inicializa una nueva instancia de la clase LibrosAutoresDAL
        /// </summary>
        public LibrosAutoresDAL()
        {
            this.respuesta = new Respuesta<ILibrosAutoresDTO>();
            this.repositorio = new RepositorioGenerico<LibrosAutores>(ContextoBD);

            dbContext = new ContextoProyecto();
            procedimientoGenerico = new ProcedimientoGenerico(dbContext);
        }

        /// <summary>
        /// Metodo editar librosAutores
        /// </summary>
        /// <param name="librosAutores">Entidad a editar</param>
        /// <returns>Respuesta tipo LibrosAutores </returns>
        public async Task<Respuesta<ILibrosAutoresDTO>> EditarLibrosAutoresAsync(ILibrosAutoresDTO librosAutores)
        {
            return await this.EjecutarTransaccionAsync<Respuesta<ILibrosAutoresDTO>, LibrosAutoresDAL>(async () =>
            {
                LibrosAutores LibrosAutoresEntidad = repositorio.BuscarPor(entidad => entidad
                    .LibroID == librosAutores.LibroID).FirstOrDefault();
                Mapeador.MapearObjetosPorPropiedad(librosAutores, LibrosAutoresEntidad);
                repositorio.Editar(LibrosAutoresEntidad);
                repositorio.Guardar();
                return respuesta;
            });
        }

        /// <summary>
        /// Metodo editar  librosAutores por filtro 
        /// </summary>
        /// <param name="librosAutores">Entidad con los datos a editar</param>
        /// <param name="propiedades">Propiedades a modificar</param>
        /// <returns>Respuesta tipo LibrosAutores </returns>
        public async Task<Respuesta<ILibrosAutoresDTO>> EditarLibrosAutoresPorFiltroAsync(ILibrosAutoresDTO librosAutores, params string[] propiedades)
        {
            return await this.EjecutarTransaccionAsync<Respuesta<ILibrosAutoresDTO>, LibrosAutoresDAL>(async () =>
            {
                LibrosAutores librosAutoresEntidad = repositorio.BuscarPor(entidad => entidad
                .LibroID == librosAutores.LibroID).FirstOrDefault();
                Mapeador.MapearObjetosPorPropiedadPorFiltro(librosAutores, librosAutoresEntidad, propiedades);
                repositorio.Editar(librosAutoresEntidad);
                repositorio.Guardar();
                return respuesta;
            });
        }


        /// <summary>
        /// Metodo editar librosAutores por query 
        /// </summary>
        /// <param name="filtro">Filtro de las entidades </param>
        /// <param name="valor">Entidad a mofificar</param>
        /// <param name="propiedades">Propiedades a modificar</param>
        /// <returns>Respuesta tipo LibrosAutores </returns>
        public async Task<Respuesta<ILibrosAutoresDTO>> EditarPorQueryLibrosAutoresAsync(Expression<Func<ILibrosAutoresDTO, bool>> filtro, ILibrosAutoresDTO valor, List<string> propiedades)
        {
            return await this.EjecutarTransaccionAsync<Respuesta<ILibrosAutoresDTO>, LibrosAutoresDAL>(async () =>
            {
                LibrosAutores LibrosAutoresEntidad = Mapeador.MapearEntidadDTO(valor, new LibrosAutores());
                Expression<Func<LibrosAutores, bool>> filtros = Mapeador.MapearExpresion<ILibrosAutoresDTO, LibrosAutores>(filtro);
                await repositorio.EditarPorQueryAsync(filtros, LibrosAutoresEntidad, propiedades);
                await repositorio.GuardarAsync();
                return respuesta;
            });
        }


        /// <summary>
        /// Metodo editar librosAutores lista 
        /// </summary>
        /// <param name="lista"> Lista con entidades a modificar</param>
        /// <returns>Respuesta tipo LibrosAutores </returns>
        public async Task<Respuesta<ILibrosAutoresDTO>> EditarListaLibrosAutoresAsync(List<ILibrosAutoresDTO> lista)
        {
            return await this.EjecutarTransaccionAsync<Respuesta<ILibrosAutoresDTO>, LibrosAutoresDAL>(async () =>
            {
                List<LibrosAutores> listaLibrosAutores = new List<LibrosAutores>();
                listaLibrosAutores = Mapeador.MapearALista<ILibrosAutoresDTO, LibrosAutores>(lista);
                await repositorio.EditarListaAsync(listaLibrosAutores);
                await repositorio.GuardarAsync();
                return respuesta;
            });
        }

        /// <summary>
        /// Metodo eliminar librosAutores
        /// </summary>
        /// <param name="librosAutores">Entidad a eliminar</param>
        /// <returns>Respuesta tipo LibrosAutores </returns>
        public async Task<Respuesta<ILibrosAutoresDTO>> EliminarLibrosAutoresAsync(Guid idLibroAutor)
        {
            return await this.EjecutarTransaccionAsync<Respuesta<ILibrosAutoresDTO>, LibrosAutoresDAL>(async () =>
            {
                LibrosAutores LibrosAutoresEntidad = repositorio.BuscarPor(entidad => entidad
                    .LibroAutorID == idLibroAutor).FirstOrDefault();
                 repositorio.Eliminar(LibrosAutoresEntidad);
                repositorio.Guardar();
                return respuesta;
            });
        }


        /// <summary>
        /// Metodo eliminar lista librosAutores 
        /// </summary>
        /// <param name="lista">Lista de entidad a eliminar</param>
        /// <returns>Respuesta tipo LibrosAutores </returns>
        public async Task<Respuesta<ILibrosAutoresDTO>> EliminarListaLibrosAutoresAsync(List<ILibrosAutoresDTO> lista)
        {
            return await this.EjecutarTransaccionAsync<Respuesta<ILibrosAutoresDTO>, LibrosAutoresDAL>(async () =>
            {
                List<LibrosAutores> listaLibrosAutores = new List<LibrosAutores>();
                listaLibrosAutores = Mapeador.MapearALista<ILibrosAutoresDTO, LibrosAutores>(lista);
                await repositorio.EliminarListaAsync(listaLibrosAutores);
                await repositorio.GuardarAsync();
                return respuesta;
            });
        }

        /// <summary>
        /// Metodo guardar librosAutores
        /// </summary>
        /// <param name="librosAutores">Entidad a guardar</param>
        /// <returns>Respuesta tipo LibrosAutores </returns>
        public async Task<Respuesta<ILibrosAutoresDTO>> GuardarLibrosAutoresAsync(ILibrosAutoresDTO librosAutores)
        {
            return await this.EjecutarTransaccionAsync<Respuesta<ILibrosAutoresDTO>, LibrosAutoresDAL>(async () =>
            {
                LibrosAutores LibrosAutoresEntidad = Mapeador.MapearEntidadDTO(librosAutores, new LibrosAutores());
                await repositorio.AgregarAsync(LibrosAutoresEntidad);
                await repositorio.GuardarAsync();
                respuesta.Entidades.Add(LibrosAutoresEntidad);
                return respuesta;

            });
        }

        /// <summary>
        /// Metodo Guardar lista librosAutores
        /// </summary>
        /// <param name="listaLibrosAutores">Lista de entidades a guardar</param>
        /// <returns>Respuesta tipo LibrosAutores </returns>
        public async Task<Respuesta<ILibrosAutoresDTO>> GuardarListaLibrosAutoresAsync(List<ILibrosAutoresDTO> listaLibrosAutores)
        {
            return await this.EjecutarTransaccionAsync<Respuesta<ILibrosAutoresDTO>, LibrosAutoresDAL>(async () =>
            {
                List<LibrosAutores> listaLibrosAutoresEntidad = Mapeador.MapearALista<ILibrosAutoresDTO, LibrosAutores>(listaLibrosAutores);
                await repositorio.AgregarListaAsync(listaLibrosAutoresEntidad);
                respuesta.Entidades.AddRange(listaLibrosAutoresEntidad);
                return respuesta;
            });
        }

        /// <summary>
        /// Metodo consultar lista librosAutores
        /// </summary>
        /// <returns>Respuesta tipo LibrosAutores </returns>
        public async Task<Respuesta<ILibrosAutoresDTO>> ConsultarListaLibrosAutoresAsync()
        {
            return await this.EjecutarTransaccionAsync<Respuesta<ILibrosAutoresDTO>, LibrosAutoresDAL>(async () =>
            {
                respuesta.Entidades = repositorio.BuscarTodos().ToList<ILibrosAutoresDTO>();
                return respuesta;

            });
        }

        /// <summary>
        /// Metodo consultar por llave usuario
        /// </summary>
        /// <param name="usuario">Entidad a consultar</param>
        /// <returns>Respuesta tipo Usuario </returns>
        public async Task<Respuesta<ILibrosAutoresConsultaDTO>> ConsultarLibrosAutoresProcedimientoAsync()
        {
            return await this.EjecutarTransaccionAsync<Respuesta<ILibrosAutoresConsultaDTO>, LibrosAutoresDAL>(async () =>
            {

                Respuesta<ILibrosAutoresConsultaDTO> respuestaLibrosAutores = new Respuesta<ILibrosAutoresConsultaDTO>();

                // Crear un diccionario de parámetros para el procedimiento almacenado
                //var parametros = new Dictionary<string, object>();

                //Agregar las propiedades  de los parametros si se necesitan
                //parametros.Add(nameof(x.id), x.id);
                //parametros.Add(nameof(x.nombre), x.nombre); 

                // Ejecutar el procedimiento almacenado con los parámetros proporcionados
                respuestaLibrosAutores.Entidades = procedimientoGenerico.EjecutarProcedimiento<LibrosAutoresConsultaDO>("ConsultarListaLibrosAutores").ToList<ILibrosAutoresConsultaDTO>();
                //respuestaLibrosAutores.Entidades = procedimientoGenerico.EjecutarProcedimiento<LibrosAutoresConsultaDO>("ConsultarListaLibrosAutores"/*, parametros*/).ToList<ILibrosAutoresConsultaDTO>();

 
                return respuestaLibrosAutores;
            });
        }
        

        /// <summary>
        /// Metodo consultar lista librosAutores por filtro 
        /// </summary>
        /// <param name="librosAutores">Entidad con los datos a filtrar</param>
        /// <param name="filtro">Filtro de las entidades </param>
        /// <returns>Respuesta tipo LibrosAutores </returns>
        public async Task<Respuesta<ILibrosAutoresDTO>> ConsultarListaLibrosAutoresPorFiltroAsync(Expression<Func<ILibrosAutoresDTO, bool>> filtro)
        {
            return await this.EjecutarTransaccionAsync<Respuesta<ILibrosAutoresDTO>, LibrosAutoresDAL>(async () =>
            {
                respuesta.Entidades = repositorio.BuscarPor(Mapeador.MapearExpresion<ILibrosAutoresDTO, LibrosAutores>(filtro)).ToList<ILibrosAutoresDTO>();
                return respuesta;

            });
        }

        /// <summary>
        /// Metodo consultar por llave librosAutores
        /// </summary>
        /// <param name="librosAutores">Entidad a consultar</param>
        /// <returns>Respuesta tipo LibrosAutores </returns>
        public async Task<Respuesta<ILibrosAutoresDTO>> ConsultarLibrosAutoresLlaveAsync(ILibrosAutoresDTO librosAutores)
        {
            return await this.EjecutarTransaccionAsync<Respuesta<ILibrosAutoresDTO>, LibrosAutoresDAL>(async () =>
            {
                respuesta.Entidades = (from entidad in ContextoBD.LibrosAutores
                                       where entidad.LibroID == librosAutores.LibroID
                                       select entidad).ToList<ILibrosAutoresDTO>();

                return respuesta;

            });
        }
    }
}

