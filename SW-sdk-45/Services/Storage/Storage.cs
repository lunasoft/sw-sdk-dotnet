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
        /// <summary>
        /// Crear una instancia de la clase Resend.
        /// </summary>
        /// <param name="urlApi">URL API.</param>
        /// <param name="url">URL Services.</param>
        /// <param name="user">Email del usuario.</param>
        /// <param name="password">Contraseña.</param>
        /// <param name="proxyPort">Puerto proxy.</param>
        /// <param name="proxy">Proxy.</param>
        public Storage(string urlApi, string url, string user, string password, string proxy = null, int proxyPort = 0) : base(urlApi, url, user, password, proxy, proxyPort)
        {
            _handler = new StorageResponseHandler();
        }
        /// <summary>
        /// Crear una instancia de la clase Resend.
        /// </summary>
        /// <param name="urlApi">URL API.</param>
        /// <param name="token">Token de autenticacion.</param>
        /// <param name="proxyPort">Puerto proxy.</param>
        /// <param name="proxy">Proxy.</param>
        public Storage(string url, string token, string proxy = null, int proxyPort = 0) : base(url, token, proxy, proxyPort)
        {
            _handler = new StorageResponseHandler();
        }
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
                return _handler.GetResponse(this.UrlApi ?? this.Url, headers, $"/datawarehouse/v1/live/{uuid}", proxy);
            }
            catch (Exception e)
            {
                return _handler.HandleException(e);
            }
        }
    }
}
