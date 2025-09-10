using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SW.Services.StampRetention
{
    public class StampRetention : BaseStampRetention
    {
        public StampRetention(string url, string user, string password, int proxyPort = 0, string proxy = null) : base(url, user, password, "stamp", proxy, proxyPort)
        {
        }
        
        public StampRetention(string url, string token, int proxyPort = 0, string proxy = null) : base(url, token, "stamp", proxy, proxyPort)
        {
        }
    }
}