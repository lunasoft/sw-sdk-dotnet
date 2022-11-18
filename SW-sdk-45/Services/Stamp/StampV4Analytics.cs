using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW.Services.Stamp
{
    public class StampV4Analytics : BaseStampV4Analytics
    {
        public StampV4Analytics(string url, string user, string password, int proxyPort = 0, string proxy = null)
            : base(url, user, password, proxyPort, proxy)
        {

        }
        public StampV4Analytics(string url, string token, int proxyPort = 0, string proxy = null)
            : base(url, token, proxyPort, proxy)
        {

        }
    }
}
