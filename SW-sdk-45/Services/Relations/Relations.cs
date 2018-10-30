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
        public Relations(string url, string user, string password, int proxyPort = 0, string proxy = null) : base(url, user, password, proxy, proxyPort)
        {
            _handler = new RelationsResponseHandler();
        }
        public Relations(string url, string token, int proxyPort = 0, string proxy = null) : base(url, token, proxy, proxyPort)
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
                var proxy = Helpers.RequestHelper.ProxySettings(this.Proxy, this.ProxyPort);
                return handler.GetPostResponse(this.Url,
                                "relations/csd", headers, content, proxy);
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
                var proxy = Helpers.RequestHelper.ProxySettings(this.Proxy, this.ProxyPort);
                return handler.GetPostResponse(this.Url,
                                "relations/xml", headers, content, proxy);
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
                var proxy = Helpers.RequestHelper.ProxySettings(this.Proxy, this.ProxyPort);
                return handler.GetPostResponse(this.Url,
                                "relations/pfx", headers, content, proxy);
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
                var proxy = Helpers.RequestHelper.ProxySettings(this.Proxy, this.ProxyPort);
                return handler.GetPostResponse(this.Url, headers, $"relations/{rfc}/{uuid}", proxy);
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
