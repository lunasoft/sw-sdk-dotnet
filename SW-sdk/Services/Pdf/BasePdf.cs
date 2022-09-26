using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SW.Helpers;

namespace SW.Services.Pdf
{
    public abstract class BasePdf : PdfService
    {
       
        public BasePdf(string url, string token, string proxy, int proxyPort) : base(url, token, proxy, proxyPort)
        {
        }
        public BasePdf(string url, string user, string password, string proxy, int proxyPort) : base(url, user, password, proxy, proxyPort)
        {
        }
        public virtual PdfResponse GenerarPdf(string xml, string logo, string templateId, Dictionary<string, string> ObservacionesAdicionales = null, bool isB64 = false)
        {
            PdfResponseHandler handler = new PdfResponseHandler();
            try
            {
                var xmlformat = xml.Replace("\"", "\'");
                var request = this.RequestPdf(xmlformat, logo, templateId, ObservacionesAdicionales);
                return handler.GetResponse(request);
            }catch(Exception ex)
            {
                return handler.HandleException(ex);
            }

        }
    }
}
