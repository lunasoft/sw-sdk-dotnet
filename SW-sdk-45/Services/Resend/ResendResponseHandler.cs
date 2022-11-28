using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SW.Helpers;

namespace SW.Services.Resend
{
    internal class ResendResponseHandler : ResponseHandler<ResendResponse>
    {
        public override ResendResponse HandleException(Exception ex)
        {
            return ex.ToResendResponse();
        }
    }
}
