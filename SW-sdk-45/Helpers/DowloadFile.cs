using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using SW.Services.Stamp;

namespace SW.Helpers
{
static internal class DowloadFile
    {
        private const int MaxRawBodyLength = 20000;

        public static StampResponseV2 DowloadFileAsync(string url, HttpClientHandler proxy)
        {
            try
            {
                // Esta descarga no envia Authorization aunque haya redireccion.
                proxy.AllowAutoRedirect = true;
                using (HttpClient client = new HttpClient(proxy))
                {
                    using (var result = client.SendAsync(new HttpRequestMessage(HttpMethod.Get, url), HttpCompletionOption.ResponseHeadersRead).Result)
                    {
                        return TryGetFile(result);
                    }
                }
            }
            catch (Exception wex)
            {
                // Incluye URL invalida o relativa.
                var root = HttpBodyReader.Unwrap(wex);
                return new StampResponseV2()
                {
                    message = root.Message,
                    status = "error",
                    messageDetail = Describe(root)
                };
            }
        }

        internal static StampResponseV2 TryGetFile(HttpResponseMessage response)
        {
            int code = (int)response.StatusCode;
            Exception readError;
            byte[] data = HttpBodyReader.ReadAll(response, out readError);
            string charset = response.Content != null && response.Content.Headers.ContentType != null ? response.Content.Headers.ContentType.CharSet : null;
            string body = HttpBodyReader.Decode(data, charset);
            if (readError != null)
            {
                // Lectura fallida o incompleta: se informa la causa y lo recibido.
                return new StampResponseV2()
                {
                    message = string.Format("No fue posible leer la descarga del XML (HTTP {0})", code),
                    status = "error",
                    messageDetail = Describe(readError) + (string.IsNullOrEmpty(body) ? "" : " | Respuesta parcial recibida: " + Truncate(body))
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
            if (value == null || value.Length <= MaxRawBodyLength)
                return value;
            return value.Substring(0, MaxRawBodyLength) + "... [respuesta truncada]";
        }
        private static string Describe(Exception ex)
        {
            var parts = new System.Collections.Generic.List<string>();
            for (var e = ex; e != null; e = e.InnerException)
                parts.Add(e.Message);
            return string.Join(" -> ", parts.ToArray());
        }
    }
}