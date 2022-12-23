using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;

namespace SW.Services.Resend
{
    [DataContract]
    public class ResendRequest
    {
        [DataMember]
        public Guid uuid { get; set; }
        [DataMember]
        public string to { get; set; }
    }
}
