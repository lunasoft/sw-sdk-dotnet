using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SW.Helpers;

namespace SW.Services.Pdf
{
    public abstract class BasePdf : PdfService
    {
       
        public BasePdf(string url, string token) : base(url, token)
        {
        }
        public BasePdf(string url, string user, string password) : base(url, user, password)
        {
        }
        public virtual PdfResponse GenerarPdf(string xml, string templateId, Dictionary<string, string> ObservacionesAdicionales = null, bool isB64 = false)
        {
            PdfResponseHandler handler = new PdfResponseHandler();
            try
            {
                string format = isB64 ? "b64" : "";
                var xmlBytes = Encoding.UTF8.GetBytes(xml);
                var request = this.RequestPdf(xmlBytes, templateId);
                return handler.GetResponse(request);
            }catch(Exception ex)
            {
                return handler.HandleException(ex);
            }

        }
    }
}
