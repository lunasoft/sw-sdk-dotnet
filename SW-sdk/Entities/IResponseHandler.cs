using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace SW
{
    internal interface IResponseHandler
    {
        SW.Entities.Response GetResponse(WebRequest request);
        SW.Entities.Response HandleException(Exception ex);
    }
}
