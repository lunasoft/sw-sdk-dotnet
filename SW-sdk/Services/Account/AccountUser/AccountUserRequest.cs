using SW.Helpers;
using System.Runtime.Serialization;

namespace SW.Services.Account.AccountUser
{
    [DataContract]
    public partial class AccountUserRequest
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string taxId { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public int stamps { get; set; }
        [DataMember]
        public bool isUnlimited { get; set; } = false;
        [DataMember]
        public string password { get; set; }
        [DataMember]
        public string notificationEmail { get; set; }
        [DataMember]
        public string phone { get; set; }
    }
}
