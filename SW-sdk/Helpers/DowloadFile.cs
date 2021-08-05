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

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    responsev2 = TryGetFile(response);
                }
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
            try
            {
                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.BadRequest)
                {
                    return new StampResponseV2
                    {
                        data = new Data_CFDI_TFD
                        {
                            cfdi = Getxml(response),
                            tfd = null
                        },
                        status = "error"
                    };
                }
                else
                    return new StampResponseV2()
                    {
                        message = ((int)response.StatusCode).ToString(),
                        status = "error",
                        messageDetail = "No fue posible descargar el XML"
                    };
            }
            catch (Exception ex)
            {
                return new StampResponseV2()
                {
                    message = ((int)response.StatusCode).ToString(),
                    status = "error",
                    messageDetail = $"No fue posible descargar el XML: {ex}"
                };
            }
        }

        internal string Getxml(HttpWebResponse response)
        {
            using (var responseStream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(responseStream))
            {
                string responseFromServer = reader.ReadToEnd();
                return responseFromServer;
            }
        }
    }
}