using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SW.Services.Account.AccountUser
{
    [DataContract]
    public partial class AccountUserUpdateRequest
    {
        [DataMember]
        public Guid idUser { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string taxId { get; set; }
        [DataMember]
        public string notificationEmail { get; set; }
        [DataMember]
        public string phone { get; set; }
        [DataMember]
        public bool isUnlimited { get; set; } = false;
    }
}
