using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW.Services.Resend
{
    public class Resend : BaseResend
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
        public Resend(string url, string urlApi, string user, string password, int proxyPort = 0, string proxy = null) : base(url, urlApi, user, password, proxy, proxyPort)
        {
        }
        /// <summary>
        /// Crear una instancia de la clase Resend.
        /// </summary>
        /// <param name="urlApi">URL API.</param>
        /// <param name="token">Token de autenticacion.</param>
        /// <param name="proxyPort">Puerto proxy.</param>
        /// <param name="proxy">Proxy.</param>
        public Resend(string urlApi, string token, int proxyPort = 0, string proxy = null) : base(urlApi, token, proxy, proxyPort)
        {
        }
    }
}
