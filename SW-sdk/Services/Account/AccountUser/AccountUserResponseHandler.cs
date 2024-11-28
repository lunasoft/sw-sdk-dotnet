using System;
using SW.Helpers;
using SW.Entities;

namespace SW.Services.Account.AccountUser
{
    internal class AccountUserActionsResponseHandler : ResponseHandler<AccountUserActionsResponse>
    {
        public override AccountUserActionsResponse HandleException(Exception ex)
        {
            return ex.ToAccountUserActionsResponse();
        }
    }
    internal class AccountGetUsersResponseHandler : ResponseHandler<AccountGetUsersResponse>
    {
        public override AccountGetUsersResponse HandleException(Exception ex)
        {
            return ex.ToAccountGetUsersResponse();
        }
    }
    internal class AccountGetUserResponseHandler : ResponseHandler<AccountGetUserResponse>
    {
        public override AccountGetUserResponse HandleException(Exception ex)
        {
            return ex.ToAccountGetUserResponse();
        }
    }
}
