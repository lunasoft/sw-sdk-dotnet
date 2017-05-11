using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SW
{
    internal interface IResponseHandler
    {
        SW.Helpers.Response GetResponse(RestClient client, RestRequest request);
        SW.Helpers.Response HandleException(Exception ex);
    }
}
