using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SW.Helpers;

namespace SW.Services.Pdf
{
    public abstract class BasePdf : PdfService
    {

        public BasePdf(string urlApi, string token, string proxy, int proxyPort) : base(urlApi, token, proxy, proxyPort)
        {
        }
        public BasePdf(string urlApi, string url, string user, string password, string proxy, int proxyPort) : base(url, urlApi, user, password, proxy, proxyPort)
        {
        }


        public virtual PdfResponse GenerarPdf(string xml, string logo, TemplatesId templateId, Dictionary<string, string> ObservacionesAdicionales = null, bool isB64 = false)
        {
            PdfResponseHandler handler = new PdfResponseHandler();
            try
            {
                if (isB64 != true)
                {
                    var xmlFormat = xml.Replace("\"", "\'");
                    var request = this.RequestPdf(xmlFormat, logo, templateId.ToString(), ObservacionesAdicionales);
                    return handler.GetResponse(request);
                }
                else
                {
                    var xmlString = Encoding.UTF8.GetString(Convert.FromBase64String(xml));
                    var xmlFormat = xmlString.Replace("\"", "\'");
                    var request = this.RequestPdf(xmlFormat, logo, templateId.ToString(), ObservacionesAdicionales);
                    return handler.GetResponse(request);
                }
            }
            catch (Exception ex)
            {
                return handler.HandleException(ex);
            }

        }
        public virtual PdfResponse GenerarPdf(string xml, string logo, string templateId, Dictionary<string, string> ObservacionesAdicionales = null, bool isB64 = false)
        {
            PdfResponseHandler handler = new PdfResponseHandler();
            try
            {
                if (isB64 != true)
                {
                    var xmlFormat = xml.Replace("\"", "\'");
                    var request = this.RequestPdf(xmlFormat, logo, templateId, ObservacionesAdicionales);
                    return handler.GetResponse(request);
                }
                else
                {
                    var xmlString = Encoding.UTF8.GetString(Convert.FromBase64String(xml));
                    var xmlFormat = xmlString.Replace("\"", "\'");
                    var request = this.RequestPdf(xmlFormat, logo, templateId, ObservacionesAdicionales);
                    return handler.GetResponse(request);
                }

            }
            catch (Exception ex)
            {
                return handler.HandleException(ex);
            }

        }

    }
}