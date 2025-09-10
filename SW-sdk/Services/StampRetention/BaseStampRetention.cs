using SW.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SW.Services.StampRetention
{
    public abstract class BaseStampRetention : StampRetentionService
    {
        private string _operation;
        
        public BaseStampRetention(string url, string user, string password, string operation, string proxy, int proxyPort) : base(url, user, password, proxy, proxyPort)
        {
            _operation = operation;
        }
        
        public BaseStampRetention(string url, string token, string operation, string proxy, int proxyPort) : base(url, token, proxy, proxyPort)
        {
            _operation = operation;
        }
        
        public virtual StampRetentionResponseV3 TimbrarV3(string xml, string email = null, string customId = null, bool isb64 = false)
        {
            StampRetentionResponseHandlerV3 handler = new StampRetentionResponseHandlerV3();
            try
            {
                string format = isb64 ? "b64" : "";
                var xmlBytes = isb64 ? Convert.FromBase64String(xml) : Encoding.UTF8.GetBytes(xml);
                var request = this.RequestStamping(xmlBytes, StampTypes.v3.ToString(), format, _operation, email, customId);
                return handler.GetResponse(request);
            }
            catch (Exception ex)
            {
                return handler.HandleException(ex);
            }
        }
    }
}