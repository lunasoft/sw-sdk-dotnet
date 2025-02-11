using SW.Entities;
using SW.Helpers;
using System;
using System.IO;
using System.Net;

namespace SW.Services.Validate
{
    public abstract class ValidateService : Services
    {
        protected ValidateService(string url, string user, string password, string proxy, int proxyPort) : base(url, user, password, proxy, proxyPort)
        {
        }
        protected ValidateService(string url, string token, string proxy, int proxyPort) : base(url, token, proxy, proxyPort)
        {
        }
        internal virtual HttpWebRequest RequestValidateXml(byte[] xml, bool? status = true)
        {
            this.SetupRequest();
            string path = (status == false) ? "validate/cfdi?validatestatus=false" : "validate/cfdi";
            var request = (HttpWebRequest)WebRequest.Create(this.Url + path);
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
            request.ContentLength = xml != null ? xml.Length : 0;
            Helpers.RequestHelper.SetupProxy(this.Proxy, this.ProxyPort, ref request);
            Helpers.RequestHelper.AddFileToRequest(xml, ref request);
            return request;
        }
    }
}