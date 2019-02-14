using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SW.Helpers;

namespace SW.Services.Csd
{
    internal class CsdResponseHandler : ResponseHandler<CsdResponse>
    {
        public override CsdResponse HandleException(Exception ex)
        {
            return ex.ToUploadCsdResponse();
        }
    }

    internal class InfoCsdResponseHandler : ResponseHandler<InfoCsdResponse>
    {
        public override InfoCsdResponse HandleException(Exception ex)
        {
            return ex.ToInfoCsdResponse();
        }
    }

    internal class ListInfoCsdResponseHandler : ResponseHandler<ListInfoCsdResponse>
    {
        public override ListInfoCsdResponse HandleException(Exception ex)
        {
            return ex.ToListInfoCsdResponse();
        }
    }
}
