using SW.Entities;
using SW.Helpers;
using System;
using System.IO;
using System.Net;

namespace SW.Services.Validate
{
    public abstract class ValidateService : Services
    {
        protected ValidateService(string url, string user, string password) : base(url, user, password)
        {
        }
        protected ValidateService(string url, string token) : base(url, token)
        {
        }
        internal virtual HttpWebRequest RequestValidateXml(byte[] xml)
        {
            this.SetupRequest();
            var request = (HttpWebRequest)WebRequest.Create(this.Url + "/validate/cfdi33");
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
            request.ContentLength = xml != null ? xml.Length : 0;
            Helpers.RequestHelper.AddFileToRequest(xml, ref request);
            return request;
        }
        internal virtual HttpWebRequest RequestValidateLrfc(string Lrfc)
        {
            this.SetupRequest();
            var request = (HttpWebRequest)WebRequest.Create(this.Url + string.Format( "lrfc/{0}", Lrfc) );
            request.Method = WebRequestMethods.Http.Get;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
            return request;
        }
        internal virtual HttpWebRequest RequestValidateLco(string Lco)
        {
            this.SetupRequest();
            var request = (HttpWebRequest)WebRequest.Create(Url + string.Format("lco/{0}", Lco));
            request.Method = WebRequestMethods.Http.Get;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
            return request;
        }
    }
}
