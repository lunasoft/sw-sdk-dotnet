namespace SW.Services.Stamp
{
    /// <summary>
    /// Servicio de Timbrado ZIP. Timbra uno o más CFDIs previamente sellados
    /// contenidos en un archivo .zip enviado al endpoint <c>cfdi/stamp/v1/zip</c>.
    /// </summary>
    public class StampZip : BaseStampZip
    {
        /// <summary>
        /// Inicializa una nueva instancia usando usuario y contraseña.
        /// </summary>
        /// <param name="url">URL Services.</param>
        /// <param name="user">Usuario.</param>
        /// <param name="password">Contraseña.</param>
        /// <param name="proxyPort">Puerto proxy.</param>
        /// <param name="proxy">Proxy.</param>
        public StampZip(string url, string user, string password, int proxyPort = 0, string proxy = null) : base(url, user, password, "stamp", proxyPort, proxy)
        {
        }

        /// <summary>
        /// Inicializa una nueva instancia usando un token Bearer.
        /// </summary>
        /// <param name="url">URL Services.</param>
        /// <param name="token">Token de autenticación.</param>
        /// <param name="proxyPort">Puerto proxy.</param>
        /// <param name="proxy">Proxy.</param>
        public StampZip(string url, string token, int proxyPort = 0, string proxy = null) : base(url, token, "stamp", proxyPort, proxy)
        {
        }
    }
}
