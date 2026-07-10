using System;

namespace SW.Helpers.Convertion
{
    /// <summary>
    /// Conversión de respuestas de timbrado V2 a V4.
    /// Porte interno equivalente a SW.Tools.Services.Convertion.Convertion,
    /// para eliminar la dependencia a SW.Tools (y su referencia heredada a
    /// BouncyCastle, que este código nunca necesitó).
    /// </summary>
    internal static class Convertion
    {
        /// <summary>
        /// Convertir respuesta de timbrado V2 a V4
        /// </summary>
        /// <param name="response">Respuesta V2 en JSON</param>
        /// <returns>Respuesta V4 en JSON</returns>
        internal static string ConvertResponseToV4(string response)
        {
            if (String.IsNullOrEmpty(response))
            {
                throw new Exception("No se ha recibido la respuesta.");
            }
            return ConvertionService.ConvertResponse(response);
        }
    }
}
