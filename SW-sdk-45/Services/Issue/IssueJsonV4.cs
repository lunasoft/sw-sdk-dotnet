using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW.Services.Issue
{
    public class IssueJsonV4 : BaseJsonV4
    {
        /// <summary>
        /// Crear una instancia de la clase IssueJsonV4.
        /// </summary>
        /// <remarks>Incluye métodos de Emisión Timbrado JSON V4 con respuestas V1, V2, V3, y V4.</remarks>
        /// <param name="url">Url Services.</param>
        /// <param name="user">Usuario.</param>
        /// <param name="password">Contraseña.</param>
        /// <param name="proxyPort">Puerto proxy.</param>
        /// <param name="proxy">Proxy.</param>
        public IssueJsonV4(string url, string user, string password, int proxyPort = 0, string proxy = null) : base(url, user, password, "issue/json", proxyPort, proxy)
        {
        }
        /// <summary>
        /// Crear una instancia de la clase IssueJsonV4.
        /// </summary>
        /// /// <remarks>Incluye métodos de Emisión Timbrado JSON V4 con respuestas V1, V2, V3, y V4.</remarks>
        /// <param name="url">Url Services.</param>
        /// <param name="token">Token de autenticación.</param>
        /// <param name="proxyPort">Puerto proxy.</param>
        /// <param name="proxy">Proxy.</param>
        public IssueJsonV4(string url, string token, int proxyPort = 0, string proxy = null) : base(url, token, "issue/json", proxyPort, proxy)
        {
        }
    }
}
