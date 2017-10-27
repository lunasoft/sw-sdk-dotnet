using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SW.Helpers;
using SW.Services.Stamp;
using RestSharp;

namespace SW.Services.Issue
{
    public class Issue : IssueService
    {
        public Issue(string url, string user, string password) : base(url, user, password)
        {
        }
        public Issue(string url, string token) : base(url, token)
        {
        }
        internal override Response Timbrar(string xml, string version = "v1", bool isb64 = false)
        {
            StampResponseHandler handler = new StampResponseHandler(version);
            try
            {
                string format = isb64 ? "b64" : "";
                byte[] xmlBytes = null;
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                xmlBytes = Encoding.UTF8.GetBytes(xml);

                RestRequest request = this.RequestStamping(xmlBytes, version, format);

                return handler.ResponseHandler.GetResponse(this.Client, request);
            }
            catch (Exception e)
            {
                return handler.ResponseHandler.HandleException(e);
            }
        }
        public StampResponseV1 TimbrarV1(string xml, bool isb64 = false)
        {
            return (StampResponseV1)Timbrar(xml, StampTypes.v1.ToString(), isb64);
        }
        public StampResponseV2 TimbrarV2(string xml, bool isb64 = false)
        {
            return (StampResponseV2)Timbrar(xml, StampTypes.v2.ToString(), isb64);
        }
        public StampResponseV3 TimbrarV3(string xml, bool isb64 = false)
        {
            return (StampResponseV3)Timbrar(xml, StampTypes.v3.ToString(), isb64);
        }
        public StampResponseV4 TimbrarV4(string xml, bool isb64 = false)
        {
            return (StampResponseV4)Timbrar(xml, StampTypes.v4.ToString(), isb64);
        }


    }
}
