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
        public static StampResponseV2 DowloadFileAsync(string url, HttpClientHandler proxy)
        {
            try
            {
                using (HttpClient client = new HttpClient(proxy))
                {
                    var result = client.GetAsync(url).Result;
                    return  TryGetFile(result);
                }
            }
            catch (HttpRequestException wex)
            {
                return new StampResponseV2()
                {
                    message = wex.Message,
                    status = "error",
                    messageDetail = wex.StackTrace
                };
            }
        } 

        internal static StampResponseV2 TryGetFile(HttpResponseMessage response)
        {
            try
            {
                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.BadRequest)
                {
                    return new StampResponseV2
                    {
                        data = new Data_CFDI_TFD
                        {   
                            cfdi = response.Content.ReadAsStringAsync().Result,
                            tfd = null
                        },
                        status = "error"
                    };
                }
                else
                {
                    return new StampResponseV2()
                    {
                        message = ((int)response.StatusCode).ToString(),
                        status = "error",
                        messageDetail = response.ReasonPhrase
                    };
                }
            }
            catch (Exception)
            {
                return new StampResponseV2()
                {
                    message = ((int)response.StatusCode).ToString(),
                    status = "error",
                    messageDetail = response.ReasonPhrase
                };
            }
        }
    }
}