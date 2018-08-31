using System;
using System.Text;
using SW.Helpers;

namespace SW.Services.Validate
{
    public class Validate : BaseValidate
    {
        ValidateResponseHandler _handler;
        public Validate(string url, string user, string password) : base(url, user, password, "validate")
        {
            _handler = new ValidateResponseHandler();
        }
        public Validate(string url, string token) : base(url, token, "validate")
        {
            _handler = new ValidateResponseHandler();
        }
    }
}
