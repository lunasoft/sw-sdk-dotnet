using System;
using System.Text;
using SW.Helpers;

namespace SW.Services.Stamp
{
    public class StampV2 : BaseStampV2
    {
        public StampV2(string url, string user, string password) : base(url, user, password, "stamp")
        {
        }
        public StampV2(string url, string token) : base(url, token, "stamp")
        {
        }
    }
}
