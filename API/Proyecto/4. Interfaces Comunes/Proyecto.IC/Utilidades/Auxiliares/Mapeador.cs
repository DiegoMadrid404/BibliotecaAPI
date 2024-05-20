/// <summary>
/// Archivo para la clase Mapeadora de Objetos
/// </summary>
/// <remarks>
/// Autor: Diego Madrid 
/// </remarks>
namespace Proyecto.IC.Utilidades.Auxiliares
{
    using Newtonsoft.Json;
    using Proyecto.IC.Utilidades.Auxiliares.Comunes;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    public static class Mapeador
    {
        /// <summary>
        /// Realiza un Mapeo entre Objetos a partir de las Propiedades
        /// </summary>
        /// <param name="origen">Objeto Origen</param>
        /// <param name="destino">Objeto Destino</param>
        /// <returns>Objeto Destino con los valores del objeto Origen</returns>
        public static object MapearObjetosPorPropiedad(object origen, object destino)
        {
            foreach (PropertyInfo propiedadOrigen in origen.GetType().GetProperties())
            {
                PropertyInfo propiedadDestino = destino.GetType().GetProperties().Where(p => p.Name == propiedadOrigen.Name).FirstOrDefault();
                if (propiedadDestino != null && propiedadDestino.GetType().Name == propiedadOrigen.GetType().Name)
                {
                    propiedadDestino.SetValue(destino, propiedadOrigen.GetValue(origen));
                }
            }
            return destino;
        }

        public static TDestino MapearEntidadDTO<TOrigen, TDestino>(this TOrigen origen, TDestino destino)
            where TDestino : TOrigen, new()
        {
            if (origen == null)
            {
                destino = default(TDestino);
            }
            else
            {
                if (destino == null)
                {
                    destino = new TDestino();
                }
            }

            foreach (PropertyInfo propiedadOrigen in origen.GetType().GetProperties())
            {
                PropertyInfo propiedadDestino = destino.GetType().GetProperties().Where(p => p.Name == propiedadOrigen.Name).FirstOrDefault();
                if (propiedadDestino != null && propiedadDestino.GetType().Name == propiedadOrigen.GetType().Name)
                {
                    propiedadDestino.SetValue(destino, propiedadOrigen.GetValue(origen));
                }
            }
            return destino;
        }

       

        /// <summary>
        /// Realiza un Mapeo serilizando y Deserializando el obejto
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="origen"></param>
        /// <returns></returns>
        public static T MapearObjetoPorJson<T>(this object origen) where T : class, new()
        {
            T destino = new T();
            destino = JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(origen));
            return destino;
        }

        /// <summary>
        /// Realiza un Mapeo entre objetos con las propiedades a Mapear
        /// </summary>
        /// <typeparam name="TR">Tipo Destino</typeparam>
        /// <typeparam name="T">Tipo Origen</typeparam>
        /// <param name="origen">Objeto Origen</param>
        /// <param name="destino">Objeto Destino</param>
        /// <param name="propiedades"></param>
        /// <returns>Objeto Destino con los valores del objeto origen</returns>
        public static TR MapearObjetosPorPropiedadPorFiltro<TR, T>(this T origen, TR destino, params string[] propiedades)
            where TR : T, new()
        {
            if (origen.EsNulo())
            {
                destino = default(TR);
            }
            else
            {
                if (destino.EsNulo())
                {
                    destino = new TR();
                }

                PropertyInfo[] propiedadesAMapear = (from x in propiedades
                                                     join p in origen.GetType().GetProperties()
                                                     on x equals p.Name
                                                     select p).ToArray();

                foreach (PropertyInfo propiedadOrigen in propiedadesAMapear)
                {
                    PropertyInfo propiedadDestino = destino.GetType().GetProperties().Where(p => p.Name == propiedadOrigen.Name).FirstOrDefault();
                    if (propiedadDestino != null && propiedadDestino.GetType().Name == propiedadOrigen.GetType().Name)
                    {
                        propiedadDestino.SetValue(destino, propiedadOrigen.GetValue(origen));
                    }
                }
                return destino;
            }

            return destino;
        }

  
       

        public static List<TDestino> MapearALista<TOrigen, TDestino>(List<TOrigen> lista) where TDestino : class
        {
            List<TDestino> respuesta;

            if (!lista.EsNuloOVacio())
            {
                string jsonLista = ExtensionesBasicas.SerializarAJson(lista);
                respuesta = MapearObjetoPorJson<List<TDestino>>(lista);
            }
            else if (lista.EsNulo())
            {
                respuesta = null;
            }
            else
            {
                respuesta = new List<TDestino>();
            }

            return respuesta;
        }

        public static Expression<Func<TDestino, bool>> MapearExpresion<TOrigen, TDestino>(Expression<Func<TOrigen, bool>> expresion) where TDestino : TOrigen
        {
            ParameterExpression parametro = Expression.Parameter(typeof(TDestino));
            Expression body = new Visitor(parametro).Visit(expresion.Body);
            return Expression.Lambda<Func<TDestino, bool>>(body, parametro);
        }
 
        public static T AsignarValorAPropiedad<T>(this T objeto, PropertyInfo propiedad, object valor)
        {
            if (valor == null)
            {
                propiedad.SetValue(objeto, null);
                return objeto;
            }
            if (propiedad.PropertyType == typeof(Int32))
            {
                propiedad.SetValue(objeto, int.Parse(valor.ToString()));
                return objeto;
            }
            if (propiedad.PropertyType == typeof(double))
            {
                propiedad.SetValue(objeto, double.Parse(valor.ToString()));
                return objeto;
            }
            if (propiedad.PropertyType == typeof(DateTime))
            {
                propiedad.SetValue(objeto, DateTime.Parse(valor.ToString()));
                return objeto;
            }
            if (propiedad.PropertyType == typeof(decimal))
            {
                propiedad.SetValue(objeto, decimal.Parse(valor.ToString()));
                return objeto;
            }
            if (propiedad.PropertyType == typeof(Guid))
            {
                propiedad.SetValue(objeto, Guid.Parse(valor.ToString()));
                return objeto;
            }

            propiedad.SetValue(objeto, valor);

            return objeto;
        }

        
    }

    public class Visitor : ExpressionVisitor
    {
        private ParameterExpression _parameter;

        public Visitor(ParameterExpression parameter)
        {
            _parameter = parameter;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return _parameter;
        }
    }
}