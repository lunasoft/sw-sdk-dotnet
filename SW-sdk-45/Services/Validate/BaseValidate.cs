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
                var headers = GetHeaders();
                var content = GetMultipartContent(xmlBytes);
                return handler.GetPostResponse(this.Url,
                                string.Format("validate/cfdi33/",
                                _operation), headers, content);
            }
            catch (Exception ex)
            {
                return handler.HandleException(ex);
            }
        }
        public virtual ValidateLcoResponse GetValidateLco(string Lco)
        {
            ValidateLcoResponseHandler handler = new ValidateLcoResponseHandler();
            try
            {
                var headers = GetHeaders();
                var content = GetValidateLco(Lco);
                return handler.GetPostResponse(this.Url,
                                headers,
                                string.Format(string.Format("validate/{0}", Lco))
                                );
            }
            catch (Exception ex)
            {
                return handler.HandleException(ex);
            }
        }
    }
}
