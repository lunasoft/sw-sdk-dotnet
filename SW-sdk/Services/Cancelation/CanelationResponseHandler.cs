using System;
using SW.Helpers;

namespace SW.Services.Cancelation
{
    internal class CanelationResponseHandler : ResponseHandler<CancelationResponse>
    {
        public override CancelationResponse HandleException(Exception ex)
        {
            return ex.ToCancelationResponse();
        }
    }
}
