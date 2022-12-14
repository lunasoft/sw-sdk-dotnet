using Newtonsoft.Json;
using SW.Entities;
using SW.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace SW.Services.Pdf
{
    public abstract class PdfService : Services
    {
        private string _operation;
        private string _apiUrl;
        /// <summary>
        /// Crear una instancia para PdfService heredado de Services
        /// </summary>
        /// <param name="urlApi">Url de la API</param>
        /// <param name="url">Url de Services</param>
        /// <param name="user">Correo del usuario</param>
        /// <param name="password">Contraseña del usuario</param>
        /// <param name="proxy">Proxy </param>
        /// <param name="proxyPort">Puerto proxy</param>
        protected PdfService(string urlApi, string url, string user, string password, string proxy, int proxyPort) : base(url, user, password, proxy, proxyPort)
        {
            _apiUrl = urlApi;
        }
        /// <summary>
        /// Crear una instancia para PdfService heredado de Services
        /// </summary>
        /// <param name="urlApi">Url de la API</param>
        /// <param name="token">Token de la cuenta del usuario</param>
        /// <param name="proxy">Proxy </param>
        /// <param name="proxyPort">Puerto proxy</param>
        protected PdfService(string urlApi, string token, string proxy, int proxyPort) : base(urlApi, token, proxy, proxyPort)
        {
            _apiUrl = urlApi;
        }
        /// <summary>
        /// Peticion post para generar PDF de un comprobante previamente timbrado.
        /// </summary>
        /// <param name="xml">XML con formato UTF-8 del comprobante del cual se requiere el PDF </param>
        /// <param name="logo">El logotipo en base 64 .</param>
        /// <param name="TemplateId">XML con formato UTF-8 del comprobante del cual se requiere el PDF </param>
        /// <param name="ObservacionesAdicionales">Informacion extra </param>
        /// <returns>request</returns>
        internal virtual HttpWebRequest RequestPdf(string xml, string logo, string TemplateId, Dictionary<string, string> ObservacionesAdicionales = null)
        {
            this.SetupRequest();
            var request = (HttpWebRequest)WebRequest.Create(_apiUrl + string.Format("/pdf/v1/api/GeneratePdf"));
            request.ContentType = "application/json";
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
            Helpers.RequestHelper.SetupProxy(this.Proxy, this.ProxyPort, ref request);
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(new PdfRequest()
            {
                xmlContent= xml,
                logo = logo,
                extras = ObservacionesAdicionales,
                templateId = TemplateId
            });
            byte[] streamLenght = Encoding.UTF8.GetBytes(body);
            request.ContentLength = streamLenght.Length;
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(body);
                streamWriter.Flush();
                streamWriter.Close();
            }
            return request;
        }
        /// <summary>
        /// Peticion post para regenerar PDF de un comprobante previamente timbrado.
        /// </summary>
        /// <param name="uuid">Folio fiscal del comprobante.</param>
        /// <returns>request</returns>
        internal virtual HttpWebRequest RequestRegeneratePdf(Guid uuid)
        {
            this.SetupRequest();
            string path = $"/pdf/v1/api/RegeneratePdf/{uuid}";
            var request = (HttpWebRequest)WebRequest.Create(this._apiUrl + path);
            request.ContentType = "application/json";
            request.ContentLength = 0;
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
            Helpers.RequestHelper.SetupProxy(this.Proxy, this.ProxyPort, ref request);
            return request;
        }
    }
}
