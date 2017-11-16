using SW.Entities;
using SW.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

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
        internal virtual MultipartFormDataContent GetMultipartContent(byte[] xml)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            ByteArrayContent fileContent = new ByteArrayContent(xml);
            content.Add(fileContent, "xml", "xml");
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
