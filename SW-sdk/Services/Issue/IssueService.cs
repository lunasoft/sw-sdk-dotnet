using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SW.Helpers;
namespace SW.Services.Issue
{
    public abstract class IssueService : Services
    {
        private readonly RestClient _client;
        protected RestClient Client
        {
            get
            {
                return _client;
            }
        }
        protected IssueService(string url, string user, string password) : base(url, user, password)
        {
            _client = new RestClient(this.Url);
        }
        protected IssueService(string url, string token) : base(url, token)
        {
            _client = new RestClient(this.Url);
        }

        internal abstract Response Timbrar(string xml, string version = "v1", bool isB64 = false);

        internal virtual RestRequest RequestStamping(byte[] xml, string version, string format)
        {
            this.SetupRequest();
            RestRequest request = new RestRequest("cfdi33/issue/{version}/{format}", Method.POST);
            request.AddHeader("Authorization", "Bearer " + Token);
            request.AddUrlSegment("version", version);
            if (!string.IsNullOrEmpty(format))
                request.AddUrlSegment("format", format);
            request.AddFileBytes("xml", xml, "xml");
            return request;
        }
    }
}
