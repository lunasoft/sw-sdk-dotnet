using SW.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace SW.Services.Relations
{
    public abstract class RelationsService : Services
    {
        protected RelationsService(string url, string user, string password) : base(url, user, password)
        {
        }
        protected RelationsService(string url, string token) : base(url, token)
        {
        }
        internal abstract RelationsResponse RelationsRequest(string cer, string key, string rfc, string password, string uuid);
        internal abstract RelationsResponse RelationsRequest(byte[] xmlCancelation);
        internal abstract RelationsResponse RelationsRequest(string pfx, string rfc, string password, string uuid);
        internal abstract RelationsResponse RelationsRequest(string rfc, string uuid);
        internal virtual Dictionary<string, string> GetHeaders()
        {
            this.SetupRequest();
            Dictionary<string, string> headers = new Dictionary<string, string>() {
                    { "Authorization", "bearer " + this.Token }
                };
            return headers;
        }
        internal virtual HttpWebRequest RequestRelations(string cer, string key, string rfc, string password, string uuid)
        {
            this.SetupRequest();
            string path = string.Format("relations/csd");
            var request = (HttpWebRequest)WebRequest.Create(this.Url + path);
            request.ContentType = "application/json";
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(new RelationsRequestCSD()
            {
                b64Cer = cer,
                b64Key = key,
                password = password,
                rfc = rfc,
                uuid = uuid
            });
            request.ContentLength = body.Length;
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(body);
                streamWriter.Flush();
                streamWriter.Close();
            }
            return request;
        }
        internal virtual HttpWebRequest RequestRelations(byte[] xmlCancelation)
        {
            this.SetupRequest();
            var request = (HttpWebRequest)WebRequest.Create(this.Url + "relations/xml");
            request.ContentType = "application/json";
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
            request.ContentLength = 0;
            Helpers.RequestHelper.AddFileToRequest(xmlCancelation, ref request);
            return request;
        }
        internal virtual HttpWebRequest RequestRelations(string pfx, string rfc, string password, string uuid)
        {
            this.SetupRequest();
            var request = (HttpWebRequest)WebRequest.Create(this.Url + "relations/pfx");
            request.ContentType = "application/json";
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add(HttpRequestHeader.Authorization, "bearer " + this.Token);
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(new RelationsRequestPFX()
            {
                b64Pfx = pfx,
                password = password,
                rfc = rfc,
                uuid = uuid
            });
            request.ContentLength = body.Length;
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(body);
                streamWriter.Flush();
                streamWriter.Close();
            }
            return request;
        }
        internal virtual HttpWebRequest RequestRelations(string rfc, string uuid)
        {
            this.SetupRequest();
            string path = $"relations/{rfc}/{uuid}";
            var request = (HttpWebRequest)WebRequest.Create(this.Url + path);
            request.ContentType = "application/json";
            request.ContentLength = 0;
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
            return request;
        }
    }
}
