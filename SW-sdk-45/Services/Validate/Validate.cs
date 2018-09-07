using System;
using System.Text;
using SW.Helpers;

namespace SW.Services.Validate
{
    public class Validate : BaseValidate
    {
        public Validate(string url, string user, string password) : base(url, user, password, "validate")
        {
        }
        public Validate(string url, string token) : base(url, token, "validate")
        {
        }
    }
}
