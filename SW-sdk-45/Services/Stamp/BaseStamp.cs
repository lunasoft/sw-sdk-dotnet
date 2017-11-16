using SW.Helpers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace SW.Services.Stamp
{
    public abstract class BaseStamp : StampService
    {
        private string _operation;
        public BaseStamp(string url, string user, string password, string operation) : base(url, user, password)
        {
            _operation = operation;
        }
        public BaseStamp(string url, string token, string operation) : base(url, token)
        {
            _operation = operation;
        }
        public virtual StampResponseV1 TimbrarV1(string xml, bool isb64 = false)
        {
            StampResponseHandlerV1 handler = new StampResponseHandlerV1();
            try
            {
                string format = isb64 ? "b64" : "";
                var xmlBytes = Encoding.UTF8.GetBytes(xml);
                var headers = GetHeaders();
                var content = GetMultipartContent(xmlBytes);
                return handler.GetPostResponse(this.Url,
                                string.Format("cfdi33/{0}/{1}/{2}",
                                _operation,
                                StampTypes.v1.ToString(),
                                format), headers, content);

            }
            catch (Exception ex)
            {
                return handler.HandleException(ex);
            }
        }
        public virtual StampResponseV2 TimbrarV2(string xml, bool isb64 = false)
        {
            StampResponseHandlerV2 handler = new StampResponseHandlerV2();
            try
            {
                string format = isb64 ? "b64" : "";
                var xmlBytes = Encoding.UTF8.GetBytes(xml);
                var headers = GetHeaders();
                var content = GetMultipartContent(xmlBytes);
                return handler.GetPostResponse(this.Url,
                                string.Format("cfdi33/{0}/{1}/{2}",
                                _operation,
                                StampTypes.v2.ToString(),
                                format), headers, content);
            }
            catch (Exception ex)
            {
                return handler.HandleException(ex);
            }
        }
        public virtual StampResponseV3 TimbrarV3(string xml, bool isb64 = false)
        {
            StampResponseHandlerV3 handler = new StampResponseHandlerV3();
            try
            {
                string format = isb64 ? "b64" : "";
                var xmlBytes = Encoding.UTF8.GetBytes(xml);
                var headers = GetHeaders();
                var content = GetMultipartContent(xmlBytes);
                return handler.GetPostResponse(this.Url,
                                string.Format("cfdi33/{0}/{1}/{2}",
                                _operation,
                                StampTypes.v3.ToString(),
                                format), headers, content);
            }
            catch (Exception ex)
            {
                return handler.HandleException(ex);
            }
        }
        public virtual StampResponseV4 TimbrarV4(string xml, bool isb64 = false)
        {
            StampResponseHandlerV4 handler = new StampResponseHandlerV4();
            try
            {
                string format = isb64 ? "b64" : "";
                var xmlBytes = Encoding.UTF8.GetBytes(xml);
                var headers = GetHeaders();
                var content = GetMultipartContent(xmlBytes);
                return handler.GetPostResponse(this.Url,
                                string.Format("cfdi33/{0}/{1}/{2}",
                                _operation,
                                StampTypes.v4.ToString(),
                                format), headers, content);
            }
            catch (Exception ex)
            {
                return handler.HandleException(ex);
            }
        }
    }
}
