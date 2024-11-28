using System.Runtime.Serialization;

namespace SW.Services.Account.AccountBalance
{
    /// <summary>
    /// Estructura del body del servicio Balance Management
    /// </summary>
    [DataContract]
    internal partial class AccountBalanceRequest
    {
        [DataMember]
        public string comment { get; set; }
        [DataMember]
        public int stamps { get; set; }
    }
}
