using RestSharp;
using SW.Helpers;

namespace SW.Services.Cancelation
{
    public abstract class CancelationService : Services
    {
        protected CancelationService(string url, string user, string password) : base(url, user, password)
        {
        }
        protected CancelationService(string url, string token) : base(url, token)
        {
        }
        public abstract CancelationResponse Cancelar(CancelationTypes cancelationTypes, string cer, string key,
                                                                                      string password, string[] uuids);
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
