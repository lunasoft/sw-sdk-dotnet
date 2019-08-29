using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW.Services.Taxpayer
{
    public abstract class TaxpayerService : Services
    {
        public TaxpayerService(string url, string user, string password, string proxy, int proxyPort) : base(url, user, password, proxy, proxyPort)
        {
        }
        public TaxpayerService(string url, string token, string proxy, int proxyPort) : base(url, token, proxy, proxyPort)
        {
        }
        public abstract TaxpayerResponse GetTaxpayer(string rfc);
    }
}
