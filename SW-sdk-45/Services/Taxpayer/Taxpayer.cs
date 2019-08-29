using SW.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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

        public override TaxpayerResponse GetTaxpayer(string rfc)
        {

            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                this.SetupRequest();
                Dictionary<string, string> headers = new Dictionary<string, string>() {
                    { "Authorization", "bearer " + this.Token }

                };
                var proxy = RequestHelper.ProxySettings(this.Proxy, this.ProxyPort);
                return _handler.GetResponse(this.Url, headers, $"taxpayers/{rfc}", proxy);
            }
            catch (Exception e)
            {
                return _handler.HandleException(e);
            }
        }
    }
}
