using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SW.Helpers;

namespace SW.Services.Pdf
{
    public abstract class BasePdf : PdfService
    {
        /// <summary>
        /// Crear una instancia para PdfService heredado de Services
        /// </summary>
        /// <param name="urlApi">Url de la API</param>
        /// <param name="token">Token de la cuenta del usuario</param>
        /// <param name="proxy">Proxy </param>
        /// <param name="proxyPort">Puerto proxy</param>
        public BasePdf(string urlApi, string token, string proxy, int proxyPort) : base( urlApi,token, proxy, proxyPort)
        {
        }
        /// <summary>
        /// Crear una instancia para BasePdf heredado de PdfService
        /// </summary>
        /// <param name="urlApi">Url de la API</param>
        /// <param name="url">Url de Services</param>
        /// <param name="user">Correo del usuario</param>
        /// <param name="password">Contraseña del usuario</param>
        /// <param name="proxy">Proxy </param>
        /// <param name="proxyPort">Puerto proxy</param>
        public BasePdf(string urlApi, string url, string user, string password, string proxy, int proxyPort) : base(urlApi, url, user, password, proxy, proxyPort)
        {
        }
        /// <summary>
        /// Servicio para generar PDF con plantillas genericas.
        /// </summary>
        /// <param name="xml">XML con formato UTF-8 del comprobante del cual se requiere el PDF </param>
        /// <param name="logo">El logotipo en base 64 .</param>
        /// <param name="templateId">XML con formato UTF-8 del comprobante del cual se requiere el PDF </param>
        /// <param name="ObservacionesAdicionales">Informacion extra </param>
        /// <param name="isB64">Booleano que muestra si es b64 o no</param>
        /// <returns>PdfResponse</returns>

        public virtual PdfResponse GenerarPdf(string xml, string logo,TemplatesId templateId, Dictionary<string, string> ObservacionesAdicionales = null, bool isB64 = false)
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
            catch(Exception ex)
            {
                return handler.HandleException(ex);
            }

        }
       
        /// <summary>
        /// Servicio para generar PDF con plantillas personalizadas.
        /// </summary>
        /// <param name="xml">XML con formato UTF-8 del comprobante del cual se requiere el PDF </param>
        /// <param name="logo">El logotipo en base 64 .</param>
        /// <param name="templateId">XML con formato UTF-8 del comprobante del cual se requiere el PDF </param>
        /// <param name="ObservacionesAdicionales">Informacion extra </param>
        /// <param name="isB64">Booleano que muestra si es b64 o no</param>
        /// <returns>PdfResponse</returns>
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
                else {
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
       
        /// <summary>
        /// Servicio para regenerar PDF de un comprobante previamente timbrado.
        /// </summary>
        /// <param name="uuid">Folio fiscal del comprobante.</param>
        /// <returns>PdfResponse</returns>
        public virtual PdfResponse RegenerarPdf(Guid uuid) {
            PdfResponseHandler handler = new PdfResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                var request = this.RequestRegeneratePdf(uuid);
                var response= handler.GetResponse(request);
                if (response.message == "Solicitud se proceso correctamente.") { response.status = "success"; }
                return response;
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }
    }
}
