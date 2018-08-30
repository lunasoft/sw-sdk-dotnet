using SW.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SW.Services.Relations
{
    internal class RelationsResponseHandler : ResponseHandler<RelationsResponse>
    {
        public override RelationsResponse HandleException(Exception ex)
        {
            return ex.ToRelationsResponse();
        }
    }
}
