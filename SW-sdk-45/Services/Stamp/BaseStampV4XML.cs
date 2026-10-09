using System;
using System.Linq;
using System.Collections.Generic;
using SW.Helpers;
using SW.Services.Storage;
using System.Text;

namespace SW.Services.Stamp
{
    public abstract class BaseStampV4XML : StampService
    {
        private string _operation;
        private string _apiUrl;

        public BaseStampV4XML(string url, string urlApi, string token, string operation, string proxy, int proxyPort) : base(url, token, proxy, proxyPort)
        {
            _operation = operation;
            _apiUrl = urlApi;
        }

        public virtual StampResponseV2 TimbrarV2(string xml, string email = null, string customId = null, bool isb64 = false, string[] extras = null)
        {
            StampResponseHandlerV2XML handler = new StampResponseHandlerV2XML(xml);

            string format = isb64 ? "b64" : "";
            var xmlBytes = Encoding.UTF8.GetBytes(xml);
            var headers = GetHeaders(email, customId, extras);
            var content = GetMultipartContent(xmlBytes);
            var proxy = RequestHelper.ProxySettings(this.Proxy, this.ProxyPort);
            var response = handler.GetPostResponse(this.Url,
                            string.Format("v4/cfdi33/{0}/{1}/{2}",
                            _operation,
                            StampTypes.v2.ToString(),
                            format), headers, content, proxy);
            if (response.status == "error" && response.message == "CFDI3307 - Timbre duplicado. El customId proporcionado está duplicado.")
                return RecoverDuplicate(response, headers);
            return response;
        }
        public virtual StampResponseV4 TimbrarV4(string xml, string email = null, string customId = null, bool isb64 = false, string[] extras = null)
        {
            StampResponseHandlerV2XML handler = new StampResponseHandlerV2XML(xml);

            string format = isb64 ? "b64" : "";
            var xmlBytes = Encoding.UTF8.GetBytes(xml);
            var headers = GetHeaders(email, customId, extras);
            var content = GetMultipartContent(xmlBytes);
            var proxy = RequestHelper.ProxySettings(this.Proxy, this.ProxyPort);
            var response = handler.GetPostResponse(this.Url,
                            string.Format("v4/cfdi33/{0}/{1}/{2}",
                            _operation,
                            StampTypes.v2.ToString(),
                            format), headers, content, proxy);
            if (response.status == "error" && response.message == "CFDI3307 - Timbre duplicado. El customId proporcionado está duplicado.")
                response = RecoverDuplicate(response, headers);
            return SafeConvert(response);
        }

        // La recuperacion solo complementa la respuesta primaria.
        private StampResponseV2 RecoverDuplicate(StampResponseV2 response, Dictionary<string, string> headers)
        {
            try
            {
                if (response.data == null || string.IsNullOrEmpty(response.data.tfd))
                    return AppendRecoveryDetail(response, "La respuesta no contiene un TFD para recuperar el XML.");

                Guid uuid;
                try
                {
                    // Guid.TryParse no esta disponible en .NET 3.5.
                    uuid = new Guid(XmlUtils.GetUUIDFromTFD(response.data.tfd));
                }
                catch (FormatException)
                {
                    return AppendRecoveryDetail(response, "El UUID del TFD no es un Guid valido.");
                }
                if (string.IsNullOrEmpty(_apiUrl) || _apiUrl.Trim().Length == 0)
                    return AppendRecoveryDetail(response, "urlApi no esta configurada para consultar Storage.");

                StorageResponseHandler storageHandler = new StorageResponseHandler();
                var xmlFromStorage = storageHandler.GetResponse(_apiUrl,
                    headers, $"datawarehouse/v1/live/{uuid}",
                    RequestHelper.ProxySettings(this.Proxy, this.ProxyPort));
                var xmlStorage = xmlFromStorage?.data?.records?.ElementAtOrDefault(0)?.urlXml;
                if (xmlFromStorage == null || xmlFromStorage.status == "error" || string.IsNullOrEmpty(xmlStorage))
                {
                    return AppendRecoveryDetail(response, "No esta disponible el URL de descarga del XML, intente más tarde"
                        + (xmlFromStorage == null ? " | Storage: sin respuesta"
                            : xmlFromStorage.status == "error"
                                ? " | Storage: " + xmlFromStorage.message + " | " + xmlFromStorage.messageDetail
                                : ""));
                }
                var dataResult = DowloadFile.DowloadFileAsync(xmlStorage, RequestHelper.ProxySettings(this.Proxy, this.ProxyPort));
                if (dataResult == null || dataResult.data == null
                    || !string.IsNullOrEmpty(dataResult.message) || !string.IsNullOrEmpty(dataResult.messageDetail))
                {
                    return AppendRecoveryDetail(response, "No fue posible descargar el XML ya timbrado: "
                        + (dataResult != null ? dataResult.message + " | " + dataResult.messageDetail : "sin respuesta"));
                }
                if (string.IsNullOrEmpty(dataResult.data.cfdi))
                    return AppendRecoveryDetail(response, "La descarga del XML esta vacia.");

                response.data.cfdi = dataResult.data.cfdi;
                return response;
            }
            catch (Exception ex)
            {
                return AppendRecoveryDetail(response, ex.Message);
            }
        }

        private static StampResponseV2 AppendRecoveryDetail(StampResponseV2 response, string detail)
        {
            string diagnostic = "No fue posible recuperar el XML ya timbrado: " + detail;
            response.messageDetail = string.IsNullOrEmpty(response.messageDetail)
                ? diagnostic : response.messageDetail + " | " + diagnostic;
            return response;
        }

        /// <summary>
        /// Convierte la respuesta V2 a V4 conservando lo que respondio SW si falla.
        /// </summary>
        private static StampResponseV4 SafeConvert(StampResponseV2 response)
        {
            try
            {
                return ConvertionHelper.ConvertV2ToV4Response(response);
            }
            catch (Exception ex)
            {
                string conversion = "No fue posible convertir la respuesta a V4: " + ex.Message;
                return new StampResponseV4()
                {
                    status = response.status,
                    message = response.message,
                    messageDetail = string.IsNullOrEmpty(response.messageDetail) ? conversion : response.messageDetail + " | " + conversion,
                    data = response.data != null ? new Data_Complete { cfdi = response.data.cfdi } : null
                };
            }
        }
    }
}