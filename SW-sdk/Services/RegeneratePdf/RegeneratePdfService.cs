using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace SW.Services.RegeneratePdf
{
    public abstract class RegeneratePdfService : Services
    {
        private string _urlApi;

        protected RegeneratePdfService(string urlApi,string url, string user, string password, string proxy, int proxyPort) : base( url, user, password, proxy, proxyPort)
        {
            _urlApi = urlApi;
        }
        protected RegeneratePdfService(string urlApi, string token, string proxy, int proxyPort) : base(urlApi, token, proxy, proxyPort)
        {
            _urlApi = urlApi;
        }

        internal abstract RegeneratePdfResponse RegeneratePdfByUUID(Guid uuid);

        internal virtual HttpWebRequest RequestRegeneratePdf(Guid uuid)
        {
            this.SetupRequest();
            string path = $"/pdf/v1/api/RegeneratePdf/{uuid}";
            var request = (HttpWebRequest)WebRequest.Create(this._urlApi + path);
            request.ContentType = "application/json";
            request.ContentLength = 0;
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
            Helpers.RequestHelper.SetupProxy(this.Proxy, this.ProxyPort, ref request);
            return request;
        }
    }
}
