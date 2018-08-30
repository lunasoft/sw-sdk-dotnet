using SW.Helpers;
using System;
using System.Net;

namespace SW.Services.Pendings
{
    public class Pending : PendingsService
    {
        PendingsResponseHandler _handler;
        public Pending(string url, string user, string password) : base(url, user, password)
        {
            _handler = new PendingsResponseHandler();
        }
        public Pending(string url, string token) : base(url, token)
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
                request.ContentType = "application/json";
                request.ContentLength = 0;
                request.Method = WebRequestMethods.Http.Get;
                var headers = GetHeaders();
                return handler.GetResponse(this.Url, headers, $"pendings/{rfc}");
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
