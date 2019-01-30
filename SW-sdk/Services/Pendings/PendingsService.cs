using System.Collections.Generic;
using System.Net;

namespace SW.Services.Pendings
{
    public abstract class PendingsService : Services
    {
        protected PendingsService(string url, string user, string password, string proxy, int proxyPort) : base(url, user, password, proxy, proxyPort)
        {
        }
        protected PendingsService(string url, string token, string proxy, int proxyPort) : base(url, token, proxy, proxyPort)
        {
        }
        internal abstract PendingsResponse PendingsRequest(string rfc);
        internal virtual HttpWebRequest RequestPendings(string rfc)
        {
            this.SetupRequest();
            string path = $"pendings/{rfc}";
            var request = (HttpWebRequest)WebRequest.Create(this.Url + path);
            request.ContentType = "application/json";
            request.ContentLength = 0;
            request.Method = WebRequestMethods.Http.Get;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
            Helpers.RequestHelper.SetupProxy(this.Proxy, this.ProxyPort, ref request);
            return request;
        }
    }
}
