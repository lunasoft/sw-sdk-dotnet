using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SW.Helpers;

namespace SW.Services.Validate
{
    internal class ValidateResponseHandler : ResponseHandler<ValidateResponse>
    {
        public override ValidateResponse HandleException(Exception ex)
        {
            return ex.ToValidateResponse();
        }
    }
}
