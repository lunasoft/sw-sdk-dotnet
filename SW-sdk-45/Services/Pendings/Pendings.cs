using SW.Helpers;
using System;
using System.Net;

namespace SW.Services.Pendings
{
    public class Pending : PendingsService
    {
        PendingsResponseHandler _handler;
        public Pending(string url, string user, string password, int proxyPort = 0, string proxy = null) : base(url, user, password, proxy, proxyPort)
        {
            _handler = new PendingsResponseHandler();
        }
        public Pending(string url, string token, int proxyPort = 0, string proxy = null) : base(url, token, proxy, proxyPort)
        {
            _handler = new PendingsResponseHandler();
        }
        internal override PendingsResponse PendingsRequest(string rfc)
        {
            PendingsResponseHandler handler = new PendingsResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                HttpWebRequest request = this.RequestPendings(rfc);
                request.ContentType = "application/json";
                request.ContentLength = 0;
                request.Method = WebRequestMethods.Http.Get;
                var headers = GetHeaders();
                var proxy = Helpers.RequestHelper.ProxySettings(this.Proxy, this.ProxyPort);
                return handler.GetResponse(this.Url, headers, $"pendings/{rfc}", proxy);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }
        /// <summary>
        /// Servicio que consulta las solicitudes de cancelación pendientes de aceptación ó rechazo.
        /// </summary>
        /// <param name="rfc">RFC del emisor.</param>
        /// <exception cref="System.Exception"></exception>
        /// <returns><see cref="PendingsResponse"/></returns>
        public PendingsResponse PendingsByRfc(string rfc)
        {
            return PendingsRequest(rfc);
        }
    }
}
