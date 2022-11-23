using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SW.Services.Resend
{
    [DataContract]
    public class PdfRequestResend
    {
        [DataMember]
        public Guid uuid { get; set; }
        [DataMember]
        public string to { get; set; }
    }
}
