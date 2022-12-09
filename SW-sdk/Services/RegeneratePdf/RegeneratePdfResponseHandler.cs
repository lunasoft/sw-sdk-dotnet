using SW.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SW.Services.RegeneratePdf
{
    internal class RegeneratePdfResponseHandler : ResponseHandler<RegeneratePdfResponse>
    {
        public override RegeneratePdfResponse HandleException(Exception ex)
        {
            return ex.ToRegeneratePdfResponse();
        }
    }
}
