using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using SW.Entities;
using SW.Helpers;

namespace SW.Services.Validate
{
    public class ValidateResponse : Response
    {
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public List<Detail> detail { get; set; }
        [DataMember]
        public string cadenaOriginalSAT { get; set; }
        [DataMember]
        public string cadenaOriginalComprobante { get; set; }
        [DataMember]
        public string uuid { get; set; }
        [DataMember]
        public string statusSat { get; set; }
        [DataMember]
        public string statusCodeSat { get; set; }
    }
    public class Detail
    {
        [DataMember]
        public List<Detail2> detail { get; set; }
        [DataMember]
        public string section { get; set; }
    }
    public class Detail2
    {
        [DataMember]
        public string message { get; set; }
        [DataMember]
        public string messageDetail { get; set; }
        [DataMember]
        public int type { get; set; }
    }
}
