using RestSharp;
using SW.Helpers;

namespace SW.Services.Stamp
{
    public abstract class StampService : Services
    {
        protected StampService(string url, string user, string password) : base(url, user, password)
        {
        }
        protected StampService(string url, string token) : base(url, token)
        {
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
        private readonly object mutex = new object();
        private RestClient _client;
        public RestClient Client
        {
            get
            {
                if (_client == null)
                {
                    lock (mutex)
                    {
                        if (_client == null)
                        {
                            _client = new RestClient(this.Url);
                        }
                    }
                }
                return _client;
            }
        }
    }
}
