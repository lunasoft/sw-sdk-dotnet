using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using SW.Helpers;

namespace SW.Services.Cancelation
{
    public class CanelationResponseHandler : IResponseHandler
    {
        public Response GetResponse(RestClient client, RestRequest request)
        {
            IRestResponse<CancelationResponse> response = client.Execute<CancelationResponse>(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                if (string.IsNullOrEmpty(response.Content) || response.Data == null)
                    return new CancelationResponse()
                    {
                        message = ((int)response.StatusCode).ToString(),
                        status = "error",
                        messageDetail = response.StatusDescription
                    };
                else
                    return response.Data;
            }
            else
                return response.Data;
        }

        public Response HandleException(Exception ex)
        {
            return ex.ToCancelationResponse();
        }
    }
}
