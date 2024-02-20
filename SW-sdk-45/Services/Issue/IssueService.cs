using SW.Entities;
using SW.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace SW.Services.Issue
{
    public abstract class IssueService : Services
    {
        protected IssueService(string url, string user, string password, string proxy, int proxyPort) : base(url, user, password, proxy, proxyPort)
        {
        }
        protected IssueService(string url, string token, string proxy, int proxyPort) : base(url, token, proxy, proxyPort)
        {
        }
        internal virtual HttpWebRequest RequestStampJson(string json, string version, string operation)
        {
            this.SetupRequest();
            var request = (HttpWebRequest)WebRequest.Create(this.Url + string.Format("v3/cfdi33/{0}/{1}", operation, version));
            request.ContentType = "application/jsontoxml";
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
            Helpers.RequestHelper.SetupProxy(this.Proxy, this.ProxyPort, ref request);
            request.ContentLength = json.Length;
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            return request;
        }
        internal virtual HttpWebRequest RequestStampJson(string json, string version, string operation, string[] emails, string customId, bool pdf)
        {
            this.SetupRequest();
            var request = (HttpWebRequest)WebRequest.Create(this.Url + string.Format("v4/cfdi33/{0}/{1}", operation, version));
            request.ContentType = "application/jsontoxml";
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
            Helpers.RequestHelper.SetupProxy(this.Proxy, this.ProxyPort, ref request);
            request.ContentLength = Encoding.UTF8.GetByteCount(json);
            if (emails != null)
            {
                Validation.ValidateFormatEmail(emails);
                string email = string.Join(",", emails);
                request.Headers.Add("email", email);
            }
            if (pdf != false)
                request.Headers.Add("extra", "pdf");
            if (customId != null)
            {
                Validation.ValidateCustomId(customId);
                request.Headers.Add("customid", customId);
            }
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            return request;
        }
        internal virtual Dictionary<string, string> GetHeaders()
        {
            this.SetupRequest();
            Dictionary<string, string> headers = new Dictionary<string, string>() {
                    { "Authorization", "bearer " + this.Token }
                };
            return headers;
        }


    }
}
