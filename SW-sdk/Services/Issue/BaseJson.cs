using SW.Helpers;
using SW.Services.Stamp;
using System;
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
                var request = this.RequestStampJson(json, StampTypes.v1.ToString(), _operation);
                return handler.GetResponse(request);
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
                var request = this.RequestStampJson(json, StampTypes.v2.ToString(), _operation);
                return handler.GetResponse(request);
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
                var request = this.RequestStampJson(json, StampTypes.v3.ToString(), _operation);
                return handler.GetResponse(request);
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
                var request = this.RequestStampJson(json, StampTypes.v4.ToString(), _operation);
                return handler.GetResponse(request);
            }
            catch (Exception ex)
            {
                return handler.HandleException(ex);
            }
        }
    }
}
