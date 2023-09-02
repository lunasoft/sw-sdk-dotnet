using System;
using SW.Helpers;
using SW.Entities;
using System.Net;
using Newtonsoft.Json;
using System.Text;
using System.IO;
namespace SW.Services.Account.AccountUser
{
    public class AccountUser : AccountUserService
    {
        private readonly string _path = "management/api/users";
        AccountGetUsersResponseHandler _handlerUsers;
        AccountGetUserResponseHandler _handlerUser;
        AccountUserActionsResponseHandler _handlerUserActions;

        public AccountUser(string url, string urlApi, string user, string password, int proxyPort = 0, string proxy = null) : base(url, urlApi, user, password, proxy, proxyPort)
        {
            _handlerUsers = new AccountGetUsersResponseHandler();
            _handlerUser = new AccountGetUserResponseHandler();
            _handlerUserActions = new AccountUserActionsResponseHandler();
        }

        public AccountUser(string urlApi, string token, int proxyPort = 0, string proxy = null) : base(urlApi, token, proxy, proxyPort)
        {
            _handlerUsers = new AccountGetUsersResponseHandler();
            _handlerUser = new AccountGetUserResponseHandler();
            _handlerUserActions = new AccountUserActionsResponseHandler();
        }
        public AccountGetUserResponse GetUserByToken()
        {
            return (AccountGetUserResponse)GetUser();
        }
        public AccountGetUsersResponse GetAllUsers()
        {
            return (AccountGetUsersResponse)GetUsers();
        }
        public AccountGetUserResponse GetUserById(Guid idUser)
        {
            return (AccountGetUserResponse)GetUser(idUser);
        }
        public AccountUserActionsResponse CreateUser(AccountUserRequest bodyRequest)
        {
            return (AccountUserActionsResponse)UserActions(AccountUserAction.Add, null, bodyRequest);
        }
        public AccountUserActionsResponse UpdateUser(Guid idUser, string rfc = null, string nombre = null, bool unlimited = false, bool activo = true)
        {
            return (AccountUserActionsResponse)UserActions(AccountUserAction.Update, idUser, new AccountUserRequest
            { rfc = rfc, name = nombre, unlimited = unlimited, activo=activo });
        }
        public AccountUserActionsResponse DeleteUser(Guid idUsuario)
        {
            return (AccountUserActionsResponse)UserActions(AccountUserAction.Delete, idUsuario);
        }
        internal override Response GetUsers()
        {
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                this.SetupRequest();
                var baseUrl = this.UrlApi ?? this.Url;
                var request = (HttpWebRequest)WebRequest.Create(baseUrl + _path);
                request.ContentType = "application/json";
                request.Method = WebRequestMethods.Http.Get;
                request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
                Helpers.RequestHelper.SetupProxy(this.Proxy, this.ProxyPort, ref request);
                request.ContentLength = 0;
                return _handlerUsers.GetResponse(request);
            }
            catch (Exception e)
            {
                return _handlerUsers.HandleException(e);
            }
        }
        internal override Response GetUser(Guid? idUser = null)
        {
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                this.SetupRequest();
                var baseUrl = this.UrlApi ?? this.Url;
                string endpoint;
                if (idUser == null) { endpoint = String.Format("{0}/{1}", _path, "info"); } else { endpoint = String.Format("{0}/{1}", _path,idUser); }
                var request = (HttpWebRequest)WebRequest.Create(baseUrl+endpoint);
                request.ContentType = "application/json";
                request.Method = WebRequestMethods.Http.Get;
                request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
                Helpers.RequestHelper.SetupProxy(this.Proxy, this.ProxyPort, ref request);
                request.ContentLength = 0;
                return _handlerUser.GetResponse(request);
            }
            catch (Exception e)
            {
                return _handlerUser.HandleException(e);
            }
        }
        internal override Response UserActions(AccountUserAction action, Guid? idUser = null, AccountUserRequest bodyRequest = null)
        {
            AccountUserActionsResponseHandler handler = new AccountUserActionsResponseHandler();
            try
            {
                HttpWebRequest request;

                switch (action)
                {
                    case AccountUserAction.Add:
                        request = RequestCreateUser(bodyRequest);
                        return handler.GetResponse(request);
                    case AccountUserAction.Update:
                        request = RequestUpdateUser(idUser, bodyRequest);
                        return handler.GetResponse(request);
                    case AccountUserAction.Delete:
                        request = RequestDeleteUser(idUser);
                        return handler.GetResponse(request);
                    default:
                        break;
                }

                return handler.HandleException(new InvalidOperationException("Acción no válida"));
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }


        internal virtual HttpWebRequest RequestCreateUser(AccountUserRequest bodyRequest )
        {
            this.SetupRequest();
            var baseUrl = this.UrlApi ?? this.Url;
            var request = (HttpWebRequest)WebRequest.Create(String.Format("{0}/{1}", baseUrl,_path));
            request.ContentType = "application/json";
            request.Method = WebRequestMethods.Http.Post;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
            Helpers.RequestHelper.SetupProxy(this.Proxy, this.ProxyPort, ref request);
            bodyRequest.profile = (int)bodyRequest.profileType;
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(new AccountUserRequest()
            {
                email=bodyRequest.email,
                name=bodyRequest.name,
                rfc=bodyRequest.rfc,
                password=bodyRequest.password,
                stamps=bodyRequest.stamps,
                unlimited=bodyRequest.unlimited,
                activo=bodyRequest.activo,
                profileType=bodyRequest.profileType,
                profile=bodyRequest.profile
                
            });
            request.ContentLength = body.Length;
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(body);
                streamWriter.Flush();
                streamWriter.Close();
            }
            return request;
        }
        internal virtual HttpWebRequest RequestUpdateUser(Guid? idUser, AccountUserRequest bodyRequest)
        {
            this.SetupRequest();
            var baseUrl = this.UrlApi ?? this.Url;
            var request = (HttpWebRequest)WebRequest.Create(String.Format("{0}/{1}/{2}", baseUrl, _path,idUser));
            request.ContentType = "application/json";
            request.Method = WebRequestMethods.Http.Put;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
            Helpers.RequestHelper.SetupProxy(this.Proxy, this.ProxyPort, ref request);
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            var body = Newtonsoft.Json.JsonConvert.SerializeObject(new AccountUserRequest()
            {
                rfc = bodyRequest.rfc,
                name = bodyRequest.name,
                unlimited = bodyRequest.unlimited,
                activo = bodyRequest.activo,
            }, settings);


            request.ContentLength = body.Length;
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(body);
                streamWriter.Flush();
                streamWriter.Close();
            }
            return request;
        }
        internal virtual HttpWebRequest RequestDeleteUser(Guid? idUser)
        {
            this.SetupRequest();
            var request = (HttpWebRequest)WebRequest.Create(this.UrlApi ?? this.Url + String.Format("{0}/{1}", _path, idUser));
            request.ContentType = "application/json";
            request.Method = "DELETE";
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
            Helpers.RequestHelper.SetupProxy(this.Proxy, this.ProxyPort, ref request);
            return request;
        }
        internal virtual HttpWebRequest GetStringContent(HttpWebRequest request, AccountUserRequest bodyRequest) {
            var body = Newtonsoft.Json.JsonConvert.SerializeObject(new AccountUserRequest()
            {
                email = bodyRequest.email,
                name = bodyRequest.name,
                password = bodyRequest.password,
                profileType = bodyRequest.profileType,
                rfc = bodyRequest.rfc,
                stamps = bodyRequest.stamps,
                unlimited = bodyRequest.unlimited,
                activo = bodyRequest.activo

            });
            bodyRequest.profile = (int)bodyRequest.profileType;
            string jsonContent = JsonConvert.SerializeObject(bodyRequest);
            byte[] byteArray = Encoding.UTF8.GetBytes(jsonContent);
            request.ContentLength = byteArray.Length;

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(byteArray, 0, byteArray.Length);
            }
            return request;
        }
    }
}
