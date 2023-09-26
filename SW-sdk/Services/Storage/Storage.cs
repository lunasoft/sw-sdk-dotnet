using SW.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace SW.Services.Storage
{
    public class Storage : StorageService
    {
        /// <summary>
        /// Crear una instancia de la clase Resend.
        /// </summary>
        /// <param name="urlApi">URL API.</param>
        /// <param name="url">URL Services.</param>
        /// <param name="user">Email del usuario.</param>
        /// <param name="password">Contraseña.</param>
        /// <param name="proxyPort">Puerto proxy.</param>
        /// <param name="proxy">Proxy.</param>
        public Storage(string urlApi, string url, string user, string password, int proxyPort = 0, string proxy = null) : base(urlApi, url, user, password, proxy, proxyPort)
        {
        }
        /// <summary>
        /// Crear una instancia de la clase Resend.
        /// </summary>
        /// <param name="urlApi">URL API.</param>
        /// <param name="token">Token de autenticacion.</param>
        /// <param name="proxyPort">Puerto proxy.</param>
        /// <param name="proxy">Proxy.</param>
        public Storage(string url, string token, int proxyPort = 0, string proxy = null) : base(url, token, proxy, proxyPort)
        {
        }
        public StorageResponse GetByUUID(Guid uuid)
        {
            return StorageByUUID(uuid);
        }
        internal override StorageResponse StorageByUUID(Guid uuid)
        {
            StorageResponseHandler handler = new StorageResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                HttpWebRequest request = this.RequestStorage(uuid);
                return handler.GetResponse(request);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }
    }
}
