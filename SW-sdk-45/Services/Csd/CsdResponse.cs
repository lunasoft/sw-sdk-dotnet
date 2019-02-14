using System.Collections.Generic;
using SW.Helpers;
using SW.Entities;
using System.Runtime.Serialization;
using System;

namespace SW.Services.Csd
{
    public class CsdResponse : Response
    {
        [DataMember]
        public string data { get; set; }
    }

    public class InfoCsdResponse : Response
    {
        [DataMember]
        public InfoCsd data { get; set; }
    }

    public class ListInfoCsdResponse : Response
    {
        [DataMember]
        public List<InfoCsd> data { get; set; }
    }

    public class InfoCsd
    {
        [DataMember]
        public string issuer_rfc { get; set; }
        [DataMember]
        public string certificate_number { get; set; }
        [DataMember]
        public bool is_active { get; set; }
        [DataMember]
        public string issuer_business_name { get; set; }
        [DataMember]
        public string valid_from { get; set; }
        [DataMember]
        public string valid_to { get; set; }
        [DataMember]
        public string certificate_type { get; set; }
    }
}