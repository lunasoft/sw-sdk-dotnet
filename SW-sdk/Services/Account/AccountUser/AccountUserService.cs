using SW.Entities;
using SW.Helpers;
using System;

namespace SW.Services.Account.AccountUser
{
    public abstract class AccountUserService : Services
    {
        protected AccountUserService(string urlApi, string token, string proxy, int proxyPort) : base(urlApi, token, proxy, proxyPort)
        {
        }
        protected AccountUserService(string url, string urlApi, string user, string password, string proxy, int proxyPort) : base(url, urlApi, user, password, proxy, proxyPort)
        {
        }
        internal abstract Response GetUser(AccountUserRequest bodyRequest);
        internal abstract Response GetUsers(AccountUserFilter? filter=null, string parameter=null, Guid? idUser=null, bool? isActive=null);
        internal abstract Response UserActions(AccountUserAction action, Guid idUser, AccountUserUpdateRequest bodyRequest = null);
    }
}
