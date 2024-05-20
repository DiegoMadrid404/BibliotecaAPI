/// <summary>
/// Archivo para la clase de exepciones
/// </summary>
/// <remarks>
/// Autor: Diego Madrid
/// </remarks> 
 
namespace Proyecto.IC.Utilidades.Auxiliares
{
    using Proyecto.IC.Utilidades.Exepciones;
    using System;

    public class Excepcion : IExcepcionDTO
    {
        public int IdExcepcion { get; set; }
        public Guid IdentificadorAplicacion { get; set; }
        public DateTime FechaExcepcion { get; set; }
        public string MensajeExcepcion { get; set; }
        public string TipoExcepcion { get; set; }
        public string OrigenExcepcion { get; set; }
        public string TrazaExcepcion { get; set; }
        public Guid Correlacion { get; set; }
        public string DatosEjecucion { get; set; }
    }
}