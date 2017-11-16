using SW.Entities;
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
        public virtual T GetPostResponse(string url, string path, Dictionary<string, string> headers, HttpContent content)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    foreach (var header in headers)
                    {
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
        public virtual T GetPostResponse(string url, Dictionary<string, string> headers, string path)
        {
            try
            {
                using (HttpClient client = new HttpClient())
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
        public virtual T GetResponse(string url, Dictionary<string, string> headers, string path)
        {
            try
            {
                using (HttpClient client = new HttpClient())
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
        public abstract T HandleException(Exception ex);
        private T TryGetResponse(HttpResponseMessage response)
        {
            try
            {
                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.BadRequest)
                {
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
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

    }
}
