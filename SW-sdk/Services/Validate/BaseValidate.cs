using System;
using SW.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SW.Services.Validate
{
    public abstract class BaseValidate : ValidateService
    {
        private string _operation;
        public BaseValidate(string url, string user, string password, string operation) : base(url, user, password)
        {
            _operation = operation;
        }
        public BaseValidate(string url,  string token, string operation) : base(url, token)
        {
            _operation = operation;
        }
        public virtual ValidateResponse Validate(string XML)
        {
            ValidateResponseHandler handler = new ValidateResponseHandler();
            try
            {
                var xmlBytes = Encoding.UTF8.GetBytes(XML);
                var request = this.RequestValidating(xmlBytes);
                return handler.GetResponse(request);
            }
            catch (Exception ex)
            {
                return handler.HandleException(ex);
            }
        }
    }
}
