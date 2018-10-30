using System;
using System.Text;
using SW.Helpers;

namespace SW.Services.Pdf
{
    public class Pdf : BasePdf
    {
        public Pdf(string url, string user, string password, int proxyPort = 0, string proxy = null) : base(url, user, password, proxy, proxyPort)
        {
        }
        public Pdf(string url, string token, int proxyPort = 0, string proxy = null) : base(url, token, proxy, proxyPort)
        {
        }
    }

   
}
