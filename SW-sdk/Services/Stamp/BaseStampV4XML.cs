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
                {
                    StorageResponseHandler storangeHandler = new StorageResponseHandler();
                    string uuid = XmlUtils.GetUUIDFromTFD(response.data.tfd);
                    Storage.Storage storage = new Storage.Storage(_apiUrl, Token);
                    var xmlFromStorange = storage.GetByUUID(new Guid(uuid));
                    var xmlStorange = xmlFromStorange?.data?.records?.ElementAtOrDefault(0)?.urlXml;
                    
                    if (string.IsNullOrEmpty(xmlStorange))
                    {
                        return new StampResponseV2()
                        {
                            data = new Data_CFDI_TFD
                            {
                                tfd = response.data.tfd
                            },
                            message = "No es posible obtener el url para decargar el XML",
                            status = "error",
                            messageDetail = "No esta disponible el URL de descarga del XML, intente más tarde"
                        };
                    }
                    DowloadFile dowloadHandle = new DowloadFile();
                    var dataResult = dowloadHandle.GetDowloadFile(xmlStorange);
                    dataResult.data.tfd = response.data.tfd;
                    dataResult.message = response.message;
                    return dataResult;
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
