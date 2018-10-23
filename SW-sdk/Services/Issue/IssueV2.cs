using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SW.Helpers;
using SW.Services.Stamp;

namespace SW.Services.Issue
{
    public class IssueV2 : BaseStampV2
    {
        public IssueV2(string url, string user, string password, int proxyPort = 0, string proxy = null) : base(url, user, password, "issue", proxyPort, proxy)
        {
        }
        public IssueV2(string url, string token, int proxyPort = 0, string proxy = null) : base(url, token, "issue", proxyPort, proxy)
        {
        }
    }
}
