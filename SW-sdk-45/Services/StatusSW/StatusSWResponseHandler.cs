using SW.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW.Services.StatusSW
{
   internal class StatusSWResponseHandler : ResponseHandler<StatusSWResponse>
    {
        public override StatusSWResponse HandleException(Exception ex)
        {
            return ex.ToStatusSWResponse();
        }
    }
}
