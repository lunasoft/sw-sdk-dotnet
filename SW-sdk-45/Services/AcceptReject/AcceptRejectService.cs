using SW.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace SW.Services.AcceptReject
{
    public abstract class AcceptRejectService : Services
    {
        protected AcceptRejectService(string url, string user, string password, string proxy, int proxyPort) : base(url, user, password, proxy, proxyPort)
        {
        }
        protected AcceptRejectService(string url, string token, string proxy, int proxyPort) : base(url, token, proxy, proxyPort)
        {
        }
        internal abstract AcceptRejectResponse AcceptRejectRequest(string cer, string key, string rfc, string password, AceptacionRechazoItem[] uuid);
        internal abstract AcceptRejectResponse AcceptRejectRequest(byte[] xmlCancelation, EnumAcceptReject enumCancelation);
        internal abstract AcceptRejectResponse AcceptRejectRequest(string pfx, string rfc, string password, AceptacionRechazoItem[] uuid);
        internal abstract AcceptRejectResponse AcceptRejectRequest(string rfc, string uuid, EnumAcceptReject enumCancelation);
        internal virtual Dictionary<string, string> GetHeaders()
        {
            this.SetupRequest();
            Dictionary<string, string> headers = new Dictionary<string, string>() {
                    { "Authorization", "bearer " + this.Token }
                };
            return headers;
        }
        internal virtual StringContent RequestAcceptReject(string cer, string key, string rfc, string password, AceptacionRechazoItem[] uuids)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(new AcceptRejectRequestCSD()
            {
                b64Cer = cer,
                b64Key = key,
                password = password,
                rfc = rfc,
                uuids = uuids
            });
            StringContent content = new StringContent(body, Encoding.UTF8, "application/json");
            return content;
        }
        internal virtual MultipartFormDataContent RequestAcceptReject(byte[] xmlCancelation, EnumAcceptReject enumAcceptReject)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            ByteArrayContent fileContent = new ByteArrayContent(xmlCancelation);
            content.Add(fileContent, "xml", "xml");
            return content;
        }
        internal virtual StringContent RequestAcceptReject(string pfx, string rfc, string password, AceptacionRechazoItem[] uuids)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(new AcceptRejectRequestPFX()
            {
                b64Pfx = pfx,
                password = password,
                rfc = rfc,
                uuids = uuids
            });
            StringContent content = new StringContent(body, Encoding.UTF8, "application/json");
            return content;
        }
        internal virtual HttpWebRequest RequestAcceptReject(string rfc, string uuid, EnumAcceptReject enumAcceptReject)
        {
            this.SetupRequest();
            string path = $"acceptreject/{rfc}/{uuid}/{enumAcceptReject.ToString()}";
            var request = (HttpWebRequest)WebRequest.Create(this.Url + path);
            request.ContentType = "application/json";
            request.ContentLength = 0;
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
            Helpers.RequestHelper.SetupProxy(this.Proxy, this.ProxyPort, ref request);
            return request;
        }
    }
}
