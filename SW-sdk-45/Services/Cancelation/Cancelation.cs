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
        public Cancelation(string url, string user, string password) : base(url, user, password)
        {
            _handler = new CanelationResponseHandler();
        }
        /// <summary>
        /// This Service is Not Implemented
        /// </summary>
        /// <param name="url"></param>
        /// <param name="token"></param>
        public Cancelation(string url, string token) : base(url, token)
        {
            _handler = new CanelationResponseHandler();
        }

        internal override CancelationResponse Cancelar(string cer, string key, string rfc, string password, string uuid)
        {
            CanelationResponseHandler handler = new CanelationResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                var headers = GetHeaders();
                var content = this.RequestCancelar(cer, key, rfc, password, uuid);
                return handler.GetPostResponse(this.Url,
                                "cfdi33/cancel/csd", headers, content);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }
        internal override CancelationResponse Cancelar(string rfc, string uuid)
        {
            CanelationResponseHandler handler = new CanelationResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                HttpWebRequest request = this.RequestCancelar(rfc, uuid);
                request.ContentType = "application/json";
                request.ContentLength = 0;
                request.Method = WebRequestMethods.Http.Post;
                var headers = GetHeaders();
                return handler.GetPostResponse(this.Url, headers, $"cfdi33/cancel/{rfc}/{uuid}");
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
                return handler.GetPostResponse(this.Url,
                                "cfdi33/cancel/xml", headers, content);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }
        internal override CancelationResponse Cancelar(string pfx, string rfc, string password, string uuid)
        {
            CanelationResponseHandler handler = new CanelationResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                var headers = GetHeaders();
                var content = this.RequestCancelar(pfx, rfc, password, uuid);
                return handler.GetPostResponse(this.Url,
                                "cfdi33/cancel/pfx", headers, content);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }
        public CancelationResponse CancelarByCSD(string cer, string key, string rfc, string password, string uuid)
        {
            return Cancelar(cer, key, rfc, password, uuid);
        }
        public CancelationResponse CancelarByXML(byte[] xmlCancelation)
        {
            return Cancelar(xmlCancelation);
        }
        public CancelationResponse CancelarByPFX(string pfx, string rfc, string password, string uuid)
        {
            return Cancelar(pfx, rfc, password, uuid);
        }
        public CancelationResponse CancelarByRfcUuid(string rfc, string uuid)
        {
            return Cancelar(rfc, uuid);
        }

    }
}
