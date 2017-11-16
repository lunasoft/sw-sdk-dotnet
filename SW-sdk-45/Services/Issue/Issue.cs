using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SW.Helpers;
using SW.Services.Stamp;

namespace SW.Services.Issue
{
    public class Issue : BaseStamp
    {
        public Issue(string url, string user, string password) : base(url, user, password, "issue")
        {
        }
        public Issue(string url, string token) : base(url, token, "issue")
        {
        }
    }
}
