using SW.Entities;
using SW.Helpers;
using System.Xml.Linq;

namespace SW.Services.Account
{
    public abstract class BalanceAccountService : Services
    {
        protected BalanceAccountService(string url, string user, string password) : base(url, user, password)
        {
        }
        protected BalanceAccountService(string url, string token) : base(url, token)
        {
        }
        internal abstract Response GetBalance();
    }
}
