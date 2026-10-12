using SW.Services.Stamp;
using System;
using System.IO;
using System.Net;

namespace SW.Helpers
{
    public class DowloadFile : Services.Services
    {
        public StampResponseV2 GetDowloadFile(string url)
        {
            StampResponseV2 responsev2 = new StampResponseV2();
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = WebRequestMethods.Http.Get;
                RequestHelper.SetupProxy(this.Proxy, this.ProxyPort, ref request);
                // Esta descarga no envia Authorization aunque haya redireccion.
                request.AllowAutoRedirect = true;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    responsev2 = TryGetFile(response);
                }
            }
            catch (WebException wex)
            {
                if (wex.Response != null)
                {
                    // Codigo de error: se devuelve la respuesta del servidor.
                    using (var errorResponse = (HttpWebResponse)wex.Response)
                    {
                        return TryGetFile(errorResponse);
                    }
                }
                responsev2.message = wex.Message;
                responsev2.status = "error";
                responsev2.messageDetail = wex.Status + " | " + wex.Message;
            }
            catch (Exception wex)
            {
                responsev2.message = wex.Message;
                responsev2.status = "error";
                responsev2.messageDetail = wex.StackTrace;
            }
            return responsev2;
        }

        internal StampResponseV2 TryGetFile(HttpWebResponse response)
        {
            int code = (int)response.StatusCode;
            Exception readError;
            byte[] data = HttpBodyReader.ReadAll(response, out readError);
            string body = HttpBodyReader.Decode(data, HttpBodyReader.ExplicitCharset(response.ContentType));
            if (readError != null)
            {
                // Lectura fallida o incompleta: se informa la causa y lo recibido.
                return new StampResponseV2()
                {
                    message = string.Format("No fue posible leer la descarga del XML (HTTP {0})", code),
                    status = "error",
                    messageDetail = readError.Message + (string.IsNullOrEmpty(body) ? "" : " | Respuesta parcial recibida: " + Truncate(body))
                };
            }
            if (code == 200 || code == 400)
            {
                var result = new StampResponseV2
                {
                    data = new Data_CFDI_TFD
                    {
                        cfdi = body,
                        tfd = null
                    },
                    status = "error"
                };
                if (code == 400)
                {
                    result.message = "No fue posible descargar el XML (HTTP 400)";
                    result.messageDetail = Truncate(body);
                }
                return result;
            }
            // Se devuelve lo que respondio el servidor.
            return new StampResponseV2()
            {
                message = string.Format("No fue posible descargar el XML (HTTP {0})", code),
                status = "error",
                messageDetail = string.IsNullOrEmpty(body) ? "El servidor respondio con un cuerpo vacio." : Truncate(body)
            };
        }
        private static string Truncate(string value)
        {
            const int max = 20000;
            if (value == null || value.Length <= max)
                return value;
            return value.Substring(0, max) + "... [respuesta truncada]";
        }
    }
}
