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
        private RestRequest RequestCancelation(string version, string cer, string key,
                                                                                      string password, string[] uuids)
        {
            RestRequest request = new RestRequest("", Method.POST);
            return request;
        }
        public override CancelationResponse Cancelar(CancelationTypes cancelationTypes, string cer, string key,
                                                                                      string password, string[] uuids)
        {
            try
            {
                new CancelationValidation(Url, User, Password, Token).ValidateRequest(cer, key, password, uuids);
                //TODO: Implement cancelation service. Not yet defined
                throw new NotImplementedException();
            }
            catch (Exception e)
            {
                return (CancelationResponse)_handler.HandleException(e);
            }
        }
    }
}
