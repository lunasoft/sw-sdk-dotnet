using SW.Entities;
using SW.Helpers;
using System;
using System.Xml.Linq;

namespace SW.Services.Account.AccountBalance
{
    public abstract class BalanceAccountService : Services
    {
        protected BalanceAccountService(string url, string user, string password, string proxy, int proxyPort) : base(url, user, password, proxy, proxyPort)
        {
        }
        protected BalanceAccountService(string url, string token, string proxy, int proxyPort) : base(url, token, proxy, proxyPort)
        {
        }
        protected BalanceAccountService(string url, string urlApi, string user, string password, string proxy, int proxyPort) : base(url, urlApi, user, password, proxy, proxyPort)
        { 
        }
        internal abstract Response GetBalance();
        internal abstract Response StampsDistribution(Guid idUser, int stamps, ActionsAccountBalance action, string comment);
    }
}
