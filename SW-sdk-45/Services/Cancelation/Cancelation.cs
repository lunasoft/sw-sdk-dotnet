using System;
using SW.Helpers;
using System.Net;

namespace SW.Services.Cancelation
{
    public class Cancelation : CancelationService
    {

        CanelationResponseHandler _handler;
        /// <summary>
        /// This Service is Not Implemented
        /// </summary>
        /// <param name="url"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        public Cancelation(string url, string user, string password, int proxyPort = 0, string proxy = null) : base(url, user, password, proxy, proxyPort)
        {
            _handler = new CanelationResponseHandler();
        }
        /// <summary>
        /// This Service is Not Implemented
        /// </summary>
        /// <param name="url"></param>
        /// <param name="token"></param>
        public Cancelation(string url, string token, int proxyPort = 0, string proxy = null) : base(url, token, proxy, proxyPort)
        {
            _handler = new CanelationResponseHandler();
        }

        internal override CancelationResponse Cancelar(string cer, string key, string rfc, string password, string uuid, string motivo, string folioSustitucion)
        {
            CanelationResponseHandler handler = new CanelationResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                var headers = GetHeaders();
                var content = this.RequestCancelar(cer, key, rfc, password, uuid, motivo, folioSustitucion);
                var proxy = Helpers.RequestHelper.ProxySettings(this.Proxy, this.ProxyPort);
                return handler.GetPostResponse(this.Url,
                                "cfdi33/cancel/csd", headers, content, proxy);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }
        internal override CancelationResponse Cancelar(string rfc, string uuid, string motivo, string folioSustitucion)
        {
            CanelationResponseHandler handler = new CanelationResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                HttpWebRequest request = this.RequestCancelar(rfc, uuid, motivo, folioSustitucion);
                request.ContentType = "application/json";
                request.ContentLength = 0;
                request.Method = WebRequestMethods.Http.Post;
                var proxy = Helpers.RequestHelper.ProxySettings(this.Proxy, this.ProxyPort);
                var headers = GetHeaders();
                return handler.GetPostResponse(this.Url, headers, $"cfdi33/cancel/{rfc}/{uuid}/{motivo}/{folioSustitucion}", proxy);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }
        internal override CancelationResponse Cancelar(byte[] xmlCancelation)
        {
            CanelationResponseHandler handler = new CanelationResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                var headers = GetHeaders();
                var content = this.RequestCancelarFile(xmlCancelation);
                var proxy = Helpers.RequestHelper.ProxySettings(this.Proxy, this.ProxyPort);
                return handler.GetPostResponse(this.Url,
                                "cfdi33/cancel/xml", headers, content, proxy);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }
        internal override CancelationResponse Cancelar(string pfx, string rfc, string password, string uuid, string motivo, string folioSustitucion)
        {
            CanelationResponseHandler handler = new CanelationResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                var headers = GetHeaders();
                var content = this.RequestCancelar(pfx, rfc, password, uuid, motivo, folioSustitucion);
                var proxy = Helpers.RequestHelper.ProxySettings(this.Proxy, this.ProxyPort);
                return handler.GetPostResponse(this.Url,
                                "cfdi33/cancel/pfx", headers, content, proxy);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }
        public CancelationResponse CancelarByCSD(string cer, string key, string rfc, string password, string uuid, string motivo, string folioSustitucion = null)
        {
            return Cancelar(cer, key, rfc, password, uuid, motivo, folioSustitucion);
        }
        public CancelationResponse CancelarByXML(byte[] xmlCancelation)
        {
            return Cancelar(xmlCancelation);
        }
        public CancelationResponse CancelarByPFX(string pfx, string rfc, string password, string uuid, string motivo, string folioSustitucion = null)
        {
            return Cancelar(pfx, rfc, password, uuid, motivo, folioSustitucion);
        }
        public CancelationResponse CancelarByRfcUuid(string rfc, string uuid, string motivo, string folioSustitucion = null)
        {
            return Cancelar(rfc, uuid, motivo, folioSustitucion);
        }

    }
}
