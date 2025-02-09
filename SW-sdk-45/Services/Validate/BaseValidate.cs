using System;
using SW.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Activation;
using System.Net.NetworkInformation;

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
        public virtual ValidateXmlResponse ValidateXml(string XML, bool? status=true)
        {
            ValidateXmlResponseHandler handler = new ValidateXmlResponseHandler();
            try
            {
                var xmlBytes = Encoding.UTF8.GetBytes(XML);
                var headers = GetHeaders();
                var content = GetMultipartContent(xmlBytes);
                var proxy = Helpers.RequestHelper.ProxySettings(this.Proxy, this.ProxyPort);
                string path = (status == false) ? "validate/cfdi?validatestatus=false" : "validate/cfdi";
                return handler.GetPostResponse(this.Url,path, headers, content, proxy);
            }
            catch (Exception ex)
            {
                return handler.HandleException(ex);
            }
        }
    }
}