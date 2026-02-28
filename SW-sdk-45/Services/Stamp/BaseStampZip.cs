using SW.Helpers;
using System;
using System.Net.Http;

namespace SW.Services.Stamp
{
    /// <summary>
    /// Clase base abstracta para los servicios de timbrado mediante archivos ZIP.
    /// </summary>
    public abstract class BaseStampZip : StampZipService
    {
        private string _operation;

        public BaseStampZip(string url, string user, string password, string operation, string proxy, int proxyPort) : base(url, user, password, proxy, proxyPort)
        {
            _operation = operation;
        }
        public BaseStampZip(string url, string token, string operation, string proxy, int proxyPort) : base(url, token, proxy, proxyPort)
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
                var headers = GetHeaders();
                MultipartFormDataContent content = GetMultipartContent(zipBytes);
                HttpClientHandler proxy = Helpers.RequestHelper.ProxySettings(this.Proxy, this.ProxyPort);
                return handler.GetPostResponse(this.Url, GetZipPath(_operation), headers, content, proxy);
            }
            catch (Exception ex)
            {
                return handler.HandleException(ex);
            }
        }


    }
}
