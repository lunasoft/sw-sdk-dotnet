using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SW.Helpers;

namespace SW.Services.Validate
{
    internal class ValidateXmlResponseHandler : ResponseHandler<ValidateXmlResponse>
    {
        public override ValidateXmlResponse HandleException(Exception ex)
        {
            return ex.ToValidateXmlResponse();
        }
    }
    internal class ValidateLrfcResponseHandler : ResponseHandler<ValidateLrfcResponse>
    {
        public override ValidateLrfcResponse HandleException(Exception ex)
        {
            return ex.ToValidateLrfcResponse();
        }
    }
    internal class ValidateLcoResponseHandler : ResponseHandler<ValidateLcoResponse>
    {
        public override ValidateLcoResponse HandleException(Exception ex)
        {
            return ex.ToValidateLcoResponse();
        }
    }
}
