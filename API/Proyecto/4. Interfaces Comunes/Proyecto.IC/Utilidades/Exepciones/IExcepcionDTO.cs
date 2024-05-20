
namespace Proyecto.IC.Utilidades.Exepciones
{
    using System;
    public interface IExcepcionDTO
    {
        int IdExcepcion { get; set; }
        Guid IdentificadorAplicacion { get; set; }
        DateTime FechaExcepcion { get; set; }
        string MensajeExcepcion { get; set; }
        string TipoExcepcion { get; set; }
        string OrigenExcepcion { get; set; }
        string TrazaExcepcion { get; set; }
        Guid Correlacion { get; set; }
        string DatosEjecucion { get; set; }
    }
}
