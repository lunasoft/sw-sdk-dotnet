using System;
using System.Text;
using SW.Helpers;

namespace SW.Services.Validate
{
    public class Validate : BaseValidate
    {
        ValidateXmlResponseHandler _handler;
        public Validate(string url, string user, string password) : base(url, user, password, "validate")
        {
            _handler = new ValidateXmlResponseHandler();
        }
        public Validate(string url, string token) : base(url, token, "validate")
        {
            _handler = new ValidateXmlResponseHandler();
        }
    }
}
