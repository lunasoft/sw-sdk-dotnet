using SW.Helpers;
using System.IO;
using System.Net;

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
        internal virtual HttpWebRequest RequestAcceptReject(string cer, string key, string rfc, string password, AceptacionRechazoItem[] uuids)
        {
            this.SetupRequest();
            string path = string.Format("acceptreject/csd");
            var request = (HttpWebRequest)WebRequest.Create(this.Url + path);
            request.ContentType = "application/json";
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
            Helpers.RequestHelper.SetupProxy(this.Proxy, this.ProxyPort, ref request);
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(new RequestsCSD()
            {
                b64Cer = cer,
                b64Key = key,
                password = password,
                rfc = rfc,
                uuids = uuids
            });
            request.ContentLength = body.Length;
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(body);
                streamWriter.Flush();
                streamWriter.Close();
            }
            return request;
        }
        internal virtual HttpWebRequest RequestAcceptReject(byte[] xmlCancelation, EnumAcceptReject enumCancelation)
        {
            this.SetupRequest();
            var request = (HttpWebRequest)WebRequest.Create(this.Url + "acceptreject/xml");
            request.ContentType = "application/json";
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
            Helpers.RequestHelper.SetupProxy(this.Proxy, this.ProxyPort, ref request);
            request.ContentLength = 0;
            Helpers.RequestHelper.AddFileToRequest(xmlCancelation, ref request);
            return request;
        }
        internal virtual HttpWebRequest RequestAcceptReject(string pfx, string rfc, string password, AceptacionRechazoItem[] uuid)
        {
            this.SetupRequest();
            var request = (HttpWebRequest)WebRequest.Create(this.Url + "acceptreject/pfx");
            request.ContentType = "application/json";
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add(HttpRequestHeader.Authorization, "bearer " + this.Token);
            Helpers.RequestHelper.SetupProxy(this.Proxy, this.ProxyPort, ref request);
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(new RequestsPFX()
            {
                b64Pfx = pfx,
                password = password,
                rfc = rfc,
                uuids = uuid
            });
            request.ContentLength = body.Length;
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(body);
                streamWriter.Flush();
                streamWriter.Close();
            }
            return request;
        }
        internal virtual HttpWebRequest RequestAcceptReject(string rfc, string uuid, EnumAcceptReject enumAcceptReject)
        {
            this.SetupRequest();
            string path = string.Format("acceptreject/{0}/{1}/{2}", rfc, uuid, enumAcceptReject.ToString());
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
