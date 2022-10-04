using Newtonsoft.Json;
using SW.Entities;
using SW.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace SW.Services.Pdf
{
    public abstract class PdfService : Services
    {
        private string _operation;
        private string _apiUrl;
        protected PdfService(string urlApi, string url, string user, string password, string proxy, int proxyPort) : base(url, user, password, proxy, proxyPort)
        {
            _apiUrl = urlApi;
        }
        protected PdfService(string urlApi, string token, string proxy, int proxyPort) : base(urlApi, token, proxy, proxyPort)
        {
            _apiUrl = urlApi;
        }
        internal virtual HttpWebRequest RequestPdf(string xml, string logo, string TemplateId, Dictionary<string, string> ObservacionesAdicionales = null)
        {
            this.SetupRequest();
            var request = (HttpWebRequest)WebRequest.Create(_apiUrl + string.Format("/pdf/v1/api/GeneratePdf"));
            request.ContentType = "application/json";
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
            Helpers.RequestHelper.SetupProxy(this.Proxy, this.ProxyPort, ref request);
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(new PdfRequest()
            {
                xmlContent= xml,
                logo = logo,
                extras = ObservacionesAdicionales,
                templateId = TemplateId
            });
            byte[] streamLenght = Encoding.UTF8.GetBytes(body);
            request.ContentLength = streamLenght.Length;
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
