/// <summary>
/// Archivo para las enumeraciones de tipo de notificacion
/// </summary>
/// <remarks>
/// Autor: Diego Madrid
/// </remarks>

namespace Proyecto.IC.Enumeraciones
{
    public enum TipoNotificacion : short
    {
        /// <summary>
        /// Respuesta con mensaje de error
        /// </summary>
        Error = 1,

        /// <summary>
        /// Respuesta con mensaje de advertencia
        /// </summary>
        Advertencia = 2,

        /// <summary>
        /// Respuesta con mensaje exitoso
        /// </summary>
        Exitoso = 3,

        /// <summary>
        /// Respuesta con mensaje informativo
        /// </summary>
        Informacion = 4
    }
}