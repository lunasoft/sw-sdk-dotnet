using System.Collections.Generic;
using SW.Helpers;
using System.Runtime.Serialization;
using SW.Entities;

namespace SW.Services.Account.AccountBalance
{
    /// <summary>
    /// Estructura de la respuesta que se obtiene del metodo Balance de timbres.
    /// </summary>
    public class AccountResponse : Response
    {
        [DataMember]
        public Data data { get; set; }
    }
    /// <summary>
    /// Estructura de la respuesta que se obtiene en los metodos Añadir y Eliminar timbres.
    /// </summary>
    public class AccountBalanceResponse : Response
    {
        [DataMember]
        public string data { get; set; }
    }
    public partial class Data
    {
        [DataMember]
        public string idUserBalance { get; set; }
        [DataMember]
        public string idUser { get; set; }
        [DataMember]
        public int stampsBalance { get; set; }
        [DataMember]
        public int stampsUsed { get; set; }
        [DataMember]
        public int stampsAssigned { get; set; }
        [DataMember]
        public bool unlimited { get; set; }
        [DataMember]
        public string expirationDate { get; set; }
        [DataMember]
        public LastTransaction lastTransaction { get; set; }
    }
    public partial class LastTransaction
    {
        [DataMember]
        public int folio { get; set; }
        [DataMember]
        public string idUSer { get; set; }
        [DataMember]
        public string idUserReceiver { get; set; }
        [DataMember]
        public string nameReceiver { get; set; }
        [DataMember]
        public int stampsIn { get; set; }
        [DataMember]
        public int? stampsOut { get; set; }
        [DataMember]
        public int stampsCurrent { get; set; }
        [DataMember]
        public string comment { get; set; }
        [DataMember]
        public string date { get; set; }
        [DataMember]
        public bool isEmailSent { get; set; }
    }
}