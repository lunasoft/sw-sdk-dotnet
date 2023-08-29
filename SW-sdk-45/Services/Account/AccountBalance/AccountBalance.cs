using System;
using SW.Helpers;
using SW.Entities;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http;

namespace SW.Services.Account.AccountBalance
{
    public class AccountBalance : BalanceAccountService
    {

        BalanceAccountResponseHandler _handler;
        BalanceResponseHandler _handlerBalance;
        /// <summary>
        /// Crear una instancia de la clase BalanceAccount.
        /// </summary>
        /// <param name="url">Url Services.</param>
        /// <param name="token">Token de autenticación</param>
        public AccountBalance(string url, string token, int proxyPort = 0, string proxy = null) : base(url, token, proxy, proxyPort)
        {
            _handler = new BalanceAccountResponseHandler();
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
            _handler = new BalanceAccountResponseHandler();
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
                new Validation(Url, UrlApi, User, Password, Token).ValidateHeaderParameters();
                this.SetupRequest();

                Dictionary<string, string> headers = new Dictionary<string, string>() {
                    { "Authorization", "bearer " + this.Token }
                };
                var baseUrl = this.UrlApi ?? this.Url;
                var proxy = Helpers.RequestHelper.ProxySettings(this.Proxy, this.ProxyPort);
                return _handler.GetResponse(baseUrl, headers, "management/api/balance", proxy);
            }
            catch (Exception e)
            {
                return _handler.HandleException(e);
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
                var endpoint = String.Format("{0}/{1}/{2}/{3}", "management/api/balance", idUser, action.ToString().ToLower(), stamps);
                var proxy = Helpers.RequestHelper.ProxySettings(this.Proxy, this.ProxyPort);
                var content = GetStringContent(comment);
                return _handlerBalance.GetResponse(baseUrl, headers, endpoint, content, proxy);
            }
            catch (Exception e)
            {
                return _handlerBalance.HandleException(e);
            }
        }
        internal virtual StringContent GetStringContent(string comment)
        {

            var request = new AccountBalanceRequest();
            request.Comment = comment;
            var content = new StringContent(JsonConvert.SerializeObject(
                request, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                }),
            Encoding.UTF8, "application/json");
            return content;

        }
    }
}
