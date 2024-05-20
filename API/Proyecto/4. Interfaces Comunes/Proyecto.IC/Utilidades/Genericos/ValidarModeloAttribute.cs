namespace Proyecto.IC.Utilidades.Genericos
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Proyecto.IC.Enumeraciones;
    using System.Linq;

    public class ValidarModeloAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {


                Respuesta<bool> respuesta = new Respuesta<bool>
                {
                    Resultado = false,
                    TipoNotificacion = TipoNotificacion.Advertencia,
                    Mensajes= context.ModelState.Values.SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList()
            };

                // El modelo no es válido, devuelve una respuesta con los errores de validación
                var errores = 
                context.Result = new BadRequestObjectResult(respuesta);
            }
            else
            {
                base.OnActionExecuting(context);
            }
        }
    }
}

