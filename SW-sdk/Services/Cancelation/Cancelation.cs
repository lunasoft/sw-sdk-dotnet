using RestSharp;
using System;
using SW.Helpers;

namespace SW.Services.Cancelation
{
    public class Cancelation : CancelationService
    {
        private CancelationResponse cancelationResponse;
        private Services service;
        private RestClient client;
        /// <summary>
        /// This Service is Not Implemented
        /// </summary>
        /// <param name="url"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        public Cancelation(string url, string user, string password) : base(url, user, password)
        {
            cancelationResponse = new CancelationResponse();
        }
        /// <summary>
        /// This Service is Not Implemented
        /// </summary>
        /// <param name="url"></param>
        /// <param name="token"></param>
        public Cancelation(string url, string token) : base(url, token)
        {
            cancelationResponse = new CancelationResponse();
        }
        private void BuildSettings()
        {
            client = new RestClient(Url);
            
        }
        private RestRequest RequestCancelation(string version, string cer, string key,
                                                                                      string password, string[] uuids)
        {
            service = SetupRequest();
            RestRequest request = new RestRequest("", Method.POST);
            return request;
        }
        public override CancelationResponse Cancelar(CancelationTypes cancelationTypes, string cer, string key, 
                                                                                      string password, string[] uuids)
        {
            try
            {
                new CancelationValidation(Url, User, Password, Token).ValidateRequest(cer, key, password, uuids);
                throw new NotImplementedException();
            }
            catch (ServicesException e)
            {
                cancelationResponse.Status = ResponseType.Fail;
                cancelationResponse.Message = e.Message;
            }
            catch (TestEnviromentException)
            {
                cancelationResponse.Status = ResponseType.Success;
                cancelationResponse.Data = new Data()
                {
                    Acuse = new DataDemoResponse().Cancelation()
                };
            }
            catch (Exception e)
            {
                cancelationResponse.Status = ResponseType.Error;
                cancelationResponse.Message = e.Message;
            }
            return cancelationResponse;
        }
    }
}
