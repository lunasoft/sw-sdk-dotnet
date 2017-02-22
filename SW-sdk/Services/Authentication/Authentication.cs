using RestSharp;
using System;
using SW.Helpers;

namespace SW.Services.Authentication
{
    public class Authentication : AuthenticationService
    {
        private AuthResponse authResponse;
        private RestClient client;
        public Authentication(string url, string user, string password) : base(url, user, password)
        {
            authResponse = new AuthResponse();
        }

        private void BuildSettings()
        {
            client = new RestClient(Url);
        }

        public override AuthResponse GetToken()
        {
            try
            {
                new AuthenticationValidation(Url, User, Password, Token);
                BuildSettings();
                var request = new RestRequest("security/authenticate", Method.POST);
                request.AddHeader("user", User);
                request.AddHeader("password", Password);
                IRestResponse<AuthResponse> response = client.Execute<AuthResponse>(request);
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    new Validation().ValidateResponseStatus(response.StatusCode);
                }
                authResponse = response.Data;
            }
            catch (ServicesException e)
            {
                authResponse.Status = ResponseType.Fail;
                authResponse.Message = e.Message;
            }
            catch (TestEnviromentException)
            {
                authResponse.Status = ResponseType.Success;
                authResponse.Data = new Data()
                {
                    token = new DataDemoResponse().Authentication()
                };
            }
            catch (Exception e)
            {
                authResponse.Status = ResponseType.Error;
                authResponse.Message = e.Message;
            }
            return authResponse;
        }
    }
}
