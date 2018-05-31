using SW.Entities;
using SW.Helpers;
using System;
using System.IO;
using System.Net;

namespace SW.Services.Stamp
{
    public abstract class StampService : Services
    {
        protected StampService(string url, string user, string password) : base(url, user, password)
        {
        }
        protected StampService(string url, string token) : base(url, token)
        {
        }
        internal virtual HttpWebRequest RequestStamping(byte[] xml, string version, string format, string operation)
        {
            this.SetupRequest();
            var request = (HttpWebRequest)WebRequest.Create(this.Url + string.Format("cfdi33/{0}/{1}/{2}", operation, version, format));
            request.ContentType = "application/json";
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
            request.ContentLength = xml != null ? xml.Length : 0;
            Helpers.RequestHelper.AddFileToRequest(xml, ref request);
            return request;
        }


    }
}
