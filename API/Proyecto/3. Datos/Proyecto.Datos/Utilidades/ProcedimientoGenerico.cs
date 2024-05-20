/// <summary>
/// Archivo para la clase de Repositorio Generico
/// </summary>
/// <remarks>
/// Autor: Diego Madrid
/// </remarks>
namespace Proyecto.Datos.Utilidades
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using Dapper;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;

    public class ProcedimientoGenerico
    {
        private readonly DbContext _dbContext;

        public ProcedimientoGenerico(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Ejecuta un procedimiento almacenado y retorna una consulta IQueryable de entidades.
        /// </summary>
        /// <typeparam name="T">Tipo de entidad esperado.</typeparam>
        /// <param name="nombreProcedimiento">Nombre del procedimiento almacenado.</param>
        /// <param name="parametros">Diccionario de parámetros para el procedimiento almacenado.</param>
        /// <returns>Consulta IQueryable de entidades resultantes.</returns>
        public IQueryable<T> EjecutarProcedimiento<T>(string nombreProcedimiento, Dictionary<string, object> parametros = null) where T : class
        {
            using (var connection = new SqlConnection(_dbContext.Database.GetDbConnection().ConnectionString))
            {

                connection.Open();


                IQueryable<T> resultado;

                if (parametros != null && parametros.Any())
                {
                    // Filtrar los parámetros nulos
                    parametros = parametros
                        .Where(p => p.Value != null && !p.Value.ToString().Equals("0"))
                        .ToDictionary(p => p.Key, p => p.Value);

                    if (parametros.Any())
                    {

                        return connection.Query<T>(nombreProcedimiento, parametros, commandType: CommandType.StoredProcedure).AsQueryable(); ;


                    }
                    return connection.Query<T>(nombreProcedimiento, null, commandType: CommandType.StoredProcedure).AsQueryable();
                }

                // Ejecutar una consulta estándar si no se proporcionan parámetros
                return connection.Query<T>(nombreProcedimiento, null, commandType: CommandType.StoredProcedure).AsQueryable();



            }
        }
    }
} 