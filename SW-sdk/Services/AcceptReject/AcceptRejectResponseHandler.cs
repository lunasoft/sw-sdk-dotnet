using System;
using SW.Helpers;

namespace SW.Services.AcceptReject
{
    internal class AcceptRejectResponseHandler : ResponseHandler<AcceptRejectResponse>
    {
        public override AcceptRejectResponse HandleException(Exception ex)
        {
            return ex.ToAcceptRejectResponse();
        }
    }
}
