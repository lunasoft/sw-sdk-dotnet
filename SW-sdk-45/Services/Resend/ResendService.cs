using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace SW.Services.Resend
{
    public abstract class ResendService : Services
    {
        private string _operation;
        private string _apiUrl;
        protected ResendService(string urlApi, string url, string user, string password, string proxy, int proxyPort) : base(url, user, password, proxy, proxyPort)
        {
            _apiUrl = urlApi;
        }
        protected ResendService(string urlApi, string token, string proxy, int proxyPort) : base(urlApi, token, proxy, proxyPort)
        {
            _apiUrl = urlApi;
        }

        internal virtual HttpWebRequest RequestResend(Guid uuid, string email)
        {
            this.SetupRequest();
            var request = (HttpWebRequest)WebRequest.Create(string.Format("{0}/comprobante/resendemail",_apiUrl));
            request.ContentType = "application/json";
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
            Helpers.RequestHelper.SetupProxy(this.Proxy, this.ProxyPort, ref request);
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(new PdfRequestResend()
            {
                uuid = uuid,
                to = email
            });
            byte[] streamLenght = Encoding.UTF8.GetBytes(body);
            request.ContentLength = streamLenght.Length;
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(body);
                streamWriter.Flush();
            }
            return request;
        }
    }
}
