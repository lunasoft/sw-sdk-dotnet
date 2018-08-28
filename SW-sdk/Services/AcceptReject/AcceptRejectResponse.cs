using System.Collections.Generic;
using SW.Helpers;
using SW.Entities;
using System.Runtime.Serialization;
using System;

namespace SW.Services.AcceptReject
{
    public class AcceptRejectResponse : Response
    {
        [DataMember(Name = "data")]
        public Data data { get; set; }
        [DataMember(Name = "codStatus")]
        public string codStatus { get; set; }

    }
    public partial class Data
    {
        [DataMember(Name = "acuse")]
        public string acuse { get; set; }
        [DataMember(Name = "folios")]
        public List<invoicesStatus> folios { get; set; }
    }
    public class invoicesStatus
    {
        public Guid uuid { get; set; }
        public string estatusUUID { get; set; }
        public string respuesta { get; set; }
    }
}