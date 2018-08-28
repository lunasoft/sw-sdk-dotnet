using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SW.Services.AcceptReject
{
    [DataContract]
    public class AcceptRejectRequestPFX
    {
        [DataMember]
        public string uuid { get; set; }
        [DataMember]
        public AceptacionRechazoItem[] uuids { get; set; }
        [DataMember]
        public string password { get; set; }
        [DataMember]
        public string rfc { get; set; }
        [DataMember]
        public string b64Pfx { get; set; }        
    }
}
