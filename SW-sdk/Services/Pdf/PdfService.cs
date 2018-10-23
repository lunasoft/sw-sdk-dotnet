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
        public string boundary = "----------------------------" + DateTime.Now.Ticks.ToString("x");
        protected PdfService(string url, string user, string password, string proxy, int proxyPort) : base(url, user, password, proxy, proxyPort)
        {
        }
        protected PdfService(string url, string token, string proxy, int proxyPort) : base(url, token, proxy, proxyPort)
        {
        }
        internal virtual HttpWebRequest RequestPdf(byte[] xml, string TemplateId, Dictionary<string, string> ObservacionesAdicionales = null)
        {
            this.SetupRequest();
            var request = (HttpWebRequest)WebRequest.Create(this.Url + string.Format("/pdf/v1/generate"));
            request.ContentType = "multipart/form-data; boundary=" + boundary;
            request.Headers.Add("Authorization", "Bearer " + Token);
            request.Headers.Add("TemplateId", TemplateId);
            request.Method = WebRequestMethods.Http.Post;
            request.KeepAlive = true;
            Stream memStream = new System.IO.MemoryStream();
            Helpers.RequestHelper.SetupProxy(this.Proxy, this.ProxyPort, ref request);
            string body = MultipartXmlContent(xml);
            string pObservacionesGenerales = BodyObservacionesAdicionales(ObservacionesAdicionales);
            body = string.Format("{0}{1}--\r\n", body, pObservacionesGenerales);
            byte[] byteArray = Encoding.UTF8.GetBytes(body);
            request.ContentLength = byteArray.Length;
            request.Timeout = byteArray.Length * 5;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            return request;
        }

        internal virtual string MultipartXmlContent(byte[] xml)
        {
            if(xml!= null && xml.Length > 0)
            {
                try
                {
                    string boundaryBegin = "--" + boundary;
                    string rn = "\r\n";
                    string contentDisposition = "Content-Disposition: {0};";
                    string name = " name=\"{0}\";";
                    string fileName = " filename=\"{0}\";";
                    string contentType = "Content-Type: \"{0}\"";
                    string body = boundaryBegin + rn +
                                 string.Format(contentDisposition, "form-data") + string.Format(name, "file") + string.Format(fileName, "xml.xml") + rn +
                                 string.Format(contentType, "application/xml") + rn + rn +
                                 Encoding.UTF8.GetString(xml) + rn +
                                 boundaryBegin;
                    return body;
                } catch (Exception ex)
                { 
                    return (ex.Message);
                }
            }
            else
            {
                return null;
            }
        }
        internal virtual string BodyObservacionesAdicionales(Dictionary<string, string> ObservacionesAdcionales)
        {
            return "\r\n" + boundary + "\r\n" +
                "Content-Disposition: form-data; name=\"extras\"" +
                JsonConvert.SerializeObject(ObservacionesAdcionales, Formatting.Indented) +
               "\r\n" + boundary;
        }
    }
}
