namespace Proyecto.IC.Utilidades.Respuestas
{
    using Proyecto.IC.Enumeraciones;
    using System.Collections.Generic;

    public interface IRespuestaDTO<T>
    {
        bool Resultado { get; set; }
        List<T> Entidades { get; set; }
        List<string> Mensajes { get; set; }
        TipoNotificacion TipoNotificacion { get; set; }
    }
}