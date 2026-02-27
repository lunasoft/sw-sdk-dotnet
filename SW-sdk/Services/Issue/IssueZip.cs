using SW.Services.Stamp;

namespace SW.Services.Issue
{
    /// <summary>
    /// Servicio de Emisión Timbrado ZIP, Sella y timbra uno CFDI sin sellar contenido en un archivo .zip .
    /// </summary>
    public class IssueZip : BaseStampZip
    {
        /// <summary>
        /// Inicializa una nueva instancia usando usuario y contraseña.
        /// </summary>
        /// <param name="url">URL Services.</param>
        /// <param name="user">Usuario.</param>
        /// <param name="password">Contraseña.</param>
        /// <param name="proxyPort">Puerto proxy.</param>
        /// <param name="proxy">Proxy.</param>
        public IssueZip(string url, string user, string password, int proxyPort = 0, string proxy = null) : base(url, user, password, "issue", proxyPort, proxy)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia usando un token Bearer.
        /// </summary>
        /// <param name="url">URL Services.</param>
        /// <param name="token">Token de autenticación.</param>
        /// <param name="proxyPort">Puerto proxy.</param>
        /// <param name="proxy">Proxy.</param>
        public IssueZip(string url, string token, int proxyPort = 0, string proxy = null) : base(url, token, "issue", proxyPort, proxy)
        {
        }
    }
}
