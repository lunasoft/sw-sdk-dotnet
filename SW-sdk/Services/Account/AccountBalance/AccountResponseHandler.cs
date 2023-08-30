using System;
using SW.Helpers;
using SW.Entities;

namespace SW.Services.Account.AccountBalance
{

    internal class AccountResponseHandler : ResponseHandler<AccountResponse>
    {
        public override AccountResponse HandleException(Exception ex)
        {
            return ex.ToAccountResponse();
        }
    }
    internal class BalanceAccountResponseHandler : ResponseHandler<AccountBalanceResponse>
    {
        public override AccountBalanceResponse HandleException(Exception ex)
        {
            return ex.ToAccountBalanceResponse();
        }
    }
}
