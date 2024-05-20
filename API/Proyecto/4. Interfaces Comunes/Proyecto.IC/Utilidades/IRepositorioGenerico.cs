/// <summary>
/// Archivo para la Interface de Repositorio Generico
/// </summary>
/// <remarks>
/// Autor: Diego Madrid 
/// </remarks>
namespace Proyecto.IC.Acciones.Utilidades
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface de Repositorio Generico
    /// </summary>
    public interface IRepositorioGenerico<T>
    {
        /// <summary>
        /// Consultar todos los registros de la base de datos
        /// </summary>
        /// <returns>Todos los registros de la entidad</returns>
        IQueryable<T> BuscarTodos();

        /// <summary>
        /// Consultar registros de la base de datos segun filtro
        /// </summary>
        /// <param name="filtro">filtro de los datos a consultar</param>
        /// <returns></returns>
        IQueryable<T> BuscarPor(Expression<Func<T, bool>> filtro);

        /// <summary>
        /// Inserta un registro en la base de datos
        /// </summary>
        /// <param name="entidad">Registro a Insertar</param>
        void Agregar(T entidad);

        /// <summary>
        /// Inserta una lista de registros en la base de datos
        /// </summary>
        /// <param name="listaEntidad">Lista de Registros</param>
        void AgregarLista(List<T> listaEntidadentidad);

        /// <summary>
        /// Eliminar un registro en la base de datos
        /// </summary>
        /// <param name="entidad">Registro a Eliminar</param>
        void Eliminar(T entidad);

        /// <summary>
        /// Eliminar lista de registros en la base de datos
        /// </summary>
        /// <param name="listaEntidad">filtro de los datos a eliminar</param>
        void EliminarLista(List<T> listaEntidad);

        /// <summary>
        /// Eliminar registros en la base de datos segun filtro
        /// </summary>
        /// <param name="filtro">The filtro.</param>
        void EliminarPorQuery(Expression<Func<T, bool>> filtro);

        /// <summary>
        /// Editar un registro en la base de datos
        /// </summary>
        /// <param name="entidad">Registro a Editar</param>
        void Editar(T entidad);

        /// <summary>
        /// Editar una lista de registros en la base de datos
        /// </summary>
        /// <param name="listaEntidad">Lista con los registros a Editar</param>
        /// <param name="propiedades">Campos a modificar</param>
        void EditarLista(List<T> listaEntidad);

        /// <summary>
        /// EditaR por query LINQ.
        /// </summary>
        /// <param name="filtro">The filtro.</param>
        /// <param name="propiedades">The propiedades.</param>
        /// <param name="Modificadores">The modificadores.</param>
        void EditarPorQuery(Expression<Func<T, bool>> filtro, Expression<Func<T, T>> propiedades);

        /// <summary>
        /// Editars the por query.
        /// </summary>
        /// <param name="filtro">The filtro.</param>
        /// <param name="valor">The valor.</param>
        /// <param name="propiedades">The propiedades.</param>
        void EditarPorQuery(Expression<Func<T, bool>> filtro, T valor, List<string> propiedades);

        /// <summary>
        /// realiza Commit de la transaccion
        /// </summary>
        void Guardar();

        /// <summary>
        /// Realiza commit de forma asincrona de la transacción
        /// </summary>
        Task GuardarAsync();

        /// <summary>
        /// Inserta un registro en la base de datos de forma asincrona
        /// </summary>
        /// <param name="entidad">Registro a Insertar</param>
        Task AgregarAsync(T entidad);

        /// <summary>
        /// Inserta una lista de registros en la base de datos de forma asincrona
        /// </summary>
        /// <param name="listaEntidad">Lista de Registros</param>
        Task AgregarListaAsync(List<T> listaEntidadentidad);

        /// <summary>
        /// Eliminar lista de registros en la base de datos de forma asincrona
        /// </summary>
        /// <param name="listaEntidad">filtro de los datos a eliminar</param>
        Task EliminarListaAsync(List<T> listaEntidad);

        /// <summary>
        /// Eliminar registros en la base de datos segun filtro de forma asincrona
        /// </summary>
        /// <param name="filtro">The filtro.</param>
        Task EliminarPorQueryAsync(Expression<Func<T, bool>> filtro);

        /// <summary>
        /// Editar una lista de registros en la base de datos de forma asincrona
        /// </summary>
        /// <param name="listaEntidad">Lista con los registros a Editar</param>
        /// <param name="propiedades">Campos a modificar</param>
        Task EditarListaAsync(List<T> listaEntidad);

        /// <summary>
        /// Editar por query LINQ. de forma asincrona
        /// </summary>
        /// <param name="filtro">The filtro.</param>
        /// <param name="propiedades">The propiedades.</param>
        /// <param name="Modificadores">The modificadores.</param>
        Task EditarPorQueryAsync(Expression<Func<T, bool>> filtro, Expression<Func<T, T>> propiedades);

        /// <summary>
        /// Editar por query de forma asincrona
        /// </summary>
        /// <param name="filtro">The filtro.</param>
        /// <param name="valor">The valor.</param>
        /// <param name="propiedades">The propiedades.</param>
        Task EditarPorQueryAsync(Expression<Func<T, bool>> filtro, T valor, List<string> propiedades);
    }
}