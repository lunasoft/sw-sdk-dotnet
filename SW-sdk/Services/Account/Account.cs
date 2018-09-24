using System;
using SW.Helpers;
using SW.Entities;
using System.Net;

namespace SW.Services.Account
{
    public class BalanceAccount : BalanceAccountService
    {

        BalanceAccountResponseHandler _handler;
        /// <summary>
        /// This Service is Not Implemented
        /// </summary>
        /// <param name="url"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        public BalanceAccount(string url, string user, string password, int proxyPort = 0, string proxy = null) : base(url, user, password, proxy, proxyPort)
        {
            _handler = new BalanceAccountResponseHandler();
        }
        /// <summary>
        /// This Service is Not Implemented
        /// </summary>
        /// <param name="url"></param>
        /// <param name="token"></param>
        public BalanceAccount(string url, string token, int proxyPort = 0, string proxy = null) : base(url, token, proxy, proxyPort)
        {
            _handler = new BalanceAccountResponseHandler();
        }

        internal override Response GetBalance()
        {
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                this.SetupRequest();
                var request = (HttpWebRequest)WebRequest.Create(this.Url + "account/balance");
                request.ContentType = "application/json";
                request.Method = WebRequestMethods.Http.Get;
                request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
                Helpers.RequestHelper.SetupProxy(this.Proxy, this.ProxyPort, ref request);
                request.ContentLength = 0;
                return _handler.GetResponse(request);
            }
            catch (Exception e)
            {
                return _handler.HandleException(e);
            }
        }

        public AccountResponse ConsultarSaldo()
        {
            return (AccountResponse)GetBalance();
        }
    }
}
