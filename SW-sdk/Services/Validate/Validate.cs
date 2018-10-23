using System;
using System.Text;
using SW.Helpers;

namespace SW.Services.Validate
{
    public class Validate : BaseValidate
    {
        public Validate(string url, string user, string password, int proxyPort = 0, string proxy=null) : base(url, user, password, "validate", proxy, proxyPort)
        {
        }
        public Validate(string url, string token, int proxyPort = 0, string proxy = null) : base(url, token, "validate", proxy, proxyPort)
        {
        }
    }
}
