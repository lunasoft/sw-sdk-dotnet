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
        /// <summary>
        /// Servicio para consultar por CSD si un comprobante cuenta con folios relacionados.
        /// </summary>
        /// <param name="cer">B64 del certificado CSD del emisor.</param>
        /// <param name="key">B64 del certificado Key del emisor.</param>
        /// <param name="rfc">RFC del emisor.</param>
        /// <param name="password">Contraseña de los certificados.</param>
        /// <param name="uuid">UUID del comprobante.</param>
        /// <exception cref="System.Exception"></exception>
        /// <returns><see cref="RelationsResponse"/></returns>
        public RelationsResponse RelationsByCSD(string cer, string key, string rfc, string password, string uuid)
        {
            return RelationsRequest(cer, key, rfc, password, uuid);
        }
        /// <summary>
        /// Servicio para consultar por XML si un comprobante cuenta con folios relacionados.
        /// </summary>
        /// <param name="xmlCancelation">XML de la consulta firmado.</param>
        /// <exception cref="System.Exception"></exception>
        /// <returns><see cref="RelationsResponse"/></returns>
        public RelationsResponse RelationsByXML(byte[] xmlCancelation)
        {
            return RelationsRequest(xmlCancelation);
        }
        /// <summary>
        /// Servicio para consultar por PFX si un comprobante cuenta con folios relacionados.
        /// </summary>
        /// <param name="pfx">B64 del PFX de los certificados del emisor.</param>
        /// <param name="rfc">RFC del emisor.</param>
        /// <param name="password">Contraseña del emisor.</param>
        /// <param name="uuid">UUID del comprobante.</param>
        /// <exception cref="System.Exception"></exception>
        /// <returns><see cref="RelationsResponse"/></returns>
        public RelationsResponse RelationsByPFX(string pfx, string rfc, string password, string uuid)
        {
            return RelationsRequest(pfx, rfc, password, uuid);
        }
        /// <summary>
        /// Servicio para consultar por UUID si un comprobante cuenta con folios relacionados.
        /// </summary>
        /// <param name="rfc">RFC del emisor.</param>
        /// <param name="uuid">UUID del comprobante.</param>
        /// <exception cref="System.Exception"></exception>
        /// <returns><see cref="RelationsResponse"/></returns>
        public RelationsResponse RelationsByRfcUuid(string rfc, string uuid)
        {
            return RelationsRequest(rfc, uuid);
        }
    }
}
