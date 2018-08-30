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
        public virtual ValidateXMLResponse ValidateXML(string XML)
        {
            ValidateXMLResponseHandler handler = new ValidateXMLResponseHandler();
            try
            {
                var xmlBytes = Encoding.UTF8.GetBytes(XML);
                var request = this.RequestValidateXml(xmlBytes);
                return handler.GetResponse(request);
            }
            catch (Exception ex)
            {
                return handler.HandleException(ex);
            }
        }
       public virtual ValidateLrfcResponse ValidateLrfc(string Lrfc)
        {
            ValidateLrfcResponseHandler handler = new ValidateLrfcResponseHandler();
            try
            {
                var request = this.RequestValidateLrfc(Lrfc);
                return handler.GetResponse(request);
            }catch (Exception ex)
            {
                return handler.HandleException(ex);
            }
        }

        public virtual ValidateLcoResponse ValidateLco(string Lco)
        {
            ValidateLcoResponseHandler handler = new ValidateLcoResponseHandler();
            try
            {
                var request = this.RequestValidateLco(Lco);
                return handler.GetResponse(request);
            }
            catch (Exception ex)
            {
                return handler.HandleException(ex);
            }
        }
    }
}
