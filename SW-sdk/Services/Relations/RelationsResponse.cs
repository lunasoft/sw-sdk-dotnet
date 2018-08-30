using SW.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SW.Services.Relations
{
    public class RelationsResponse : Response
    {
        [DataMember(Name = "data")]
        public Data data { get; set; }
        [DataMember]
        public string codStatus { get; set; }
    }
    public partial class Data
    {
        [DataMember]
        public Guid uuidConsultado { get; set; }
        [DataMember]
        public string resultado { get; set; }
        [DataMember]
        public List<invoicesStatus> uuidsRelacionadosPadres { get; set; }
        [DataMember]
        public List<invoicesStatus> uuidsRelacionadosHijos { get; set; }
    }

    public class invoicesStatus
    {
        public Guid uuid { get; set; }
        public string rfcEmisor { get; set; }
        public string rfcReceptor { get; set; }
    }
}
