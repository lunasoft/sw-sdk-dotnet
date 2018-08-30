using System.Runtime.Serialization;

namespace SW.Services.Relations
{
    [DataContract]
    public class RelationsRequestPFX
    {
        [DataMember]
        public string uuid { get; set; }
        [DataMember]
        public string password { get; set; }
        [DataMember]
        public string rfc { get; set; }
        [DataMember]
        public string b64Pfx { get; set; }
    }
}
