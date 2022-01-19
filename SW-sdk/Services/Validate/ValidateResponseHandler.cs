using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SW.Helpers;

namespace SW.Services.Validate
{
    internal class ValidateXMLResponseHandler : ResponseHandler<ValidateXMLResponse>
    {
        public override ValidateXMLResponse HandleException(Exception ex)
        {
            return ex.ToValidateXMLResponse();
        }
    }
}
