using SW.Helpers;
using SW.Services.Stamp;
using System;
using System.Net.Http;
using System.Text;

namespace SW.Services.Issue
{
    public abstract class BaseStampJson : IssueService
    {
        private string _operation;
        public BaseStampJson(string url, string user, string password, string operation, int proxyPort = 0, string proxy = null) : base(url, user, password, proxy, proxyPort)
        {
            _operation = operation;
        }
        public BaseStampJson(string url, string token, string operation, int proxyPort = 0, string proxy = null) : base(url, token, proxy, proxyPort)
        {
            _operation = operation;
        }
        public virtual StampResponseV1 TimbrarJsonV1(string json)
        {
            StampResponseHandlerV1 handler = new StampResponseHandlerV1();
            try
            {
                var headers = GetHeaders();
                StringContent content = new StringContent(json, Encoding.UTF8, "application/jsontoxml");
                var proxy = Helpers.RequestHelper.ProxySettings(this.Proxy, this.ProxyPort);
                return handler.GetPostResponse(this.Url,
                                string.Format("v3/cfdi33/{0}/{1}/{2}",
                                _operation,
                                StampTypes.v1.ToString(),
                                ""), headers, content, proxy);

            }
            catch (Exception ex)
            {
                return handler.HandleException(ex);
            }
        }
        public virtual StampResponseV2 TimbrarJsonV2(string json)
        {
            StampResponseHandlerV2 handler = new StampResponseHandlerV2();
            try
            {
                var headers = GetHeaders();
                StringContent content = new StringContent(json, Encoding.UTF8, "application/jsontoxml");
                var proxy = Helpers.RequestHelper.ProxySettings(this.Proxy, this.ProxyPort);
                return handler.GetPostResponse(this.Url,
                                string.Format("v3/cfdi33/{0}/{1}/{2}",
                                _operation,
                                StampTypes.v2.ToString(),
                                ""), headers, content, proxy);
            }
            catch (Exception ex)
            {
                return handler.HandleException(ex);
            }
        }
        public virtual StampResponseV3 TimbrarJsonV3(string json)
        {
            StampResponseHandlerV3 handler = new StampResponseHandlerV3();
            try
            {
                var headers = GetHeaders();
                StringContent content = new StringContent(json, Encoding.UTF8, "application/jsontoxml");
                var proxy = Helpers.RequestHelper.ProxySettings(this.Proxy, this.ProxyPort);
                return handler.GetPostResponse(this.Url,
                                string.Format("v3/cfdi33/{0}/{1}/{2}",
                                _operation,
                                StampTypes.v3.ToString(),
                                ""), headers, content, proxy);
            }
            catch (Exception ex)
            {
                return handler.HandleException(ex);
            }
        }
        public virtual StampResponseV4 TimbrarJsonV4(string json)
        {
            StampResponseHandlerV4 handler = new StampResponseHandlerV4();
            try
            {
                var headers = GetHeaders();
                StringContent content = new StringContent(json, Encoding.UTF8, "application/jsontoxml");
                var proxy = Helpers.RequestHelper.ProxySettings(this.Proxy, this.ProxyPort);
                return handler.GetPostResponse(this.Url,
                                string.Format("v3/cfdi33/{0}/{1}/{2}",
                                _operation,
                                StampTypes.v4.ToString(),
                                ""), headers, content, proxy);
            }
            catch (Exception ex)
            {
                return handler.HandleException(ex);
            }
        }
    }
}
