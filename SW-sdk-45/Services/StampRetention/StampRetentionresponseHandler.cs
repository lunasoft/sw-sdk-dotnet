using System;
using System.Collections.Generic;
using System.Net.Http;
using SW.Helpers;

namespace SW.Services.StampRetention
{
    internal class StampRetentionResponseHandlerV3 : ResponseHandler<StampRetentionResponseV3>
    {
        public StampRetentionResponseHandlerV3()
        {
        }

        public StampRetentionResponseHandlerV3(string xmlOriginal) : base(xmlOriginal)
        {
        }

        public override StampRetentionResponseV3 GetPostResponse(string url, string path, Dictionary<string, string> headers, HttpContent content, HttpClientHandler proxy)
        {
            var response = base.GetPostResponse(url, path, headers, content, proxy);
            return response;
        }
        public override StampRetentionResponseV3 HandleException(Exception ex)
        {
            return ex.ToStampRetentionResponseV3();
        }
    }
}
