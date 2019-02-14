using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SW.Helpers;
using System.Net;

namespace SW.Services.Csd
{
    public class CsdUtils : CsdService
    {
        CsdResponseHandler _handler;
        public CsdUtils(string url, string user, string password, int proxyPort = 0, string proxy = null) : base(url, user, password, proxy, proxyPort)
        {
            _handler = new CsdResponseHandler();
        }
        public CsdUtils(string url, string token, int proxyPort = 0, string proxy = null) : base(url, token, proxy, proxyPort)
        {
            _handler = new CsdResponseHandler();
        }

        internal override CsdResponse UploadCsd(string cer, string key, string password, string certificateType, bool isActive)
        {
            CsdResponseHandler handler = new CsdResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                if(String.IsNullOrEmpty(cer) || String.IsNullOrEmpty(key))
                {
                    throw new ServicesException("El certificado o llave privada vienen vacios");
                }
                HttpWebRequest request = this.RequestUploadCsd(cer, key, password, certificateType, isActive);
                return handler.GetResponse(request);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }

        internal override CsdResponse DisableCsd(string certificateNumber)
        {
            CsdResponseHandler handler = new CsdResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                HttpWebRequest request = this.RequestDisableCsd(certificateNumber);
                return handler.GetResponse(request);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }

        internal override InfoCsdResponse InfoCsd(string certificateNumber)
        {
            InfoCsdResponseHandler handler = new InfoCsdResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                HttpWebRequest request = this.RequestInfoCsd(certificateNumber);
                return handler.GetResponse(request);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }

        internal override InfoCsdResponse ActiveCsd(string rfc, string type)
        {
            InfoCsdResponseHandler handler = new InfoCsdResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                HttpWebRequest request = this.RequestActiveCsd(rfc, type);
                return handler.GetResponse(request);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }

        internal override ListInfoCsdResponse ListCsd()
        {
            ListInfoCsdResponseHandler handler = new ListInfoCsdResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                HttpWebRequest request = this.RequestListCsd();
                return handler.GetResponse(request);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }

        internal override ListInfoCsdResponse ListCsdByType(string type)
        {
            ListInfoCsdResponseHandler handler = new ListInfoCsdResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                HttpWebRequest request = this.RequestListCsdByType(type);
                return handler.GetResponse(request);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }

        internal override ListInfoCsdResponse ListCsdByRfc(string type)
        {
            ListInfoCsdResponseHandler handler = new ListInfoCsdResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                HttpWebRequest request = this.RequestListCsdByRfc(type);
                return handler.GetResponse(request);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }

        public CsdResponse UploadMyCsd(string cer, string key, string password, string certificateType, bool isActive)
        {
            return UploadCsd(cer, key, password, certificateType, isActive);
        }
        public CsdResponse DisableMyCsd(string certificateNumber)
        {
            return DisableCsd(certificateNumber);
        }
        public InfoCsdResponse SearchMyCsd(string certificateNumber)
        {
            return InfoCsd(certificateNumber);
        }
        public InfoCsdResponse SearchActiveCsd(string rfc, string type)
        {
            return ActiveCsd(rfc, type);
        }
        public ListInfoCsdResponse GetListCsd()
        {
            return ListCsd();
        }
        public ListInfoCsdResponse GetListCsdByType(string type)
        {
            return ListCsdByType(type);
        }
        public ListInfoCsdResponse GetListCsdByRfc(string rfc)
        {
            return ListCsdByRfc(rfc);
        }
    }
}
