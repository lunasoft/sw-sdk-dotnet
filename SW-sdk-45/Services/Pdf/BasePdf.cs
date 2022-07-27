using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SW.Helpers;

namespace SW.Services.Pdf
{
    public abstract class BasePdf : PdfService
    {
        private string _operation;
        public BasePdf(string url, string token, string operation, string proxy, int proxyPort) : base(url, token, proxy, proxyPort)
        {
            _operation = operation;
        }
        public BasePdf(string url, string user, string password, string operation, string proxy, int proxyPort) : base(url, user, password, proxy, proxyPort)
        {
            _operation = operation;
        }
        /// <summary>
        /// Servicio para generar el PDF de un comprobante timbrado.
        /// </summary>
        /// <param name="xml">XML del comprobante.</param>
        /// <param name="templateId">Id del template para identificar la versión y tipo de comprobante.</param>
        /// <param name="ObservacionesAdicionales">Agrega comentarios adicionales a la plantilla.</param>
        /// <param name="isB64">Especificar si el XML del comprobante se envía en B64.</param>
        /// <exception cref="System.Exception"></exception>
        /// <returns><see cref="PdfResponse"/></returns>
        public virtual PdfResponse GenerarPdf(string xml, string templateId, Dictionary<string, string> ObservacionesAdicionales = null, bool isB64 = false)
        {
            PdfResponseHandler handler = new PdfResponseHandler();
            try
            {
                string format = isB64 ? "b64" : "";
                var xmlBytes = Encoding.UTF8.GetBytes(xml);
                var headers = GetHeaders();
                var content = GetMultipartContent(xmlBytes, ObservacionesAdicionales);
                var proxy = Helpers.RequestHelper.ProxySettings(this.Proxy, this.ProxyPort);
                return handler.GetPostResponse(this.Url,
                                string.Format("/pdf/v1/generate",
                                _operation), headers, content, proxy);
            }
            catch (Exception ex)
            {
                return handler.HandleException(ex);
            }
        }
    }
}
