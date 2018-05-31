using SW.Helpers;
using System.IO;
using System.Net;

namespace SW.Services.Cancelation
{
    public abstract class CancelationService : Services
    {
        protected CancelationService(string url, string user, string password) : base(url, user, password)
        {
        }
        protected CancelationService(string url, string token) : base(url, token)
        {
        }
        internal abstract CancelationResponse Cancelar(string cer, string key, string rfc, string password, string uuid);
        internal abstract CancelationResponse Cancelar(byte[] xmlCancelation);
        internal abstract CancelationResponse Cancelar(string pfx, string rfc, string password, string uuid);
        internal abstract CancelationResponse Cancelar(string rfc, string uuid);
        internal virtual HttpWebRequest RequestCancelar(string cer, string key, string rfc, string password, string uuid)
        {
            this.SetupRequest();
            var request = (HttpWebRequest)WebRequest.Create(this.Url + "cfdi33/cancel/csd");
            request.ContentType = "application/json";
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(new CancelationRequestCSD()
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
        internal virtual HttpWebRequest RequestCancelar(string rfc, string uuid)
        {
            this.SetupRequest();
            string path = string.Format("cfdi33/cancel/{0}/{1}", rfc, uuid);
            var request = (HttpWebRequest)WebRequest.Create(this.Url + path);
            request.ContentType = "application/json";
            request.ContentLength = 0;
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
            return request;
        }
        internal virtual HttpWebRequest RequestCancelar(string pfx, string rfc, string password, string uuid)
        {
            this.SetupRequest();
            var request = (HttpWebRequest)WebRequest.Create(this.Url + "cfdi33/cancel/pfx");
            request.ContentType = "application/json";
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add(HttpRequestHeader.Authorization, "bearer " + this.Token);
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(new CancelationRequestPFX()
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
        internal virtual HttpWebRequest RequestCancelar(byte[] xmlCancelation)
        {
            this.SetupRequest();
            var request = (HttpWebRequest)WebRequest.Create(this.Url + "cfdi33/cancel/xml");
            request.ContentType = "application/json";
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
            request.ContentLength = 0;
            Helpers.RequestHelper.AddFileToRequest(xmlCancelation, ref request);
            return request;
        }
    }
}
