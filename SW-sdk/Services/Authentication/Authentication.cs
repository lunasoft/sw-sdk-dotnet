using RestSharp;
using System;
using SW.Helpers;

namespace SW.Services.Authentication
{
    public class Authentication : AuthenticationService
    {
        AuthenticationResponseHandler _handler;
        public Authentication(string url, string user, string password) : base(url, user, password)
        {
            _handler = new AuthenticationResponseHandler();
        }
        public override AuthResponse GetToken()
        {
            try
            {
                new AuthenticationValidation(Url, User, Password, Token);
                var request = new RestRequest("security/authenticate", Method.POST);
                request.AddHeader("user", User);
                request.AddHeader("password", Password);
                return (AuthResponse)_handler.GetResponse(this.Client, request);
            }
            catch (Exception e)
            {
                return (AuthResponse)_handler.HandleException(e);
            }
        }
    }
}
