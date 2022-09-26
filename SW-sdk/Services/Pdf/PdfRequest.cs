using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SW.Services.Pdf
{
    [DataContract]
    public class PdfRequest
    {
        [DataMember]
        public string xmlContent { get; set; }
        [DataMember]
        public string logo { get; set; }
        [DataMember]
        public Dictionary<string, string> extras { get; set; }
        [DataMember]
        public string templateId { get; set; }
    }
}
