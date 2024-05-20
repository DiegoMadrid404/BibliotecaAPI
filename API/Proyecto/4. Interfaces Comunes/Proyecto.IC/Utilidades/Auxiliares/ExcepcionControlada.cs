/// <summary>
/// Archivo para la clase de exepciones controladas
/// </summary>
/// <remarks>
/// Autor: Diego Madrid
/// </remarks> 
 
namespace Proyecto.IC.Utilidades.Auxiliares
{
    using System;
    using System.Runtime.Serialization;

    public class ExcepcionControladaException : Exception, ISerializable
    {
        public ExcepcionControladaException() : base()
        {
        }

        public ExcepcionControladaException(bool generaLog) : base()
        {
            GeneroLog = generaLog;
        }

        public ExcepcionControladaException(string codigo, string mensaje, bool generaLog) : base(mensaje)
        {
            GeneroLog = generaLog;
            Codigo = codigo;
        }

        public bool GeneroLog { get; set; }
        public string Codigo { get; set; }
    }
}