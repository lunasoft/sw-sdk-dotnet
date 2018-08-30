using System.Collections.Generic;
using System.Net;

namespace SW.Services.Pendings
{
    public abstract class PendingsService : Services
    {
        protected PendingsService(string url, string user, string password) : base(url, user, password)
        {
        }
        protected PendingsService(string url, string token) : base(url, token)
        {
        }
        internal abstract PendingsResponse PendingsRequest(string rfc);
        internal virtual Dictionary<string, string> GetHeaders()
        {
            this.SetupRequest();
            Dictionary<string, string> headers = new Dictionary<string, string>() {
                    { "Authorization", "bearer " + this.Token }
                };
            return headers;
        }
        internal virtual HttpWebRequest RequestPendings(string rfc)
        {
            this.SetupRequest();
            string path = $"pendings/{rfc}";
            var request = (HttpWebRequest)WebRequest.Create(this.Url + path);
            request.ContentType = "application/json";
            request.ContentLength = 0;
            request.Method = WebRequestMethods.Http.Get;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
            return request;
        }
    }
}
