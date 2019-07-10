using System;
using System.Text;
using SW.Helpers;

namespace SW.Services.Stamp
{
    public class Stamp : BaseStamp
    {
        public Stamp(string url, string user, string password, int proxyPort = 0, string proxy = null) : base(url, user, password, "stamp", proxyPort, proxy)
        {
        }
        public Stamp(string url, string token, int proxyPort = 0, string proxy = null) : base(url, token, "stamp", proxyPort, proxy)
        {
        }
    }
}
