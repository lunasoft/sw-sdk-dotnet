using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SW.Helpers;

namespace SW.Services.Resend
{
    public abstract class BaseResend : ResendService
    {
        public BaseResend(string urlApi, string token, string proxy, int proxyPort) : base(urlApi, token, proxy, proxyPort)
        {
        }
        public BaseResend(string urlApi, string url, string user, string password, string proxy, int proxyPort) : base(url, urlApi, user, password, proxy, proxyPort)
        {
        }
        /// <summary>
        /// Servicio para realizar el reenvio del XML y el PDF.
        /// </summary>
        /// <param name="uuid">UUID .</param>
        /// <param name="emails">Listado de correos (Maximo 5).</param>
        /// <returns></returns>
        public virtual ResendResponse ResendEmail(Guid uuid, string[] emails)
        {
            ResendResponseHandler handler = new ResendResponseHandler();
            try
            {
                if (emails is null || emails.Length > 5 || !Validation.ValidateEmail(emails))
                {
                    throw new Exception("El listado contiene mas de 5 correos o el formato es incorrecto");
                }
                string email = string.Join(",", emails);
                var request = this.RequestResend(uuid, email);
                return handler.GetResponseRequest(request);
            }
            catch (Exception ex)
            {
                return handler.HandleException(ex);
            }
        }
    }
}
