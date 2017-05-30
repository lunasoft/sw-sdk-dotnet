using RestSharp;

namespace SW.Services.Authentication
{
    public abstract class AuthenticationService : Services
    {
        
        public AuthenticationService(string url, string user, string password) : base(url, user, password)
        {
        }
        public abstract AuthResponse GetToken();
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
