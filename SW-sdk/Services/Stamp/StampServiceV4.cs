using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace SW.Services.Stamp
{
    public abstract class StampServiceV4 : Services
    {
        protected StampServiceV4(string url, string user, string password, string proxy, int proxyPort) : base(url, user, password, proxy, proxyPort)
        {
        }
        protected StampServiceV4(string url, string token, string proxy, int proxyPort) : base(url, token, proxy, proxyPort)
        {
        }
        internal virtual HttpWebRequest RequestStamping(byte[] xml, string version, string format, string operation, string email, string customId)
        {
            this.SetupRequest();
            HttpWebRequest.DefaultMaximumErrorResponseLength = (xml.Length > 1000000 ? 1000000 : xml.Length + 1) * 2;
            var request = (HttpWebRequest)WebRequest.Create(this.Url + string.Format("v4/cfdi33/{0}/{1}/{2}", operation, version, format));
            Helpers.RequestHelper.SetupProxy(this.Proxy, this.ProxyPort, ref request);
            request.ProtocolVersion = HttpVersion.Version10;
            request.Timeout = 300000;
            request.ReadWriteTimeout = 600000;
            request.KeepAlive = false;
            request.ServicePoint.Expect100Continue = false;
            request.ContentType = "application/json";
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
            if(email!=null)
                request.Headers.Add("email", email);
            if (customId != null)
                request.Headers.Add("customid", customId);
            request.ContentLength = xml != null ? xml.Length : 0;
            Helpers.RequestHelper.AddFileToRequest(xml, ref request);
            return request;
        }
    }
}
