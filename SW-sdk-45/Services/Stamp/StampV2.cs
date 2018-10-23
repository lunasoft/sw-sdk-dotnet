using System;
using System.Text;
using SW.Helpers;

namespace SW.Services.Stamp
{
    public class StampV2 : BaseStampV2
    {
        public StampV2(string url, string user, string password, int proxyPort = 0, string proxy = null) : base(url, user, password, "stamp", proxy, proxyPort)
        {
        }
        public StampV2(string url, string token, int proxyPort = 0, string proxy = null) : base(url, token, "stamp", proxy, proxyPort)
        {
        }
    }
}
