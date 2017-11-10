using System;
using System.Text;
using SW.Helpers;

namespace SW.Services.Stamp
{
    public class Stamp : BaseStamp
    {
        public Stamp(string url, string user, string password) : base(url, user, password, "stamp")
        {
        }
        public Stamp(string url, string token) : base(url, token, "stamp")
        {
        }
    }
}
