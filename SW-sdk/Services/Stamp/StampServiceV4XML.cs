using SW.Entities;
using SW.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace SW.Services.Stamp
{
    public abstract class StampServiceV4XML : Services
    {
        protected StampServiceV4XML(string url, string token, string proxy, int proxyPort) : base(url, token, proxy, proxyPort)
        {
        }
        
        internal virtual HttpWebRequest RequestStamping(byte[] xml, string version, string format, string operation, Dictionary<string, string> headers)
        {
            this.SetupRequest();
            var request = (HttpWebRequest)WebRequest.Create(this.Url + string.Format("v4/cfdi33/{0}/{1}/{2}", operation, version, format));
            request.ContentType = "application/json";
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
            foreach (var item in headers)
            {
                request.Headers.Add(item.Key, item.Value);
            }
            request.ContentLength = xml != null ? xml.Length : 0;
            Helpers.RequestHelper.AddFileToRequest(xml, ref request);
            Helpers.RequestHelper.SetupProxy(this.Proxy, this.ProxyPort, ref request);
            return request;
        }
        
        internal virtual Dictionary<string, string> GetHeaders(string email, string customId)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            if (email != null && ValidateEmail(email))
            {
                headers.Add("email", email);
            }
            if (customId != null)
            {
                headers.Add("customId", customId);
            }
            return headers;
        }

        internal virtual bool ValidateEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
