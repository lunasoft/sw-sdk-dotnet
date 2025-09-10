using System;
using SW.Helpers;

namespace SW.Services.StampRetention
{
    internal class StampRetentionResponseHandlerV3 : ResponseHandler<StampRetentionResponseV3>
    {
        public override StampRetentionResponseV3 HandleException(Exception ex)
        {
            return ex.ToStampRetentionResponseV3();
        }
    }
}