using RestSharp;
using SW.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SW.Services.Authentication
{
    internal class AuthenticationResponseHandler : IResponseHandler
    {
        public Response GetResponse(RestClient client, RestRequest request)
        {
            IRestResponse<AuthResponse> response = client.Execute<AuthResponse>(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                if (string.IsNullOrEmpty(response.Content) || response.Data == null)
                    return new AuthResponse()
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
            return ex.ToAuthResponse();
        }
    }
}
