/// <summary>
/// Archivo para la clase de Repositorio Generico
/// </summary>
/// <remarks>
/// Autor: Diego Madrid
/// </remarks>
namespace Proyecto.Datos.Utilidades
{
    using EFCore.BulkExtensions;
    using Microsoft.EntityFrameworkCore;
    using Proyecto.Datos.Contexto;
    using Proyecto.IC.Acciones.Utilidades;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Dapper;

    /// <summary>
    /// Clase de Repositorio Generico
    /// </summary>
    public class RepositorioGenerico<T> : IRepositorioGenerico<T> where T : class
    {
        #region "ATRIBUTOS"

        private T entidad;

        /// <summary>
        /// Contexto para el Repositorio
        /// </summary>
        internal DbContext contexto; 

        #endregion "ATRIBUTOS"

        #region "CONSTRUCTORES"

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="contexto">Contexto de Entity</param>
        public RepositorioGenerico(DbContext contexto)
        {
            this.contexto = contexto; 

        }

        #endregion "CONSTRUCTORES"

        #region "INSERT"

        /// <summary>
        /// Inserta un registro en la base de datos
        /// </summary>
        /// <param name="entidad">Registro a Insertar</param>
        public void Agregar(T entidad)
        {
            contexto.Set<T>().Add(entidad);
            this.entidad = entidad;
        }

        /// <summary>
        /// Inserta una lista de registros en la base de datos
        /// </summary>
        /// <param name="listaEntidad">Lista de Registros</param>
        public void AgregarLista(List<T> listaEntidad)
        {
            contexto.BulkInsert(listaEntidad);
        }

        /// <summary>
        /// Inserta un registro en la base de datos de forma asincrona
        /// </summary>
        /// <param name="entidad">Registro a Insertar</param>
        public async Task AgregarAsync(T entidad)
        {
            await contexto.Set<T>().AddAsync(entidad);
            this.entidad = entidad;
        }

        /// <summary>
        /// Inserta una lista de registros en la base de datos de forma asincrona
        /// </summary>
        /// <param name="listaEntidad">Lista de Registros</param>
        public async Task AgregarListaAsync(List<T> listaEntidad)
        {
            await contexto.BulkInsertAsync(listaEntidad);
        }

        #endregion "INSERT"

        #region "UPDATE"

        /// <summary>
        /// Editar un registro en la base de datos
        /// </summary>
        /// <param name="entidad">Registro a Editar</param>
        public void Editar(T entidad)
        {
            contexto.Entry(entidad).State = EntityState.Modified;
            this.entidad = entidad;
        }

        /// <summary>
        /// Editar una lista de registros en la base de datos
        /// </summary>
        /// <param name="listaEntidad">Lista con los registros a Editar</param>
        /// <param name="propiedades">Campos a modificar</param>
        public void EditarLista(List<T> listaEntidad)
        {
            contexto.BulkUpdate(listaEntidad);
        }

        /// <summary>
        /// Editar por query LINQ.
        /// </summary>
        /// <param name="filtro">The filtro.</param>
        /// <param name="propiedades">The propiedades.</param>
        public void EditarPorQuery(Expression<Func<T, bool>> filtro, Expression<Func<T, T>> propiedades)
        {
            contexto.Set<T>().Where(filtro).BatchUpdate(propiedades);
        }

        /// <summary>
        /// Editars the por query.
        /// </summary>
        /// <param name="filtro">The filtro.</param>
        /// <param name="valor">The valor.</param>
        /// <param name="propiedades">The propiedades.</param>
        public void EditarPorQuery(Expression<Func<T, bool>> filtro, T valor, List<string> propiedades)
        {
            contexto.Set<T>().Where(filtro).BatchUpdate(valor, propiedades);
        }

        /// <summary>
        /// Editar una lista de registros en la base de datos de forma asincrona
        /// </summary>
        /// <param name="listaEntidad">Lista con los registros a Editar</param>
        /// <param name="propiedades">Campos a modificar</param>
        public async Task EditarListaAsync(List<T> listaEntidad)
        {
            await contexto.BulkUpdateAsync(listaEntidad);
        }

        /// <summary>
        /// Editar por query LINQ.
        /// </summary>
        /// <param name="filtro">The filtro.</param>
        /// <param name="propiedades">The propiedades.</param>
        public async Task EditarPorQueryAsync(Expression<Func<T, bool>> filtro, Expression<Func<T, T>> propiedades)
        {
            await contexto.Set<T>().Where(filtro).BatchUpdateAsync(propiedades);
        }

        /// <summary>
        /// Editars the por query de forma asincrona
        /// </summary>
        /// <param name="filtro">The filtro.</param>
        /// <param name="valor">The valor.</param>
        /// <param name="propiedades">The propiedades.</param>
        public async Task EditarPorQueryAsync(Expression<Func<T, bool>> filtro, T valor, List<string> propiedades)
        {
            await contexto.Set<T>().Where(filtro).BatchUpdateAsync(valor, propiedades);
        }

        #endregion "UPDATE"

        #region "DELETE"

        /// <summary>
        /// Eliminar un registro en la base de datos
        /// </summary>
        /// <param name="entidad">Registro a Eliminar</param>
        public void Eliminar(T entidad)
        {
            contexto.Set<T>().Remove(entidad);
            this.entidad = entidad;
        }

        /// <summary>
        /// Eliminar registros en la base de datos segun filtro
        /// </summary>
        /// <param name="listaEntidad">Lista con los registros a eliminar</param>
        public void EliminarLista(List<T> listaEntidad)
        {
            contexto.BulkDelete(listaEntidad);
        }

        /// <summary>
        /// Eliminar registros en la base de datos segun filtro
        /// </summary>
        /// <param name="filtro">The filtro.</param>
        public void EliminarPorQuery(Expression<Func<T, bool>> filtro)
        {
            contexto.Set<T>().Where(filtro).BatchDelete();
        }

        /// <summary>
        /// Eliminar registros en la base de datos segun filtro de forma asincrona
        /// </summary>
        /// <param name="listaEntidad">Lista con los registros a eliminar</param>
        public async Task EliminarListaAsync(List<T> listaEntidad)
        {
            await contexto.BulkDeleteAsync(listaEntidad);
        }

        /// <summary>
        /// Eliminar registros en la base de datos segun filtro de forma asincrona
        /// </summary>
        /// <param name="filtro">The filtro.</param>
        public async Task EliminarPorQueryAsync(Expression<Func<T, bool>> filtro)
        {
            await contexto.Set<T>().Where(filtro).BatchDeleteAsync();
        }

        #endregion "DELETE"
 
      
        //public DataTable EjecutarProcedimiento(string nombreProcedimiento, Dictionary<string, object> parametros = null)
        //{
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        using (SqlCommand command = new SqlCommand(nombreProcedimiento, connection))
        //        {
        //            command.CommandType = CommandType.StoredProcedure;

        //            if (parametros != null)
        //            {
        //                foreach (var parametro in parametros)
        //                {
        //                    command.Parameters.AddWithValue(parametro.Key, parametro.Value ?? DBNull.Value);
        //                }
        //            }

        //            connection.Open();

        //            //IEnumerable<T> results = connection.Query<T>(nombreProcedimiento, parametros, commandType: CommandType.StoredProcedure);

        //            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
        //            {
        //                DataTable dataTable = new DataTable();
        //                adapter.Fill(dataTable);
        //                return dataTable;
        //            }
        //        }
        //    }
        //}

        //public IQueryable<T> EjecutarProcedimiento(string nombreProcedimiento, Dictionary<string, object> parametros = null)
        //{
        //    IQueryable<T> resultado;

        //    if (parametros != null && parametros.Any())
        //    {
        //        // Filtrar los parámetros nulos
        //        parametros = parametros
        //            .Where(p => p.Value != null && !p.Value.ToString().Equals("0"))
        //            .ToDictionary(p => p.Key, p => p.Value);

        //        if (parametros.Any())
        //        {
        //            string parametroString = string.Join(", ", parametros.Select(kv => "@" + kv.Key + "='" + kv.Value.ToString()+"'"));

        //            resultado = contexto.Set<T>().FromSqlRaw($"EXEC {nombreProcedimiento} {parametroString}");

        //        }
        //        else
        //        {
        //            // Ejecutar una consulta estándar si todos los parámetros son nulos
        //            resultado = contexto.Set<T>().FromSqlRaw($"EXEC {nombreProcedimiento}");
        //        }
        //    }
        //    else
        //    {
        //        // Ejecutar una consulta estándar si no se proporcionan parámetros
        //        resultado = contexto.Set<T>().FromSqlRaw($"EXEC {nombreProcedimiento}");
        //    }

        //    return resultado;
        //}




        /// <summary>
        /// Consultar todos los registros de la base de datos
        /// </summary>
        /// <returns>Todos los registros de la entidad</returns>
        public IQueryable<T> BuscarTodos()
        {
            IQueryable<T> resultado = contexto.Set<T>();
            return resultado;
        }

        /// <summary>
        /// Consultar registros de la base de datos segun filtro
        /// </summary>
        /// <param name="filtro">filtro de los datos a consultar</param>
        /// <returns></returns>
        public IQueryable<T> BuscarPor(Expression<Func<T, bool>> filtro)
        {
            IQueryable<T> resultado = contexto.Set<T>().Where(filtro);
            return resultado;
        }

        /// <summary>
        /// realiza Commit de la transaccion
        /// </summary>
        public void Guardar()
        {
            contexto.SaveChanges();
        }

        /// <summary>
        /// Realiza commit de forma asincrona de la transacción
        /// </summary>
        public async Task GuardarAsync()
        {
            await contexto.SaveChangesAsync();
        }
    }
}