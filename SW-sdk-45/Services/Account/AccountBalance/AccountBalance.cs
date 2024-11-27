using System;
using SW.Helpers;
using SW.Entities;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http;
using System.Net;
using Org.BouncyCastle.Asn1.Ocsp;
using System.IO;

namespace SW.Services.Account.AccountBalance
{
    public class AccountBalance : AccountService
    {

        AccountBalanceResponseHandler _handler;
        BalanceResponseHandler _handlerBalance;
        /// <summary>
        /// Crear una instancia de la clase BalanceAccount.
        /// </summary>
        /// <param name="url">Url Services.</param>
        /// <param name="token">Token de autenticación</param>
        public AccountBalance(string url, string token, int proxyPort = 0, string proxy = null) : base(url, token, proxy, proxyPort)
        {
            _handler = new AccountBalanceResponseHandler();
            _handlerBalance = new BalanceResponseHandler();
        }
        /// <summary>
        /// Crear una instancia de la clase BalanceAccount.
        /// </summary>
        /// <param name="url">Url Services.</param>
        /// <param name="urlApi"></Url Api.param>
        /// <param name="user">Usuario.</param>
        /// <param name="password">Contraseña.</param>
        public AccountBalance(string url, string urlApi, string user, string password, int proxyPort = 0, string proxy = null) : base(url, urlApi, user, password, proxy, proxyPort)
        {
            _handlerBalance = new BalanceResponseHandler();
            _handler = new AccountBalanceResponseHandler();
        }
        /// <summary>
        /// Metodo que obtiene el balance de timbres del usuario.
        /// </summary>
        public BalanceResponse ConsultarSaldo()
        {
            return (BalanceResponse)GetBalance();
        }
        /// <summary>
        /// Metodo para añadir timbres a una cuenta hijo desde la cuenta dealer.
        /// </summary>
        /// <param name="idUser">ID del usuario al que se le asignaran los timbres.</param>
        /// <param name="stamps">Cantidad de timbres a agregar.</param>
        /// <param name="comment">Comentario agregado al movimiento.</param>
        /// <returns><see cref="AccountBalanceResponse"/></returns>
        public AccountBalanceResponse AgregarTimbres(Guid idUser, int stamps, string comment)
        {
            return (AccountBalanceResponse)StampsDistribution(idUser, stamps, ActionsAccountBalance.Add, comment);
        }
        /// <summary>
        /// Metodo para eliminar timbres a una cuenta hijo desde la cuenta dealer.
        /// </summary>
        /// <param name="idUser">ID del usuario al que se le eliminarán los timbres.</param>
        /// <param name="stamps">Cantidad de timbres a eliminar.</param>
        /// <param name="comment">Comentario agregado al movimiento.</param>
        /// <returns><see cref="AccountBalanceResponse"/></returns>
        public AccountBalanceResponse EliminarTimbres(Guid idUser, int stamps, string comment)
        {
            return (AccountBalanceResponse)StampsDistribution(idUser, stamps, ActionsAccountBalance.Remove, comment);
        }
        internal override Response GetBalance()
        {
            try
            {
                new Validation(Url, UrlApi, User, Password, Token).ValidateHeaderParameters();
                this.SetupRequest();
                Dictionary<string, string> headers = new Dictionary<string, string>() {
                    { "Authorization", "bearer " + this.Token }
                };
                var baseUrl = this.UrlApi ?? this.Url;
                var proxy = Helpers.RequestHelper.ProxySettings(this.Proxy, this.ProxyPort);
                return _handlerBalance.GetResponse(baseUrl, headers, "management/v2/api/users/balance", proxy);
            }
            catch (Exception e)
            {
                return _handlerBalance.HandleException(e);
            }
        }
        internal override Response StampsDistribution(Guid idUser, int stamps, ActionsAccountBalance action, string comment)
        {
            try
            {
                new Validation(Url, UrlApi, User, Password, Token).ValidateHeaderParameters();
                this.SetupRequest();
                Dictionary<string, string> headers = new Dictionary<string, string>() {
                    { "Authorization", "bearer " + this.Token }
                };
                var baseUrl = this.UrlApi ?? this.Url;
                var endpoint = String.Format("{0}/{1}/{2}", "/management/v2/api/dealers/users", idUser, "stamps");
                var request = (HttpWebRequest)WebRequest.Create(baseUrl + endpoint);
                var proxy = Helpers.RequestHelper.ProxySettings(this.Proxy, this.ProxyPort);
                request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
                GetMethod(request, action);
                request.ContentType = "application/json";
                GetStringContent(request, comment, stamps);
                return _handler.GetResponseRequest(request);
            }
            catch (Exception e)
            {
                return _handler.HandleException(e);
            }
        }
        internal virtual HttpWebRequest GetStringContent(HttpWebRequest request, string comment, int stamps)
        {
            var balanceRequest = new AccountBalanceRequest
            {
                comment = comment,
                stamps = stamps
            };
            string jsonContent = JsonConvert.SerializeObject(balanceRequest);
            byte[] byteArray = Encoding.UTF8.GetBytes(jsonContent);
            request.ContentLength = byteArray.Length;
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(byteArray, 0, byteArray.Length);
            }
            return request;
        }
        internal virtual HttpWebRequest GetMethod(HttpWebRequest request, ActionsAccountBalance action)
        {
            request.Method = action == ActionsAccountBalance.Add
                ? WebRequestMethods.Http.Post
                : "DELETE";
            return request;
        }
    }
}
