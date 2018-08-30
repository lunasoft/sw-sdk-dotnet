using SW.Entities;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SW.Services.Pendings
{
    public class PendingsResponse : Response
    {        
        [DataMember(Name = "data")]
        public Data data { get; set; }
        [DataMember]
        public string codStatus { get; set; }
    }
    public partial class Data
    {
        [DataMember]
        public string codEstatus { get; set; }
        [DataMember]
        public List<string> uuid { get; set; }
    }
}

