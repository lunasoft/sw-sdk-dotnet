using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SW.Helpers;

namespace SW.Services.Csd
{
    internal class CsdResponseHandler : ResponseHandler<UploadCsdResponse>
    {
        public override UploadCsdResponse HandleException(Exception ex)
        {
            return ex.ToCargaCsdResponse();
        }
    }
}
