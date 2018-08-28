using System;
using SW.Helpers;
using System.Net;
using System.Net.Http;
using System.Text;

namespace SW.Services.AcceptReject
{
    public class AcceptReject : AcceptRejectService
    {

        AcceptRejectResponseHandler _handler;
        public AcceptReject(string url, string user, string password) : base(url, user, password)
        {
            _handler = new AcceptRejectResponseHandler();
        }
        public AcceptReject(string url, string token) : base(url, token)
        {
            _handler = new AcceptRejectResponseHandler();
        }
        internal override AcceptRejectResponse AcceptRejectRequest(string cer, string key, string rfc, string password, AceptacionRechazoItem[] uuids)
        {
            AcceptRejectResponseHandler handler = new AcceptRejectResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                var headers = GetHeaders();
                var content = this.RequestAcceptReject(cer, key, rfc, password, uuids);
                return handler.GetPostResponse(this.Url,
                                "acceptreject/csd", headers, content);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }
        internal override AcceptRejectResponse AcceptRejectRequest(byte[] xmlCancelation, EnumAcceptReject enumAcceptReject)
        {
            AcceptRejectResponseHandler handler = new AcceptRejectResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                var headers = GetHeaders();
                var content = this.RequestAcceptReject(xmlCancelation, enumAcceptReject);
                return handler.GetPostResponse(this.Url,
                                "acceptreject/xml", headers, content);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }
        internal override AcceptRejectResponse AcceptRejectRequest(string pfx, string rfc, string password, AceptacionRechazoItem[] uuid)
        {
            AcceptRejectResponseHandler handler = new AcceptRejectResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                var headers = GetHeaders();
                var content = this.RequestAcceptReject(pfx, rfc, password, uuid);
                return handler.GetPostResponse(this.Url,
                                "acceptreject/pfx", headers, content);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }
        internal override AcceptRejectResponse AcceptRejectRequest(string rfc, string uuid, EnumAcceptReject enumAcceptReject)
        {
            AcceptRejectResponseHandler handler = new AcceptRejectResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                HttpWebRequest request = this.RequestAcceptReject(rfc, uuid, enumAcceptReject);
                request.ContentType = "application/json";
                request.ContentLength = 0;
                request.Method = WebRequestMethods.Http.Post;
                var headers = GetHeaders();
                return handler.GetPostResponse(this.Url, headers, $"acceptreject/{rfc}/{uuid}/{enumAcceptReject.ToString()}");
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }


        public AcceptRejectResponse AcceptByCSD(string cer, string key, string rfc, string password, AceptacionRechazoItem[] uuids)
        {
            return AcceptRejectRequest(cer, key, rfc, password, uuids);
        }
        public AcceptRejectResponse AcceptByXML(byte[] xmlCancelation, EnumAcceptReject enumCancelation)
        {
            return AcceptRejectRequest(xmlCancelation, enumCancelation);
        }
        public AcceptRejectResponse AcceptByPFX(string pfx, string rfc, string password, AceptacionRechazoItem[] uuid)
        {
            return AcceptRejectRequest(pfx, rfc, password, uuid);
        }
        public AcceptRejectResponse AcceptByRfcUuid(string rfc, string uuid, EnumAcceptReject enumAcceptReject)
        {
            return AcceptRejectRequest(rfc, uuid, enumAcceptReject);
        }
    }
}
