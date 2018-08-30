using System;
using SW.Helpers;

namespace SW.Services.Pendings
{
    internal class PendingsResponseHandler : ResponseHandler<PendingsResponse>
    {
        public override PendingsResponse HandleException(Exception ex)
        {
            return ex.ToPendingsResponse();
        }
    }
}
