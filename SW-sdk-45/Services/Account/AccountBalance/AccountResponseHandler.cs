using System;
using SW.Helpers;
using SW.Entities;

namespace SW.Services.Account.AccountBalance
{
    internal class AccountBalanceResponseHandler : ResponseHandler<AccountBalanceResponse>
    {
        public override AccountBalanceResponse HandleException(Exception ex)
        {
            return ex.ToAccountBalanceResponse();
        }

    }
    internal class BalanceResponseHandler : ResponseHandler<BalanceResponse>
    {
        public override BalanceResponse HandleException(Exception ex)
        {
            return ex.ToBalanceResponse();
        }

    }
}
