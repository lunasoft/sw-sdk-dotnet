using SW.Entities;
using SW.Helpers;
using System;
using System.IO;
using System.Net;

namespace SW.Services.Stamp
{
    public abstract class StampService : Services
    {
        protected StampService(string url, string user, string password, string proxy, int proxyPort) : base(url, user, password, proxy, proxyPort)
        {
        }
        protected StampService(string url, string token, string proxy, int proxyPort) : base(url, token, proxy, proxyPort)
        {
        }
        internal virtual HttpWebRequest RequestStamping(byte[] xml, string version, string format, string operation)
        {
            this.SetupRequest();
            HttpWebRequest.DefaultMaximumErrorResponseLength = (1000000 + xml.Length > 1000000 ? 1000000 : xml.Length + 1) * 2;
            var request = (HttpWebRequest)WebRequest.Create(this.Url + string.Format("cfdi33/{0}/{1}/{2}", operation, version, format));
            Helpers.RequestHelper.SetupProxy(this.Proxy, this.ProxyPort, ref request);
            request.ProtocolVersion = HttpVersion.Version10;
            request.Timeout = 300000;
            request.ReadWriteTimeout = 600000;
            request.KeepAlive = false;
            request.ServicePoint.Expect100Continue = false;
            request.ContentType = "application/json";
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
            request.ContentLength = xml != null ? xml.Length : 0;
            Helpers.RequestHelper.AddFileToRequest(xml, ref request);
            return request;
        }
    }
}
