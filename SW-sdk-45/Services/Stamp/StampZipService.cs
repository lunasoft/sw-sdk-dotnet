namespace SW.Services.Stamp
{
    /// <summary>
    /// Clase base abstracta que extiende <see cref="StampService"/> para los endpoints de timbrado ZIP.
    /// Reutiliza <c>GetHeaders()</c> y <c>GetMultipartContent()</c> de <see cref="StampService"/>.
    /// </summary>
    public abstract class StampZipService : StampService
    {
        protected StampZipService(string url, string user, string password, string proxy, int proxyPort) : base(url, user, password, proxy, proxyPort)
        {
        }
        protected StampZipService(string url, string token, string proxy, int proxyPort) : base(url, token, proxy, proxyPort)
        {
        }

        /// <summary>
        /// Construye la ruta del endpoint ZIP según la operación indicada.
        /// </summary>
        /// <param name="operation">Operación del endpoint: "stamp" o "issue".</param>
        /// <returns>Ruta relativa al endpoint ZIP.</returns>
        internal string GetZipPath(string operation)
        {
            return string.Format("cfdi/{0}/v1/zip", operation);
        }
    }
}
