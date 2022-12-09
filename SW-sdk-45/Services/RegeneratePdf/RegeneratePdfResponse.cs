using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SW.Entities;
using System.Runtime.Serialization;

namespace SW.Services.RegeneratePdf
{
    [DataContract]
    public class RegeneratePdfResponse : Response
    {
        [DataMember(Name = "data")]
        public string data { get; set; }
    }
}
