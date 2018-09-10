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
        public BasePdf(string url, string token, string operation) : base(url, token)
        {
            _operation = operation;
        }
        public BasePdf(string url, string user, string password, string operation) : base(url, user, password)
        {
            _operation = operation;
        }
        public virtual PdfResponse GenerarPdf(string xml, string templateId, Dictionary<string, string> ObservacionesAdicionales = null, bool isB64 = false)
        {
            PdfResponseHandler handler = new PdfResponseHandler();
            try
            {
                string format = isB64 ? "b64" : "";
                var xmlBytes = Encoding.UTF8.GetBytes(xml);
                var headers = GetHeaders();
                var content = GetMultipartContent(xmlBytes, ObservacionesAdicionales);
                return handler.GetPostResponse(this.Url,
                                string.Format("/pdf/v1/generate",
                                _operation), headers, content);
            }
            catch (Exception ex)
            {
                return handler.HandleException(ex);
            }

        }
    }
}
