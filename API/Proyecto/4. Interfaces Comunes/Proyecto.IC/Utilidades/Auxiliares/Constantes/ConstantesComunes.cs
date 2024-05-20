/// <summary>
/// Archivo para las constantes
/// </summary>
/// <remarks>
/// Autor: Diego Madrid
/// </remarks>

namespace Proyecto.IC.Utilidades.Auxiliares.Constantes
{
    using System.Collections.Generic;

    /// <summary>
    /// Clase estatica para las constantes
    /// </summary>
    public class ConstantesComunes
    {
        public static readonly List<string> METODOSAIGNORAR = new List<string>(new[] { "GetExecutingMethodName", "ExecuteWrapper", "ExecuteWrapperBL", "ExecuteWrapperDB", "ExecuteWrapperProxy", "InvokeMethod", "UnsafeInvokeInternal" });

        public const string METODOEMPIEZACON = "<>";

        public const string PUNTO = ".";

        public const string ABRIRPARENTESIS = "(";

        public const string CERRARPARENTESIS = ")";

        public const string ABRIRLLAVE = "{";

        public const string CERRARLLAVE = "}";

        public const string ABRIRCORCHETE = "[";

        public const string CERRARCORCHETE = "]";

        public const string MAYORQUE = ">";

        public const string MENORQUE = "<";

        public const string GUION = "-";

        public const string GUIONBAJO = "_";

        public const string VALORVACIO = "";

        public const string GUIDVACIO = "00000000-0000-0000-0000-000000000000";

        public const string DOSPUNTOS = ":";

        public const string METODOVACIO = "-GetExecutingMethodName vacio-";

        public const string CODIGODEFECTOEXCEPCIONNEGOCIO = "000";

        public const string CODIGODEFECTOEXCEPCIONCONTROLADA = "001";

        public const string CODIGODEFECTOEXCEPCIONNOCONTROLADA = "002";

        public const string ESPACIOBLANCO = "\u0020";

        public const string ABECEDARIO = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public const string VALOR0 = "0";

        public const string SELECCIONE = "[Seleccione]";

        public const string TODOS = "[Todos]";

        public const string TODAS = "[Todas]";

        public const string COMA = ",";

        public const string NUEVALINEA = "\r\n ";

        public const string SLASH = @"/";

        public const string DOSPUNTOSSEGUIDOS = "..";

        public const string ARROBA = "@";

        public const string FORMATOFECHA = "dd/MM/yyyy";

        public const string CULTURACOLOMBIA = "es-CO";

        public const string FORMATOFECHAHORA = "dd/MM/yyyy HH:mm:ss";

        public const string FORMATOHORA = "HH:mm:ss";

        public const string FORMATODECIMAL2 = "#,##0.00";

        public const string FORMATODECIMAL2PESOS = "$ #,##0.00";

        public const string FORMATODECIMAL4 = "#,##0.0000";

        public const string FORMATODECIMAL4PESOS = "$ #,##0.0000";

        public const string IPLOCAL = "127.0.0.1";

        public const string PUNTOYCOMA = ";";

        public const string PARTEDIA = "dd";

        public const string PARTEMES = "mm";

        public const string PARTEANIO = "yyyy";

        public const string IGUAL = "=";
    }
}