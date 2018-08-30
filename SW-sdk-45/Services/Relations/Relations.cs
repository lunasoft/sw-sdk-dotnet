using SW.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SW.Services.Relations
{
    public class Relations : RelationsService
    {
        RelationsResponseHandler _handler;
        public Relations(string url, string user, string password) : base(url, user, password)
        {
            _handler = new RelationsResponseHandler();
        }
        public Relations(string url, string token) : base(url, token)
        {
            _handler = new RelationsResponseHandler();
        }
        internal override RelationsResponse RelationsRequest(string cer, string key, string rfc, string password, string uuid)
        {
            RelationsResponseHandler handler = new RelationsResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                var headers = GetHeaders();
                var content = this.RequestRelations(cer, key, rfc, password, uuid);
                return handler.GetPostResponse(this.Url,
                                "relations/csd", headers, content);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }
        internal override RelationsResponse RelationsRequest(byte[] xmlCancelation)
        {
            RelationsResponseHandler handler = new RelationsResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                var headers = GetHeaders();
                var content = this.RequestRelations(xmlCancelation);
                return handler.GetPostResponse(this.Url,
                                "relations/xml", headers, content);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }
        internal override RelationsResponse RelationsRequest(string pfx, string rfc, string password, string uuid)
        {
            RelationsResponseHandler handler = new RelationsResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                var headers = GetHeaders();
                var content = this.RequestRelations(pfx, rfc, password, uuid);
                return handler.GetPostResponse(this.Url,
                                "relations/pfx", headers, content);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }
        internal override RelationsResponse RelationsRequest(string rfc, string uuid)
        {
            RelationsResponseHandler handler = new RelationsResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                HttpWebRequest request = this.RequestRelations(rfc, uuid);
                request.ContentType = "application/json";
                request.ContentLength = 0;
                request.Method = WebRequestMethods.Http.Post;
                var headers = GetHeaders();
                return handler.GetPostResponse(this.Url, headers, $"relations/{rfc}/{uuid}");
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }


        public RelationsResponse RelationsByCSD(string cer, string key, string rfc, string password, string uuid)
        {
            return RelationsRequest(cer, key, rfc, password, uuid);
        }
        public RelationsResponse RelationsByXML(byte[] xmlCancelation)
        {
            return RelationsRequest(xmlCancelation);
        }
        public RelationsResponse RelationsByPFX(string pfx, string rfc, string password, string uuid)
        {
            return RelationsRequest(pfx, rfc, password, uuid);
        }
        public RelationsResponse RelationsByRfcUuid(string rfc, string uuid)
        {
            return RelationsRequest(rfc, uuid);
        }
    }
}
