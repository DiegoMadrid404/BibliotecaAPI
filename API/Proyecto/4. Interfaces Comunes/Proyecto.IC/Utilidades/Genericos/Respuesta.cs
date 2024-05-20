using Proyecto.IC.Utilidades.Respuestas;

namespace Proyecto.IC.Utilidades.Genericos
{
    using Proyecto.IC.Enumeraciones;
    using System.Collections.Generic;

    public class Respuesta<T> : IRespuestaDTO<T>
    {
        public bool Resultado { get; set; }
        public List<T> Entidades { get; set; }
        public List<string> Mensajes { get; set; }
        public TipoNotificacion TipoNotificacion { get; set; }

        public Respuesta()
        {
            Entidades = new List<T>();
            Mensajes = new List<string>();
        }
    }
}