using System.Linq;
using SW.Helpers;
using SW.Services.Storage;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
            var headers =  GetHeaders(email, customId, extras);
            var content = GetMultipartContent(xmlBytes);
            var proxy = RequestHelper.ProxySettings(this.Proxy, this.ProxyPort);
            var response = handler.GetPostResponse(this.Url,
                            string.Format("v4/cfdi33/{0}/{1}/{2}",
                            _operation,
                            StampTypes.v2.ToString(),
                            format), headers, content, proxy);
            if(response.status == "error" && response.message == "CFDI3307 - Timbre duplicado. El customId proporcionado está duplicado.")
            {
                StorageResponseHandler storangeHandler = new StorageResponseHandler();
                string uuid = XmlUtils.GetUUIDFromTFD(response.data.tfd);
                var xmlFromStorange = storangeHandler.GetResponse(_apiUrl,
                                        headers, $"datawarehouse/v1/live/{uuid}",
                                        RequestHelper.ProxySettings(this.Proxy, this.ProxyPort));
                var xmlStorange = xmlFromStorange.data.records.ElementAtOrDefault(0)?.urlXml;
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
                
                var dataResult = DowloadFile.DowloadFileAsync(xmlStorange,
                                    RequestHelper.ProxySettings(this.Proxy, this.ProxyPort));
                dataResult.data.tfd = response.data.tfd;
                dataResult.message = response.message;
                return dataResult;
            }
            return response;
        }
    }
}