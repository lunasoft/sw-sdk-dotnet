using SW.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace SW.Services
{
    internal abstract class ResponseHandler<T>
        where T : Response, new()
    {
        public virtual T GetResponse(HttpWebRequest request)
        {
            try
            {
                // Configurar para aceptar TLS 1.2 y TLS 1.1
                System.Net.ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072 | (SecurityProtocolType)768;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    return TryGetResponse(response);
                }
            }
            catch (WebException wex)
            {
                if (wex.Response == null)
                {
                    return new T()
                    {
                        message = "error",
                        status = "error",
                        messageDetail = wex.Message
                    };
                }
                var response = (HttpWebResponse)wex.Response;
                return TryGetResponse(response);
            }
        }
        public abstract T HandleException(Exception ex);
        private T TryGetResponse(HttpWebResponse response)
        {
            try
            {
                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.BadRequest || response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    using (var responseStream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(responseStream))
                    {
                        string responseFromServer = reader.ReadToEnd();
                        return DeserializeResponse(responseFromServer);
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
        private T DeserializeResponse(string responseFromServer)
        {
            var settings = new Newtonsoft.Json.JsonSerializerSettings();
            settings.Error = (sender, args) =>
            {
                args.ErrorContext.Handled = true;
            };
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseFromServer, settings);
        }

    }
}
