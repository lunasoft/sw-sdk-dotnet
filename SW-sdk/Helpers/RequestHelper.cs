using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using static SW.Services.Services;

namespace SW.Helpers
{
    internal class RequestHelper
    {
        internal static string NormalizeBaseUrl(string url)
        {
            return !url.EndsWith("/") ? url + "/" : url;
        }
        internal static void SetupProxy(string proxy, int port, ref HttpWebRequest request)
        {
            if (!string.IsNullOrEmpty(proxy))
            {
                WebProxy myproxy = new WebProxy(proxy, port);
                request.Proxy = myproxy;
            }
        }

        internal static void AddFileToRequest(byte[] file, ref HttpWebRequest request)
        {
            string boundary = "----------------------------" + DateTime.Now.Ticks.ToString("x");

            request.ContentType = "multipart/form-data; boundary=" + boundary;
            request.Method = "POST";
            request.KeepAlive = true;
            Stream memStream = new System.IO.MemoryStream();
            var boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" +
                                                                    boundary + "\r\n");
            var endBoundaryBytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" +
                                                                        boundary + "--");


            string formdataTemplate = "\r\n--" + boundary +
                                        "\r\nContent-Disposition: form-data; name=\"{0}\";\r\n\r\n{1}";

            string headerTemplate =
                 "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n" +
                 "Content-Type: application/octet-stream\r\n\r\n";

            memStream.Write(boundarybytes, 0, boundarybytes.Length);
            var header = string.Format(headerTemplate, "xml", "xml");
            var headerbytes = System.Text.Encoding.UTF8.GetBytes(header);

            memStream.Write(headerbytes, 0, headerbytes.Length);

            using (var fileStream = new MemoryStream(file))
            {
                var buffer = new byte[1024];
                var bytesRead = 0;
                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    memStream.Write(buffer, 0, bytesRead);
                }
            }

            memStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
            request.ContentLength = memStream.Length;

            using (Stream requestStream = request.GetRequestStream())
            {
                memStream.Position = 0;
                byte[] tempBuffer = new byte[memStream.Length];
                memStream.Read(tempBuffer, 0, tempBuffer.Length);
                memStream.Close();
                requestStream.Write(tempBuffer, 0, tempBuffer.Length);
            }
        }
    }

    public class RequestCSD : RequestJson
    {
        [DataMember]
        public string b64Cer { get; set; }
        [DataMember]
        public string b64Key { get; set; }
    }

    public class RequestPFX : RequestJson
    {
        [DataMember]
        public string b64Pfx { get; set; }
    }

    public class RequestsCSD : RequestsJson
    {
        [DataMember]
        public AceptacionRechazoItem[] uuids { get; set; }
        [DataMember]
        public string b64Cer { get; set; }
        [DataMember]
        public string b64Key { get; set; }
    }

    public class RequestsPFX : RequestsJson
    {
        [DataMember]
        public AceptacionRechazoItem[] uuids { get; set; }
        [DataMember]
        public string b64Pfx { get; set; }
    }


    [DataContract]
    public class AceptacionRechazoItem
    {
        [DataMember]
        public string uuid { get; set; }
        private EnumAcceptReject _action;

        [DataMember]
        [JsonConverter(typeof(StringEnumConverter))]
        public EnumAcceptReject action
        {
            get { return _action; }
            set { _action = value; }
        }

    }
}
