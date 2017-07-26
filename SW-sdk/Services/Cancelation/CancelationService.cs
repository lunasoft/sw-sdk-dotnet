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
        internal abstract Response Cancelar(string cer, string key, string rfc, string password, string uuid);
        internal abstract Response Cancelar(byte[] xmlCancelation);

        internal virtual RestRequest RequestCancelar(string cer, string key, string rfc, string password, string uuid)
        {
            this.SetupRequest();
            RestRequest request = new RestRequest("/cfdi33/cancel/csd", Method.POST);
            request.AddHeader("Content-type", "application/json");
            request.AddHeader("Authorization", "Bearer " + Token);
            request.AddJsonBody(
                new
                {
                    uuid = uuid,
                    password = password,
                    rfc = rfc,
                    b64Cer = cer,
                    b64Key = key
                });
            
            return request;
        }
        internal virtual RestRequest RequestCancelar(byte[] xmlCancelation)
        {
            this.SetupRequest();
            RestRequest request = new RestRequest("/cfdi33/cancel/xml", Method.POST);
            request.AddHeader("Authorization", "Bearer " + Token);
            request.AddFileBytes("xml", xmlCancelation, "xml");
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
