using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;

namespace SW.Services.Authentication
{
    public class Authentication : AuthenticationService
    {
        AuthenticationResponseHandler _handler;
        public Authentication(string url, string user, string password, int proxyPort = 0, string proxy = null) : base(url, user, password, proxy, proxyPort)
        {
            _handler = new AuthenticationResponseHandler();
        }
        public override AuthResponse GetToken()
        {
            try
            {
                new AuthenticationValidation(Url, User, Password, Token);

                Dictionary<string, string> headers = new Dictionary<string, string>() {
                    { "user", this.User },
                    { "password", this.Password }
                };
                var proxy = Helpers.RequestHelper.ProxySettings(this.Proxy, this.ProxyPort);
                return _handler.GetPostResponse(this.Url, headers, "security/authenticate", proxy);

            }
            catch (Exception e)
            {
                return _handler.HandleException(e);
            }
        }
    }
}
