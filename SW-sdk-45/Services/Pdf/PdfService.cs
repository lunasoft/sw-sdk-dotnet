using Newtonsoft.Json.Linq;
using SW.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace SW.Services.Pdf
{
    public abstract class PdfService : Services
    {
        private readonly string _apiUrl;

        /// <summary>
        /// Crear una instancia para PdfService heredado de Services
        /// </summary>
        /// <param name="urlApi">URL de la API</param>
        /// <param name="url">URL de Services</param>
        /// <param name="user">Correo del usuario</param>
        /// <param name="password">Contraseña del usuario</param>
        /// <param name="proxy">Proxy</param>
        /// <param name="proxyPort">Puerto proxy</param>
        protected PdfService(string urlApi, string url, string user, string password, string proxy, int proxyPort)
            : base(url, user, password, proxy, proxyPort)
        {
            _apiUrl = urlApi;
        }

        /// <summary>
        /// Crear una instancia para PdfService heredado de Services
        /// </summary>
        /// <param name="urlApi">URL de la API</param>
        /// <param name="token">Token de la cuenta del usuario</param>
        /// <param name="proxy">Proxy</param>
        /// <param name="proxyPort">Puerto proxy</param>
        protected PdfService(string urlApi, string token, string proxy, int proxyPort)
            : base(urlApi, token, proxy, proxyPort)
        {
            _apiUrl = urlApi;
        }

        /// <summary>
        /// Crea y configura una solicitud HTTP.
        /// </summary>
        /// <param name="endpoint">Endpoint de la API.</param>
        /// <returns>Una instancia configurada de <see cref="HttpWebRequest"/>.</returns>
        private HttpWebRequest CreateHttpRequest(string endpoint)
        {
            var request = (HttpWebRequest)WebRequest.Create(_apiUrl + endpoint);
            request.ContentType = "application/json";
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add(HttpRequestHeader.Authorization, $"Bearer " + this.Token);
            RequestHelper.SetupProxy(Proxy, ProxyPort, ref request);
            return request;
        }

        /// <summary>
        /// Envía los datos en formato JSON al cuerpo de la solicitud.
        /// </summary>
        /// <param name="request">Instancia de la solicitud HTTP.</param>
        /// <param name="body">Cuerpo de la solicitud en formato JSON.</param>
        private void WriteRequestBody(HttpWebRequest request, string body)
        {
            byte[] bodyBytes = Encoding.UTF8.GetBytes(body);
            request.ContentLength = bodyBytes.Length;

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(body);
            }
        }

        /// <summary>
        /// Petición POST para generar PDF de un comprobante previamente timbrado.
        /// </summary>
        /// <param name="xml">XML con formato UTF-8 del comprobante.</param>
        /// <param name="logo">El logotipo en base64.</param>
        /// <param name="templateId">Identificador de la plantilla.</param>
        /// <param name="extras">Información adicional.</param>
        /// <returns>Una instancia configurada de <see cref="HttpWebRequest"/>.</returns>
        internal virtual HttpWebRequest RequestPdf(string xml, string logo = null, string templateId = null, Dictionary<string, object> extras = null)
        {
            SetupRequest();

            var request = CreateHttpRequest("/pdf/v1/api/GeneratePdf");
            var jsonBody = new JObject
            {
                ["xmlContent"] = xml,
                ["logo"] = logo,
                ["templateId"] = templateId
            };
            if (extras != null)
            {
                foreach (var kvp in extras)
                {
                    jsonBody[kvp.Key] = JToken.FromObject(kvp.Value);
                }
            }
            var body = jsonBody.ToString();
            WriteRequestBody(request, body);
            return request;
        }

        /// <summary>
        /// Petición POST para regenerar PDF de un comprobante previamente timbrado.
        /// </summary>
        /// <param name="uuid">Folio fiscal del comprobante.</param>
        /// <param name="logo">El logotipo en base64.</param>
        /// <param name="templateId">Identificador de la plantilla.</param>
        /// <param name="extras">Información adicional.</param>
        /// <returns>Una instancia configurada de <see cref="HttpWebRequest"/>.</returns>
        internal virtual HttpWebRequest RequestRegeneratePdf(Guid uuid, string logo = null, string templateId = null, Dictionary<string, object> extras = null)
        {
            SetupRequest();

            var request = CreateHttpRequest($"/pdf/v1/api/RegeneratePdf/{uuid}");
            var jsonBody = new JObject
            {
                ["logo"] = logo,
                ["templateId"] = templateId
            };
            if (extras != null)
            {
                foreach (var kvp in extras)
                {
                    jsonBody[kvp.Key] = JToken.FromObject(kvp.Value);
                }
            }
            var body = jsonBody.ToString();
            WriteRequestBody(request, body);
            return request;
        }
    }
}
