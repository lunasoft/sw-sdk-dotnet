using System;
using SW.Helpers;

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
            _url = url;
            _token = token;
            _expirationDate = DateTime.Now.AddYears(_timeSession);
        }
        public Services(string url, string user, string password)
        {
            _url = url;
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
                    _token = response.Data.token;
                    _expirationDate = DateTime.Now.AddHours(_timeSession);
                }
            }
            return this;
        }
    }
}
