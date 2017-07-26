using RestSharp;
using System;
using SW.Helpers;

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

        internal override Response Cancelar(string cer, string key, string rfc, string password, string uuid)
        {
            CanelationResponseHandler handler = new CanelationResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                RestRequest request = this.RequestCancelar(cer, key, rfc, password, uuid);

                return handler.GetResponse(this.Client, request);
            }
            catch (Exception e)
            { 
                return handler.HandleException(e);
            }
        }

        internal override Response Cancelar(byte[] xmlCancelation)
        {
            CanelationResponseHandler handler = new CanelationResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                RestRequest request = this.RequestCancelar(xmlCancelation);

                return handler.GetResponse(this.Client, request);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }

        public CancelationResponse CancelarByCSD(string cer, string key, string rfc, string password, string uuid)
        {
            return (CancelationResponse)Cancelar(cer, key, rfc, password, uuid);
        }
        public CancelationResponse CancelarByXML(byte[] xmlCancelation)
        {
            return (CancelationResponse)Cancelar(xmlCancelation);
        }
    }
}
