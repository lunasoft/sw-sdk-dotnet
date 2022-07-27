using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                if (String.IsNullOrEmpty(cer) || String.IsNullOrEmpty(key))
                {
                    throw new ServicesException("El certificado o llave privada vienen vacios");
                }
                var headers = GetHeaders();
                var content = this.RequestCsd(cer, key, password, certificateType, isActive);
                var proxy = Helpers.RequestHelper.ProxySettings(this.Proxy, this.ProxyPort);
                return handler.GetPostResponse(this.Url,
                                "certificates/save", headers, content, proxy);
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
                var headers = GetHeaders();
                var proxy = Helpers.RequestHelper.ProxySettings(this.Proxy, this.ProxyPort);
                return handler.GetDeleteResponse(this.Url, headers,
                                "certificates/" + certificateNumber, proxy);
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
                var headers = GetHeaders();
                var proxy = Helpers.RequestHelper.ProxySettings(this.Proxy, this.ProxyPort);
                return handler.GetResponse(this.Url, headers,
                                "certificates/" + certificateNumber, proxy);
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
                var headers = GetHeaders();
                var proxy = Helpers.RequestHelper.ProxySettings(this.Proxy, this.ProxyPort);
                return handler.GetResponse(this.Url, headers,
                                "certificates/rfc/" + rfc + "/" + type, proxy);
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
                var headers = GetHeaders();
                var proxy = Helpers.RequestHelper.ProxySettings(this.Proxy, this.ProxyPort);
                return handler.GetResponse(this.Url, headers,
                                "certificates", proxy);
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
                var headers = GetHeaders();
                var proxy = Helpers.RequestHelper.ProxySettings(this.Proxy, this.ProxyPort);
                return handler.GetResponse(this.Url, headers,
                                "certificates/type/" + type, proxy);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }
        internal override ListInfoCsdResponse ListCsdByRfc(string rfc)
        {
            ListInfoCsdResponseHandler handler = new ListInfoCsdResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                var headers = GetHeaders();
                var proxy = Helpers.RequestHelper.ProxySettings(this.Proxy, this.ProxyPort);
                return handler.GetResponse(this.Url, headers,
                                "certificates/rfc/" + rfc, proxy);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }
        /// <summary>
        /// Servicio para cargar un certificado a una cuenta.
        /// </summary>
        /// <param name="cer">B64 del certificado CSD.</param>
        /// <param name="key">B64 del certificado Key.</param>
        /// <param name="password">Contraseña de los certificados.</param>
        /// <param name="certificateType">Tipo de certificado a ser cargado, default: stamp</param>
        /// <param name="isActive">Estatus inicial del certificado, default: true</param>
        /// <exception cref="System.Exception"></exception>
        /// <returns><see cref="CsdResponse"/></returns>
        public CsdResponse UploadMyCsd(string cer, string key, string password, string certificateType, bool isActive)
        {
            return UploadCsd(cer, key, password, certificateType, isActive);
        }
        /// <summary>
        /// Servicio para eliminar un certificado existente en una cuenta.
        /// </summary>
        /// <param name="certificateNumber">Numero del certificado a eliminar.</param>
        /// <exception cref="System.Exception"></exception>
        /// <returns><see cref="CsdResponse"/></returns>
        public CsdResponse DisableMyCsd(string certificateNumber)
        {
            return DisableCsd(certificateNumber);
        }
        /// <summary>
        /// Servicio que busca por numero de certificado un certificado cargado en una cuenta.
        /// </summary>
        /// <param name="certificateNumber">Numero del certificado a buscar.</param>
        /// <exception cref="System.Exception"></exception>
        /// <returns><see cref="InfoCsdResponse"/></returns>
        public InfoCsdResponse SearchMyCsd(string certificateNumber)
        {
            return InfoCsd(certificateNumber);
        }
        /// <summary>
        /// SErvicio que busca por RFC y tipo de certificado un certificado cargado en una cuenta 
        /// </summary>
        /// <param name="rfc"></param>
        /// <param name="type"></param>
        /// <exception cref="System.Exception"></exception>
        /// <returns><see cref="InfoCsdResponse"/></returns>
        public InfoCsdResponse SearchActiveCsd(string rfc, string type)
        {
            return ActiveCsd(rfc, type);
        }
        /// <summary>
        /// Servicio que obtiene todos los certificados cargados en una cuenta.
        /// </summary>
        /// <exception cref="System.Exception"></exception>
        /// <returns><see cref="ListInfoCsdResponse"/></returns>
        public ListInfoCsdResponse GetListCsd()
        {
            return ListCsd();
        }
        /// <summary>
        /// Servicio que obtiene todos los certificados cargados en una cuenta filtrados por tipo de certificado.
        /// </summary>
        /// <param name="type">Tipo de certificado, default: stamp.</param>
        /// <exception cref="System.Exception"></exception>
        /// <returns><see cref="ListInfoCsdResponse"/></returns>
        public ListInfoCsdResponse GetListCsdByType(string type)
        {
            return ListCsdByType(type);
        }
        /// <summary>
        /// Servicio que obtiene todos los certificados cargados en una cuenta filtrados por RFC.
        /// </summary>
        /// <param name="rfc">RFC del certificado.</param>
        /// <exception cref="System.Exception"></exception>
        /// <returns><see cref="ListInfoCsdResponse"/></returns>
        public ListInfoCsdResponse GetListCsdByRfc(string rfc)
        {
            return ListCsdByRfc(rfc);
        }
    }
}
