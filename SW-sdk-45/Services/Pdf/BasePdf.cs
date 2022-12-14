using System;
using System.Collections.Generic;
using System.Text;
using SW.Helpers;

namespace SW.Services.Pdf
{
    public abstract class BasePdf : PdfService
    {
       
        public BasePdf(string urlApi, string token, string proxy, int proxyPort) : base( urlApi,token, proxy, proxyPort)
        {
        }
        public BasePdf(string urlApi, string url, string user, string password, string proxy, int proxyPort) : base(urlApi, url, user, password, proxy, proxyPort)
        {
        }


        public virtual PdfResponse GenerarPdf(string xml, string logo,TemplatesId templateId, Dictionary<string, string> ObservacionesAdicionales = null, bool isB64 = false)
        {
            PdfResponseHandler handler = new PdfResponseHandler();
            try
            {
                if (isB64 != true)
                {
                    var xmlFormat = xml.Replace("\"", "\'");
                    var request = this.RequestPdf(xmlFormat, logo, templateId.ToString(), ObservacionesAdicionales);
                    return handler.GetResponseRequest(request);
                }
                else
                {
                    var xmlString = Encoding.UTF8.GetString(Convert.FromBase64String(xml));
                    var xmlFormat = xmlString.Replace("\"", "\'");
                    var request = this.RequestPdf(xmlFormat, logo, templateId.ToString(), ObservacionesAdicionales);
                    return handler.GetResponseRequest(request);
                }
            }
            catch(Exception ex)
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
                    return handler.GetResponseRequest(request);
                }
                else {
                    var xmlString = Encoding.UTF8.GetString(Convert.FromBase64String(xml));
                    var xmlFormat = xmlString.Replace("\"", "\'");
                    var request = this.RequestPdf(xmlFormat, logo, templateId, ObservacionesAdicionales);
                    return handler.GetResponseRequest(request);
                }
                
            }
            catch (Exception ex)
            {
                return handler.HandleException(ex);
            }

        }
        public virtual PdfResponse RegenerarPdf(Guid uuid)
        {
            PdfResponseHandler handler = new PdfResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                var request = this.RequestRegeneratePdf(uuid);
                return handler.GetResponseRequest(request);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }

    }
}