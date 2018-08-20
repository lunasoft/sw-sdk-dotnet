using SW.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SW.Services.AcceptReject
{
    [DataContract]
    public class AcceptRejectRequestCSD
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
        public string b64Cer { get; set; }
        [DataMember]
        public string b64Key { get; set; }
    }
    [DataContract]
    public class AceptacionRechazoItem
    {
        [DataMember]
        public string uuid { get; set; }
        [DataMember]
        private EnumAcceptReject _action;

        public EnumAcceptReject action
        {
            get { return _action; }
            set { action = value; }
        }

    }
}
