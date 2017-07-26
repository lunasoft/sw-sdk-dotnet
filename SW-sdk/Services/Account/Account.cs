using RestSharp;
using System;
using SW.Helpers;

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
            BalanceAccountResponseHandler handler = new BalanceAccountResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                RestRequest request = this.RequestAccount();

                return handler.GetResponse(this.Client, request);
            }
            catch (Exception e)
            { 
                return handler.HandleException(e);
            }
        }

        public AccountResponse ConsultarSaldo()
        {
            return (AccountResponse)GetBalance();
        }
    }
}
