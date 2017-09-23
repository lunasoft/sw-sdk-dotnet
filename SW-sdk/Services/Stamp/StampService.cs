using RestSharp;
using SW.Helpers;

namespace SW.Services.Stamp
{
    public abstract class StampService : Services
    {
        protected StampService(string url, string user, string password) : base(url, user, password)
        {
            _client = new RestClient(this.Url);
        }
        protected StampService(string url, string token) : base(url, token)
        {
            _client = new RestClient(this.Url);
        }
        internal abstract Response Timbrar(string xml, string version = "v1", bool isb64 = false);        
        internal virtual RestRequest RequestStamping(byte[] xml, string version, string format)
        {
            this.SetupRequest();
            RestRequest request = new RestRequest("cfdi33/stamp/{version}/{format}", Method.POST);
            request.AddHeader("Authorization", "Bearer " + Token);
            request.AddUrlSegment("version", version);
            if (!string.IsNullOrEmpty(format))
                request.AddUrlSegment("format", format);
            request.AddFileBytes("xml", xml, "xml");
            return request;
        }
        private readonly RestClient _client;
        protected RestClient Client
        {
            get
            {
                return _client;
            }
        }
    }
}
