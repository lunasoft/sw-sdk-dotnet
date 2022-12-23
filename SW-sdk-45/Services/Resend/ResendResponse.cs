using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using SW.Entities;

namespace SW.Services.Resend
{
    [DataContract]
    public class ResendResponse : Response
    {
        [DataMember(Name = "data")]
        public string data { get; set; }
    }
}
