using RestSharp;
using SW.Helpers;
using System.Xml.Linq;

namespace SW.Services.Account
{
    public abstract class BalanceAccountService : Services
    {
        protected BalanceAccountService(string url, string user, string password) : base(url, user, password)
        {
        }
        protected BalanceAccountService(string url, string token) : base(url, token)
        {
        }
        internal abstract Response GetBalance();

        internal virtual RestRequest RequestAccount()
        {
            this.SetupRequest();
            RestRequest request = new RestRequest("/account/balance", Method.GET);
            request.AddHeader("Content-type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Token);
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
