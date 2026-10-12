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
                        message = wex.Status.ToString(),
                        status = "error",
                        messageDetail = wex.Message
                    };
                }
                using (var response = (HttpWebResponse)wex.Response)
                {
                    return TryGetResponse(response);
                }
            }
        }
        public abstract T HandleException(Exception ex);
        /// <summary>
        /// Devuelve lo que respondio el servicio; si no se puede leer o interpretar, informa la causa y el cuerpo recibido.
        /// </summary>
        private T TryGetResponse(HttpWebResponse response)
        {
            int code = (int)response.StatusCode;
            Exception readError;
            string body = ReadBody(response, out readError);
            return ResponseInterpreter.Interpret<T>(code, response.StatusDescription, body, readError);
        }
        private static string ReadBody(HttpWebResponse response, out Exception error)
        {
            byte[] data = SW.Helpers.HttpBodyReader.ReadAll(response, out error);
            return SW.Helpers.HttpBodyReader.Decode(data, SW.Helpers.HttpBodyReader.ExplicitCharset(response.ContentType));
        }
    }
}
