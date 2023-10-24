using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace SW.Services.Storage
{
    public abstract class StorageService : Services
    {
        protected StorageService(string urlApi, string url, string user, string password, string proxy, int proxyPort) : base(url, urlApi, user, password, proxy, proxyPort)
        {
        }
        protected StorageService(string url, string token, string proxy, int proxyPort) : base(url, token, proxy, proxyPort)
        {
        }
        internal abstract StorageResponse StorageByUUID(Guid uuid);

        internal virtual HttpWebRequest RequestStorage(Guid uuid)
        {
            this.SetupRequest();
            string path = $"/datawarehouse/v1/live/{uuid}";
            var baseUrl = this.UrlApi ?? this.Url;
            var request = (HttpWebRequest)WebRequest.Create(baseUrl + path);
            request.ContentType = "application/json";
            request.ContentLength = 0;
            request.Method = WebRequestMethods.Http.Get;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
            Helpers.RequestHelper.SetupProxy(this.Proxy, this.ProxyPort, ref request);
            return request;
        }
    }
}
