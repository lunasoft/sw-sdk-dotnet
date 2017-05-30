using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SW.Helpers;

namespace SW.Services.Stamp
{

    internal class StampResponseHandlerV1 : IResponseHandler
    {
        public Response GetResponse(RestClient client, RestRequest request)
        {
            IRestResponse<StampResponseV1> response = client.Execute<StampResponseV1>(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return response.ToStampResponseV1Error();
            else
                return response.Data;
        }

        public Response HandleException(Exception ex)
        {
            return ex.ToStampResponseV1();
        }
    }
    internal class StampResponseHandlerV2 : IResponseHandler
    {
        public Response GetResponse(RestClient client, RestRequest request)
        {
            IRestResponse<StampResponseV2> response = client.Execute<StampResponseV2>(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return response.ToStampResponseV2Error();
            else
                return response.Data;
        }

        public Response HandleException(Exception ex)
        {
            return ex.ToStampResponseV2();
        }
    }
    internal class StampResponseHandlerV3 : IResponseHandler
    {
        public Response GetResponse(RestClient client, RestRequest request)
        {
            IRestResponse<StampResponseV3> response = client.Execute<StampResponseV3>(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return response.ToStampResponseV3Error();
            else
                return response.Data;
        }

        public Response HandleException(Exception ex)
        {
            return ex.ToStampResponseV3();
        }
    }
    internal class StampResponseHandlerV4 : IResponseHandler
    {
        public Response GetResponse(RestClient client, RestRequest request)
        {
            IRestResponse<StampResponseV4> response = client.Execute<StampResponseV4>(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return response.ToStampResponseV4Error();
            else
                return response.Data;
        }
        public Response HandleException(Exception ex)
        {
            return ex.ToStampResponseV4();
        }
    }
    internal class StampResponseHandler
    {
        IResponseHandler _responseHandler;
        public StampResponseHandler(string version)
        {
            switch (version)
            {
                case "v1":
                    ResponseHandler = new StampResponseHandlerV1();
                    break;
                case "v2":
                    ResponseHandler = new StampResponseHandlerV2();
                    break;
                case "v3":
                    ResponseHandler = new StampResponseHandlerV3();
                    break;
                case "v4":
                    ResponseHandler = new StampResponseHandlerV4();
                    break;
                default:
                    throw new NotImplementedException("Version de timbrado no valida.");
            }
        }

        internal IResponseHandler ResponseHandler
        {
            get
            {
                return _responseHandler;
            }

            set
            {
                _responseHandler = value;
            }
        }
    }
}
