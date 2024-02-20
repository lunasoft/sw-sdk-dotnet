using System;
using System.Runtime.Serialization;
using SW.Helpers;

namespace SW.Services
{
    public class Services
    {
        private string _token;
        private string _url;
        private string _urlApi;
        private string _user;
        private string _password;
        private string _proxy;
        private int _proxyPort;
        private DateTime _expirationDate;
        private int _timeSession = 2;
        public string Token
        {
            get { return _token; }
        }
        public string Url
        {
            get { return _url; }
        }
        public string UrlApi
        {
            get { return _urlApi; }
        }
        public string User
        {
            get { return _user; }
        }
        public string Password
        {
            get { return _password; }
        }
        public string Proxy
        {
            get { return _proxy; }
        }
        public int ProxyPort
        {
            get { return _proxyPort; }
        }
        public DateTime ExpirationDate
        {
            get { return _expirationDate;  }
        }
        public Services()
        {
        }
        public Services(string url, string token, string proxy, int proxyPort)
        {
            _url = Helpers.RequestHelper.NormalizeBaseUrl(url); ;
            _token = token;
            _expirationDate = DateTime.Now.AddYears(_timeSession);
            _proxy = proxy;
            _proxyPort = proxyPort;
        }
        public Services(string url, string user, string password, string proxy, int proxyPort)
        {
            _url = Helpers.RequestHelper.NormalizeBaseUrl(url); ;
            _user = user;
            _password = password;
            _proxy = proxy;
            _proxyPort = proxyPort;
        }
        public Services(string url, string urlApi, string user, string password, string proxy, int proxyPort)
        {
            _url = Helpers.RequestHelper.NormalizeBaseUrl(url); ;
            _urlApi = Helpers.RequestHelper.NormalizeBaseUrl(urlApi); ;
            _user = user;
            _password = password;
            _proxy = proxy;
            _proxyPort = proxyPort;
        }
        public Services SetupRequest()
        {
            if (string.IsNullOrEmpty(Token) || DateTime.Now > ExpirationDate)
            {
                Authentication.Authentication auth = new Authentication.Authentication(Url,User,Password, ProxyPort, Proxy);
                var response = auth.GetToken();
                if (response.status == ResponseType.success.ToString())
                {
                    _token = response.data.token;
                    _expirationDate = DateTime.Now.AddHours(_timeSession);
                }
            }
            return this;
        }
        [DataContract]
        public class RequestJson
        {
            [DataMember]
            public string uuid { get; set; }
            [DataMember]
            public string password { get; set; }
            [DataMember]
            public string rfc { get; set; }
        }
        [DataContract]
        public class RequestsJson
        {
            [DataMember]
            public AceptacionRechazoItem[] uuids { get; set; }
            [DataMember]
            public string uuid { get; set; }
            [DataMember]
            public string password { get; set; }
            [DataMember]
            public string rfc { get; set; }
        }
    }
} 
