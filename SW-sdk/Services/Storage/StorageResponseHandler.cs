using SW.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SW.Services.Storage
{
    internal class StorageResponseHandler : ResponseHandler<StorageResponse>
    {
        public override StorageResponse HandleException(Exception ex)
        {
            return ex.ToStorageResponse();
        }
    }
}
