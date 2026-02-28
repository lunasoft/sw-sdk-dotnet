using SW.Helpers;
using System;

namespace SW.Services.Stamp
{
    /// <summary>
    /// Clase base abstracta para los servicios de timbrado mediante archivos ZIP.
    /// </summary>
    public abstract class BaseStampZip : StampZipService
    {
        private string _operation;

        public BaseStampZip(string url, string user, string password, string operation, int proxyPort, string proxy) : base(url, user, password, proxy, proxyPort)
        {
            _operation = operation;
        }
        public BaseStampZip(string url, string token, string operation, int proxyPort, string proxy) : base(url, token, proxy, proxyPort)
        {
            _operation = operation;
        }

        /// <summary>
        /// Timbra un archivo ZIP y retorna la respuesta en formato V1 (solo TFD).
        /// </summary>
        /// <param name="zipBytes">Bytes del archivo .zip enviado.</param>
        /// <returns>Respuesta con el Timbre Fiscal Digital (TFD).</returns>
        public virtual StampResponseV1 TimbrarZipV1(byte[] zipBytes)
        {
            StampResponseHandlerV1 handler = new StampResponseHandlerV1();
            try
            {
                var request = this.RequestStampingZip(zipBytes, _operation);
                return handler.GetResponse(request);
            }
            catch (Exception ex)
            {
                return handler.HandleException(ex);
            }
        }


    }
}
