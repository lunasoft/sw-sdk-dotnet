using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace SW.Services.Taxpayer
{
    public abstract class TaxpayerService : Services
    {
        public TaxpayerService(string url, string user, string password, string proxy, int proxyPort) : base(url, user, password, proxy, proxyPort)
        {
        }
        public TaxpayerService(string url, string token, string proxy, int proxyPort) : base(url, token, proxy, proxyPort)
        {
        }
        internal abstract TaxpayerResponse TaxpayerByRfc(string rfc);

        internal virtual HttpWebRequest RequestTaxpayer(string rfc)
        {
            this.SetupRequest();
            string path = $"taxpayers/{rfc}";
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
