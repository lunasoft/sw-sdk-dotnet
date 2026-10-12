using SW.Helpers;
using SW.Services.Storage;
using System;
using System.Linq;
using System.Text;

namespace SW.Services.Stamp
{
    public abstract class BaseStampV4XML : StampServiceV4XML
    {
        private string _operation;
        private string _apiUrl;
        public BaseStampV4XML(string url, string urlApi, string token, string operation, int proxyPort = 0, string proxy = null) : base(url, token, proxy, proxyPort)
        {
            _apiUrl = urlApi;
            _operation = operation;
        }
        public virtual StampResponseV2 TimbrarV2(string xml, string email = null, string customId = null, bool isb64 = false)
        {
            StampResponseHandlerV2 handler = new StampResponseHandlerV2();
            try
            {
                string format = isb64 ? "b64" : "";
                var xmlBytes = Encoding.UTF8.GetBytes(xml);
                var headers = GetHeaders(email, customId);
                var request = this.RequestStamping(xmlBytes, StampTypes.v2.ToString(), format, _operation, headers);
                var response =  handler.GetResponse(request);
                if (response.status == "error" && response.message == "CFDI3307 - Timbre duplicado. El customId proporcionado está duplicado.")
                    return RecoverDuplicate(response);
                return response;
            }
            catch (Exception ex)
            {
                return handler.HandleException(ex);
            }
        }

        // La recuperacion solo complementa la respuesta primaria.
        private StampResponseV2 RecoverDuplicate(StampResponseV2 response)
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

                Storage.Storage storage = new Storage.Storage(_apiUrl, Token);
                var xmlFromStorage = storage.GetByUUID(uuid);
                var xmlStorage = xmlFromStorage?.data?.records?.ElementAtOrDefault(0)?.urlXml;
                if (xmlFromStorage == null || xmlFromStorage.status == "error" || string.IsNullOrEmpty(xmlStorage))
                {
                    return AppendRecoveryDetail(response, "No esta disponible el URL de descarga del XML, intente más tarde"
                        + (xmlFromStorage == null ? " | Storage: sin respuesta"
                            : xmlFromStorage.status == "error"
                                ? " | Storage: " + xmlFromStorage.message + " | " + xmlFromStorage.messageDetail
                                : ""));
                }
                DowloadFile downloadHandler = new DowloadFile();
                var dataResult = downloadHandler.GetDowloadFile(xmlStorage);
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
    }
}
