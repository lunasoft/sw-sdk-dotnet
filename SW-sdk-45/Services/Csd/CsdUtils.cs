using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SW.Helpers;
using System.Net;

namespace SW.Services.Csd
{
    public class CsdUtils : CsdService
    {
        CsdResponseHandler _handler;
        public CsdUtils(string url, string user, string password, int proxyPort = 0, string proxy = null) : base(url, user, password, proxy, proxyPort)
        {
            _handler = new CsdResponseHandler();
        }
        public CsdUtils(string url, string token, int proxyPort = 0, string proxy = null) : base(url, token, proxy, proxyPort)
        {
            _handler = new CsdResponseHandler();
        }

        internal override UploadCsdResponse UploadCsd(string cer, string key, string password, string certificateType, bool isActive)
        {
            CsdResponseHandler handler = new CsdResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                if (String.IsNullOrEmpty(cer) || String.IsNullOrEmpty(key))
                {
                    throw new ServicesException("El certificado o llave privada vienen vacios");
                }
                var headers = GetHeaders();
                var content = this.RequestCsd(cer, key, password, certificateType, isActive);
                var proxy = Helpers.RequestHelper.ProxySettings(this.Proxy, this.ProxyPort);
                return handler.GetPostResponse(this.Url,
                                "certificates/save", headers, content, proxy);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }

        public UploadCsdResponse UploadMyCsd(string cer, string key, string password, string certificateType, bool isActive)
        {
            return UploadCsd(cer, key, password, certificateType, isActive);
        }
    }
}
