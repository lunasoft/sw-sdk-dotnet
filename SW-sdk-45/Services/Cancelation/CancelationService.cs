using SW.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;

namespace SW.Services.Cancelation
{
    public abstract class CancelationService : Services
    {
        protected CancelationService(string url, string user, string password) : base(url, user, password)
        {
        }
        protected CancelationService(string url, string token) : base(url, token)
        {
        }
        internal abstract CancelationResponse Cancelar(string cer, string key, string rfc, string password, string uuid);
        internal abstract CancelationResponse Cancelar(byte[] xmlCancelation);
        internal abstract CancelationResponse Cancelar(string pfx, string rfc, string password, string uuid);
        internal virtual Dictionary<string, string> GetHeaders()
        {
            this.SetupRequest();
            Dictionary<string, string> headers = new Dictionary<string, string>() {
                    { "Authorization", "bearer " + this.Token }
                };
            return headers;
        }
        internal virtual StringContent RequestCancelar(string cer, string key, string rfc, string password, string uuid)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(new CancelationRequestCSD()
            {
                b64Cer = cer,
                b64Key = key,
                password = password,
                rfc = rfc,
                uuid = uuid
            });
            StringContent content = new StringContent(body, Encoding.UTF8, "application/json");
            return content;
        }
        internal virtual StringContent RequestCancelar(string pfx, string rfc, string password, string uuid)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(new CancelationRequestPFX()
            {
                b64Pfx = pfx,
                password = password,
                rfc = rfc,
                uuid = uuid
            });
            StringContent content = new StringContent(body, Encoding.UTF8, "application/json");
            return content;
        }
        internal virtual MultipartFormDataContent RequestCancelarFile(byte[] xmlCancelation)
        {
            this.SetupRequest();
            MultipartFormDataContent content = new MultipartFormDataContent();
            ByteArrayContent fileContent = new ByteArrayContent(xmlCancelation);
            content.Add(fileContent, "xml", "xml");
            return content;
        }
    }
}
