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
        private readonly string _path = "management/v2/api/dealers/users";
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
        }

        /// <summary>
        /// Metodo para crear un nuevo usuario
        /// </summary>
        /// <param name="bodyRequest">Informacion asignada para el nuevo usuario</param>
        /// <returns></returns>
        public AccountGetUserResponse CreateUser(AccountUserRequest bodyRequest)
        {
            return (AccountGetUserResponse)GetUser(bodyRequest);
        }
        /// <summary>
        /// Metodo para actualizar los datos de un usuario previamente registrado
        /// </summary>
        /// <param name="idUser">Id del usuario a modificar</param>
        /// <param name="name">Nombre del usuario</param>
        /// <param name="taxId">Registro Federal de Contribuyente</param>
        /// <param name="notificationEmail">Correo para notificaciones del usuario</param>
        /// <param name="phone">Número de telefono del usuario</param>
        /// <param name="isUnlimited">Timbres ilimitados o no</param>
        /// <returns></returns>
        public AccountUserActionsResponse UpdateUser(Guid idUser, string name = null, string taxId = null, string notificationEmail = null, string phone = null, bool isUnlimited = false)
        {
            return (AccountUserActionsResponse)UserActions(AccountUserAction.Update, idUser, new AccountUserUpdateRequest
            { idUser = idUser, name = name, taxId = taxId, notificationEmail = notificationEmail, phone = phone, isUnlimited = isUnlimited });
        }
        /// <summary>
        /// Metodo para eliminar un usuario
        /// </summary>
        /// <param name="idUser">Id para identificar el usuario a eliminar</param>
        /// <returns></returns>
        public AccountUserActionsResponse DeleteUser(Guid idUser)
        {
            return (AccountUserActionsResponse)UserActions(AccountUserAction.Delete, idUser);
        }
        /// <summary>
        /// Metodo para obtener la informacion de todas las cuentas hijas de una cuenta distribuidor
        /// </summary>
        /// <returns></returns>
        public AccountGetUsersResponse GetAllUsers()
        {
            return (AccountGetUsersResponse)GetUsers(AccountUserFilter.All);
        }
        /// <summary>
        /// Metodo para obtener la informacion de la cuentas hijas filtrada por Email
        /// </summary>
        /// <param name="email">Email del usuario a consultar</param>
        /// <returns></returns>
        public AccountGetUsersResponse GetUserByEmail(string email)
        {
            return (AccountGetUsersResponse)GetUsers(AccountUserFilter.Email, email);
        }
        /// <summary>
        /// Metodo para obtener la informacion de la cuentas hijas filtrada por RFC
        /// </summary>
        /// <param name="taxId">RFC del usuario a consultar</param>
        /// <returns></returns>
        public AccountGetUsersResponse GetUserByTaxId(string taxId)
        {
            return (AccountGetUsersResponse)GetUsers(AccountUserFilter.TaxId, taxId);
        }
        /// <summary>
        /// Metodo para obtener la informacion de la cuentas hijas filtrada por ID
        /// </summary>
        /// <param name="idUser">IdUser del usuario a consultar</param>
        /// <returns></returns>
        public AccountGetUsersResponse GetUserById(Guid idUser)
        {
            return (AccountGetUsersResponse)GetUsers(AccountUserFilter.Id, null, idUser);
        }
        /// <summary>
        /// Metodo para obtener la informacion de la cuentas hijas filtrada por si esta activa
        /// </summary>
        /// <param name="isActive">Parametro para indicar si se busca por cuentas activas o desactivadas</param>
        /// <returns></returns>
        public AccountGetUsersResponse GetUserByIsActive(bool isActive)
        {
            return (AccountGetUsersResponse)GetUsers(AccountUserFilter.Id, null, null, isActive);
        }

        #region ConfigureResponse
        internal override Response GetUser(AccountUserRequest bodyRequest)
        {
            AccountGetUserResponseHandler handlerCreate = new AccountGetUserResponseHandler();
            try
            {
                var request = ConfigureHttpRequestWithBody(_path, WebRequestMethods.Http.Post, bodyRequest);
                var response = handlerCreate.GetResponseRequest(request);
                if (response.status == "400") { response.status = "error"; }
                return response;
            }
            catch (Exception e)
            {
                return handlerCreate.HandleException(e);
            }
        }
        internal override Response UserActions(AccountUserAction action, Guid idUser, AccountUserUpdateRequest bodyRequest = null)
        {
            AccountUserActionsResponseHandler handler = new AccountUserActionsResponseHandler();
            try
            {
                HttpWebRequest request;
                switch (action)
                {
                    case AccountUserAction.Update:
                        request = RequestUpdateUser(idUser, bodyRequest);
                        var responseUpdate = handler.GetResponseRequest(request);
                        if (responseUpdate.status == "400") { responseUpdate.status = "error"; }
                        return responseUpdate;
                    case AccountUserAction.Delete:
                        request = RequestDeleteUser(idUser);
                        var responseDelete = handler.GetResponseRequest(request);
                        if (responseDelete.message == "204")
                        {
                            responseDelete.status = "success";
                            responseDelete.message = "Usuario eliminado con exito";
                        }
                        return responseDelete;
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
        internal override Response GetUsers(AccountUserFilter? filter = null, string parameter = null, Guid? idUser = null, bool? isActive = null)
        {
            AccountGetUsersResponseHandler handlerUsers = new AccountGetUsersResponseHandler();
            try
            {
                HttpWebRequest request;
                switch (filter)
                {
                    case AccountUserFilter.All:
                        request = ConfigureHttpRequest(_path, WebRequestMethods.Http.Get);
                        return handlerUsers.GetResponseRequest(request);
                    case AccountUserFilter.Email:
                        request = RequestGetUserEmail(parameter);
                        return handlerUsers.GetResponseRequest(request);
                    case AccountUserFilter.TaxId:
                        request = RequestGetUserTaxId(parameter);
                        return handlerUsers.GetResponseRequest(request);
                    case AccountUserFilter.Id:
                        request = RequestGetUserId(idUser);
                        return handlerUsers.GetResponseRequest(request);
                    case AccountUserFilter.IsActive:
                        request = RequestGetUserIsActive(isActive);
                        return handlerUsers.GetResponseRequest(request);
                    default:
                        break;
                }
                return handlerUsers.HandleException(new InvalidOperationException("Acción no válida"));
            }
            catch (Exception e)
            {
                return handlerUsers.HandleException(e);
            }
        }
        #endregion

        #region HttpRequest
        internal virtual HttpWebRequest RequestUpdateUser(Guid idUser, AccountUserUpdateRequest bodyRequest)
        {
            var endpoint = $"{_path}/{idUser}";
            return ConfigureHttpRequestWithBody(endpoint, WebRequestMethods.Http.Put, bodyRequest);
        }
        internal virtual HttpWebRequest RequestDeleteUser(Guid idUser)
        {
            this.SetupRequest();
            var request = (HttpWebRequest)WebRequest.Create(this.UrlApi ?? this.Url + String.Format("{0}/{1}", _path, idUser));
            request.ContentType = "application/json";
            request.Method = "DELETE";
            request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
            Helpers.RequestHelper.SetupProxy(this.Proxy, this.ProxyPort, ref request);
            return request;
        }
        internal virtual HttpWebRequest RequestGetUserEmail(string email)
        {
            var endpoint = $"{_path}?Email={email}";
            return ConfigureHttpRequest(endpoint, WebRequestMethods.Http.Get);
        }
        internal virtual HttpWebRequest RequestGetUserTaxId(string TaxId)
        {
            var endpoint = $"{_path}?TaxId={TaxId}";
            return ConfigureHttpRequest(endpoint, WebRequestMethods.Http.Get);
        }
        internal virtual HttpWebRequest RequestGetUserId(Guid? IdUser)
        {
            var endpoint = $"{_path}?IdUser={IdUser}";
            return ConfigureHttpRequest(endpoint, WebRequestMethods.Http.Get);
        }
        internal virtual HttpWebRequest RequestGetUserIsActive(bool? isActive)
        {
            var endpoint = $"{_path}?IsActive={isActive}";
            return ConfigureHttpRequest(endpoint, WebRequestMethods.Http.Get);
        }
        #endregion

        #region ConfigureHttpRequest
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
        #endregion
    }
}
