using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SW.Services.StatusSW
{
    public class StatusSWResponse : Entities.Response
    {
        [DataMember]
        public Data data { get; set; }
        public class Data
        {
            [DataMember]
            public string description { get; set; }
            [DataMember]
            public string status { get; set; }
            [DataMember]
            public string name { get; set; }
        }
    }
}
