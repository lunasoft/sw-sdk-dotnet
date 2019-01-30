using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SW.Services.Csd
{
    [DataContract]
    public class UploadCsdRequest
    {
        [DataMember]
        public string b64Cer { get; set; }
        [DataMember]
        public string b64Key { get; set; }
        [DataMember]
        public string password { get; set; }
        [DataMember]
        public string type { get; set; }
        [DataMember]
        public bool is_active { get; set; }
    }
}
