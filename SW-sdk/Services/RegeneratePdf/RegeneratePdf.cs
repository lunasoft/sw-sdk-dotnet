using SW.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace SW.Services.RegeneratePdf
{
    public class RegeneratePdf : RegeneratePdfService
    {
        RegeneratePdfResponseHandler _handler;
        public RegeneratePdf(string urlApi, string url, string user, string password, int proxyPort = 0, string proxy = null) : base(urlApi,url,user, password, proxy, proxyPort)
        {
            _handler = new RegeneratePdfResponseHandler();
        }
        
        public RegeneratePdf(string urlApi, string token, int proxyPort = 0, string proxy = null) : base(urlApi, token, proxy, proxyPort)
        {
            _handler = new RegeneratePdfResponseHandler();
        }
        public RegeneratePdfResponse GetByUUID(Guid uuid)
        {
            return RegeneratePdfByUUID(uuid);
        }
        internal override RegeneratePdfResponse RegeneratePdfByUUID(Guid uuid)
        {
            RegeneratePdfResponseHandler handler = new RegeneratePdfResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                HttpWebRequest request = this.RequestRegeneratePdf(uuid);
                return handler.GetResponse(request);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }
    }
}