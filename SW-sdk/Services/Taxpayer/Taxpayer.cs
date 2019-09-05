using SW.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace SW.Services.Taxpayer
{
    public class Taxpayer : TaxpayerService
    {
        TaxpayerResponseHandler _handler;
        public Taxpayer(string url, string user, string password, int proxyPort = 0, string proxy = null) : base(url, user, password, proxy, proxyPort)
        {
            _handler = new TaxpayerResponseHandler();
        }
        public Taxpayer(string url, string token, int proxyPort = 0, string proxy = null) : base(url, token, proxy, proxyPort)
        {
            _handler = new TaxpayerResponseHandler();
        }

      
        public TaxpayerResponse GetTaxpayer(string rfc)
        {
            return TaxpayerByRfc(rfc);
        }

        internal override TaxpayerResponse TaxpayerByRfc(string rfc)
        {
            TaxpayerResponseHandler handler = new TaxpayerResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                HttpWebRequest request = this.RequestTaxpayer(rfc);
                return handler.GetResponse(request);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }
    }
}
