using RestSharp;
using SW.Services.Authentication;
using SW.Services.Cancelation;
using SW.Services.Stamp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SW.Helpers
{
    internal static class ResponseHelper
    {
        internal static AuthResponse ToAuthResponse(this Exception ex)
        {
            return new AuthResponse()
            {
                message = ex.Message,
                status = "error",
                messageDetail = ex.GetErrorDetail()
            };
        }
        internal static CancelationResponse ToCancelationResponse(this Exception ex)
        {
            return new CancelationResponse()
            {
                message = ex.Message,
                status = "error",
                messageDetail = ex.GetErrorDetail()
            };
        }
        internal static StampResponseV1 ToStampResponseV1(this Exception ex)
        {
            return new StampResponseV1()
            {
                message = ex.Message,
                status = "error",
                messageDetail = ex.GetErrorDetail()
            };
        }
        internal static StampResponseV2 ToStampResponseV2(this Exception ex)
        {
            return new StampResponseV2()
            {
                message = ex.Message,
                status = "error",
                messageDetail = ex.GetErrorDetail()
            };
        }
        internal static StampResponseV3 ToStampResponseV3(this Exception ex)
        {
            return new StampResponseV3()
            {
                message = ex.Message,
                status = "error",
                messageDetail = ex.GetErrorDetail()
            };
        }
        internal static StampResponseV4 ToStampResponseV4(this Exception ex)
        {
            return new StampResponseV4()
            {
                message = ex.Message,
                status = "error",
                messageDetail = ex.GetErrorDetail()
            };
        }
        internal static Response ToResponseError(IRestResponse response, Response data)
        {
            if (string.IsNullOrEmpty(response.Content) || data == null)
            {
                if ((int)response.StatusCode == 0 || string.IsNullOrEmpty(response.StatusDescription))
                    return new Response()
                    {
                        message = "ErrorException",
                        status = "error",
                        messageDetail = response.ErrorMessage
                    };
                else
                    return new Response
                    {
                        message = ((int)response.StatusCode).ToString(),
                        status = "error",
                        messageDetail = response.StatusDescription
                    };
            }
            else
                return data;
        }
        internal static AuthResponse ToAuthResponseError(this IRestResponse<AuthResponse> response)
        {
            return ConvertData<Response, AuthResponse>(ToResponseError(response, response.Data));
        }
        internal static CancelationResponse ToCancelationResponseError(this IRestResponse<CancelationResponse> response)
        {
            return ConvertData<Response, CancelationResponse>(ToResponseError(response, response.Data));
        }
        internal static StampResponseV1 ToStampResponseV1Error(this IRestResponse<StampResponseV1> response)
        {
            return ConvertData<Response, StampResponseV1>(ToResponseError(response, response.Data));
        }
        internal static StampResponseV2 ToStampResponseV2Error(this IRestResponse<StampResponseV2> response)
        {
            return ConvertData<Response, StampResponseV2>(ToResponseError(response, response.Data));
        }
        internal static StampResponseV3 ToStampResponseV3Error(this IRestResponse<StampResponseV3> response)
        {
            return ConvertData<Response, StampResponseV3>(ToResponseError(response, response.Data));
        }
        internal static StampResponseV4 ToStampResponseV4Error(this IRestResponse<StampResponseV4> response)
        {
            return ConvertData<Response, StampResponseV4>(ToResponseError(response, response.Data));
        }
        internal static string GetErrorDetail(this Exception ex)
        {
            if (ex.InnerException != null)
                return ex.InnerException.Message;
            else
                return "";
        }
        internal static T1 ConvertData<T, T1>(T from)
        {
            string fromStr = SimpleJson.SerializeObject(from);
            return SimpleJson.DeserializeObject<T1>(fromStr);
        }

    }
}
