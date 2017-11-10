using System;
using SW.Helpers;
using SW.Entities;

namespace SW.Services.Account
{
    internal class BalanceAccountResponseHandler : ResponseHandler<AccountResponse>
    {
        public override AccountResponse HandleException(Exception ex)
        {
            return ex.ToAccountResponse();
        }
    }
}
