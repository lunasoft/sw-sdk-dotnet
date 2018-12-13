using SW.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace SW.Services.Csd
{
    public abstract class CsdService : Services
    {
        protected CsdService(string url, string user, string password, string proxy, int proxyPort) : base(url, user, password, proxy, proxyPort)
        {
        }
        protected CsdService(string url, string token, string proxy, int proxyPort) : base(url, token, proxy, proxyPort)
        {
        }
        internal abstract UploadCsdResponse UploadCsd(string cer, string key, string password, string certificateType, bool isActive);
        internal virtual Dictionary<string, string> GetHeaders()
        {
            this.SetupRequest();
            Dictionary<string, string> headers = new Dictionary<string, string>() {
                    { "Authorization", "bearer " + this.Token }
                };
            return headers;
        }
        internal virtual HttpWebRequest RequestUploadCsd(string cer, string key, string password, string certificateType, bool isActive)
        {
            this.SetupRequest();
            var request = (HttpWebRequest)WebRequest.Create(this.Url + "csd/save");
            request.ContentType = "application/json";
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
            Helpers.RequestHelper.SetupProxy(this.Proxy, this.ProxyPort, ref request);
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(new UploadCsdRequest()
            {
                b64Cer = cer,
                b64Key = key,
                password = password,
                certificate_type = certificateType,
                is_active = isActive
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
