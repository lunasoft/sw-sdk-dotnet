using System;
using System.Text;
using SW.Helpers;

namespace SW.Services.Pdf
{
    public class Pdf : BasePdf
    {
        /// <summary>
        /// Inicializa una nueva instancia de generación de PDF. 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="proxyPort"></param>
        /// <param name="proxy"></param>
        public Pdf(string url, string user, string password, int proxyPort = 0, string proxy = null) : base(url, user, password, "pdf", proxy, proxyPort)
        {
        }
        /// <summary>
        /// Inicializa una nueva instancia de generación de PDF.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="token"></param>
        /// <param name="proxyPort"></param>
        /// <param name="proxy"></param>
        public Pdf(string url, string token, int proxyPort = 0, string proxy = null) : base(url, token, "pdf", proxy, proxyPort)
        {
        }
    }
}
