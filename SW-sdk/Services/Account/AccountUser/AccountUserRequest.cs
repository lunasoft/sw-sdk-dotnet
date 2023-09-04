using SW.Helpers;
using System.Runtime.Serialization;

namespace SW.Services.Account.AccountUser
{
    [DataContract]
    public partial class AccountUserRequest
    {
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string password { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string rfc { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public int stamps { get; set; }
        [DataMember]
        public bool unlimited { get; set; } = false;
        [DataMember]
        public bool activo { get; set; } = true;
        [DataMember]
        public AccountUserProfile profileType { get; set; }
    }
    public partial class AccountUserRequest
    {
        [DataMember]
        internal int profile { get; set; }
    }
}
