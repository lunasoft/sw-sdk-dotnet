using Newtonsoft.Json;
using SW.Entities;
using SW.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace SW.Services.Pdf
{
    public abstract class PdfService : Services
    {
        protected PdfService(string url, string user, string password, string proxy, int proxyPort) : base(url, user, password, proxy, proxyPort)
        {
        }
        protected PdfService(string url, string token, string proxy, int proxyPort) : base(url, token, proxy, proxyPort)
        {
        }

        internal virtual MultipartFormDataContent GetMultipartContent(byte[] xml, Dictionary<string, string> ObservacionesAdcionales)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            ByteArrayContent fileContent = new ByteArrayContent(xml);
            content.Add(fileContent, "xml", "xml");
            content.Add(new StringContent(JsonConvert.SerializeObject(ObservacionesAdcionales, Formatting.Indented)), "extras");
            return content;
        }
        internal virtual Dictionary<string, string> GetHeaders()
        {
            this.SetupRequest();
            Dictionary<string, string> headers = new Dictionary<string, string>() {
                    { "Authorization", "bearer " + this.Token }
                };
            return headers;
        }
    }
}
