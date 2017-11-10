using SW.Entities;
using SW.Helpers;
using System.Runtime.Serialization;

namespace SW.Services.Authentication
{
    public class AuthResponse : Response
    {
        [DataMember]
        public Data data { get; set; }
    }
    public partial class Data
    {
        [DataMember]
        public string token { get; set; }
    }
}