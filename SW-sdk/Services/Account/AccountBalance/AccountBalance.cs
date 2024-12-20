﻿using System;
using SW.Helpers;
using SW.Entities;
using System.Net;
using Newtonsoft.Json;
using System.Text;
using System.IO;

namespace SW.Services.Account.AccountBalance
{
    public class AccountBalance : AccountService
    {

        BalanceAccountResponseHandler _handler;
        AccountResponseHandler _handlerBalance;
        /// <summary>
        /// Crear una instancia de la clase BalanceAccount.
        /// </summary>
        /// <param name="url">Url Services.</param>
        /// <param name="token">Token de autenticación</param>
        public AccountBalance(string url, string token, int proxyPort = 0, string proxy = null) : base(url, token, proxy, proxyPort)
        {
            _handler = new BalanceAccountResponseHandler();
            _handlerBalance = new AccountResponseHandler();
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
            _handler = new BalanceAccountResponseHandler();
            _handlerBalance = new AccountResponseHandler();
        }
        /// <summary>
        /// Metodo que obtiene el balance de timbres del usuario.
        /// </summary>
        public AccountResponse ConsultarSaldo()
        {
            return (AccountResponse)GetBalance();
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
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                this.SetupRequest();
                var baseUrl = this.UrlApi ?? this.Url;
                var request = (HttpWebRequest)WebRequest.Create(baseUrl + "management/v2/api/users/balance");
                request.ContentType = "application/json";
                request.Method = WebRequestMethods.Http.Get;
                request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
                Helpers.RequestHelper.SetupProxy(this.Proxy, this.ProxyPort, ref request);
                request.ContentLength = 0;
                return _handlerBalance.GetResponse(request);
            }
            catch (Exception e)
            {
                return _handlerBalance.HandleException(e);
            }
        }
        internal override Response StampsDistribution(Guid idUser, int stamps, ActionsAccountBalance action, string content)
        {
            try
            {
                new Validation(Url, UrlApi, User, Password, Token).ValidateHeaderParameters();
                this.SetupRequest();
                var endpoint = String.Format("{0}/{1}/{2}", "/management/v2/api/dealers/users",idUser,"stamps");
                var baseUrl = this.UrlApi ?? this.Url;
                var request = (HttpWebRequest)WebRequest.Create(baseUrl + endpoint);
                request.ContentType = "application/json";
                GetMethod(request, action);
                request.Headers.Add(HttpRequestHeader.Authorization.ToString(), "bearer " + this.Token);
                Helpers.RequestHelper.SetupProxy(this.Proxy, this.ProxyPort, ref request);
                request.ContentLength = 0;
                GetStringContent(request, content, stamps);
                return _handler.GetResponse(request);
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
