using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SW.Helpers;

namespace SW.Services.Pdf
{
    internal class PdfResponseHandler : ResponseHandler<PdfResponse>
    {
        public override PdfResponse HandleException(Exception ex)
        {
            return ex.ToPdfResponse();
        }
    }
}
