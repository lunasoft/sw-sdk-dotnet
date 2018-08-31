using System;
using System.Runtime.Serialization;
using SW.Helpers;
using SW.Services.AcceptReject;

namespace SW.Services
{
    public class Services
    {
        private string _token;
        private string _url;
        private string _user;
        private string _password;
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
        public string User
        {
            get { return _user; }
        }
        public string Password
        {
            get { return _password; }
        }
        public DateTime ExpirationDate
        {
            get { return _expirationDate;  }
        }
        public Services()
        {
        }
        public Services(string url, string token)
        {
            _url = Helpers.RequestHelper.NormalizeBaseUrl(url); ;
            _token = token;
            _expirationDate = DateTime.Now.AddYears(_timeSession);
        }
        public Services(string url, string user, string password)
        {
            _url = Helpers.RequestHelper.NormalizeBaseUrl(url); ;
            _user = user;
            _password = password;
        }
        public Services SetupRequest()
        {
            if (string.IsNullOrEmpty(Token) || DateTime.Now > ExpirationDate)
            {
                Authentication.Authentication auth = new Authentication.Authentication(Url,User,Password);
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
