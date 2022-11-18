using SW.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW.Services.Stamp
{
    public class BaseStampV4Analytics : StampServiceV4
    {
        public BaseStampV4Analytics(string url, string user, string password, int proxyPort, string proxy)
            : base(url, user, password, proxy, proxyPort)
        {
        }
        public BaseStampV4Analytics(string url, string token, int proxyPort, string proxy)
            : base(url, token, proxy, proxyPort)
        {
        }
        public virtual StampResponseV4 TimbrarV4(string xml, string customId, string email = null)
        {
            StampResponseHandlerV4 handler = new StampResponseHandlerV4();
            try
            {
                var xmlBytes = Encoding.UTF8.GetBytes(xml);
                var headers = GetHeaders(email, customId);
                var content = GetMultipartContent(xmlBytes);
                var proxy = Helpers.RequestHelper.ProxySettings(this.Proxy, this.ProxyPort);
                return handler.GetPostResponse(this.Url,
                                string.Format("v4/cfdi33/{0}/{1}/{2}",
                                "stamp",
                                StampTypes.v4.ToString(),
                                "analytics"), headers, content, proxy);
            }
            catch (Exception ex)
            {
                return handler.HandleException(ex);
            }
        }
    }
}
