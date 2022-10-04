using System;
using System.Text;
using SW.Helpers;

namespace SW.Services.Pdf
{
    public class Pdf : BasePdf
    {
        public Pdf(string url, string urlApi, string user, string password, int proxyPort = 0, string proxy = null) : base(url, urlApi, user, password, proxy, proxyPort)
        {
        }
        public Pdf(string url, string urlApi, string token, int proxyPort = 0, string proxy = null) : base(url, urlApi, token, proxy, proxyPort)
        {
        }
    }

   
}
