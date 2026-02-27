using SW.Helpers;
using System;
using System.Net;

namespace SW.Services.Stamp
{
    /// <summary>
    /// Clase base abstracta que construye la solicitud HTTP para los endpoints de timbrado ZIP.
    /// </summary>
    public abstract class StampZipService : Services
    {
        protected StampZipService(string url, string user, string password, string proxy, int proxyPort) : base(url, user, password, proxy, proxyPort)
        {
        }
        protected StampZipService(string url, string token, string proxy, int proxyPort) : base(url, token, proxy, proxyPort)
        {
        }

        /// <summary>
        /// Construye el <see cref="HttpWebRequest"/> para el endpoint de timbrado ZIP.
        /// </summary>
        /// <param name="zipBytes">Contenido del archivo .zip a enviar.</param>
        /// <param name="operation">Operación del endpoint: "stamp" o "issue".</param>
        /// <returns>Solicitud HTTP configurada con multipart/form-data.</returns>
        internal virtual HttpWebRequest RequestStampingZip(byte[] zipBytes, string operation)
        {
            this.SetupRequest();
            HttpWebRequest.DefaultMaximumErrorResponseLength = (zipBytes.Length > 1000000 ? 1000000 : zipBytes.Length + 1) * 2;
            var request = (HttpWebRequest)WebRequest.Create(this.Url + string.Format("cfdi/{0}/v1/zip", operation));
            Helpers.RequestHelper.SetupProxy(this.Proxy, this.ProxyPort, ref request);
            request.ProtocolVersion = HttpVersion.Version10;
            request.Timeout = 300000;
            request.ReadWriteTimeout = 600000;
            request.KeepAlive = false;
            request.ServicePoint.Expect100Continue = false;
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
            Helpers.RequestHelper.AddFileToRequest(zipBytes, ref request);
            return request;
        }
    }
}
