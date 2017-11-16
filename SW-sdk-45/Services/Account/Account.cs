using System;
using SW.Helpers;
using SW.Entities;
using System.Net;
using System.Collections.Generic;

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
        public BalanceAccount(string url, string user, string password) : base(url, user, password)
        {
            _handler = new BalanceAccountResponseHandler();
        }
        /// <summary>
        /// This Service is Not Implemented
        /// </summary>
        /// <param name="url"></param>
        /// <param name="token"></param>
        public BalanceAccount(string url, string token) : base(url, token)
        {
            _handler = new BalanceAccountResponseHandler();
        }

        internal override Response GetBalance()
        {
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                this.SetupRequest();

                Dictionary<string, string> headers = new Dictionary<string, string>() {
                    { "Authorization", "bearer " + this.Token }
                };

                return _handler.GetResponse(this.Url, headers, "account/balance");
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
