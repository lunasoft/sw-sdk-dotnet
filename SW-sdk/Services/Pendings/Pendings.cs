using SW.Helpers;
using System;
using System.Net;

namespace SW.Services.Pendings
{
    public class Pending : PendingsService
    {
        PendingsResponseHandler _handler;
        public Pending(string url, string user, string password, int proxyPort = 0, string proxy = null) : base(url, user, password, proxy, proxyPort)
        {
            _handler = new PendingsResponseHandler();
        }
        public Pending(string url, string token, int proxyPort = 0, string proxy = null) : base(url, token, proxy, proxyPort)
        {
            _handler = new PendingsResponseHandler();
        }
        internal override PendingsResponse PendingsRequest(string rfc)
        {
            PendingsResponseHandler handler = new PendingsResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                HttpWebRequest request = this.RequestPendings(rfc);
                return handler.GetResponse(request);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }
        public PendingsResponse PendingsByRfc(string rfc)
        {
            return PendingsRequest(rfc);
        }
    }
}
