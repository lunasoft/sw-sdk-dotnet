using SW.Entities;
using SW.Helpers;
using System;
using System.IO;
using System.Net;

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


    }
}
