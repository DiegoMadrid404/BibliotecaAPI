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
    public interface IProcedimientoGenerico<T>
    {
        /// <summary>
        /// Consultar todos los registros de la base de datos
        /// </summary>
        /// <returns>Todos los registros de la entidad</returns>
        public IQueryable<T> EjecutarProcedimiento(string nombreProcedimiento, Dictionary<string, object> parametros = null);

    }
}