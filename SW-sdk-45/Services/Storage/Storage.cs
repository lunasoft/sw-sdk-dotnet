using SW.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW.Services.Storage
{
    public class Storage : StorageService
    {
        StorageResponseHandler _handler;
        public Storage(string url, string token, string proxy = null, int proxyPort = 0) : base(url, token, proxy, proxyPort)
        {
            _handler = new StorageResponseHandler();
        }
        /// <summary>
        /// Servicio para recuperar un XML timbrado.
        /// </summary>
        /// <param name="uuid">UUID del comprobante.</param>
        /// <exception cref="System.Exception"></exception>
        /// <returns><see cref="StorageResponse"/></returns>
        public override StorageResponse GetByUUID(Guid uuid)
        {
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                this.SetupRequest();
                Dictionary<string, string> headers = new Dictionary<string, string>()
                {
                    { "Authorization", "bearer " + this.Token }
                };
                var proxy = RequestHelper.ProxySettings(this.Proxy, this.ProxyPort);
                return _handler.GetResponse(this.Url, headers, $"/datawarehouse/v1/live/{uuid}", proxy);
            }
            catch (Exception e)
            {
                return _handler.HandleException(e);
            }
        }
    }
}
