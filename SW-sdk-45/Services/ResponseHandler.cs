using SW.Entities;
using SW.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace SW.Services
{
    internal abstract class ResponseHandler<T>
        where T : Response, new()
    {
        public ResponseHandler() { }
        public readonly string _xmlOriginal;
        public ResponseHandler(string xmlOriginal)
        {
            _xmlOriginal = xmlOriginal;
        }
        public virtual T GetPostResponse(string url, string path, Dictionary<string, string> headers, HttpContent content, HttpClientHandler proxy)
        {
            try
            {
                using (HttpClient client = new HttpClient(proxy))
                {
                    client.BaseAddress = new Uri(url);
                    foreach (var header in headers)
                    {
                        if(header.Value!=null)
                            client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                    var result = client.PostAsync(path, content).Result;
                    return TryGetResponse(result);
                }
            }
            catch (HttpRequestException wex)
            {
                return new T()
                {
                    message = wex.Message,
                    status = "error",
                    messageDetail = wex.StackTrace
                };
            }
        }
        public virtual T GetPostResponse(string url, Dictionary<string, string> headers, string path, HttpClientHandler proxy)
        {
            try
            {
                using (HttpClient client = new HttpClient(proxy))
                {
                    foreach (var header in headers)
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                    client.BaseAddress = new Uri(url);
                    var result = client.PostAsync(path, null).Result;
                    return TryGetResponse(result);
                }
            }
            catch (HttpRequestException wex)
            {
                return new T()
                {
                    message = wex.Message,
                    status = "error",
                    messageDetail = wex.StackTrace
                };
            }
        }

        public virtual T GetDeleteResponse(string url, Dictionary<string, string> headers, string path, HttpClientHandler proxy)
        {
            try
            {
                using (HttpClient client = new HttpClient(proxy))
                {
                    foreach (var header in headers)
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                    client.BaseAddress = new Uri(url);
                    var result = client.DeleteAsync(path).Result;
                    return TryGetResponse(result);
                }
            }
            catch (HttpRequestException wex)
            {
                return new T()
                {
                    message = wex.Message,
                    status = "error",
                    messageDetail = wex.StackTrace
                };
            }
        }
        //NewRs
        public virtual T GetResponseRequest(HttpWebRequest request)
        {
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    return TryGetResponseRequest(response);
                }
            }
            catch (WebException wex)
            {
                var response = (HttpWebResponse)wex.Response;
                return TryGetResponseRequest(response);
            }
        }
        private T TryGetResponseRequest(HttpWebResponse response)
        {
            try
            {
                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.BadRequest)
                {
                    using (var responseStream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(responseStream))
                    {
                        string responseFromServer = reader.ReadToEnd();
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseFromServer);
                    }
                }
                else
                    return new T()
                    {
                        message = ((int)response.StatusCode).ToString(),
                        status = "error",
                        messageDetail = response.StatusDescription
                    };
            }
            catch (Exception)
            {
                return new T()
                {
                    message = ((int)response.StatusCode).ToString(),
                    status = "error",
                    messageDetail = response.StatusDescription
                };
            }
        }

        public virtual T GetResponse(string url, Dictionary<string, string> headers, string path, HttpClientHandler proxy)
        {
            try
            {
                using (HttpClient client = new HttpClient(proxy))
                {
                    foreach (var header in headers)
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                    client.BaseAddress = new Uri(url);
                    var result = client.GetAsync(path).Result;
                    return TryGetResponse(result);
                }
            }
            catch (HttpRequestException wex)
            {
                return new T()
                {
                    message = wex.Message,
                    status = "error",
                    messageDetail = wex.StackTrace
                };
            }
        }
        public virtual T GetResponse(string url, Dictionary<string, string> headers, string path, HttpContent content, HttpClientHandler proxy)
        {
            try
            {
                using (HttpClient client = new HttpClient(proxy))
                {
                    foreach (var header in headers)
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                    client.BaseAddress = new Uri(url);
                    var result = client.PostAsync(path,content).Result;
                    return TryGetResponse(result);
                }
            }
            catch (HttpRequestException wex)
            {
                return new T()
                {
                    message = wex.Message,
                    status = "error",
                    messageDetail = wex.StackTrace
                };
            }
        }
        public abstract T HandleException(Exception ex);
        internal virtual T TryGetResponse(HttpResponseMessage response)
        {
            try
            {
                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.BadRequest|| response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    var stringResult = response.Content.ReadAsStringAsync().Result;
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(stringResult);
                }
                else
                    return new T()
                    {
                        message = ((int)response.StatusCode).ToString(),
                        status = "error",
                        messageDetail = response.ReasonPhrase
                    };
            }
            catch (Exception)
            {
                return new T()
                {
                    message = ((int)response.StatusCode).ToString(),
                    status = "error",
                    messageDetail = response.ReasonPhrase
                };
            }
        }
        internal virtual string GetCfdiData(Response response, string cfdi, bool isb64)
        {
            try
            {
               
                return XmlUtils.AddAddenda(_xmlOriginal, cfdi, isb64);
                
            }
            catch (Exception)
            {
            }
            return cfdi;
        }
        internal virtual bool Has307AndAddenda(Response response, Stamp.Data_CFDI data)
        {
            try
            {
                if (response.status == "error" &&
               (response.message != null && response.message.Trim().ToLower().Replace(".", "").Contains("307 el comprobante contiene un timbre previo"))
               && (data != null && !string.IsNullOrEmpty(data.cfdi)))
                {
                    return true;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }
        internal virtual bool Has307AndAddenda(Response response, Stamp.Data_CFDI_TFD data)
        {
            try
            {
                if (response.status == "error" &&
               (response.message != null && response.message.Trim().ToLower().Replace(".", "").Contains("307 el comprobante contiene un timbre previo"))
               && (data != null && !string.IsNullOrEmpty(data.cfdi)))
                {
                    return true;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }
    }
}
