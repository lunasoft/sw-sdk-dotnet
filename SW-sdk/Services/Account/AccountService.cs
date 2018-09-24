using SW.Entities;
using SW.Helpers;
using System.Xml.Linq;

namespace SW.Services.Account
{
    public abstract class BalanceAccountService : Services
    {
        protected BalanceAccountService(string url, string user, string password, string proxy, int proxyPort) : base(url, user, password, proxy, proxyPort)
        {
        }
        protected BalanceAccountService(string url, string token, string proxy, int proxyPort) : base(url, token, proxy, proxyPort)
        {
        }
        internal abstract Response GetBalance();
    }
}
