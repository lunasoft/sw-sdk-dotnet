using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SW.Helpers;

namespace SW.Services.Stamp
{
    public abstract class StampServiceV4 : Services
    {
        protected StampServiceV4(string url, string user, string password, string proxy, int proxyPort) : base(url, user, password, proxy, proxyPort)
        {

        }
        protected StampServiceV4(string url, string token, string proxy, int proxyPort) : base(url, token, proxy, proxyPort)
        {
        }
        internal virtual MultipartFormDataContent GetMultipartContent(byte[] xml)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            ByteArrayContent fileContent = new ByteArrayContent(xml);
            content.Add(fileContent, "xml", "xml");
            return content;
        }
        internal virtual Dictionary<string, string> GetHeaders(string email = null, string customId = null)
        {
            if (customId != null)
            {
                Validation.ValidateCustomId(customId);
                if (customId.Length > 100)
                {
                    customId = customId.HashTo256();
                }
            }
            this.SetupRequest();
            Dictionary<string, string> headers = new Dictionary<string, string>() {
                    { "Authorization", "bearer " + this.Token },
                    { "email", email },
                    { "customid", customId }
                };
            return headers;
        }
    }
}
