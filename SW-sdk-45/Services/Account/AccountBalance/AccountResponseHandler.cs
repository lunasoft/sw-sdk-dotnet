using System;
using SW.Helpers;
using SW.Entities;

namespace SW.Services.Account.AccountBalance
{
    internal class BalanceAccountResponseHandler : ResponseHandler<AccountResponse>
    {
        public override AccountResponse HandleException(Exception ex)
        {
            return ex.ToAccountResponse();
        }

    }
    internal class BalanceResponseHandler : ResponseHandler<AccountBalanceResponse>
    {
        public override AccountBalanceResponse HandleException(Exception ex)
        {
            return ex.ToAccountBalanceResponse();
        }

    }
}
