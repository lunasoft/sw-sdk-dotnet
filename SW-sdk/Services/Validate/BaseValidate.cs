using System;
using SW.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SW.Services.Validate
{
    public abstract class BaseValidate : ValidateService
    {
        public BaseValidate(string url, string user, string password, string proxy, int proxyPort) : base(url, user, password, proxy, proxyPort)
        {
        }
        public BaseValidate(string url, string token, string proxy, int proxyPort) : base(url, token, proxy, proxyPort)
        {
        }
        public virtual ValidateXMLResponse ValidateXML(string XML, bool? Status = true)
        {
            ValidateXMLResponseHandler handler = new ValidateXMLResponseHandler();
            try
            {
                var xmlBytes = Encoding.UTF8.GetBytes(XML);
                var request = this.RequestValidateXml(xmlBytes,Status);
                return handler.GetResponse(request);
            }
            catch (Exception ex)
            {
                return handler.HandleException(ex);
            }
        }
    }
}