using SW.Entities;
using SW.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
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
