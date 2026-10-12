using SW.Entities;
using SW.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
        public virtual T GetPostResponse(string url, string path, HttpContent content, HttpClientHandler proxy)
        {
            try
            {
                // Configurar el protocolo TLS 1.2
                System.Net.ServicePointManager.SecurityProtocol =
                    SecurityProtocolType.Tls12 |
                    SecurityProtocolType.Tls11 |
                    SecurityProtocolType.Tls;

                using (HttpClient client = new HttpClient(proxy))
                {
                    client.BaseAddress = new Uri(url);

                    using (var result = Send(client, HttpMethod.Post, path, content))
                    {
                        return TryGetResponse(result);
                    }
                }
            }
            catch (Exception wex) when (IsTransportError(wex))
            {
                return new T()
                {
                    message = Unwrap(wex).Message,
                    status = "error",
                    messageDetail = Describe(Unwrap(wex))
                };
            }
        }


        public virtual T GetPostResponse(string url, string path, Dictionary<string, string> headers, HttpContent content, HttpClientHandler proxy)
        {
            try
            {
                // Configurar el protocolo TLS 1.2
                System.Net.ServicePointManager.SecurityProtocol =
                    SecurityProtocolType.Tls12 |
                    SecurityProtocolType.Tls11 |
                    SecurityProtocolType.Tls;

                using (HttpClient client = new HttpClient(proxy))
                {
                    client.BaseAddress = new Uri(url);
                    foreach (var header in headers)
                    {
                        if (header.Value != null)
                            client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                    using (var result = Send(client, HttpMethod.Post, path, content))
                    {
                        return TryGetResponse(result);
                    }
                }
            }
            catch (AggregateException aggEx)
            {
                //Obtiene las excepciones internas para encontrar TaskCanceledException
                foreach (var innerEx in aggEx.InnerExceptions)
                {
                    if (innerEx is TaskCanceledException tex)
                    {
                        return new T()
                        {
                            message = aggEx.Message,
                            status = "500",
                            messageDetail = tex.Message
                        };
                    }
                }
                return new T()
                {
                    message = Unwrap(aggEx).Message,
                    status = "error",
                    messageDetail = Describe(Unwrap(aggEx))
                };
            }
            //Captura directa de timeout (cuando no está dentro de AggregateException)
            catch (TaskCanceledException tex)
            {
                return new T()
                {
                    message = tex.Message,
                    status = "500",
                    messageDetail = tex.StackTrace
                };
            }
            catch (Exception wex) when (IsTransportError(wex))
            {
                return new T()
                {
                    message = Unwrap(wex).Message,
                    status = "error",
                    messageDetail = Describe(Unwrap(wex))
                };
            }
        }
        public virtual T GetPostResponse(string url, Dictionary<string, string> headers, string path, HttpClientHandler proxy)
        {
            try
            {
                // Configurar el protocolo TLS 1.2
                System.Net.ServicePointManager.SecurityProtocol =
                    SecurityProtocolType.Tls12 |
                    SecurityProtocolType.Tls11 |
                    SecurityProtocolType.Tls;

                using (HttpClient client = new HttpClient(proxy))
                {
                    foreach (var header in headers)
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                    client.BaseAddress = new Uri(url);
                    using (var result = Send(client, HttpMethod.Post, path, null))
                    {
                        return TryGetResponse(result);
                    }
                }
            }
            catch (Exception wex) when (IsTransportError(wex))
            {
                return new T()
                {
                    message = Unwrap(wex).Message,
                    status = "error",
                    messageDetail = Describe(Unwrap(wex))
                };
            }
        }

        public virtual T GetDeleteResponse(string url, Dictionary<string, string> headers, string path, HttpClientHandler proxy)
        {
            try
            {
                // Configurar el protocolo TLS 1.2
                System.Net.ServicePointManager.SecurityProtocol =
                    SecurityProtocolType.Tls12 |
                    SecurityProtocolType.Tls11 |
                    SecurityProtocolType.Tls;

                using (HttpClient client = new HttpClient(proxy))
                {
                    foreach (var header in headers)
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                    client.BaseAddress = new Uri(url);
                    using (var result = Send(client, HttpMethod.Delete, path, null))
                    {
                        return TryGetResponse(result);
                    }
                }
            }
            catch (Exception wex) when (IsTransportError(wex))
            {
                return new T()
                {
                    message = Unwrap(wex).Message,
                    status = "error",
                    messageDetail = Describe(Unwrap(wex))
                };
            }
        }
        //NewRs
        public virtual T GetResponseRequest(HttpWebRequest request)
        {
            try
            {
                // Configurar el protocolo TLS 1.2
                System.Net.ServicePointManager.SecurityProtocol =
                    SecurityProtocolType.Tls12 |
                    SecurityProtocolType.Tls11 |
                    SecurityProtocolType.Tls; 

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    return TryGetResponseRequest(response);
                }
            }
            catch (WebException wex)
            {
                if (wex.Response == null)
                {
                    return new T()
                    {
                        message = wex.Status.ToString(),
                        status = "error",
                        messageDetail = wex.Message
                    };
                }
                using (var response = (HttpWebResponse)wex.Response)
                {
                    return TryGetResponseRequest(response);
                }
            }
        }
        private T TryGetResponseRequest(HttpWebResponse response)
        {
            Exception readError;
            byte[] data = HttpBodyReader.ReadAll(response, out readError);
            return ResponseInterpreter.Interpret<T>((int)response.StatusCode, response.StatusDescription, HttpBodyReader.Decode(data, HttpBodyReader.ExplicitCharset(response.ContentType)), readError);
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
                    using (var result = Send(client, HttpMethod.Get, path, null))
                    {
                        return TryGetResponse(result);
                    }
                }
            }
            catch (Exception wex) when (IsTransportError(wex))
            {
                return new T()
                {
                    message = Unwrap(wex).Message,
                    status = "error",
                    messageDetail = Describe(Unwrap(wex))
                };
            }
        }
        public abstract T HandleException(Exception ex);
        /// <summary>
        /// Envia la peticion leyendo solo los encabezados; el cuerpo se lee despues conservando el codigo HTTP.
        /// </summary>
        private static HttpResponseMessage Send(HttpClient client, HttpMethod method, string path, HttpContent content)
        {
            var request = new HttpRequestMessage(method, path);
            if (content != null)
                request.Content = content;
            return client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).Result;
        }
        internal virtual T TryGetResponse(HttpResponseMessage response)
        {
            Exception readError;
            byte[] data = HttpBodyReader.ReadAll(response, out readError);
            string charset = response.Content.Headers.ContentType != null ? response.Content.Headers.ContentType.CharSet : null;
            return ResponseInterpreter.Interpret<T>((int)response.StatusCode, response.ReasonPhrase, HttpBodyReader.Decode(data, charset), readError);
        }
        private static bool IsTransportError(Exception ex)
        {
            return ex is HttpRequestException || ex is AggregateException;
        }
        private static string Describe(Exception ex)
        {
            var parts = new System.Collections.Generic.List<string>();
            for (var e = ex; e != null; e = e.InnerException)
                parts.Add(e.Message);
            return string.Join(" -> ", parts.ToArray());
        }
        private static Exception Unwrap(Exception ex)
        {
            var agg = ex as AggregateException;
            if (agg == null)
                return ex;
            var flat = agg.Flatten();
            return flat.InnerExceptions.Count > 0 ? flat.InnerExceptions[0] : ex;
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
