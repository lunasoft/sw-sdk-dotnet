using System;
using SW.Helpers;
using System.Net;
using System.Net.Http;
using System.Text;

namespace SW.Services.AcceptReject
{
    public class AcceptReject : AcceptRejectService
    {

        AcceptRejectResponseHandler _handler;
        public AcceptReject(string url, string user, string password, int proxyPort = 0, string proxy = null) : base(url, user, password, proxy, proxyPort)
        {
            _handler = new AcceptRejectResponseHandler();
        }
        public AcceptReject(string url, string token, int proxyPort = 0, string proxy = null) : base(url, token, proxy, proxyPort)
        {
            _handler = new AcceptRejectResponseHandler();
        }
        internal override AcceptRejectResponse AcceptRejectRequest(string cer, string key, string rfc, string password, AceptacionRechazoItem[] uuids)
        {
            AcceptRejectResponseHandler handler = new AcceptRejectResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                var headers = GetHeaders();
                var proxy = Helpers.RequestHelper.ProxySettings(this.Proxy, this.ProxyPort);
                var content = this.RequestAcceptReject(cer, key, rfc, password, uuids);
                return handler.GetPostResponse(this.Url,
                                "acceptreject/csd", headers, content, proxy);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }
        internal override AcceptRejectResponse AcceptRejectRequest(byte[] xmlCancelation, EnumAcceptReject enumAcceptReject)
        {
            AcceptRejectResponseHandler handler = new AcceptRejectResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                var headers = GetHeaders();
                var proxy = Helpers.RequestHelper.ProxySettings(this.Proxy, this.ProxyPort);
                var content = this.RequestAcceptReject(xmlCancelation, enumAcceptReject);
                return handler.GetPostResponse(this.Url,
                                "acceptreject/xml", headers, content, proxy);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }
        internal override AcceptRejectResponse AcceptRejectRequest(string pfx, string rfc, string password, AceptacionRechazoItem[] uuid)
        {
            AcceptRejectResponseHandler handler = new AcceptRejectResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                var headers = GetHeaders();
                var proxy = Helpers.RequestHelper.ProxySettings(this.Proxy, this.ProxyPort);
                var content = this.RequestAcceptReject(pfx, rfc, password, uuid);
                return handler.GetPostResponse(this.Url,
                                "acceptreject/pfx", headers, content, proxy);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }
        internal override AcceptRejectResponse AcceptRejectRequest(string rfc, string uuid, EnumAcceptReject enumAcceptReject)
        {
            AcceptRejectResponseHandler handler = new AcceptRejectResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                HttpWebRequest request = this.RequestAcceptReject(rfc, uuid, enumAcceptReject);
                request.ContentType = "application/json";
                request.ContentLength = 0;
                request.Method = WebRequestMethods.Http.Post;
                var headers = GetHeaders();
                var proxy = Helpers.RequestHelper.ProxySettings(this.Proxy, this.ProxyPort);
                return handler.GetPostResponse(this.Url, headers, $"acceptreject/{rfc}/{uuid}/{enumAcceptReject.ToString()}", proxy);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }

        /// <summary>
        /// Servicio para aceptar o rechazar por CSD una solicitud de cancelación.
        /// </summary>
        /// <param name="cer">B64 del certificado CSD del emisor.</param>
        /// <param name="key">B64 del certificado Key del emisor.</param>
        /// <param name="rfc">RFC del emisor.</param>
        /// <param name="password">Contraseña del certificado del emisor.</param>
        /// <param name="uuids">Array de objetos <typeparamref name="AceptacionRechazoItem"></typeparamref></param>
        /// <exception cref="System.Exception"></exception>
        /// <returns><see cref="AcceptRejectResponse"/></returns>
        public AcceptRejectResponse AcceptByCSD(string cer, string key, string rfc, string password, AceptacionRechazoItem[] uuids)
        {
            return AcceptRejectRequest(cer, key, rfc, password, uuids);
        }
        /// <summary>
        /// Servicio para aceptar o rechazar por XML una solicitud de cancelación.
        /// </summary>
        /// <param name="xmlCancelation"></param>
        /// <param name="enumCancelation"></param>
        /// <exception cref="System.Exception"></exception>
        /// <returns><see cref="AcceptRejectResponse"/></returns>
        public AcceptRejectResponse AcceptByXML(byte[] xmlCancelation, EnumAcceptReject enumCancelation)
        {
            return AcceptRejectRequest(xmlCancelation, enumCancelation);
        }
        /// <summary>
        /// Servicio para aceptar o rechazar por PFX una solicitud de cancelación.
        /// </summary>
        /// <param name="pfx"></param>
        /// <param name="rfc"></param>
        /// <param name="password"></param>
        /// <param name="uuid"></param>
        /// <exception cref="System.Exception"></exception>
        /// <returns><see cref="AcceptRejectResponse"/></returns>
        public AcceptRejectResponse AcceptByPFX(string pfx, string rfc, string password, AceptacionRechazoItem[] uuid)
        {
            return AcceptRejectRequest(pfx, rfc, password, uuid);
        }
        /// <summary>
        /// Servicio para aceptar o rechazar por UUID una solicitud de cancelación.
        /// </summary>
        /// <param name="rfc"></param>
        /// <param name="uuid"></param>
        /// <param name="enumAcceptReject"></param>
        /// <exception cref="System.Exception"></exception>
        /// <returns><see cref="AcceptRejectResponse"/></returns>
        public AcceptRejectResponse AcceptByRfcUuid(string rfc, string uuid, EnumAcceptReject enumAcceptReject)
        {
            return AcceptRejectRequest(rfc, uuid, enumAcceptReject);
        }
    }
}
