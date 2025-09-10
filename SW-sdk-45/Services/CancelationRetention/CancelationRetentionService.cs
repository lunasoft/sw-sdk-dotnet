using SW.Services.Cancelation;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace SW.Services.CancelationRetention
{
    public abstract class CancelationRetentionService : Services
    {
        protected CancelationRetentionService(string url, string user, string password, string proxy, int proxyPort) : base(url, user, password, proxy, proxyPort)
        {
        }
        protected CancelationRetentionService(string url, string token, string proxy, int proxyPort) : base(url, token, proxy, proxyPort)
        {
        }

        internal abstract CancelationResponse CancelarRetention(byte[] xmlCancelation);
        internal abstract CancelationResponse CancelarRetention(string cer, string key, string rfc, string password, string uuid, string motivo, string folioSustitucion);
        internal abstract CancelationResponse CancelarRetention(string pfx, string rfc, string password, string uuid, string motivo, string folioSustitucion);

        internal virtual Dictionary<string, string> GetHeaders()
        {
            this.SetupRequest();
            Dictionary<string, string> headers = new Dictionary<string, string>() {
                    { "Authorization", "bearer " + this.Token }
                };
            return headers;
        }
        internal virtual MultipartFormDataContent RequestCancelar(byte[] xmlCancelation)
        {
            this.SetupRequest();
            MultipartFormDataContent content = new MultipartFormDataContent();
            ByteArrayContent fileContent = new ByteArrayContent(xmlCancelation);
            content.Add(fileContent, "xml", "xml");
            return content;
        }

        internal virtual StringContent RequestCancelar(string cer, string key, string rfc, string password, string uuid, string motivo, string folioSustitucion)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(new CancelationRequestCSD()
            {
                b64Cer = cer,
                b64Key = key,
                password = password,
                rfc = rfc,
                uuid = uuid,
                motivo = motivo,
                folioSustitucion = folioSustitucion
            });
            StringContent content = new StringContent(body, Encoding.UTF8, "application/json");
            return content;
        }

        internal virtual StringContent RequestCancelar(string pfx, string rfc, string password, string uuid, string motivo, string folioSustitucion)
        {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(new CancelationRequestPFX()
            {
                b64Pfx = pfx,
                password = password,
                rfc = rfc,
                uuid = uuid,
                motivo = motivo,
                folioSustitucion = folioSustitucion
            });
            StringContent content = new StringContent(body, Encoding.UTF8, "application/json");
            return content;
        }
    }
}
