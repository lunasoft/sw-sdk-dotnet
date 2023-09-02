using SW.Entities;
using System.Runtime.Serialization;

namespace SW.Services.Account.AccountUser
{
    [DataContract]
    public class AccountGetUserResponse : Response
    {
        [DataMember]
        public AccountUserData data { get; set; }
    }
    [DataContract]
    public class AccountGetUsersResponse : Response
    {
        [DataMember]
        public AccountUserData[] data { get; set; }
    }
    [DataContract]
    public class AccountUserData
    {
        [DataMember]
        public string idUsuario { get; set; }
        [DataMember]
        public string idCliente { get; set; }
        [DataMember]
        public int stamps { get; set; }
        [DataMember]
        public bool unlimited { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string apellidoPaterno { get; set; }
        [DataMember]
        public string apellidoMaterno { get; set; }
        [DataMember]
        public string username { get; set; }
        [DataMember]
        public string fechaUltimoPassword { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public bool administrador { get; set; }
        [DataMember]
        public int profile { get; set; }
        [DataMember]
        public bool activo { get; set; }
        [DataMember]
        public string registeredDate { get; set; }
        [DataMember]
        public bool eliminado { get; set; }
        [DataMember]
        public string tokenAccess { get; set; }
    }
    [DataContract]
    public class AccountUserActionsResponse : Response
    {
        [DataMember]
        internal string data { get; set; }
    }
}
