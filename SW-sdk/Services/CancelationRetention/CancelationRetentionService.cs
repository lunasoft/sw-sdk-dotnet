using SW.Services.Cancelation;
using System.IO;
using System.Net;
using System.Text;

namespace SW.Services.CancelationRetention
{
    public abstract class CancelationRetentionService : Services
    {
        protected CancelationRetentionService(string url, string user, string password, string proxy, int proxyPort) : base(url, user, password, proxy, proxyPort)
        { 
        }
        protected CancelationRetentionService(string url, string token, string proxy, int proxyPort) : base(url, token, proxy, proxyPort)
        {
        }

        internal abstract CancelationResponse CancelarRetention(byte[] xmlCancelation);
        internal abstract CancelationResponse CancelarRetention(string cer, string key, string rfc, string password, string uuid, string motivo, string folioSustitucion);
        internal abstract CancelationResponse CancelarRetention(string pfx, string rfc, string password, string uuid, string motivo, string folioSustitucion);

        internal virtual HttpWebRequest RequestCancelar(byte[] xmlCancelation)
        {
            this.SetupRequest();
            var request = (HttpWebRequest)WebRequest.Create(this.Url + "retencion/cancel/xml");
            request.ContentType = "application/json";
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
            request.ContentLength = 0;
            Helpers.RequestHelper.AddFileToRequest(xmlCancelation, ref request);
            Helpers.RequestHelper.SetupProxy(this.Proxy, this.ProxyPort, ref request);
            return request;
        }

        internal virtual HttpWebRequest RequestCancelar(string cer, string key, string rfc, string password, string uuid, string motivo, string folioSustitucion)
        {
            this.SetupRequest();
            var request = (HttpWebRequest)WebRequest.Create(this.Url + "retencion/cancel/csd");
            request.ContentType = "application/json";
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
            Helpers.RequestHelper.SetupProxy(this.Proxy, this.ProxyPort, ref request);
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(new CancelationRequestCSD()
            {
                b64Cer = cer,
                b64Key = key,
                password = password,
                rfc = rfc,
                uuid = uuid,
                motivo = motivo,
                folioSustitucion = folioSustitucion
            });
            byte[] bodyBytes = Encoding.UTF8.GetBytes(body);
            request.ContentLength = bodyBytes.Length;
            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(bodyBytes, 0, bodyBytes.Length);
                requestStream.Flush();
                requestStream.Close();
            }
            return request;
        }
        internal virtual HttpWebRequest RequestCancelar(string pfx, string rfc, string password, string uuid, string motivo, string folioSustitucion)
        {
            this.SetupRequest();
            var request = (HttpWebRequest)WebRequest.Create(this.Url + "retencion/cancel/pfx");
            request.ContentType = "application/json";
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add(HttpRequestHeader.Authorization, "bearer " + this.Token);
            Helpers.RequestHelper.SetupProxy(this.Proxy, this.ProxyPort, ref request);
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(new CancelationRequestPFX()
            {
                b64Pfx = pfx,
                password = password,
                rfc = rfc,
                uuid = uuid,
                motivo = motivo,
                folioSustitucion = folioSustitucion
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
    }
}
