using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using SW.Entities;
using SW.Helpers;

namespace SW.Services.Pdf
{
    [DataContract]
    public class PdfResponse : Response
    {
        [DataMember(Name = "data")]
        public Data data { get; set; }
        [DataMember]
        public int responseCode { get; set; }
    }

    public class Data
    {
        [DataMember]
        public string contentB64 { get; set; }
        [DataMember]
        public int contentSizeBytes { get; set; }
        [DataMember]
        public string uuid { get; set; }
        [DataMember]
        public string serie { get; set; }
        [DataMember]
        public string folio { get; set; }
        [DataMember]
        public DateTime stampDate { get; set; }
        [DataMember]
        public DateTime issuedDate { get; set; }
        [DataMember]
        public string rfcIssuer { get; set; }
        [DataMember]
        public string rfcReceptor { get; set; }
        [DataMember]
        public string total { get; set; }
    }

}
