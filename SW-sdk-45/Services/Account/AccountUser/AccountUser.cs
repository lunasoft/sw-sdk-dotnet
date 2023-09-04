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
        /// <summary>
        /// Sobrecarga para usar el servicio Account User con credenciales para autenticarse
        /// </summary>
        /// <param name="url">URL de servicios</param>
        /// <param name="urlApi">URL de API a consumir</param>
        /// <param name="user">Correo electronico de usuario</param>
        /// <param name="password">Secuencia de caracteres privadas del usuario para el acceso a su cuenta</param>
        /// <param name="proxyPort">Proxy Port</param>
        /// <param name="proxy">Proxy</param>
        public AccountUser(string url, string urlApi, string user, string password, int proxyPort = 0, string proxy = null) : base(url, urlApi, user, password, proxy, proxyPort)
        {
            _handlerUsers = new AccountGetUsersResponseHandler();
            _handlerUser = new AccountGetUserResponseHandler();
        }
        /// <summary>
        /// Sobrecarga para usar el servicio Account User con el token del usuario
        /// </summary>
        /// <param name="urlApi">URL de API a consumir</param>
        /// <param name="token">Token de acceso a la cuenta del cliente</param>
        /// <param name="proxyPort">Proxy port</param>
        /// <param name="proxy"Proxy></param>
        public AccountUser(string urlApi, string token, int proxyPort = 0, string proxy = null) : base(urlApi, token, proxy, proxyPort)
        {
            _handlerUsers = new AccountGetUsersResponseHandler();
            _handlerUser = new AccountGetUserResponseHandler();
        }
        /// <summary>
        /// Metodo para Obtener la informacion del usuario por medio de su token de acceso
        /// </summary>
        /// <returns></returns>
        public AccountGetUserResponse GetUserByToken()
        {
            return (AccountGetUserResponse)GetUser();
        }
        /// <summary>
        /// Metodo para obtener la informacion de todas las cuentas hijas de una cuenta distribuidor
        /// </summary>
        /// <returns></returns>
        public AccountGetUsersResponse GetAllUsers()
        {
            return (AccountGetUsersResponse)GetUsers();
        }
        /// <summary>
        /// Metodo para obtener la informacion de un usuario mediante su ID
        /// </summary>
        /// <param name="idUser">Secuencia de caracteres asignados al cliente a consultar</param>
        /// <returns></returns>
        public AccountGetUserResponse GetUserById(Guid idUser)
        {
            return (AccountGetUserResponse)GetUser(idUser);
        }
        /// <summary>
        /// Metodo para crear un nuevo usuario
        /// </summary>
        /// <param name="bodyRequest">Informacion asignada para el nuevo usuario</param>
        /// <returns></returns>
        public AccountUserActionsResponse CreateUser(AccountUserRequest bodyRequest)
        {
            return (AccountUserActionsResponse)UserActions(AccountUserAction.Add, null, bodyRequest);
        }
        /// <summary>
        /// Metodo para actualizar los datos de un usuario previamente registrado
        /// </summary>
        /// <param name="idUser">Id del usuario a modificar</param>
        /// <param name="rfc">Registro Federal de Contribuyentes</param>
        /// <param name="nombre">Nombre del usuario</param>
        /// <param name="unlimited">Timbres ilimitados o no</param>
        /// <param name="activo">Estatus del usuario</param>
        /// <returns></returns>
        public AccountUserActionsResponse UpdateUser(Guid idUser, string rfc = null, string nombre = null, bool unlimited = false, bool activo = true)
        {
            return (AccountUserActionsResponse)UserActions(AccountUserAction.Update, idUser, new AccountUserRequest
            { rfc = rfc, name = nombre, unlimited = unlimited, activo=activo });
        }
        /// <summary>
        /// Metodo para eliminar un usuario
        /// </summary>
        /// <param name="idUsuario">Id para identificar el usuario a eliminar</param>
        /// <returns></returns>
        public AccountUserActionsResponse DeleteUser(Guid idUsuario)
        {
            return (AccountUserActionsResponse)UserActions(AccountUserAction.Delete, idUsuario);
        }


        private HttpWebRequest ConfigureHttpRequest(string endpoint, string method)
        {
            new Validation(Url, User, Password, Token).ValidateHeaderParameters();
            this.SetupRequest();
            var baseUrl = this.UrlApi ?? this.Url;
            var request = (HttpWebRequest)WebRequest.Create(baseUrl + endpoint);
            request.ContentType = "application/json";
            request.Method = method;
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
            Helpers.RequestHelper.SetupProxy(this.Proxy, this.ProxyPort, ref request);
            request.ContentLength = 0;
            return request;
        }

        internal override Response GetUsers()
        {
            try
            {
                var request = ConfigureHttpRequest(_path, WebRequestMethods.Http.Get);
                return _handlerUsers.GetResponseRequest(request);
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
                string endpoint = idUser == null ? $"{_path}/info" : $"{_path}/{idUser}";
                var request = ConfigureHttpRequest(endpoint, WebRequestMethods.Http.Get);
                return _handlerUser.GetResponseRequest(request);
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
                        return handler.GetResponseRequest(request);
                    case AccountUserAction.Update:
                        request = RequestUpdateUser(idUser, bodyRequest);
                        return handler.GetResponseRequest(request);
                    case AccountUserAction.Delete:
                        request = RequestDeleteUser(idUser);
                        return handler.GetResponseRequest(request);
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

        private HttpWebRequest ConfigureHttpRequestWithBody(string endpoint, string method, object requestBody)
        {
            var request = ConfigureHttpRequest(endpoint, method);
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            string jsonContent = JsonConvert.SerializeObject(requestBody,settings);
            byte[] byteArray = Encoding.UTF8.GetBytes(jsonContent);
            request.ContentLength = byteArray.Length;

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(byteArray, 0, byteArray.Length);
            }
            return request;
        }
        internal virtual HttpWebRequest RequestCreateUser(AccountUserRequest bodyRequest)
        {
            bodyRequest.profile = (int)bodyRequest.profileType;
            var endpoint = $"{_path}";
            return ConfigureHttpRequestWithBody(endpoint, WebRequestMethods.Http.Post, bodyRequest);
        }

        internal virtual HttpWebRequest RequestUpdateUser(Guid? idUser, AccountUserRequest bodyRequest)
        {
            var endpoint = $"{_path}/{idUser}";
            return ConfigureHttpRequestWithBody(endpoint, WebRequestMethods.Http.Put, bodyRequest);
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
    }
}
