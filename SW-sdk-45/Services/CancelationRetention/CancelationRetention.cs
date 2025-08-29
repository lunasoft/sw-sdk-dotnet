using SW.Services.Cancelation;
using System;
using SW.Helpers;

namespace SW.Services.CancelationRetention
{
    public class CancelationRetention : CancelationRetentionService
    {
        CancelationResponseHandler _handler;

        /// <summary>
        /// This Service is Not Implemented
        /// </summary>
        /// <param name="url"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        public CancelationRetention(string url, string user, string password, int proxyPort = 0, string proxy = null) : base(url, user, password, proxy, proxyPort)
        {
            _handler = new CancelationResponseHandler();
        }
        /// <summary>
        /// This Service is Not Implemented
        /// </summary>
        /// <param name="url"></param>
        /// <param name="token"></param>
        public CancelationRetention(string url, string token, int proxyPort = 0, string proxy = null) : base(url, token, proxy, proxyPort)
        {
            _handler = new CancelationResponseHandler();
        }

        internal override CancelationResponse CancelarRetention(byte[] xmlCancelation)
        {
            CancelationResponseHandler handler = new CancelationResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                var headers = GetHeaders();
                var content = this.RequestCancelar(xmlCancelation);
                var proxy = Helpers.RequestHelper.ProxySettings(this.Proxy, this.ProxyPort);
                return handler.GetPostResponse(this.Url,
                                "retencion/cancel/xml", headers, content, proxy);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }

        internal override CancelationResponse CancelarRetention(string cer, string key, string rfc, string password, string uuid, string motivo, string folioSustitucion)
        {
            CancelationResponseHandler handler = new CancelationResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                var headers = GetHeaders();
                var content = this.RequestCancelar(cer, key, rfc, password, uuid, motivo, folioSustitucion);
                var proxy = Helpers.RequestHelper.ProxySettings(this.Proxy, this.ProxyPort);
                return handler.GetPostResponse(this.Url,
                                "retencion/cancel/csd", headers, content, proxy);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }

        internal override CancelationResponse CancelarRetention(string pfx, string rfc, string password, string uuid, string motivo, string folioSustitucion)
        {
            CancelationResponseHandler handler = new CancelationResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                var headers = GetHeaders();
                var content = this.RequestCancelar(pfx, rfc, password, uuid, motivo, folioSustitucion);
                var proxy = Helpers.RequestHelper.ProxySettings(this.Proxy, this.ProxyPort);
                return handler.GetPostResponse(this.Url,
                                "retencion/cancel/pfx", headers, content, proxy);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }

        /// <summary>
        /// Servicio de cancelación de retenciones por XML.
        /// </summary>
        /// <param name="xmlCancelation">XML de cancelación de retenciones.</param>
        /// <returns><see cref="CancelationRetResponse"/></returns>
        public CancelationResponse CancelarUno(byte[] xmlCancelation)
        {
            return CancelarRetention(xmlCancelation);
        }

        /// <summary>
        /// Servicio de cancelación de retenciones utilizando el certificado CSD.
        /// </summary>
        /// <param name="cer">Certificado CSD del emisor en formato B64.</param>
        /// <param name="key">Key del certificado del emisor en formato B64.</param>
        /// <param name="rfc">RFC del emisor.</param>
        /// <param name="password">Contraseña del certificado.</param>
        /// <param name="uuid">Folio fiscal del comprobante a cancelar.</param>
        /// <param name="motivo">Motivo de cancelación.</param>
        /// <param name="folioSustitucion">Folio fiscal del comprobante que sustituye.</param>
        /// <returns><see cref="CancelationRetResponse"/></returns>
        public CancelationResponse CancelarUnoCSD(string cer, string key, string rfc, string password, string uuid, string motivo, string folioSustitucion = null)
        {
            return CancelarRetention(cer, key, rfc, password, uuid, motivo, folioSustitucion);
        }

        /// <summary>
        /// Servicio de cancelación de retenciones utilizando un PFX.
        /// </summary>
        /// <param name="pfx">Certificados en formato PFX.</param>
        /// <param name="rfc">RFC del emisor.</param>
        /// <param name="password">Contraseña del PFX.</param>
        /// <param name="uuid">Folio fiscal del comprobante a cancelar.</param>
        /// <param name="motivo">Motivo de cancelación.</param>
        /// <param name="folioSustitucion">Folio fiscal del comprobante que sustituye.</param>
        /// <returns><see cref="CancelationRetResponse"/></returns>
        public CancelationResponse CancelarUnoPFX(string pfx, string rfc, string password, string uuid, string motivo, string folioSustitucion = null)
        {
            return CancelarRetention(pfx, rfc, password, uuid, motivo, folioSustitucion);
        }
    }
}
