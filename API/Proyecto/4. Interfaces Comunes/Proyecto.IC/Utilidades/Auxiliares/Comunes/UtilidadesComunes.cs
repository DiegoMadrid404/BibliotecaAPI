/// <summary>
/// Archivo para la clase de metodos de extension de utilidades comunes
/// </summary>
/// <remarks>
/// Autor: Diego Madrid
/// </remarks>
namespace UtilitariosNet.Clases.Comunes
{
    using Proyecto.IC.Recursos;
    using Proyecto.IC.Utilidades.Auxiliares.Comunes;
    using Proyecto.IC.Utilidades.Auxiliares.Constantes;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics;
    using System.Reflection;

    public class UtilidadesComunes
    {
        /// <summary>
        /// Obtiene el identificador de la aplicacion
        /// </summary>
        /// <returns>El Identificador unico del aplicativo</returns>
        public static Guid TraerIdAplicacion()
        {
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings[rcsUtilitarios.IdAplicacion]))
            {
                return new Guid(ConfigurationManager.AppSettings[rcsUtilitarios.IdAplicacion]);
            }
            else
            {
                throw new Exception(string.Format(rcsUtilitarios.ParametroNoConfiguradoCorrectamente, rcsUtilitarios.IdAplicacion));
            }
        }

        public static string TraerUrlServicioRedis()
        {
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings[rcsUtilitarios.UrlDatabaseRedis]))
            {
                return ConfigurationManager.AppSettings[rcsUtilitarios.UrlDatabaseRedis].ToString();
            }
            else
            {
                throw new Exception(string.Format(rcsUtilitarios.ParametroNoConfiguradoCorrectamente, rcsUtilitarios.UrlDatabaseRedis));
            }
        }

        public string ObtenerMetodoEjecucion(Type tipoClase)
        {
            try
            {
                List<string> metodosAIgnorar = new List<string>();

                if (tipoClase.EsNulo())
                {
                    metodosAIgnorar = ConstantesComunes.METODOSAIGNORAR;
                }

                string resultado = string.Concat((tipoClase.EsNulo() ? string.Empty : tipoClase.FullName));
                StackTrace traza = new StackTrace(false);

                bool esElMetodo, esElMetodoOpcional;

                foreach (StackFrame marco in traza.GetFrames())
                {
                    MethodBase metodo = marco.GetMethod();

                    if (tipoClase.EsNulo())
                    {
                        esElMetodo = metodosAIgnorar.Find(m => metodo.Name.Contains(m)).EsNulo();
                        esElMetodoOpcional = metodo.DeclaringType.Name.StartsWith(ConstantesComunes.METODOEMPIEZACON);
                    }
                    else
                    {
                        esElMetodo = esElMetodoOpcional = metodo.DeclaringType == tipoClase && tipoClase.IsAssignableFrom(metodo.DeclaringType);
                    }

                    if (esElMetodo)
                    {
                        resultado = string.Concat(metodo.DeclaringType.FullName, ConstantesComunes.PUNTO, metodo.Name, ConstantesComunes.ABRIRPARENTESIS, ConstantesComunes.CERRARPARENTESIS);

                        if (!esElMetodoOpcional)
                        {
                            break;
                        }
                    }
                }

                return resultado;
            }
            catch (Exception)
            {
                return ConstantesComunes.METODOVACIO;
            }
        }
    }
}