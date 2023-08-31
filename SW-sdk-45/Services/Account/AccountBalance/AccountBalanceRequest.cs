using System.Runtime.Serialization;


namespace SW.Services.Account.AccountBalance
{
    /// <summary>
    /// Estructura del body del servicio Balance Management
    /// </summary>
    [DataContract]
    internal class AccountBalanceRequest
    {
        [DataMember]
        public string Comment { get; set; }
    }
}
