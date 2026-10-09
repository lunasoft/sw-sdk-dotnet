using System;
using System.IO;
using System.Net;
using System.Text;

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
                var request = (HttpWebRequest)WebRequest.Create(this.Url + "v2/security/authenticate");
                request.ContentType = "application/json";
                request.ContentLength = 0;
                request.Method = WebRequestMethods.Http.Post;
                Helpers.RequestHelper.SetupProxy(this.Proxy, this.ProxyPort, ref request);
                var body = Newtonsoft.Json.JsonConvert.SerializeObject(new AuthenticationRequest()
                {
                    user = this.User,
                    password = this.Password,
                });
                byte[] bodyBytes = Encoding.UTF8.GetBytes(body);
                request.ContentLength = bodyBytes.Length;
                using (var requestStream = request.GetRequestStream())
                {
                    requestStream.Write(bodyBytes, 0, bodyBytes.Length);
                }
                return _handler.GetResponse(request);
            }
            catch (Exception e)
            {
                return _handler.HandleException(e);
            }
        }
    }
}
