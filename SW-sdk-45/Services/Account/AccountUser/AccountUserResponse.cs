using SW.Entities;
using System.Collections.Generic;
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
        public List<AccountUserData> data { get; set; }
    }
    [DataContract]
    public class AccountUserActionsResponse : Response
    {
        [DataMember]
        public string data { get; set; }
    }
    [DataContract]
    public class AccountUserData
    {
        [DataMember]
        public string idUser { get; set; }
        [DataMember]
        public string idDealer { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string taxId { get; set; }
        [DataMember]
        public string username { get; set; }
        [DataMember]
        public string lastPasswordChange { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public bool isAdmin { get; set; }
        [DataMember]
        public int profile { get; set; }
        [DataMember]
        public bool isActive { get; set; }
        [DataMember]
        public string registeredDate { get; set; }
        [DataMember]
        public string accessToken { get; set; }
        [DataMember]
        public string phone { get; set; }
    }
}
