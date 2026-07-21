using System;

namespace SW.Helpers.Convertion
{
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
