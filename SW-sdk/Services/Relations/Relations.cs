using SW.Helpers;
using System;
using System.Net;

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
                HttpWebRequest request = this.RequestRelations(cer, key, rfc, password, uuid);
                return handler.GetResponse(request);
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
                HttpWebRequest request = this.RequestRelations(xmlCancelation);
                return handler.GetResponse(request);
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
                HttpWebRequest request = this.RequestRelations(pfx, rfc, password, uuid);
                return handler.GetResponse(request);
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
                return handler.GetResponse(request);
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
