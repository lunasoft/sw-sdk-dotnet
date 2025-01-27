using System;
using System.Collections.Generic;
using System.Text;
using SW.Helpers;

namespace SW.Services.Pdf
{
    public abstract class BasePdf : PdfService
    {
        /// <summary>
        /// Crea una instancia de PdfService heredado de Services.
        /// </summary>
        /// <param name="urlApi">URL de la API.</param>
        /// <param name="token">Token de la cuenta del usuario.</param>
        /// <param name="proxy">Dirección del proxy.</param>
        /// <param name="proxyPort">Puerto del proxy.</param>
        public BasePdf(string urlApi, string token, string proxy, int proxyPort)
            : base(urlApi, token, proxy, proxyPort)
        {
        }

        /// <summary>
        /// Crea una instancia de BasePdf heredado de PdfService.
        /// </summary>
        /// <param name="urlApi">URL de la API.</param>
        /// <param name="url">URL de Services.</param>
        /// <param name="user">Correo del usuario.</param>
        /// <param name="password">Contraseña del usuario.</param>
        /// <param name="proxy">Dirección del proxy.</param>
        /// <param name="proxyPort">Puerto del proxy.</param>
        public BasePdf(string urlApi, string url, string user, string password, string proxy, int proxyPort)
            : base(urlApi, url, user, password, proxy, proxyPort)
        {
        }

        /// <summary>
        /// Genera un PDF utilizando una plantilla genérica.
        /// </summary>
        /// <param name="xml">Cadena XML del comprobante.</param>
        /// <param name="logo">Logotipo en base64.</param>
        /// <param name="templateId">Identificador de la plantilla.</param>
        /// <param name="observacionesAdicionales">Información extra.</param>
        /// <param name="isB64">Indica si el XML está en formato base64.</param>
        /// <returns>Respuesta con el PDF generado.</returns>
        public virtual PdfResponse GenerarPdf(string xml, string logo, TemplatesId templateId, Dictionary<string, string> observacionesAdicionales = null, bool isB64 = false)
        {
            PdfResponseHandler handler = new PdfResponseHandler();
            try
            {
                var xmlContent = isB64 ? Encoding.UTF8.GetString(Convert.FromBase64String(xml)) : xml;
                xmlContent = xmlContent.Replace("\"", "'");

                var request = this.RequestPdf(xmlContent, logo, templateId.ToString(), observacionesAdicionales);
                return handler.GetResponseRequest(request);
            }
            catch (Exception ex)
            {
                return handler.HandleException(ex);
            }
        }

        /// <summary>
        /// Genera un PDF utilizando una plantilla personalizada.
        /// </summary>
        /// <param name="xml">Cadena XML del comprobante.</param>
        /// <param name="logo">Logotipo en base64.</param>
        /// <param name="templateId">Identificador de la plantilla.</param>
        /// <param name="observacionesAdicionales">Información extra.</param>
        /// <param name="isB64">Indica si el XML está en formato base64.</param>
        /// <returns>Respuesta con el PDF generado.</returns>
        public virtual PdfResponse GenerarPdf(string xml, string logo, string templateId, Dictionary<string, string> observacionesAdicionales = null, bool isB64 = false)
        {
            PdfResponseHandler handler = new PdfResponseHandler();
            try
            {
                var xmlContent = isB64 ? Encoding.UTF8.GetString(Convert.FromBase64String(xml)) : xml;
                xmlContent = xmlContent.Replace("\"", "'");

                var request = this.RequestPdf(xmlContent, logo, templateId, observacionesAdicionales);
                return handler.GetResponseRequest(request);
            }
            catch (Exception ex)
            {
                return handler.HandleException(ex);
            }
        }

        /// <summary>
        /// Regenera un PDF de un comprobante previamente timbrado.
        /// </summary>
        /// <param name="uuid">Folio fiscal del comprobante.</param>
        /// <param name="logo">Logotipo en base64 (opcional).</param>
        /// <param name="templateId">Identificador de la plantilla (opcional).</param>
        /// <param name="extras">Información adicional (opcional).</param>
        /// <returns>Respuesta con el PDF regenerado.</returns>
        public virtual PdfResponse RegenerarPdf(Guid uuid, string logo = null, string templateId = null, Dictionary<string, string> extras = null)
        {
            PdfResponseHandler handler = new PdfResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();

                var request = this.RequestRegeneratePdf(uuid, logo, templateId, extras);
                var response = handler.GetResponseRequest(request);

                if (response.message == "Solicitud se proceso correctamente.")
                {
                    response.status = "success";
                }

                return response;
            }
            catch (Exception ex)
            {
                return handler.HandleException(ex);
            }
        }
    }
}

